namespace Annex.Test.Pollution;

using Annex.Linq;
using Annex.Pollution;

public sealed class Extensions_InTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(string[] comparands) =>
        Should.Throw<ArgumentNullException>(() => default(string?)!.In(comparands))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void NullComparandsThrowsArgumentNullException(string sut) =>
        Should.Throw<ArgumentNullException>(() => sut.In((null as string[])!))
            .ParamName
            .ShouldBe("comparands");

    [Theory]
    [AutoDomainData]
    public void NullComparerThrowsArgumentNullException(string sut, string[] comparands) =>
        Should.Throw<ArgumentNullException>(() => sut.In(null!, comparands))
            .ParamName
            .ShouldBe("comparer");

    [Theory]
    [AutoDomainData]
    public void WhenOneMatch(int sut, int n, Generator<int> g, Random rand) =>
        sut.In(Enumerable.Repeat(sut, 1)
            .Concat(g.Where(x => x != sut).Take(n))
            .Shuffle(rand)
            .ToArray())
            .ShouldBeTrue();

    [Theory]
    [AutoDomainData]
    public void WhenMultipleMatches(int sut, int n, Generator<int> g, Random rand) =>
        sut.In(Enumerable.Repeat(sut, 5)
            .Concat(g.Take(n))
            .Shuffle(rand)
            .ToArray())
            .ShouldBeTrue();

    [Theory]
    [AutoDomainData]
    public void WhenNoMatches(int sut, int n, Generator<int> g) =>
        sut.In(g.Where(x => x != sut).Take(n).ToArray()).ShouldBeFalse();

    [Theory]
    [AutoDomainData]
    public void UsesComparer(int sut, int n, IEqualityComparer<int> comparer)
    {
        comparer.Equals(Arg.Any<int>(), Arg.Any<int>()).Returns(false);

        sut.In(comparer, Enumerable.Repeat(sut, n).ToArray()).ShouldBeFalse();
    }
}
