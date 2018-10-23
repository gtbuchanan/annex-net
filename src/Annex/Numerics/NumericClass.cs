using JetBrains.Annotations;
using System;

namespace Annex.Numerics
{
    /// <summary>
    /// Represents a class of numeric types.
    /// </summary>
    [PublicAPI, Flags]
    public enum NumericClass : byte
    {
        /// <summary>
        /// Represents numbers without fractional parts.
        /// </summary>
        Integral = 0b1,

        /// <summary>
        /// Represents numbers with both integer and fractional parts.
        /// </summary>
        FloatingPoint = 0b10,

        /// <summary>
        /// Represents numbers with a real number part and an imaginary number part.
        /// </summary>
        Complex = 0b100,

        /// <summary>
        /// Represents a quantity having direction as well as magnitude, especially
        /// as determining the position of one point in space relative to another.
        /// </summary>
        Vector = 0b1000,

        /// <summary>
        /// All numeric types.
        /// </summary>
        All = Integral | FloatingPoint | Complex | Vector
    }
}
