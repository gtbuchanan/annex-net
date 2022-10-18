namespace Annex.Test.Pollution;

using Annex.Pollution;

public sealed class StructExtensions_NullIfTest
{
    public static readonly object[] NullCases =
    {
        new object?[] { default(int) },
        new object?[] { Guid.Empty },
        new object?[] { default(DateTimeOffset) },
    };

    [Theory]
    [AutoDomainData]
    public void ReturnsThisIfDifferentValue(int sut, Generator<int> g) =>
        sut.NullIf(g.First(n => n != sut)).ShouldBe(sut);

    [Theory]
    [TestCaseSource(nameof(NullCases))]
    public void ReturnsNullIfSameValue(object sut)
    {
        object result = NullIf((dynamic)sut, (dynamic)sut);

        result.ShouldBeNull();
    }

    [Theory]
    [AutoDomainData]
    public void UsesComparer(int sut, IEqualityComparer<int> comparer)
    {
        comparer.Equals(Arg.Any<int>(), Arg.Any<int>()).Returns(false);

        sut.NullIf(sut, comparer).ShouldBe(sut);
    }

    private static T? NullIf<T>(T sut, T value)
        where T : struct =>
        sut.NullIf(value);
}
