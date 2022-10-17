#if !NET6_0_OR_GREATER
namespace Annex.Test.Strings;

using System.Diagnostics.CodeAnalysis;
using Annex.Strings;

public sealed class StringExtensions_ContainsTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(string value) =>
        Should.Throw<ArgumentNullException>(() => default(string?)!
            .Contains(value, StringComparison.InvariantCultureIgnoreCase));

    [Theory]
    [AutoDomainData]
    public void NullValueThrowsArgumentNullException(string sut) =>
        Should.Throw<ArgumentNullException>(() => sut
            .Contains(null!, StringComparison.InvariantCultureIgnoreCase));

    [Theory]
    [AutoDomainData]
    public void InvalidComparisonTypeThrowsArgumentOutOfRangeException(
        string sut, string value, Generator<int> g)
    {
        var stringComparison = (StringComparison)g.First(n => n > 1000);

        var ex = Should.Throw<ArgumentOutOfRangeException>(
            () => sut.Contains(value, stringComparison));

        ex.ShouldSatisfyAllConditions(
            () => ex.ParamName.ShouldBe("comparisonType"),
            () => ex.ActualValue.ShouldBe(stringComparison));
    }

    [Theory]
    [TestCase(
        "ThIs HaS sOmE CRAZY text",
        "has some",
        StringComparison.InvariantCultureIgnoreCase,
        true)]
    [TestCase(
        "ThIs HaS sOmE CRAZY text",
        "what?",
        StringComparison.InvariantCultureIgnoreCase,
        false)]
    [TestCase(
        "ThIs HaS sOmE CRAZY text",
        "has some",
        StringComparison.InvariantCulture,
        false)]
    public void Contains(
        string sut,
        string value,
        StringComparison comparisonType,
        bool expected) =>
        sut.Contains(value, comparisonType).ShouldBe(expected);
}
#endif
