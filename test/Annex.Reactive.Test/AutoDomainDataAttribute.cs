using Annex.Reactive.Test.Customizations;
using AutoFixture;
using AutoFixture.NUnit3;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Reactive.Test
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
