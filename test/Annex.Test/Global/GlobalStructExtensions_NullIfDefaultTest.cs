using Annex.Global;
using AutoFixture;
using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;

namespace Annex.Test.Global
{
    public sealed class GlobalStructExtensions_NullIfDefaultTest
    {
        public static TestCaseData[] NullCases =
        {
            new TestCaseData(default(int)),
            new TestCaseData(Guid.Empty),
            new TestCaseData(default(DateTimeOffset))
        };

        [Test, AutoDomainData]
        public void ReturnsThisIfDifferentValue(Generator<int> g)
        {
            var sut = g.First(n => n != default);

            sut.NullIfDefault().ShouldBe(sut);
        }

        [TestCaseSource(nameof(NullCases))]
        public void ReturnsNullIfSameValue(object sut)
        {
            object result = NullIfDefault((dynamic)sut);

            result.ShouldBeNull();
        }

        private static T? NullIfDefault<T>(T sut) where T : struct => sut.NullIfDefault();
    }
}
