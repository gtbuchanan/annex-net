using Annex.Numerics;
using NUnit.Framework;

namespace Annex.Test.Numerics
{
    /// <see href="http://floating-point-gui.de/errors/NearlyEqualsTest.java">Adapted from floating-point-gui.de</see>
    public sealed class FloatExtensions_NearlyEqualsTest
    {
        private const float EPSILON = 0.00001f;

        // Regular large numbers - generally not problematic
        [TestCase(1000000f, 1000001f, EPSILON, ExpectedResult = true)]
        [TestCase(1000001f, 1000000f, EPSILON, ExpectedResult = true)]
        [TestCase(10000f, 10001f, EPSILON, ExpectedResult = false)]
        [TestCase(10001f, 10000f, EPSILON, ExpectedResult = false)]
        // Negative large numbers
        [TestCase(-1000000f, -1000001f, EPSILON, ExpectedResult = true)]
        [TestCase(-1000001f, -1000000f, EPSILON, ExpectedResult = true)]
        [TestCase(-10000f, -10001f, EPSILON, ExpectedResult = false)]
        [TestCase(-10001f, -10000f, EPSILON, ExpectedResult = false)]
        // Numbers around 1
        [TestCase(1.0000001f, 1.0000002f, EPSILON, ExpectedResult = true)]
        [TestCase(1.0000002f, 1.0000001f, EPSILON, ExpectedResult = true)]
        [TestCase(1.0002f, 1.0001f, EPSILON, ExpectedResult = false)]
        [TestCase(1.0001f, 1.0002f, EPSILON, ExpectedResult = false)]
        // Numbers around -1
        [TestCase(-1.0000001f, -1.0000002f, EPSILON, ExpectedResult = true)]
        [TestCase(-1.0000002f, -1.0000001f, EPSILON, ExpectedResult = true)]
        [TestCase(-1.0002f, -1.0001f, EPSILON, ExpectedResult = false)]
        [TestCase(-1.0001f, -1.0002f, EPSILON, ExpectedResult = false)]
        // Numbers between 1 and 0
        [TestCase(0.000000001000001f, 0.000000001000002f, EPSILON, ExpectedResult = true)]
        [TestCase(0.000000001000002f, 0.000000001000001f, EPSILON, ExpectedResult = true)]
        [TestCase(0.000000000001002f, 0.000000000001001f, EPSILON, ExpectedResult = false)]
        [TestCase(0.000000000001001f, 0.000000000001002f, EPSILON, ExpectedResult = false)]
        // Numbers between -1 and 0
        [TestCase(-0.000000001000001f, -0.000000001000002f, EPSILON, ExpectedResult = true)]
        [TestCase(-0.000000001000002f, -0.000000001000001f, EPSILON, ExpectedResult = true)]
        [TestCase(-0.000000000001002f, -0.000000000001001f, EPSILON, ExpectedResult = false)]
        [TestCase(-0.000000000001001f, -0.000000000001002f, EPSILON, ExpectedResult = false)]
        // Small differences away from zero
        [TestCase(0.3f, 0.30000003f, EPSILON, ExpectedResult = true)]
        [TestCase(-0.3f, -0.30000003f, EPSILON, ExpectedResult = true)]
        // Comparisons involving zero
        [TestCase(0.0f, 0.0f, EPSILON, ExpectedResult = true)]
        [TestCase(0.0f, -0.0f, EPSILON, ExpectedResult = true)]
        [TestCase(-0.0f, -0.0f, EPSILON, ExpectedResult = true)]
        [TestCase(0.00000001f, 0.0f, EPSILON, ExpectedResult = false)]
        [TestCase(0.0f, 0.00000001f, EPSILON, ExpectedResult = false)]
        [TestCase(-0.00000001f, 0.0f, EPSILON, ExpectedResult = false)]
        [TestCase(0.0f, -0.00000001f, EPSILON, ExpectedResult = false)]
        [TestCase(0.0f, 1e-40f, 0.01f, ExpectedResult = true)]
        [TestCase(1e-40f, 0.0f, 0.01f, ExpectedResult = true)]
        [TestCase(1e-40f, 0.0f, 0.000001f, ExpectedResult = false)]
        [TestCase(0.0f, 1e-40f, 0.000001f, ExpectedResult = false)]
        [TestCase(0.0f, -1e-40f, 0.1f, ExpectedResult = true)]
        [TestCase(-1e-40f, 0.0f, 0.1f, ExpectedResult = true)]
        [TestCase(-1e-40f, 0.0f, 0.00000001f, ExpectedResult = false)]
        [TestCase(0.0f, -1e-40f, 0.00000001f, ExpectedResult = false)]
        // Comparisons involving extreme values (overflow potential)
        [TestCase(float.MaxValue, float.MaxValue, EPSILON, ExpectedResult = true)]
        [TestCase(float.MaxValue, -float.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(-float.MaxValue, float.MaxValue, EPSILON, ExpectedResult = false)]
        //[TestCase(float.MaxValue, float.MaxValue / 2, EPSILON, ExpectedResult = false)]
        [TestCase(float.MaxValue, -float.MaxValue / 2, EPSILON, ExpectedResult = false)]
        [TestCase(-float.MaxValue, float.MaxValue / 2, EPSILON, ExpectedResult = false)]
        // Comparisons involving infinities
        [TestCase(float.PositiveInfinity, float.PositiveInfinity, EPSILON, ExpectedResult = true)]
        [TestCase(float.NegativeInfinity, float.NegativeInfinity, EPSILON, ExpectedResult = true)]
        [TestCase(float.NegativeInfinity, float.PositiveInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(float.PositiveInfinity, float.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(float.NegativeInfinity, -float.MaxValue, EPSILON, ExpectedResult = false)]
        // Comparisons involving NaN values
        [TestCase(float.NaN, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, 0.0f, EPSILON, ExpectedResult = false)]
        [TestCase(-0.0f, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, -0.0f, EPSILON, ExpectedResult = false)]
        [TestCase(0.0f, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, float.PositiveInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(float.PositiveInfinity, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, float.NegativeInfinity, EPSILON, ExpectedResult = false)]
        [TestCase(float.NegativeInfinity, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, float.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(float.MaxValue, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, -float.MaxValue, EPSILON, ExpectedResult = false)]
        [TestCase(-float.MaxValue, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, float.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(float.Epsilon, float.NaN, EPSILON, ExpectedResult = false)]
        [TestCase(float.NaN, -float.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(-float.Epsilon, float.NaN, EPSILON, ExpectedResult = false)]
        // Comparisons of numbers on opposite sides of 0
        [TestCase(1.000000001f, -1.0f, EPSILON, ExpectedResult = false)]
        [TestCase(-1.0f, 1.000000001f, EPSILON, ExpectedResult = false)]
        [TestCase(-1.000000001f, 1.0f, EPSILON, ExpectedResult = false)]
        [TestCase(1.0f, -1.000000001f, EPSILON, ExpectedResult = false)]
        [TestCase(10 * float.Epsilon, 10 * -float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(10000 * float.Epsilon, 10000 * -float.Epsilon, EPSILON, ExpectedResult = false)]
        // The really tricky part - comparisons of numbers very close to zero.
        [TestCase(float.Epsilon, float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(float.Epsilon, -float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(-float.Epsilon, float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(float.Epsilon, 0, EPSILON, ExpectedResult = true)]
        [TestCase(0, float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(-float.Epsilon, 0, EPSILON, ExpectedResult = true)]
        [TestCase(0, -float.Epsilon, EPSILON, ExpectedResult = true)]
        [TestCase(0.000000001f, -float.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(0.000000001f, float.Epsilon, EPSILON, ExpectedResult = false)]
        [TestCase(float.Epsilon, 0.000000001f, EPSILON, ExpectedResult = false)]
        [TestCase(-float.Epsilon, 0.000000001f, EPSILON, ExpectedResult = false)]
        public bool NearlyEquals(float sut, float value, float epsilon) => sut.NearlyEquals(value, epsilon);
    }
}
