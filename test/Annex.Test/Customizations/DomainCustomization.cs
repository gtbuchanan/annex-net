namespace Annex.Test.Customizations;

using AutoFixture.AutoNSubstitute;

internal sealed class DomainCustomization : CompositeCustomization
{
    public DomainCustomization()
        : base(
        new AutoNSubstituteCustomization { GenerateDelegates = true },
        new NameValueCollectionCustomization())
    {
    }
}
