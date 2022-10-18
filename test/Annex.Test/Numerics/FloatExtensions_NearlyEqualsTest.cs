namespace Annex.Test.Numerics;

using System.Diagnostics.CodeAnalysis;
using Annex.Numerics;

/// <summary>
/// Tests for <see cref="FloatExtensions.NearlyEquals(float, float, double)"/>.
/// </summary>
/// <see href="http://floating-point-gui.de/errors/NearlyEqualsTest.java">Adapted from floating-point-gui.de.</see>
public sealed class FloatExtensions_NearlyEqualsTest
{
    private const float EPSILON = 0.00001f;

    //// Regular large numbers - generally not problematic

    [Theory]
    [TestCase(1000000f, 1000001f, EPSILON, true)]
    [TestCase(1000001f, 1000000f, EPSILON, true)]
    [TestCase(10000f, 10001f, EPSILON, false)]
    [TestCase(10001f, 10000f, EPSILON, false)]

    // Negative large numbers
    [TestCase(-1000000f, -1000001f, EPSILON, true)]
    [TestCase(-1000001f, -1000000f, EPSILON, true)]
    [TestCase(-10000f, -10001f, EPSILON, false)]
    [TestCase(-10001f, -10000f, EPSILON, false)]

    // Numbers around 1
    [TestCase(1.0000001f, 1.0000002f, EPSILON, true)]
    [TestCase(1.0000002f, 1.0000001f, EPSILON, true)]
    [TestCase(1.0002f, 1.0001f, EPSILON, false)]
    [TestCase(1.0001f, 1.0002f, EPSILON, false)]

    // Numbers around -1
    [TestCase(-1.0000001f, -1.0000002f, EPSILON, true)]
    [TestCase(-1.0000002f, -1.0000001f, EPSILON, true)]
    [TestCase(-1.0002f, -1.0001f, EPSILON, false)]
    [TestCase(-1.0001f, -1.0002f, EPSILON, false)]

    // Numbers between 1 and 0
    [TestCase(0.000000001000001f, 0.000000001000002f, EPSILON, true)]
    [TestCase(0.000000001000002f, 0.000000001000001f, EPSILON, true)]
    [TestCase(0.000000000001002f, 0.000000000001001f, EPSILON, false)]
    [TestCase(0.000000000001001f, 0.000000000001002f, EPSILON, false)]

    // Numbers between -1 and 0
    [TestCase(-0.000000001000001f, -0.000000001000002f, EPSILON, true)]
    [TestCase(-0.000000001000002f, -0.000000001000001f, EPSILON, true)]
    [TestCase(-0.000000000001002f, -0.000000000001001f, EPSILON, false)]
    [TestCase(-0.000000000001001f, -0.000000000001002f, EPSILON, false)]

    // Small differences away from zero
    [TestCase(0.3f, 0.30000003f, EPSILON, true)]
    [TestCase(-0.3f, -0.30000003f, EPSILON, true)]

    // Comparisons involving zero
    [TestCase(0.0f, 0.0f, EPSILON, true)]
    [TestCase(0.00000001f, 0.0f, EPSILON, false)]
    [TestCase(0.0f, 0.00000001f, EPSILON, false)]
    [TestCase(-0.00000001f, 0.0f, EPSILON, false)]
    [TestCase(0.0f, -0.00000001f, EPSILON, false)]
    [TestCase(0.0f, 1e-40f, 0.01f, true)]
    [TestCase(1e-40f, 0.0f, 0.01f, true)]
    [TestCase(1e-40f, 0.0f, 0.000001f, false)]
    [TestCase(0.0f, 1e-40f, 0.000001f, false)]
    [TestCase(0.0f, -1e-40f, 0.1f, true)]
    [TestCase(-1e-40f, 0.0f, 0.1f, true)]
    [TestCase(-1e-40f, 0.0f, 0.00000001f, false)]
    [TestCase(0.0f, -1e-40f, 0.00000001f, false)]

    // Comparisons involving extreme values (overflow potential)
    [TestCase(float.MaxValue, float.MaxValue, EPSILON, true)]
    [TestCase(float.MaxValue, -float.MaxValue, EPSILON, false)]
    [TestCase(-float.MaxValue, float.MaxValue, EPSILON, false)]
    [TestCase(float.MaxValue, float.MaxValue / 2, EPSILON, false)]
    [TestCase(float.MaxValue, -float.MaxValue / 2, EPSILON, false)]
    [TestCase(-float.MaxValue, float.MaxValue / 2, EPSILON, false)]

    // Comparisons involving infinities
    [TestCase(float.PositiveInfinity, float.PositiveInfinity, EPSILON, true)]
    [TestCase(float.NegativeInfinity, float.NegativeInfinity, EPSILON, true)]
    [TestCase(float.NegativeInfinity, float.PositiveInfinity, EPSILON, false)]
    [TestCase(float.PositiveInfinity, float.MaxValue, EPSILON, false)]
    [TestCase(float.NegativeInfinity, -float.MaxValue, EPSILON, false)]

    // Comparisons involving NaN values
    [TestCase(float.NaN, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, 0.0f, EPSILON, false)]
    [TestCase(-0.0f, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, -0.0f, EPSILON, false)]
    [TestCase(0.0f, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, float.PositiveInfinity, EPSILON, false)]
    [TestCase(float.PositiveInfinity, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, float.NegativeInfinity, EPSILON, false)]
    [TestCase(float.NegativeInfinity, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, float.MaxValue, EPSILON, false)]
    [TestCase(float.MaxValue, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, -float.MaxValue, EPSILON, false)]
    [TestCase(-float.MaxValue, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, float.Epsilon, EPSILON, false)]
    [TestCase(float.Epsilon, float.NaN, EPSILON, false)]
    [TestCase(float.NaN, -float.Epsilon, EPSILON, false)]
    [TestCase(-float.Epsilon, float.NaN, EPSILON, false)]

    // Comparisons of numbers on opposite sides of 0
    [TestCase(1.000000001f, -1.0f, EPSILON, false)]
    [TestCase(-1.0f, 1.000000001f, EPSILON, false)]
    [TestCase(-1.000000001f, 1.0f, EPSILON, false)]
    [TestCase(1.0f, -1.000000001f, EPSILON, false)]
    [TestCase(10 * float.Epsilon, 10 * -float.Epsilon, EPSILON, true)]
    [TestCase(10000 * float.Epsilon, 10000 * -float.Epsilon, EPSILON, false)]

    // The really tricky part - comparisons of numbers very close to zero.
    [TestCase(float.Epsilon, float.Epsilon, EPSILON, true)]
    [TestCase(float.Epsilon, -float.Epsilon, EPSILON, true)]
    [TestCase(-float.Epsilon, float.Epsilon, EPSILON, true)]
    [TestCase(float.Epsilon, 0, EPSILON, true)]
    [TestCase(0, float.Epsilon, EPSILON, true)]
    [TestCase(-float.Epsilon, 0, EPSILON, true)]
    [TestCase(0, -float.Epsilon, EPSILON, true)]
    [TestCase(0.000000001f, -float.Epsilon, EPSILON, false)]
    [TestCase(0.000000001f, float.Epsilon, EPSILON, false)]
    [TestCase(float.Epsilon, 0.000000001f, EPSILON, false)]
    [TestCase(-float.Epsilon, 0.000000001f, EPSILON, false)]
    [SuppressMessage(
        "Usage",
        "xUnit1025:InlineData should be unique within the Theory it belongs to",
        Justification = "False positive")]
    public void NearlyEquals(float sut, float value, float epsilon, bool expected) =>
        sut.NearlyEquals(value, epsilon).ShouldBe(expected);
}
