using Annex.Global;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Test.Global
{
    public sealed class GlobalStructExtensions_NullIfTest
    {
        public static TestCaseData[] NullCases =
        {
            new TestCaseData(default(int)),
            new TestCaseData(Guid.Empty),
            new TestCaseData(default(DateTimeOffset))
        };

        [Test, AutoDomainData]
        public void ReturnsThisIfDifferentValue(int sut, Generator<int> g) =>
            sut.NullIf(g.First(n => n != sut)).ShouldBe(sut);

        [TestCaseSource(nameof(NullCases))]
        public void ReturnsNullIfSameValue(object sut)
        {
            object result = NullIf((dynamic)sut, (dynamic)sut);

            result.ShouldBeNull();
        }

        private static T? NullIf<T>(T sut, T value) where T : struct => sut.NullIf(value);

        [Test, AutoDomainData]
        public void UsesComparer(int sut, IEqualityComparer<int> comparer)
        {
            comparer.Equals(Arg.Any<int>(), Arg.Any<int>()).Returns(false);

            sut.NullIf(sut, comparer).ShouldBe(sut);
        }
    }
}
