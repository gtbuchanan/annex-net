namespace Annex.Test.Numerics;

using System;
using Annex.Numerics;

[TestFixture(typeof(int))]
[TestFixture(typeof(double))]
[TestFixture(typeof(decimal))]
[TestFixture(typeof(DateTimeOffset))]
[Category("Interval")]
public class RightIntervalEndpointFixture<T>
{
    public sealed class Unbounded : RightIntervalEndpointFixture<T>
    {
        [Test]
        public void ToStringInvalidFormatThrowsFormatException()
        {
            var sut = LeftIntervalEndpoint<T>.Unbounded;

            Should.Throw<FormatException>(() => sut.ToString("X", null));
        }

        [Test]
        public void ToStringReturnsPositiveInfinity()
        {
            var sut = RightIntervalEndpoint<T>.Unbounded;

            sut.ToString().ShouldBe("+∞)");
        }
    }

    public sealed new class ToString : LeftIntervalEndpointFixture<T>
    {
        [Test]
        public void InvalidFormatThrowsFormatException()
        {
            var sut = RightIntervalEndpoint<T>.Unbounded;

            Should.Throw<FormatException>(() => sut.ToString("X", null));
        }
    }
}
