namespace Annex.Test.Numerics;

using Annex.Numerics;

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

    /// <summary>
    /// Tests that <see cref="FloatExtensions.ToExactString(float)"/> returns the expected result.
    /// </summary>
    /// <param name="sut">The float under test.</param>
    /// <param name="expected">The expected result.</param>
    /// <see href="https://www.h-schmidt.net/FloatConverter/IEEE754.html">IEEE-754 Floating Point Converter.</see>
    [Theory]
    [TestCase(-100f, "-100")]
    [TestCase(0f, "0")]
    [TestCase(1e-5f, "0.00000999999974737875163555145263671875")]
    [TestCase(0.01f, "0.00999999977648258209228515625")]
    [TestCase(1f, "1")]
    [TestCase(1.1f, "1.10000002384185791015625")]
    [TestCase(100f, "100")]
    public void ReturnsExpectedResult(float sut, string expected) =>
        sut.ToExactString().ShouldBe(expected);
}
