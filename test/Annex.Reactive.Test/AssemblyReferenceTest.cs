namespace Annex.Reactive.Test;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public sealed class AssemblyReferenceTest
{
    [Test]
    public void DoesNotReferenceJetBrainsAnnotations() =>
        typeof(ThisAssembly).Assembly
            .GetReferencedAssemblies()
            .Select(a => a.Name)
            .ShouldNotContain("JetBrains.Annotations");
}
