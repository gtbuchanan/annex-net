using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Numerics
{
    public static partial class FloatExtensions
    {
        /// <summary>
        ///     Returns a value indicating whether this instance and a specified <see cref="float"/>
        ///     object represent nearly the same value. A more reliable version of <see cref="float.Equals(float)"/>.
        /// </summary>
        /// <param name="this">The target <see cref="float"/>.</param>
        /// <param name="value">The <see cref="float"/> to compare.</param>
        /// <param name="epsilon">The precision of the comparison.</param>
        /// <returns><c>true</c> if the value is nearly equal to the target, otherwise <c>false</c>.</returns>
        /// <see href="https://stackoverflow.com/a/44355368/1409101">Adapted from StackOverflow</see>
        [Pure]
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator",
            Justification = "Purpose of method is to compare floats.")]
        public static bool NearlyEquals(this float @this, float value, double epsilon)
        {
            const float MIN_NORMAL = (1 << 23) * float.Epsilon;
            var absA = Math.Abs(@this);
            var absB = Math.Abs(value);
            var diff = Math.Abs(@this - value);

            // Shortcut, handles infinities
            if (@this == value)
                return true;

            // Either are zero or both are extremely close to it
            // relative error is less meaningful here
            if (@this == 0 || value == 0 || diff < MIN_NORMAL)
                return diff < epsilon * MIN_NORMAL;

            // Use relative error
            return diff / Math.Min(absA + absB, float.MaxValue) < epsilon;
        }
    }
}
