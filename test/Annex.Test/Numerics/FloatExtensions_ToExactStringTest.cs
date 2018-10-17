using Annex.Numerics;
using NUnit.Framework;
using Shouldly;

namespace Annex.Test.Numerics
{
    public sealed class FloatExtensions_ToExactStringTest
    {
        [Test]
        public void PositiveInfinity() =>
            float.PositiveInfinity.ToExactString().ShouldBe("+Infinity");

        [Test]
        public void NegativeInfinity() =>
            float.NegativeInfinity.ToExactString().ShouldBe("-Infinity");

        [Test]
        public void NaN() => float.NaN.ToExactString().ShouldBe("NaN");

        /// <see href="https://www.h-schmidt.net/FloatConverter/IEEE754.html">IEEE-754 Floating Point Converter</see>
        [TestCase(-100f, ExpectedResult = "-100")]
        [TestCase(0f, ExpectedResult = "0")]
        [TestCase(1e-5f, ExpectedResult = "0.00000999999974737875163555145263671875")]
        [TestCase(0.01f, ExpectedResult = "0.00999999977648258209228515625")]
        [TestCase(1f, ExpectedResult = "1")]
        [TestCase(1.1f, ExpectedResult = "1.10000002384185791015625")]
        [TestCase(100f, ExpectedResult = "100")]
        public string ReturnsExpectedResult(float sut) => sut.ToExactString();
    }
}
