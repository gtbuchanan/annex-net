namespace Annex.Numerics;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

/// <summary>
/// A class to allow the conversion of doubles to string representations of
/// their exact decimal values. The implementation aims for readability over
/// efficiency.
/// </summary>
/// <seealso href="http://jonskeet.uk/csharp/DoubleConverter.cs">Adapted from Jon Skeet</seealso>
/// <seealso href="http://csharpindepth.com/Articles/General/FloatingPoint.aspx">C# in Depth: Binary floating point and .NET</seealso>
[SuppressMessage(
    "Menees",
    "MEN010:The numeric literal should be replaced with a named constant",
    Justification = "This was copied and I don't want to understand it.")]
internal static class DoubleConverter
{
    /// <summary>
    /// Converts the given double to a string representation of its exact decimal value.
    /// </summary>
    /// <param name="d">The double to convert.</param>
    /// <returns>A string representation of the double's exact decimal value.</returns>
    public static string ToExactString(double d)
    {
        if (double.IsPositiveInfinity(d))
            return "+Infinity";
        if (double.IsNegativeInfinity(d))
            return "-Infinity";
        if (double.IsNaN(d))
            return "NaN";

        // Translate the double into sign, exponent and mantissa.
        var bits = BitConverter.DoubleToInt64Bits(d);

        // Note that the shift is sign-extended, hence the test against -1 not 1
        var negative = bits < 0;
        var exponent = (int)((bits >> 52) & 0x7ffL);
        var mantissa = bits & 0xfffffffffffffL;

        // Subnormal numbers; exponent is effectively one higher,
        // but there's no extra normalization bit in the mantissa
        if (exponent == 0)
            exponent++;

        // Normal numbers; leave exponent as it is but add extra
        // bit to the front of the mantissa
        else
            mantissa |= 1L << 52;

        // Bias the exponent. It's actually biased by 1023, but we're
        // treating the mantissa as m.0 rather than 0.m, so we need
        // to subtract another 52 from it.
        exponent -= 1075;

        if (mantissa == 0)
            return "0";

        // Normalize i.e., Mantissa is even
        while ((mantissa & 1) == 0)
        {
            mantissa >>= 1;
            exponent++;
        }

        // Construct a new decimal expansion with the mantissa
        var ad = new ArbitraryDecimal(mantissa);

        // If the exponent is less than 0, we need to repeatedly
        // divide by 2 - which is the equivalent of multiplying
        // by 5 and dividing by 10.
        if (exponent < 0)
        {
            for (var i = 0; i < -exponent; i++)
                ad.MultiplyBy(5);
            ad.Shift(-exponent);
        }

        // Otherwise, we need to repeatedly multiply by 2
        else
        {
            for (var i = 0; i < exponent; i++)
                ad.MultiplyBy(2);
        }

        // Finally, return the string with an appropriate sign
        return negative ? "-" + ad : ad.ToString();
    }

    private sealed class ArbitraryDecimal
    {
        private byte[] digits;

        private int decimalPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArbitraryDecimal"/> class.
        /// Constructs an arbitrary decimal expansion from the given long.
        /// The long must not be negative.
        /// </summary>
        /// <param name="x">The long value.</param>
        internal ArbitraryDecimal(long x)
        {
            var tmp = x.ToString(CultureInfo.InvariantCulture);
            this.digits = new byte[tmp.Length];
            for (var i = 0; i < tmp.Length; i++)
                this.digits[i] = (byte)(tmp[i] - '0');
            this.Normalize();
        }

        /// <summary>
        /// Converts the value to a proper decimal string representation.
        /// </summary>
        public override string ToString()
        {
            var digitString = new char[this.digits.Length];
            for (var i = 0; i < this.digits.Length; i++)
                digitString[i] = (char)(this.digits[i] + '0');

            // Simplest case - nothing after the decimal point,
            // and last real digit is non-zero, eg value=35
            if (this.decimalPoint == 0)
                return new string(digitString);

            // Fairly simple case - nothing after the decimal
            // point, but some 0s to add, eg value=350
            if (this.decimalPoint < 0)
            {
                return new string(digitString) +
                    new string('0', -this.decimalPoint);
            }

            // Nothing before the decimal point, eg 0.035
            if (this.decimalPoint >= digitString.Length)
            {
                return "0." +
                    new string('0', this.decimalPoint - digitString.Length) +
                    new string(digitString);
            }

            // Most complicated case - part of the string comes
            // before the decimal point, part comes after it,
            // eg 3.5
            return new string(digitString, 0, digitString.Length - this.decimalPoint) +
                "." +
                new string(digitString, digitString.Length - this.decimalPoint, this.decimalPoint);
        }

        /// <summary>
        /// Multiplies the current expansion by the given amount, which should
        /// only be 2 or 5.
        /// </summary>
        /// <param name="amount">The amount to multiply by.</param>
        internal void MultiplyBy(int amount)
        {
            var result = new byte[this.digits.Length + 1];
            for (var i = this.digits.Length - 1; i >= 0; i--)
            {
                var resultDigit = (this.digits[i] * amount) + result[i + 1];
                result[i] = (byte)(resultDigit / 10);
                result[i + 1] = (byte)(resultDigit % 10);
            }

            if (result[0] != 0)
                this.digits = result;
            else
                Array.Copy(result, 1, this.digits, 0, this.digits.Length);

            this.Normalize();
        }

        /// <summary>
        /// Shifts the decimal point; a negative value makes the decimal expansion
        /// bigger (as fewer digits come after the decimal place) and a positive
        /// value makes the decimal expansion smaller.
        /// </summary>
        /// <param name="amount">The amount to shift.</param>
        internal void Shift(int amount) => this.decimalPoint += amount;

        /// <summary>
        /// Removes leading/trailing zeroes from the expansion.
        /// </summary>
        private void Normalize()
        {
            int first;
            for (first = 0; first < this.digits.Length; first++)
            {
                if (this.digits[first] != 0)
                    break;
            }

            int last;
            for (last = this.digits.Length - 1; last >= 0; last--)
            {
                if (this.digits[last] != 0)
                    break;
            }

            if (first == 0 && last == this.digits.Length - 1)
                return;

            var tmp = new byte[last - first + 1];
            for (var i = 0; i < tmp.Length; i++)
                tmp[i] = this.digits[i + first];

            this.decimalPoint -= this.digits.Length - (last + 1);
            this.digits = tmp;
        }
    }
}
