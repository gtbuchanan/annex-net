using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Numerics
{
    public static partial class FloatExtensions
    {
        /// <summary>
        /// Returns a value indicating if this instance and a specified <see cref="float"/>
        /// object represent nearly the same value. A more reliable version of <see cref="float.Equals(float)"/>.
        /// </summary>
        /// <param name="this">The source <see cref="float"/>.</param>
        /// <param name="value">The <see cref="float"/> to compare.</param>
        /// <param name="epsilon">The precision of the comparison.</param>
        /// <returns><c>true</c> if the value is nearly equal to the target, otherwise <c>false</c>.</returns>
        /// <seealso href="https://stackoverflow.com/a/44355368/1409101">Adapted from StackOverflow</seealso>
        /// <seealso href="http://csharpindepth.com/Articles/General/FloatingPoint.aspx">C# in Depth: Binary floating point and .NET</seealso>
        [Pure]
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator",
            Justification = "Purpose of method is to compare floats.")]
        public static bool NearlyEquals(this float @this, float value, double epsilon)
        {
            // Handle infinities
            if (@this == value)
                return true;

            // Either are zero or both are extremely close to it
            // so relative error is less meaningful here
            var diff = Math.Abs(@this - value);
            if (@this == 0 || value == 0 || diff < FloatEx.MinNormal)
                return diff < epsilon * FloatEx.MinNormal;

            // Use relative error
            var absA = Math.Abs(@this);
            var absB = Math.Abs(value);
            return diff / Math.Min(absA + absB, float.MaxValue) < epsilon;
        }
    }
}
