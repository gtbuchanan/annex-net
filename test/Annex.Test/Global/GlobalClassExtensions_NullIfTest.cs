using Annex.Global;
using Annex.Strings;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Global
{
    public sealed class GlobalClassExtensions_NullIfTest
    {
        public static TestCaseData[] NullCases =
        {
            new TestCaseData(string.Empty),
            new TestCaseData(Enumerable.Empty<string>()),
            new TestCaseData(new object())
        };

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException(string value) =>
            Should.Throw<ArgumentNullException>(() => default(string).NullIf(value))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullValueThrowsArgumentNullException(string sut) =>
            Should.Throw<ArgumentNullException>(() => sut.NullIf(null))
                .ParamName.ShouldBe("value");

        [Test, AutoDomainData]
        public void ReturnsThisIfDifferentValue(object sut, object value) =>
            sut.NullIf(value).ShouldBe(sut);

        [TestCaseSource(nameof(NullCases))]
        public void ReturnsNullIfSameValue(object sut)
        {
            object result = NullIf((dynamic)sut, (dynamic)sut);

            result.ShouldBeNull();
        }

        private static T NullIf<T>(T sut, T value) where T : class => sut.NullIf(value);

        [Test, AutoDomainData]
        public void UsesComparer(object sut, IEqualityComparer<object> comparer)
        {
            comparer.Equals(Arg.Any<object>(), Arg.Any<object>()).Returns(false);

            sut.NullIf(sut, comparer).ShouldBe(sut);
        }

        [Test, AutoDomainData]
        public void NullIfTest(object obj, string s, string s2, int n)
        {
            obj.NullIf(obj).ShouldBeNull();
            s.NullIf(s).ShouldBeNull();
            s.NullIf(s2).ShouldBe(s);
            n.NullIf(n).ShouldBeNull();
            string.Empty.NullIfEmpty().ShouldBeNull();
            new string(' ', 10).NullIfWhiteSpace();
            Guid.Empty.NullIfDefault().ShouldBeNull();
            default(int).NullIfDefault().ShouldBeNull();
        }
    }
}
