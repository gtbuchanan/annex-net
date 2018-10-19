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
    public sealed class EnumerableExtensions_RandomElementTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(Random random) =>
            Should.Throw<ArgumentNullException>(() => ((IEnumerable<object>)null).RandomElement(random))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullRandomThrowsArgumentNullException([Substitute]IEnumerable<object> sut) =>
            Should.Throw<ArgumentNullException>(() => sut.RandomElement(null))
                .ParamName.ShouldBe("random");

        [Test, AutoDomainData]
        public void EmptySequenceThrowsInvalidOperationException(Random random) =>
            Should.Throw<InvalidOperationException>(() =>
                Enumerable.Empty<int>().RandomElement(random));

        [Test, AutoDomainData]
        public void MaintainsUniformProbability(
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

            sut.RandomElement(randomSub).ShouldBe(values[5]);
            enumerator.Received(values.Length).MoveNext();
        }
    }
}
