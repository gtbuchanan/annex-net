using Annex.Collections;
using Annex.Linq;
using AutoFixture.NUnit3;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Specialized;
using System.Linq;

namespace Annex.Test.Collections
{
    internal sealed class NameValueLookupTest
    {
        [Test]
        public void Constructor_NullNvcThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => new NameValueLookup(null))
                .ParamName.ShouldBe("nvc");

        [Test, AutoDomainData]
        public void Count_ShouldReturnNvcCount(
            [Frozen]NameValueCollection nvc, NameValueLookup sut) =>
            sut.Count.ShouldBe(nvc.Count);

        [Test, AutoDomainData]
        public void Contains_NoKeyReturnsFalse(string key) =>
            new NameValueLookup(new NameValueCollection())
                .Contains(key).ShouldBeFalse();

        [Test, AutoDomainData]
        public void Contains_NullValueReturnsTrue(string key) =>
            new NameValueLookup(new NameValueCollection { { key, null } })
                .Contains(key).ShouldBeTrue();

        [Test, AutoDomainData]
        public void Contains_NonNullValueReturnsTrue(
            [Frozen]NameValueCollection nvc, NameValueLookup sut) =>
            sut.Contains(nvc.AllKeys.First()).ShouldBeTrue();

        [Test, AutoDomainData]
        public void Indexer_NullValueReturnsEmptyEnumerable(string key) =>
            new NameValueLookup(new NameValueCollection { { key, null } })[key]
                .ShouldBeEmpty();

        [Test, AutoDomainData]
        public void Indexer_NonNullValueReturnsValue(
            [Frozen]NameValueCollection nvc, NameValueLookup sut)
        {
            var key = nvc.AllKeys.First();
            sut[key].ShouldBe(nvc.GetValues(key));
        }

        [Test, AutoDomainData]
        public void GetEnumerator_ReturnsEquivalentGroupings(
            [Frozen]NameValueCollection nvc, NameValueLookup sut) =>
            sut.AsEnumerable().ShouldBe(nvc.AllKeys
                .Select(key => Grouping.Create(key, nvc.GetValuesSafe(key))));
    }
}
