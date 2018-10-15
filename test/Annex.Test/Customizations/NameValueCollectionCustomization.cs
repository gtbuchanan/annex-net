using Annex.Collections;
using AutoFixture;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Annex.Test.Customizations
{
    internal sealed class NameValueCollectionCustomization : ICustomization
    {
        public void Customize(IFixture fixture) =>
            fixture.Register<IDictionary<string, IEnumerable<string>>, NameValueCollection>(
                d => d.ToNameValueCollection());
    }
}
