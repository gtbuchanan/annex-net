namespace Annex.Test.Collections;

using System.Collections.Specialized;
using Annex.Collections;

public sealed class NameValueCollectionExtensions_ToLookupTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((NameValueCollection?)null)!.ToLookup())
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void ReturnsNameValueLookup(NameValueCollection sut) =>
        sut.ToLookup().ShouldBeOfType<NameValueLookup>()
            .Nvc
            .ShouldBe(sut);
}
