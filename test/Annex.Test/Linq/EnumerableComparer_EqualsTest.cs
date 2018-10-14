using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Test.Linq
{
    public sealed class EnumerableComparer_EqualsTest
    {
        [Test]
        public void NullEquality() =>
            EnumerableComparer<object>.Default
                .Equals(null, null)
                .ShouldBeTrue();

        [Test, AutoDomainData]
        public void ReferenceEquality(IEnumerable<object> sut) =>
            EnumerableComparer<object>.Default
                .Equals(sut, sut)
                .ShouldBeTrue();

        [Test, AutoDomainData]
        public void ItemEquality(IEnumerable<object> sut) =>
            EnumerableComparer<object>.Default
                .Equals(sut.ToArray(), sut.ToList())
                .ShouldBeTrue();
    }
}
