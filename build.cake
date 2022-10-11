// Add-ins
#addin "nuget:?package=Cake.Codecov&version=1.0.1"
#addin "nuget:?package=Cake.DocFx&version=1.0.0"
#addin "nuget:?package=Cake.Git&version=2.0.0"
#addin "nuget:?package=Cake.GitVersioning&version=3.5.113"
#addin "nuget:?package=Cake.Http&version=2.0.0"
#addin "nuget:?package=Cake.Json&version=7.0.1"
#addin "nuget:?package=Newtonsoft.Json&version=13.0.1"

// Tools
#tool "nuget:?package=Codecov&version=1.13.0"
#tool "nuget:?package=docfx.console&version=2.59.4"
#tool "nuget:?package=OpenCover&version=4.7.1221"
#tool "nuget:?package=ReportGenerator&version=5.1.10"
#tool "nuget:?package=vswhere&version=3.0.3"

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
var isAzurePipelines = AzurePipelines.IsRunningOnAzurePipelines;
var isFork = !StringComparer.OrdinalIgnoreCase.Equals(
    "gtbuchanan/annex-net",
    AzurePipelines.Environment.Repository.RepoName);
var branchName = isAzurePipelines
    ? AzurePipelines.Environment.Repository.SourceBranchName
    : GitBranchCurrent(".").FriendlyName;
var vsDirectory = string.IsNullOrWhiteSpace(vsDirectoryString)
    ? VSWhereLatest()
    : Directory(vsDirectoryString);
var msBuildPath = string.IsNullOrWhiteSpace(msBuildPathString)
    ? vsDirectory.CombineWithFilePath("./Msbuild/Current/Bin/MSBuild.exe")
    : File(msBuildPathString);
Information(msBuildPath);

// Paths
var solutionFile = File("./Annex.sln");
var docFxConfigFile = File("./docs/docfx.json");
var testResultDirectory = artifactDirectory + Directory("TestResults");
var testResultFileName = "TestResults.trx";
var testResultFile = testResultDirectory + File(testResultFileName);
var testCoverageFile = testResultDirectory + File("TestCoverage.OpenCover.xml");
var testCoverageCoberturaFile = testResultDirectory + File("TestCoverage.Cobertura.xml");
var testCoverageReportDirectory = artifactDirectory + Directory("TestCoverageReport");
var testCoverageReportFile = testCoverageReportDirectory + File("index.htm");
var documentationDirectory = artifactDirectory + Directory("Documentation");
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
        Action<ICakeContext> dotNetVsTest = context => {
            var testDllFiles = context.GetFiles($"{binaryDirectory}/{configuration}/**/*.Test.dll");
            context.DotNetVSTest(testDllFiles, new DotNetVSTestSettings {
                ArgumentCustomization = args => args
                    .Append($"--ResultsDirectory:{testResultDirectory}"),
                Logger = $"trx;LogFileName={testResultFileName}",
                Parallel = true
            });
        };

        EnsureDirectoryExists(testResultDirectory);
        OpenCover(dotNetVsTest, testCoverageFile,
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
            .WithFilter("-[*]PublicApiGenerator.*")
            .WithFilter("-[*]Microsoft.*")
            .WithFilter("-[*]System.*")
            .WithFilter("-[DiffEngine]*")
            .WithFilter("-[Shouldly]*")
            .WithFilter("-[Microsoft.Reactive.Testing]*")
            .WithFilter("-[NUnit3.TestAdapter]*"));
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

Task("BuildDocumentation")
    .Does(() => {
        DocFxMetadata(docFxConfigFile);
        DocFxBuild(docFxConfigFile, new DocFxBuildSettings {
            OutputPath = documentationDirectory
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
        AzurePipelines.Commands.PublishTestResults(new AzurePipelinesPublishTestResultsData {
            Configuration = configuration,
            Platform = "x64",
            TestResultsFiles = new [] { (FilePath)testResultFile },
            TestRunTitle = "Unit Tests",
            TestRunner = AzurePipelinesTestRunnerType.VSTest
        });
    });

Task("PublishTestCoverageResults")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("ReportTestCoverage")
    .Does(() => {
        AzurePipelines.Commands.PublishCodeCoverage(new AzurePipelinesPublishCodeCoverageData {
            CodeCoverageTool = AzurePipelinesCodeCoverageToolType.Cobertura,
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

Task("PublishDocumentationArtifacts")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("BuildDocumentation")
    .Does(() => {
        var artifactName = "Documentation";
        Information($"##vso[artifact.upload containerfolder={artifactName};artifactname={artifactName}]{documentationDirectory}");
    });

Task("PublishPackageArtifacts")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .WithCriteria(!isFork, "Fork")
    .IsDependentOn("Build")
    .Does(() => {
        var artifactName = "Packages";
        Information($"##vso[artifact.upload containerfolder={artifactName};artifactname={artifactName}]{packageDirectory}");
    });

Task("PublishAzurePipeline")
    .WithCriteria(isAzurePipelines, "Not Azure Pipelines")
    .IsDependentOn("PublishTestResults")
    .IsDependentOn("PublishTestCoverageResults")
    .IsDependentOn("PublishTestArtifacts")
    .IsDependentOn("PublishDocumentationArtifacts")
    .IsDependentOn("PublishPackageArtifacts");

Task("Default")
    .IsDependentOn("LaunchTestCoverageReport")
    .IsDependentOn("UploadTestCoverage")
    .IsDependentOn("PublishAzurePipeline");

///////////////////////////////////////////

RunTarget(target);
