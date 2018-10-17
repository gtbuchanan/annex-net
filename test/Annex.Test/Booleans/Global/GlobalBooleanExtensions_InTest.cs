using Annex.Booleans.Global;
using Annex.Linq;
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
    public sealed class GlobalBooleanExtensions_InTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException(string[] comparands) =>
            Should.Throw<ArgumentNullException>(() => default(string).In(comparands))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullComparandsThrowsArgumentNullException(string sut) =>
            Should.Throw<ArgumentNullException>(() => sut.In((string[])null))
                .ParamName.ShouldBe("comparands");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullComparerThrowsArgumentNullException(string sut, string[] comparands) =>
            Should.Throw<ArgumentNullException>(() => sut.In(null, comparands))
                .ParamName.ShouldBe("comparer");

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void WhenOneMatch(int sut, int n, Generator<int> g, Random rand) =>
            sut.In(Enumerable.Repeat(sut, 1)
                    .Concat(g.Where(x => x != sut).Take(n))
                    .Shuffle(rand)
                    .ToArray())
                .ShouldBeTrue();

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void WhenMultipleMatches(int sut, int n, Generator<int> g, Random rand) =>
            sut.In(Enumerable.Repeat(sut, 5)
                    .Concat(g.Take(n))
                    .Shuffle(rand)
                    .ToArray())
                .ShouldBeTrue();

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void WhenNoMatches(int sut, int n, Generator<int> g) =>
            sut.In(g.Where(x => x != sut).Take(n).ToArray()).ShouldBeFalse();

        [Test, AutoDomainData]
        public void UsesComparer(int sut, int n, IEqualityComparer<int> comparer)
        {
            comparer.Equals(Arg.Any<int>(), Arg.Any<int>()).Returns(false);

            sut.In(comparer, Enumerable.Repeat(sut, n).ToArray()).ShouldBeFalse();
        }
    }
}
