using Annex.Linq;
using AutoFixture;
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
        public void NullThisThrowsArgumentNullException(Random random) =>
            Should.Throw<ArgumentNullException>(() => ((IEnumerable<object>)null).Shuffle(random))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullRandomThrowsArgumentNullException([Substitute]IEnumerable<object> sut) =>
            Should.Throw<ArgumentNullException>(() => sut.Shuffle(null))
                .ParamName.ShouldBe("random");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void IsLazilyEvaluated([Substitute]IEnumerable<object> sut, Random random)
        {
            sut.Shuffle(random);

            sut.DidNotReceive().GetEnumerator();
        }

        [Test, AutoDomainData]
        public void ReturnsRandomizedInput(Generator<object> g, [Substitute]Random random)
        {
            var sut = g.Take(3).ToArray();
            random.Next(Arg.Any<int>()).Returns(1, 2, 0);

            sut.Shuffle(random).ShouldBe(new[] { sut[1], sut[2], sut[0] });
        }
    }
}
