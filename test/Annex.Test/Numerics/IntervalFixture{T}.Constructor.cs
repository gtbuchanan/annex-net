namespace Annex.Test.Numerics;

using Annex.Numerics;

public partial class IntervalFixture<T>
{
    public sealed class Constructor1 : IntervalFixture<T>
    {
        [Theory]
        [AutoDomainData]
        public void NullLeftEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() =>
                    new Interval<T>(null!, value, Comparer<T>.Default))
                .ParamName
                .ShouldBe("leftEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullRightEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() =>
                    new Interval<T>(value, null!, Comparer<T>.Default))
                .ParamName
                .ShouldBe("rightEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullComparerThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() =>
                    new Interval<T>(value, value, null!))
                .ParamName
                .ShouldBe("comparer");
    }

    public sealed class Constructor2 : IntervalFixture<T>
    {
        [Theory]
        [AutoDomainData]
        public void NullLeftEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() => new Interval<T>(null!, value))
                .ParamName
                .ShouldBe("leftEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullRightEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() => new Interval<T>(value, null!))
                .ParamName
                .ShouldBe("rightEndpoint");
    }
}
