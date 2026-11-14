namespace Annex.Test.Numerics;

using Annex.Numerics;

[Category("Interval")]
public sealed class LeftIntervalEndpointFixture
{
    public sealed class FromSingle
    {
        [Test]
        public void NullReturnsUnbounded() =>
        LeftIntervalEndpoint.FromSingle(null)
            .ShouldBe(LeftIntervalEndpoint<float>.Unbounded);

        [Test]
        public void NaNThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => LeftIntervalEndpoint.FromSingle(float.NaN))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void PositiveInfinityThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => LeftIntervalEndpoint.FromSingle(float.PositiveInfinity))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void NegativeInfinityReturnsUnbounded() =>
            LeftIntervalEndpoint.FromSingle(float.NegativeInfinity)
                .ShouldBe(LeftIntervalEndpoint<float>.Unbounded);
    }

    public sealed class FromDouble
    {
        [Test]
        public void NullReturnsUnbounded() =>
        LeftIntervalEndpoint.FromDouble(null)
            .ShouldBe(LeftIntervalEndpoint<double>.Unbounded);

        [Test]
        public void NaNThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => LeftIntervalEndpoint.FromDouble(double.NaN))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void PositiveInfinityThrowsArgumentException() =>
            Should.Throw<ArgumentException>(() => LeftIntervalEndpoint.FromDouble(double.PositiveInfinity))
                .ParamName
                .ShouldBe("value");

        [Test]
        public void NegativeInfinityReturnsUnbounded() =>
            LeftIntervalEndpoint.FromDouble(double.NegativeInfinity)
                .ShouldBe(LeftIntervalEndpoint<double>.Unbounded);
    }
}
