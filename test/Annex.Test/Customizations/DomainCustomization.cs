using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Annex.Test.Customizations
{
    internal sealed class DomainCustomization : CompositeCustomization
    {
        public DomainCustomization() : base(
            new AutoNSubstituteCustomization { GenerateDelegates = true },
            new NameValueCollectionCustomization()) { }
    }
}
