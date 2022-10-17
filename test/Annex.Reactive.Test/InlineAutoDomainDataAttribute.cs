namespace Annex.Reactive.Test;

using System.Diagnostics.CodeAnalysis;
using Annex.Reactive.Test.Customizations;
using AutoFixture;
using AutoFixture.NUnit3;

[ExcludeFromCodeCoverage]
internal sealed class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoDomainDataAttribute(
        params object[] arguments)
        : base(FixtureFactory, arguments)
    {
    }

    private static IFixture FixtureFactory() => new Fixture().Customize(new DomainCustomization());
}
