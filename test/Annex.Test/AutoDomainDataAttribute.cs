using Annex.Test.Customizations;
using AutoFixture;
using AutoFixture.NUnit3;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test
{
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    internal sealed class AutoDomainDataAttribute : AutoDataAttribute
    {
        /// <inheritdoc />
        public AutoDomainDataAttribute() : base(FixtureFactory) { }

        private static IFixture FixtureFactory() => new Fixture().Customize(new DomainCustomization());
    }
}
