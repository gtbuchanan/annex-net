using System.Runtime.CompilerServices;

[assembly: FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: Timeout(10_000)]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
