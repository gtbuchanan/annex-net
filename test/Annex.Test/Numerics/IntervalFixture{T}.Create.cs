namespace Annex.Test.Numerics;

using Annex.Numerics;

public partial class IntervalFixture<T>
{
    public sealed class Create1 : IntervalFixture<T>
    {
        [Theory]
        [AutoDomainData]
        public void NullLeftEndpointThrowsArgumentNullException(T value) =>
           Should.Throw<ArgumentNullException>(() =>
                   Interval.Create(null!, value, Comparer<T>.Default))
               .ParamName
               .ShouldBe("leftEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullRightEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() =>
                    Interval.Create(value, null!, Comparer<T>.Default))
                .ParamName
                .ShouldBe("rightEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullComparerThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() => Interval.Create<T>(value, value, null!))
                .ParamName
                .ShouldBe("comparer");

        [Theory]
        [AutoDomainData]
        public void ForwardsConstruction(T leftValue, T rightValue, IComparer<T> comparer)
        {
            var sut = Interval.Create(leftValue, rightValue, comparer);

            sut.ShouldBe(new Interval<T>(leftValue, rightValue, comparer));
        }
    }

    public sealed class Create2 : IntervalFixture<T>
    {
        [Theory]
        [AutoDomainData]
        public void NullLeftEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() => Interval.Create<T>(null!, value))
                .ParamName
                .ShouldBe("leftEndpoint");

        [Theory]
        [AutoDomainData]
        public void NullRightEndpointThrowsArgumentNullException(T value) =>
            Should.Throw<ArgumentNullException>(() => Interval.Create<T>(value, null!))
                .ParamName
                .ShouldBe("rightEndpoint");

        [Theory]
        [AutoDomainData]
        public void ForwardsConstruction(T leftValue, T rightValue)
        {
            var sut = Interval.Create<T>(leftValue, rightValue);

            sut.ShouldBe(new Interval<T>(leftValue, rightValue));
        }
    }
}
