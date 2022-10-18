namespace Annex.Test.Customizations;

using System.Collections.Specialized;
using Annex.Collections;

public sealed class NameValueCollectionCustomization : ICustomization
{
    public void Customize(IFixture fixture) =>
        fixture.Register<IDictionary<string, IEnumerable<string>>, NameValueCollection>(
            d => d.ToNameValueCollection());
}
