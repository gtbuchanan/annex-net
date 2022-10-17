namespace Annex.Test;

using System.Diagnostics.CodeAnalysis;
using Annex.Test.Customizations;
using AutoFixture.NUnit3;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
internal sealed class AutoDomainDataAttribute : AutoDataAttribute
{
    public AutoDomainDataAttribute()
        : base(FixtureFactory)
    {
    }

    private static IFixture FixtureFactory() => new Fixture().Customize(new DomainCustomization());
}
