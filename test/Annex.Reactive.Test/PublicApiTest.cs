#if NETFRAMEWORK // TODO: Remove when ShouldMatchApproved supports in .NET Core
namespace Annex.Reactive.Test;

using System.Diagnostics.CodeAnalysis;
using PublicApiGenerator;

[ExcludeFromCodeCoverage]
public sealed class PublicApiTest
{
    [Test]
    public void IsApproved() =>
        typeof(ThisAssembly).Assembly
            .GeneratePublicApi(
                new ApiGeneratorOptions
                {
                    ExcludeAttributes = new[]
                    {
                        typeof(ExcludeFromCodeCoverageAttribute).FullName,
                    },
                    IncludeAssemblyAttributes = false,
                })
            .ShouldMatchApproved(c => c
                .WithFilenameGenerator((_, _, fileType, extension) =>
                    $"PublicApi.{fileType}.{extension}"));
}
#endif
