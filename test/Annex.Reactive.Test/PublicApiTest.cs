#if NETFRAMEWORK // TODO: Remove when ShouldMatchApproved supports in .NET Core
using NUnit.Framework;
using PublicApiGenerator;
using Shouldly;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Reactive.Test
{
    [ExcludeFromCodeCoverage]
    public sealed class PublicApiTest
    {
        [Test]
        public void IsApproved() =>
            ApiGenerator
                .GeneratePublicApi(
                    typeof(ThisAssembly).Assembly,
                    shouldIncludeAssemblyAttributes: false)
                .ShouldMatchApproved(c => c
                    .WithFilenameGenerator((_, __, fileType, extension) =>
                        $"PublicApi.{fileType}.{extension}"));
    }
}
#endif
