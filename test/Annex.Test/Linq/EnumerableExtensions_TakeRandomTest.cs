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
    public sealed class EnumerableExtensions_TakeRandomTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(int count, Random random) =>
            Should.Throw<ArgumentNullException>(() => ((IEnumerable<object>)null).TakeRandom(count, random))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullRandomThrowsArgumentNullException([Substitute]IEnumerable<object> sut, int count) =>
            Should.Throw<ArgumentNullException>(() => sut.TakeRandom(count, null))
                .ParamName.ShouldBe("random");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void CountLessThanOneThrowsArgumentException(
            [Substitute]IEnumerable<object> sut, Random random) =>
            Should.Throw<ArgumentException>(() => sut.TakeRandom(0, random))
                .ParamName.ShouldBe("count");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [SuppressMessage("ReSharper", "IteratorMethodResultIsIgnored")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void IsLazilyEvaluated([Substitute]IEnumerable<object> sut, int count, Random random)
        {
            sut.TakeRandom(count, random);

            sut.DidNotReceive().GetEnumerator();
        }

        [Test]
        [InlineAutoDomainData(1)]
        [InlineAutoDomainData(3)]
        public void EmptySequenceReturnsEmptySequence(int count, Random random) =>
            Enumerable.Empty<int>().TakeRandom(count, random).ShouldBeEmpty();

        [Test, AutoDomainData]
        public void OneMaintainsUniformProbability(
            [Substitute]IEnumerable<int> sut, [Substitute]IEnumerator<int> enumerator,
            Generator<int> g, [Substitute]Random randomSub)
        {
            var values = g.Take(10).ToArray();
            enumerator.MoveNext().Returns(true,
                Enumerable.Repeat(true, values.Length - 2).Concat(new[] { false }).ToArray());
            enumerator.Current.Returns(values[0], values.Skip(1).ToArray());
            sut.GetEnumerator().Returns(enumerator);
            var randomSet = Enumerable.Range(1, values.Length)
                .Select(n => n % 2 == 0 && n < 8 ? 0 : g.First(x => x > 0)).ToArray();
            randomSub.Next(Arg.Any<int>()).Returns(randomSet[0], randomSet.Skip(1).ToArray());

            sut.TakeRandom(1, randomSub).ShouldBe(new [] { values[5] });
            enumerator.Received(values.Length).MoveNext();
        }

        [Test, AutoDomainData]
        public void ReturnsRandomizedInput(Generator<object> g, [Substitute]Random random)
        {
            var sut = g.Take(3).ToArray();
            random.Next(Arg.Any<int>()).Returns(1, 2, 0);

            sut.TakeRandom(2, random).ShouldBe(new[] { sut[1], sut[2] });
        }
    }
}
