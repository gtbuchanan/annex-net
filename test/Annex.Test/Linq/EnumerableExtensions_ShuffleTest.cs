namespace Annex.Test.Linq;

using System.Diagnostics.CodeAnalysis;
using Annex.Linq;
using AutoFixture.AutoNSubstitute;

public sealed class EnumerableExtensions_ShuffleTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(Random random) =>
        Should.Throw<ArgumentNullException>(() => ((IEnumerable<object>?)null)!.Shuffle(random))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void NullRandomThrowsArgumentNullException([Substitute] IEnumerable<object> sut) =>
        Should.Throw<ArgumentNullException>(() => sut.Shuffle(null!))
            .ParamName
            .ShouldBe("random");

    [Theory]
    [AutoDomainData]
    public void IsLazilyEvaluated([Substitute] IEnumerable<object> sut, Random random)
    {
        sut.Shuffle(random);

        sut.DidNotReceive().GetEnumerator();
    }

    [Theory]
    [AutoDomainData]
    [SuppressMessage(
        "Security",
        "CA5394:Do not use insecure randomness",
        Justification = "Intentional for purposes of test")]
    public void ReturnsRandomizedInput(Generator<object> g, [Substitute] Random random)
    {
        var sut = g.Take(3).ToArray();
        random.Next(Arg.Any<int>()).Returns(1, 2, 0);

        sut.Shuffle(random).ShouldBe(new[] { sut[1], sut[2], sut[0] });
    }
}
