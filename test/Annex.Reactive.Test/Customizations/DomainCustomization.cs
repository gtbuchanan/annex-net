using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Annex.Reactive.Test.Customizations
{
    internal sealed class DomainCustomization : CompositeCustomization
    {
        public DomainCustomization() : base(
            new AutoNSubstituteCustomization { GenerateDelegates = true }) { }
    }
}
