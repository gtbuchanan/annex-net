namespace Annex.Test.Numerics;

using Annex.Numerics;

public sealed class IntervalTest
{
    [Test]
    public void Unbounded_IsUnbounded()
    {
        var sut = Interval<int>.Unbounded;

        sut.ShouldSatisfyAllConditions(
            () => sut.LeftEndpoint.ShouldBe(LeftIntervalEndpoint<int>.Unbounded),
            () => sut.RightEndpoint.ShouldBe(RightIntervalEndpoint<int>.Unbounded),
            () => sut.Comparer.ShouldBe(Comparer<int>.Default),
            () => sut.Classification.ShouldBe(IntervalClassification.Unbounded));
    }

    [Theory]
    [AutoDomainData]
    public void Contains_WhenUnbounded_ReturnsTrue(int value)
    {
        var sut = Interval<int>.Unbounded;

        sut.Contains(value).ShouldBeTrue();
    }
}
