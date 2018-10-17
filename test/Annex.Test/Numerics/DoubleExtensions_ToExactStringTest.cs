using Annex.Numerics;
using NUnit.Framework;
using Shouldly;

namespace Annex.Test.Numerics
{
    public sealed class DoubleExtensions_ToExactStringTest
    {
        [Test]
        public void PositiveInfinity() =>
            double.PositiveInfinity.ToExactString().ShouldBe("+Infinity");

        [Test]
        public void NegativeInfinity() =>
            double.NegativeInfinity.ToExactString().ShouldBe("-Infinity");

        [Test]
        public void NaN() => double.NaN.ToExactString().ShouldBe("NaN");

        [TestCase(-100d, ExpectedResult = "-100")]
        [TestCase(0d, ExpectedResult = "0")]
        [TestCase(1e-5d, ExpectedResult = "0.000010000000000000000818030539140313095458623138256371021270751953125")]
        [TestCase(0.01d, ExpectedResult = "0.01000000000000000020816681711721685132943093776702880859375")]
        [TestCase(1d, ExpectedResult = "1")]
        [TestCase(1.1d, ExpectedResult = "1.100000000000000088817841970012523233890533447265625")]
        [TestCase(100d, ExpectedResult = "100")]
        public string ReturnsExpectedResult(double sut) => sut.ToExactString();
    }
}
