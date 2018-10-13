using Annex.Linq;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Linq
{
    public sealed class EnumerableExtensions_ShuffleTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(Random rng) =>
            Should.Throw<ArgumentNullException>(() => ((IEnumerable<object>)null).Shuffle(rng).ToArray());

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullRandomThrowsArgumentNullException([Substitute]IEnumerable<object> sut) =>
            Should.Throw<ArgumentNullException>(() => sut.Shuffle(null).ToArray());

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void IsLazilyEvaluated([Substitute] IEnumerable<object> sut, Random rng)
        {
            sut.Shuffle(rng);

            sut.DidNotReceive().GetEnumerator();
        }

        [Test, AutoDomainData]
        public void ReturnsRandomizedInput(object obj1, object obj2, object obj3, [Substitute]Random rng)
        {
            var sut = new[] { obj1, obj2, obj3 };
            rng.Next(Arg.Any<int>()).Returns(1, 2, 0);

            sut.Shuffle(rng).ShouldBe(new[] { obj2, obj3, obj1 });
        }
    }
}
