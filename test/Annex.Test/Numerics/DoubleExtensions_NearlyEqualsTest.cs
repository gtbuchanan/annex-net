using Annex.Numerics;
using NUnit.Framework;

namespace Annex.Test.Numerics
{
    /// <see href="http://doubleing-point-gui.de/errors/NearlyEqualsTest.java">Adapted from doubleing-point-gui.de</see>
    public sealed class DoubleExtensions_NearlyEqualsTest
    {
        private const double EPSILON = 0.00000000000001d;

        // Regular large numbers - generally not problematic
        [TestCase(100000000000000d, 100000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(100000000000001d, 100000000000000d, EPSILON, ExpectedResult = true)]
        [TestCase(10000d, 10001d, EPSILON, ExpectedResult = false)]
        [TestCase(10001d, 10000d, EPSILON, ExpectedResult = false)]
        // Negative large numbers
        [TestCase(-100000000000000d, -100000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(-100000000000001d, -100000000000000d, EPSILON, ExpectedResult = true)]
        [TestCase(-10000d, -10001d, EPSILON, ExpectedResult = false)]
        [TestCase(-10001d, -10000d, EPSILON, ExpectedResult = false)]
        // Numbers around 1
        [TestCase(1.000000000000001d, 1.000000000000002d, EPSILON, ExpectedResult = true)]
        [TestCase(1.000000000000002d, 1.000000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(1.0002d, 1.0001d, EPSILON, ExpectedResult = false)]
        [TestCase(1.0001d, 1.0002d, EPSILON, ExpectedResult = false)]
        // Numbers around -1
        [TestCase(-1.000000000000001d, -1.000000000000002d, EPSILON, ExpectedResult = true)]
        [TestCase(-1.000000000000002d, -1.000000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(-1.0002d, -1.0001d, EPSILON, ExpectedResult = false)]
        [TestCase(-1.0001d, -1.0002d, EPSILON, ExpectedResult = false)]
        // Numbers between 1 and 0
        [TestCase(0.00000000100000000000001d, 0.00000000100000000000002d, EPSILON, ExpectedResult = true)]
        [TestCase(0.00000000100000000000002d, 0.00000000100000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(0.000000000001002d, 0.000000000001001d, EPSILON, ExpectedResult = false)]
        [TestCase(0.000000000001001d, 0.000000000001002d, EPSILON, ExpectedResult = false)]
        // Numbers between -1 and 0
        [TestCase(-0.00000000100000000000001d, -0.00000000100000000000002d, EPSILON, ExpectedResult = true)]
        [TestCase(-0.00000000100000000000002d, -0.00000000100000000000001d, EPSILON, ExpectedResult = true)]
        [TestCase(-0.000000000001002d, -0.000000000001001d, EPSILON, ExpectedResult = false)]
        [TestCase(-0.000000000001001d, -0.000000000001002d, EPSILON, ExpectedResult = false)]
        // Small differences away from zero
        [TestCase(0.3d, 0.3000000000000003d, EPSILON, ExpectedResult = true)]
        [TestCase(-0.3d, -0.3000000000000003d, EPSILON, ExpectedResult = true)]
        // Comparisons involving zero
        [TestCase(0.0d, 0.0d, EPSILON, ExpectedResult = true)]
        [TestCase(0.0d, -0.0d, EPSILON, ExpectedResult = true)]
        [TestCase(-0.0d, -0.0d, EPSILON, ExpectedResult = true)]
        [TestCase(0.00000001d, 0.0d, EPSILON, ExpectedResult = false)]
        [TestCase(0.0d, 0.00000001d, EPSILON, ExpectedResult = false)]
        [TestCase(-0.00000001d, 0.0d, EPSILON, ExpectedResult = false)]
        [TestCase(0.0d, -0.00000001d, EPSILON, ExpectedResult = false)]
        //[TestCase(0.0d, 1e-40d, 0.01d, ExpectedResult = true)] // TODO: Fix tests
        //[TestCase(1e-40d, 0.0d, 0.01d, ExpectedResult = true)]
        [TestCase(1e-40d, 0.0d, 0.000001d, ExpectedResult = false)]
        [TestCase(0.0d, 1e-40d, 0.000001d, ExpectedResult = false)]
        //[TestCase(0.0d, -1e-40d, 0.1d, ExpectedResult = true)] // TODO: Fix tests
        //[TestCase(-1e-40d, 0.0d, 0.1d, ExpectedResult = true)]
        [TestCase(-1e-40d, 0.0d, 0.00000001d, ExpectedResult = false)]
        [TestCase(0.0d, -1e-40d, 0.00000001d, ExpectedResult = false)]
        // Comparisons involving extreme values (overflow potential)
        [TestCase(double.MaxValue, double.MaxValue, EPSILON, ExpectedResult = true)]
        [TestCase(double.MaxValue, -double.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(-double.MaxValue, double.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(double.MaxValue, double.MaxValue / 2, EPSILON, ExpectedResult = false)]
        [TestCase(double.MaxValue, -double.MaxValue / 2, EPSILON, ExpectedResult = false)]
        [TestCase(-double.MaxValue, double.MaxValue / 2, EPSILON, ExpectedResult = false)]
        // Comparisons involving infinities
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, EPSILON, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, EPSILON, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.PositiveInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, -double.MaxValue, EPSILON, ExpectedResult = false)]
        // Comparisons involving NaN values
        [TestCase(double.NaN, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, 0.0d, EPSILON, ExpectedResult = false)]
        [TestCase(-0.0d, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, -0.0d, EPSILON, ExpectedResult = false)]
        [TestCase(0.0d, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, double.PositiveInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, double.NegativeInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, double.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(double.MaxValue, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, -double.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(-double.MaxValue, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, double.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(double.Epsilon, double.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(double.NaN, -double.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(-double.Epsilon, double.NaN, EPSILON, ExpectedResult = false)]
        // Comparisons of numbers on opposite sides of 0
        [TestCase(1.000000001d, -1.0d, EPSILON, ExpectedResult = false)]
        [TestCase(-1.0d, 1.000000001d, EPSILON, ExpectedResult = false)]
        [TestCase(-1.000000001d, 1.0d, EPSILON, ExpectedResult = false)]
        [TestCase(1.0d, -1.000000001d, EPSILON, ExpectedResult = false)]
        [TestCase(10 * double.Epsilon, 10 * -double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(10000 * double.Epsilon, 10000 * -double.Epsilon, EPSILON, ExpectedResult = false)]
        // The really tricky part - comparisons of numbers very close to zero.
        [TestCase(double.Epsilon, double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(double.Epsilon, -double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(-double.Epsilon, double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(double.Epsilon, 0, EPSILON, ExpectedResult = true)]
        [TestCase(0, double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(-double.Epsilon, 0, EPSILON, ExpectedResult = true)]
        [TestCase(0, -double.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(0.000000001d, -double.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(0.000000001d, double.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(double.Epsilon, 0.000000001d, EPSILON, ExpectedResult = false)]
        [TestCase(-double.Epsilon, 0.000000001d, EPSILON, ExpectedResult = false)]
        public bool NearlyEquals(double sut, double value, double epsilon) => sut.NearlyEquals(value, epsilon);
    }
}
