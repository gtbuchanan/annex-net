namespace Annex.Test.Collections;

using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Annex.Collections;
using Annex.Linq;

public sealed class NameValueLookupTest
{
    [Test]
    public void Constructor_NullNvcThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => new NameValueLookup(null!))
            .ParamName
            .ShouldBe("nvc");

    [Theory]
    [AutoDomainData]
    public void Count_ShouldReturnNvcCount(NameValueCollection nvc)
    {
        var sut = new NameValueLookup(nvc);

        sut.Count.ShouldBe(nvc.Count);
    }

    [Theory]
    [AutoDomainData]
    public void Contains_NoKeyReturnsFalse(string key) =>
        new NameValueLookup(new NameValueCollection())
            .Contains(key)
            .ShouldBeFalse();

    [Theory]
    [AutoDomainData]
    public void Contains_NullKeyReturnsTrue(string value) =>
        new NameValueLookup(new NameValueCollection { { null, value } })
            .Contains(null)
            .ShouldBeTrue();

    [Theory]
    [AutoDomainData]
    public void Contains_NullValueReturnsTrue(string key) =>
        new NameValueLookup(new NameValueCollection { { key, null } })
            .Contains(key)
            .ShouldBeTrue();

    [Theory]
    [AutoDomainData]
    public void Contains_NonNullValueReturnsTrue(NameValueCollection nvc)
    {
        var sut = new NameValueLookup(nvc);

        sut.Contains(nvc.AllKeys[0]).ShouldBeTrue();
    }

    [Theory]
    [AutoDomainData]
    public void Indexer_NullKeyReturnsValue(string value)
    {
        var sut = new NameValueLookup(new NameValueCollection { { null, value } });

        sut[null].ShouldBe(EnumerableEx.Return(value));
    }

    [Theory]
    [AutoDomainData]
    public void Indexer_NullValueReturnsEmptyEnumerable(string key) =>
        new NameValueLookup(new NameValueCollection { { key, null } })[key]
            .ShouldBeEmpty();

    [Theory]
    [AutoDomainData]
    public void Indexer_NonNullValueReturnsValue(NameValueCollection nvc)
    {
        var sut = new NameValueLookup(nvc);

        var key = nvc.AllKeys[0];
        sut[key].ShouldBe(nvc.GetValues(key));
    }

    [Theory]
    [AutoDomainData]
    public void GetEnumerator_ReturnsEquivalentGroupings(NameValueCollection nvc)
    {
        var sut = new NameValueLookup(nvc);

        sut.AsEnumerable().ShouldBe(nvc.AllKeys
            .Select(key => Grouping.Create(key!, nvc.GetValuesSafe(key))));
    }

    [Theory]
    [AutoDomainData]
    public void GetEnumerator2_ReturnsEquivalentGroupings(NameValueCollection nvc)
    {
        IEnumerable sut = new NameValueLookup(nvc);

        var enumerator = sut.GetEnumerator();
        foreach (var key in nvc.AllKeys)
        {
            enumerator.MoveNext().ShouldBeTrue();
            enumerator.Current.ShouldBe(nvc.GetValues(key));
        }

        enumerator.MoveNext().ShouldBeFalse();
    }
}
