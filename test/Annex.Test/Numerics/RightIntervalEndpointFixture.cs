namespace Annex.Test.Numerics;

using Annex.Numerics;

[Category("Interval")]
public sealed class RightIntervalEndpointFixture
{
    public sealed class FromSingle
    {
        [Test]
        public void NullReturnsUnbounded() =>
        RightIntervalEndpoint.FromSingle(null)
            .ShouldBe(RightIntervalEndpoint<float>.Unbounded);

        [Test]
        public void NaNThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => RightIntervalEndpoint.FromSingle(float.NaN))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void NegativeInfinityThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => RightIntervalEndpoint.FromSingle(float.NegativeInfinity))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void PositiveInfinityReturnsUnbounded() =>
            RightIntervalEndpoint.FromSingle(float.PositiveInfinity)
                .ShouldBe(RightIntervalEndpoint<float>.Unbounded);
    }

    public sealed class FromDouble
    {
        [Test]
        public void NullReturnsUnbounded() =>
        RightIntervalEndpoint.FromDouble(null)
            .ShouldBe(RightIntervalEndpoint<double>.Unbounded);

        [Test]
        public void NaNThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => RightIntervalEndpoint.FromDouble(double.NaN))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void NegativeInfinityThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => RightIntervalEndpoint.FromDouble(double.NegativeInfinity))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void PositiveInfinityReturnsUnbounded() =>
            RightIntervalEndpoint.FromDouble(double.PositiveInfinity)
                .ShouldBe(RightIntervalEndpoint<double>.Unbounded);
    }
}
