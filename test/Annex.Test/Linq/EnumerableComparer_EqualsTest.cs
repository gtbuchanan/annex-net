using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void ReferenceEquality(IEnumerable<object> sut) =>
            EnumerableComparer<object>.Default
                .Equals(sut, sut)
                .ShouldBeTrue();

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void ItemEquality(IEnumerable<object> sut) =>
            EnumerableComparer<object>.Default
                .Equals(sut.ToArray(), sut.ToList())
                .ShouldBeTrue();
    }
}
