namespace Annex.Test.Numerics;

using System.Diagnostics.CodeAnalysis;
using Annex.Numerics;

/// <summary>
/// Tests for <see cref="DoubleExtensions.NearlyEquals(double, double, double)"/>.
/// </summary>
/// <see href="http://doubleing-point-gui.de/errors/NearlyEqualsTest.java">Adapted from doubleing-point-gui.de.</see>
public sealed class DoubleExtensions_NearlyEqualsTest
{
    private const double EPSILON = 0.00000000000001d;

    //// Regular large numbers - generally not problematic

    [Theory]
    [TestCase(100000000000000d, 100000000000001d, EPSILON, true)]
    [TestCase(100000000000001d, 100000000000000d, EPSILON, true)]
    [TestCase(10000d, 10001d, EPSILON, false)]
    [TestCase(10001d, 10000d, EPSILON, false)]

    // Negative large numbers
    [TestCase(-100000000000000d, -100000000000001d, EPSILON, true)]
    [TestCase(-100000000000001d, -100000000000000d, EPSILON, true)]
    [TestCase(-10000d, -10001d, EPSILON, false)]
    [TestCase(-10001d, -10000d, EPSILON, false)]

    // Numbers around 1
    [TestCase(1.000000000000001d, 1.000000000000002d, EPSILON, true)]
    [TestCase(1.000000000000002d, 1.000000000000001d, EPSILON, true)]
    [TestCase(1.0002d, 1.0001d, EPSILON, false)]
    [TestCase(1.0001d, 1.0002d, EPSILON, false)]

    // Numbers around -1
    [TestCase(-1.000000000000001d, -1.000000000000002d, EPSILON, true)]
    [TestCase(-1.000000000000002d, -1.000000000000001d, EPSILON, true)]
    [TestCase(-1.0002d, -1.0001d, EPSILON, false)]
    [TestCase(-1.0001d, -1.0002d, EPSILON, false)]

    // Numbers between 1 and 0
    [TestCase(
        0.00000000100000000000001d,
        0.00000000100000000000002d,
        EPSILON,
        true)]
    [TestCase(
        0.00000000100000000000002d,
        0.00000000100000000000001d,
        EPSILON,
        true)]
    [TestCase(0.000000000001002d, 0.000000000001001d, EPSILON, false)]
    [TestCase(0.000000000001001d, 0.000000000001002d, EPSILON, false)]

    // Numbers between -1 and 0
    [TestCase(
        -0.00000000100000000000001d,
        -0.00000000100000000000002d,
        EPSILON,
        true)]
    [TestCase(
        -0.00000000100000000000002d,
        -0.00000000100000000000001d,
        EPSILON,
        true)]
    [TestCase(-0.000000000001002d, -0.000000000001001d, EPSILON, false)]
    [TestCase(-0.000000000001001d, -0.000000000001002d, EPSILON, false)]

    // Small differences away from zero
    [TestCase(0.3d, 0.3000000000000003d, EPSILON, true)]
    [TestCase(-0.3d, -0.3000000000000003d, EPSILON, true)]

    // Comparisons involving zero
    [TestCase(0.0d, 0.0d, EPSILON, true)]
    [TestCase(0.0d, -0.0d, EPSILON, true)]
    [TestCase(-0.0d, -0.0d, EPSILON, true)]
    [TestCase(0.00000001d, 0.0d, EPSILON, false)]
    [TestCase(0.0d, 0.00000001d, EPSILON, false)]
    [TestCase(-0.00000001d, 0.0d, EPSILON, false)]
    [TestCase(0.0d, -0.00000001d, EPSILON, false)]

    // [TestCase(0.0d, 1e-40d, 0.01d, true)] // TODO: Fix tests
    // [TestCase(1e-40d, 0.0d, 0.01d, true)]
    [TestCase(1e-40d, 0.0d, 0.000001d, false)]
    [TestCase(0.0d, 1e-40d, 0.000001d, false)]

    // [TestCase(0.0d, -1e-40d, 0.1d, true)] // TODO: Fix tests
    // [TestCase(-1e-40d, 0.0d, 0.1d, true)]
    [TestCase(-1e-40d, 0.0d, 0.00000001d, false)]
    [TestCase(0.0d, -1e-40d, 0.00000001d, false)]

    // Comparisons involving extreme values (overflow potential)
    [TestCase(double.MaxValue, double.MaxValue, EPSILON, true)]
    [TestCase(double.MaxValue, -double.MaxValue, EPSILON, false)]
    [TestCase(-double.MaxValue, double.MaxValue, EPSILON, false)]
    [TestCase(double.MaxValue, double.MaxValue / 2, EPSILON, false)]
    [TestCase(double.MaxValue, -double.MaxValue / 2, EPSILON, false)]
    [TestCase(-double.MaxValue, double.MaxValue / 2, EPSILON, false)]

    // Comparisons involving infinities
    [TestCase(double.PositiveInfinity, double.PositiveInfinity, EPSILON, true)]
    [TestCase(double.NegativeInfinity, double.NegativeInfinity, EPSILON, true)]
    [TestCase(double.NegativeInfinity, double.PositiveInfinity, EPSILON, false)]
    [TestCase(double.PositiveInfinity, double.MaxValue, EPSILON, false)]
    [TestCase(double.NegativeInfinity, -double.MaxValue, EPSILON, false)]

    // Comparisons involving NaN values
    [TestCase(double.NaN, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, 0.0d, EPSILON, false)]
    [TestCase(-0.0d, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, -0.0d, EPSILON, false)]
    [TestCase(0.0d, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, double.PositiveInfinity, EPSILON, false)]
    [TestCase(double.PositiveInfinity, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, double.NegativeInfinity, EPSILON, false)]
    [TestCase(double.NegativeInfinity, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, double.MaxValue, EPSILON, false)]
    [TestCase(double.MaxValue, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, -double.MaxValue, EPSILON, false)]
    [TestCase(-double.MaxValue, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, double.Epsilon, EPSILON, false)]
    [TestCase(double.Epsilon, double.NaN, EPSILON, false)]
    [TestCase(double.NaN, -double.Epsilon, EPSILON, false)]
    [TestCase(-double.Epsilon, double.NaN, EPSILON, false)]

    // Comparisons of numbers on opposite sides of 0
    [TestCase(1.000000001d, -1.0d, EPSILON, false)]
    [TestCase(-1.0d, 1.000000001d, EPSILON, false)]
    [TestCase(-1.000000001d, 1.0d, EPSILON, false)]
    [TestCase(1.0d, -1.000000001d, EPSILON, false)]
    [TestCase(10 * double.Epsilon, 10 * -double.Epsilon, EPSILON, true)]
    [TestCase(10000 * double.Epsilon, 10000 * -double.Epsilon, EPSILON, false)]

    // The really tricky part - comparisons of numbers very close to zero.
    [TestCase(double.Epsilon, double.Epsilon, EPSILON, true)]
    [TestCase(double.Epsilon, -double.Epsilon, EPSILON, true)]
    [TestCase(-double.Epsilon, double.Epsilon, EPSILON, true)]
    [TestCase(double.Epsilon, 0, EPSILON, true)]
    [TestCase(0, double.Epsilon, EPSILON, true)]
    [TestCase(-double.Epsilon, 0, EPSILON, true)]
    [TestCase(0, -double.Epsilon, EPSILON, true)]
    [TestCase(0.000000001d, -double.Epsilon, EPSILON, false)]
    [TestCase(0.000000001d, double.Epsilon, EPSILON, false)]
    [TestCase(double.Epsilon, 0.000000001d, EPSILON, false)]
    [TestCase(-double.Epsilon, 0.000000001d, EPSILON, false)]
    [SuppressMessage(
        "Menees",
        "MEN003:Method is too long",
        Justification = "Test case attributes are counted towards method length")]
    public void NearlyEquals(double sut, double value, double epsilon, bool expected) =>
        sut.NearlyEquals(value, epsilon).ShouldBe(expected);
}
