namespace Annex.Test.Customizations;

using System.Collections.Specialized;

public sealed class NameValueCollectionCustomizationTest
{
    [Test]
    public void CreatesNonEmptyNameValueCollection() =>
        new Fixture().Customize(new NameValueCollectionCustomization())
            .Create<NameValueCollection>()
            .Count
            .ShouldBeGreaterThan(0);
}
