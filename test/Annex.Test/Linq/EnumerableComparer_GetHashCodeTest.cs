using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Test.Linq
{
    public sealed class EnumerableComparer_GetHashCodeTest
    {
        [Test]
        public void NullObjThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() =>
                EnumerableComparer<object>.Default.GetHashCode(null));

        [Test, AutoDomainData]
        public void ItemEquality(IEnumerable<object> sut)
        {
            var comparer = EnumerableComparer<object>.Default;
            comparer.GetHashCode(sut)
                .ShouldBe(comparer.GetHashCode(sut.ToArray()));
        }
    }
}
