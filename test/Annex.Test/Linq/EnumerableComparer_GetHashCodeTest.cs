using Annex.Linq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Linq
{
    public sealed class EnumerableComparer_GetHashCodeTest
    {
        [Test]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullObjThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() =>
                EnumerableComparer<object>.Default.GetHashCode(null));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void ItemEquality(IEnumerable<object> sut)
        {
            var comparer = EnumerableComparer<object>.Default;
            comparer.GetHashCode(sut)
                .ShouldBe(comparer.GetHashCode(sut.ToArray()));
        }
    }
}
