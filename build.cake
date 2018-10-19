// Add-ins
#addin "nuget:?package=Cake.Codecov&version=0.4.0"
#addin "nuget:?package=Cake.Git&version=0.19.0"
#addin "nuget:?package=Cake.GitVersioning&version=2.2.13"
#addin "nuget:?package=Cake.Http&version=0.5.0"
#addin "nuget:?package=Cake.Json&version=3.0.1"
#addin "nuget:?package=Newtonsoft.Json&version=9.0.1"

// Tools
#tool "nuget:?package=Codecov&version=1.1.0"
#tool "nuget:?package=OpenCover&version=4.6.519"
#tool "nuget:?package=ReportGenerator&version=4.0.0-rc4"
#tool "nuget:?package=vswhere&version=2.5.2"

// Arguments
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var artifactDirectory = Directory(Argument("artifactDirectory",
    EnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY") ?? "./artifacts"));
var codecovToken = Argument("codecovToken", EnvironmentVariable("CODECOV_TOKEN"));
var vsDirectoryString = Argument("vsDirectory", string.Empty);
var msBuildPathString = Argument("msBuildPath", string.Empty);

// Build Info
var version = GitVersioningGetVersion().SemVer2;
var isWindows = IsRunningOnWindows();
var isLocal = BuildSystem.IsLocalBuild;
var isAzurePipelines = TFBuild.IsRunningOnVSTS || TFBuild.IsRunningOnTFS;
var isFork = !StringComparer.OrdinalIgnoreCase.Equals(
    "gtbuchanan/annex-net",
    TFBuild.Environment.Repository.RepoName);
var branchName = isAzurePipelines
    ? TFBuild.Environment.Repository.Branch
    : GitBranchCurrent(".").FriendlyName;
var vsDirectory = string.IsNullOrWhiteSpace(vsDirectoryString)
    ? VSWhereLatest()
    : Directory(vsDirectoryString);
var msBuildPath = string.IsNullOrWhiteSpace(msBuildPathString)
    ? vsDirectory.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe")
    : File(msBuildPathString);
Information(msBuildPath);

// Paths
var solutionFile = File("./Annex.sln");
var testResultDirectory = artifactDirectory + Directory("TestResults");
var testResultFileName = "TestResults.trx";
var testResultFile = testResultDirectory + File(testResultFileName);
var testCoverageFile = testResultDirectory + File("TestCoverage.OpenCover.xml");
var testCoverageCoberturaFile = testResultDirectory + File("TestCoverage.Cobertura.xml");
var testCoverageReportDirectory = artifactDirectory + Directory("TestCoverageReport");
var testCoverageReportFile = testCoverageReportDirectory + File("index.htm");
var packageDirectory = artifactDirectory + Directory("Packages");
var binaryDirectory = artifactDirectory + Directory("Binaries");

///////////////////////////////////////////

Setup(_ => Information($"Version {version} from branch {branchName}"));

Task("Clean")
    .Does(() => {
        CleanDirectory(artifactDirectory);
        MSBuild(solutionFile,
            new MSBuildSettings {
                Configuration = configuration,
                MaxCpuCount = 0,
                ToolPath = msBuildPath,
                Verbosity = Verbosity.Minimal
            }
            .WithTarget("clean"));
    });

Task("Build")
    .IsDependentOn("Clean")
    .Does(() => {
        MSBuild(solutionFile,
            new MSBuildSettings {
                Configuration = configuration,
                MaxCpuCount = 0,
                Restore = true,
                ToolPath = msBuildPath,
                Verbosity = Verbosity.Minimal
            }
            .WithTarget("build;pack")
            .WithProperty("BaseOutputPath", (MakeAbsolute(binaryDirectory) + "/").Quote())
            .WithProperty("PackageOutputPath", MakeAbsolute(packageDirectory).ToString().Quote()));
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        Action<ICakeContext> dotNetCoreVsTest = context => {
            var testDllFiles = context.GetFiles($"{binaryDirectory}/{configuration}/net461/*.Test.dll");
            context.DotNetCoreVSTest(testDllFiles, new DotNetCoreVSTestSettings {
                ArgumentCustomization = args => args
                    .Append($"--ResultsDirectory:{testResultDirectory}"),
                Framework = ".NETFramework,Version=v4.6.1",
                Logger = $"trx;LogFileName={testResultFileName}",
                Parallel = true,
                Platform = VSTestPlatform.x64
            });
        };

        EnsureDirectoryExists(testResultDirectory);
        OpenCover(dotNetCoreVsTest, testCoverageFile,
            new OpenCoverSettings{
                ArgumentCustomization = args =>
                    args.Append("-register")
            }
            .WithFilter("+[*]*")
            .WithFilter("-[*.Test]*.*Test")
            .ExcludeByAttribute("*.Test*")
            .ExcludeByAttribute("*.Theory*")
            .ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
            // Generated
            .WithFilter("-[*]ProcessedByFody")
            .WithFilter("-[*]ThisAssembly")
            .WithFilter("-[*]PublicApiGenerator.*"));
    });

Task("ReportTestCoverage")
    .IsDependentOn("Test")
    .Does(() => {
        ReportGenerator(testCoverageFile, testCoverageReportDirectory, new ReportGeneratorSettings {
            ArgumentCustomization = args => args
                .Append($"-reporttypes:HtmlInline;Cobertura")
        });
        MoveFile(testCoverageReportDirectory + File("Cobertura.xml"), testCoverageCoberturaFile);
    });

Task("LaunchTestCoverageReport")
    .WithCriteria(isLocal, "CI environment")
    .WithCriteria(isWindows, "Not Windows")
    .IsDependentOn("ReportTestCoverage")
    .Does(() => {
        StartProcess("cmd", new ProcessSettings {
            Arguments = $"/C start \"\" {testCoverageReportFile}"
        });
    });

Task("UploadTestCoverage")
    .WithCriteria(!isLocal, "Local environment")
    .WithCriteria(!string.IsNullOrEmpty(codecovToken), "Missing Codecov token")
    .IsDependentOn("ReportTestCoverage")
    .Does(() => {
        Codecov(new CodecovSettings {
            Branch = branchName,
            Build = version,
            Files = new [] { testCoverageFile.ToString() },
            Token = codecovToken
        });
    });

Task("PublishTestResults")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("Test")
    .Does(() => {
        TFBuild.Commands.PublishTestResults(new TFBuildPublishTestResultsData {
            Configuration = configuration,
            Platform = "x64",
            TestResultsFiles = new string[] { testResultFile },
            TestRunTitle = "Unit Tests",
            TestRunner = TFTestRunnerType.VSTest
        });
    });

Task("PublishTestCoverageResults")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("ReportTestCoverage")
    .Does(() => {
        TFBuild.Commands.PublishCodeCoverage(new TFBuildPublishCodeCoverageData {
            CodeCoverageTool = TFCodeCoverageToolType.Cobertura,
            ReportDirectory = testCoverageReportDirectory,
            SummaryFileLocation = testCoverageCoberturaFile
        });
    });

Task("PublishTestArtifacts")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("ReportTestCoverage")
    .Does(() => {
        var artifactName = "TestResults";
        Information($"##vso[artifact.upload containerfolder={artifactName};artifactname={artifactName}]{testResultDirectory}");
    });

Task("PublishPackageArtifacts")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .WithCriteria(!isFork, "Fork")
    .IsDependentOn("Build")
    .Does(() => {
        var artifactName = "Packages";
        Information($"##vso[artifact.upload containerfolder={artifactName};artifactname={artifactName}]{packageDirectory}");
    });

Task("Default")
    .IsDependentOn("LaunchTestCoverageReport")
    .IsDependentOn("UploadTestCoverage")
    .IsDependentOn("PublishTestResults")
    .IsDependentOn("PublishTestCoverageResults")
    .IsDependentOn("PublishTestArtifacts")
    .IsDependentOn("PublishPackageArtifacts");

///////////////////////////////////////////

RunTarget(target);
