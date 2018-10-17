using Annex.Booleans.Global;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Booleans.Global
{
    public sealed class GlobalBooleanExtensions_InRangeTest
    {
        public static TestCaseData[] Cases =
        {
            // Include
            new TestCaseData(1, 1, 1, BoundType.Include) { ExpectedResult = true },
            new TestCaseData(1, 1, 10, BoundType.Include) { ExpectedResult = true },
            new TestCaseData(1, 10, 1, BoundType.Include) { ExpectedResult = true },
            new TestCaseData(5, 10, 1, BoundType.Include) { ExpectedResult = true },
            new TestCaseData("a", "a", "a", BoundType.Include) { ExpectedResult = true },
            new TestCaseData("a", "a", "d", BoundType.Include) { ExpectedResult = true },
            new TestCaseData("a", "d", "a", BoundType.Include) { ExpectedResult = true },
            new TestCaseData("b", "d", "a", BoundType.Include) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(1), BoundType.Include) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(10), BoundType.Include) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(10), new IntWrapper(1), BoundType.Include) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(5), new IntWrapper(10), new IntWrapper(1), BoundType.Include) { ExpectedResult = true },

            // Exclude
            new TestCaseData(1, 1, 1, BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(1, 1, 10, BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(1, 10, 1, BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(5, 10, 1, BoundType.Exclude) { ExpectedResult = true },
            new TestCaseData("a", "a", "a", BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData("a", "a", "d", BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData("a", "d", "a", BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData("b", "d", "a", BoundType.Exclude) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(1), BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(10), BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(1), new IntWrapper(10), new IntWrapper(1), BoundType.Exclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(5), new IntWrapper(10), new IntWrapper(1), BoundType.Exclude) { ExpectedResult = true },

            // IncludeExclude
            new TestCaseData(1, 1, 1, BoundType.IncludeExclude) { ExpectedResult = false },
            new TestCaseData(1, 1, 10, BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData(1, 10, 1, BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData(5, 10, 1, BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData("a", "a", "a", BoundType.IncludeExclude) { ExpectedResult = false },
            new TestCaseData("a", "a", "d", BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData("a", "d", "a", BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData("b", "d", "a", BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(1), BoundType.IncludeExclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(10), BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(10), new IntWrapper(1), BoundType.IncludeExclude) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(5), new IntWrapper(10), new IntWrapper(1), BoundType.IncludeExclude) { ExpectedResult = true },

            // ExcludeInclude
            new TestCaseData(1, 1, 1, BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(1, 1, 10, BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(1, 10, 1, BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(5, 10, 1, BoundType.ExcludeInclude) { ExpectedResult = true },
            new TestCaseData("a", "a", "a", BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData("a", "a", "d", BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData("a", "d", "a", BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData("b", "d", "a", BoundType.ExcludeInclude) { ExpectedResult = true },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(1), BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(1), new IntWrapper(1), new IntWrapper(10), BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(1), new IntWrapper(10), new IntWrapper(1), BoundType.ExcludeInclude) { ExpectedResult = false },
            new TestCaseData(new IntWrapper(5), new IntWrapper(10), new IntWrapper(1), BoundType.ExcludeInclude) { ExpectedResult = true }
        };

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException(string bound1, string bound2) =>
            Should.Throw<ArgumentNullException>(() => default(string).InRange(bound1, bound2))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void UndefinedBoundTypeThrowsArgumentOutOfRangeException(int sut, Generator<int> g, int bound1, int bound2) =>
            Should.Throw<ArgumentOutOfRangeException>(() => sut.InRange(bound1, bound2, (BoundType)g.First(n => n > 100)))
                .ParamName.ShouldBe("boundType");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullBound1ThrowsArgumentNullException(string sut, string bound2) =>
            Should.Throw<ArgumentNullException>(() => sut.InRange(null, bound2))
                .ParamName.ShouldBe("bound1");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullBound2ThrowsArgumentNullException(string sut, string bound1) =>
            Should.Throw<ArgumentNullException>(() => sut.InRange(bound1, null))
                .ParamName.ShouldBe("bound2");

        [TestCaseSource(nameof(Cases))]
        public bool ReturnsExpectedResult(object sut, object bounds1, object bounds2, BoundType boundType) =>
            InRange((dynamic)sut, (dynamic)bounds1, (dynamic)bounds2, boundType);

        private static bool InRange<T>(T sut, T bound1, T bound2, BoundType boundType) =>
            sut.InRange(bound1, bound2, boundType);

        [Test, AutoDomainData]
        public void UsesComparer(int sut, IComparer<int> comparer)
        {
            comparer.Compare(Arg.Any<int>(), Arg.Any<int>()).Returns(-1);

            sut.InRange(sut - 1, sut + 1, comparer: comparer).ShouldBeFalse();
        }

        [ExcludeFromCodeCoverage]
        private sealed class IntWrapper : IComparable, IComparable<IntWrapper>
        {
            private int N { get; }

            public IntWrapper(int n) => N = n;

            public int CompareTo(object obj) => N.CompareTo(obj);

            public int CompareTo(IntWrapper other) => N.CompareTo(other.N);

            public override string ToString() => $"[{N}]";
        }
    }
}
