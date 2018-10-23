using Annex.Strings;
using AutoFixture;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Annex.Test.Strings
{
    public sealed class StringExtensions_ContainsTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(string value) =>
            Should.Throw<ArgumentNullException>(() => default(string).Contains(value, StringComparison.InvariantCultureIgnoreCase));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullValueThrowsArgumentNullException(string sut) =>
            Should.Throw<ArgumentNullException>(() => sut.Contains(null, StringComparison.InvariantCultureIgnoreCase));

        [Test, AutoDomainData]
        public void InvalidComparisonTypeThrowsArgumentOutOfRangeException(string sut, string value, Generator<int> g)
        {
            var stringComparison = (StringComparison)g.First(n => n > 1000);

            var ex = Should.Throw<ArgumentOutOfRangeException>(() => sut.Contains(value, stringComparison));

            ex.ShouldSatisfyAllConditions(
                () => ex.ParamName.ShouldBe("comparisonType"),
                () => ex.ActualValue.ShouldBe(stringComparison));
        }

        [TestCase("ThIs HaS sOmE CRAZY text", "has some", StringComparison.InvariantCultureIgnoreCase, ExpectedResult = true)]
        [TestCase("ThIs HaS sOmE CRAZY text", "what?", StringComparison.InvariantCultureIgnoreCase, ExpectedResult = false)]
        [TestCase("ThIs HaS sOmE CRAZY text", "has some", StringComparison.InvariantCulture, ExpectedResult = false)]
        public bool Contains(string sut, string value, StringComparison comparisonType) =>
            sut.Contains(value, comparisonType);
    }
}
