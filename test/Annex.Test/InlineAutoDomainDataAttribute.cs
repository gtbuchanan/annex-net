using Annex.Test.Customizations;
using AutoFixture;
using AutoFixture.NUnit3;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test
{
    [ExcludeFromCodeCoverage]
    internal sealed class InlineAutoDomainDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoDomainDataAttribute(params object[] arguments) : base(FixtureFactory, arguments) { }

        private static IFixture FixtureFactory() => new Fixture().Customize(new DomainCustomization());
    }
}
