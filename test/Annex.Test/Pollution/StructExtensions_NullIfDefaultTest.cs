namespace Annex.Test.Pollution;

using Annex.Pollution;

public sealed class StructExtensions_NullIfDefaultTest
{
    public static readonly object?[] NullCases =
    {
        new object?[] { default(int) },
        new object?[] { Guid.Empty },
        new object?[] { default(DateTimeOffset) },
    };

    [Theory]
    [AutoDomainData]
    public void ReturnsThisIfDifferentValue(Generator<int> g)
    {
        var sut = g.First(n => n != default);

        sut.NullIfDefault().ShouldBe(sut);
    }

    [Theory]
    [TestCaseSource(nameof(NullCases))]
    public void ReturnsNullIfSameValue(object sut)
    {
        object result = NullIfDefault((dynamic)sut);

        result.ShouldBeNull();
    }

    private static T? NullIfDefault<T>(T sut)
        where T : struct =>
        sut.NullIfDefault();
}
