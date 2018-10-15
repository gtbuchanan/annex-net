using Annex.Collections;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace Annex.Test.Collections
{
    public sealed class NameValueCollectionExtensions_ToLookupTest
    {
        [Test]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((NameValueCollection)null).ToLookup())
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void ReturnsNameValueLookup(NameValueCollection sut) =>
            sut.ToLookup().ShouldBeOfType<NameValueLookup>()
                .Nvc.ShouldBe(sut);
    }
}
