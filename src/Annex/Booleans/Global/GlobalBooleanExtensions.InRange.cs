using EnumsNET;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Booleans.Global
{
    public static partial class GlobalBooleanExtensions
    {
        private static readonly HashSet<BoundType> Excludes = new HashSet<BoundType>
        {
            BoundType.Exclude,
            BoundType.ExcludeInclude,
            BoundType.IncludeExclude
        };

        /// <summary>
        ///     Determines if the value is within the specified range.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The value to compare.</param>
        /// <param name="bound1">The first bound.</param>
        /// <param name="bound2">The second bound.</param>
        /// <param name="boundType">The type of bounds.</param>
        /// <param name="comparer">The comparer implementation to use when comparing values.</param>
        /// <returns><c>true</c> if the value is within bounds, otherwise <c>false</c>.</returns>
        public static bool InRange<T>([NotNull]this T @this,
            [NotNull]T bound1, [NotNull]T bound2,
            BoundType boundType = BoundType.Include,
            [CanBeNull]IComparer<T> comparer = null)
        {
            if (!boundType.IsDefined())
                throw new ArgumentOutOfRangeException(nameof(boundType), boundType, null);
            if (comparer == null)
                comparer = Comparer<T>.Default;
            var boundCompare = comparer.Compare(bound1, bound2);
            var lowCompare = comparer.Compare(@this, boundCompare <= 0 ? bound1 : bound2);
            var highCompare = comparer.Compare(@this, boundCompare >= 0 ? bound1 : bound2);
            return (lowCompare != 0 || highCompare != 0 || !Excludes.Contains(boundType))
                && InRange(boundType, lowCompare, highCompare);
        }

        private static bool InRange(BoundType boundType, int lowCompare, int highCompare)
        {
            switch (boundType)
            {
                case BoundType.Include:
                    return lowCompare >= 0 && highCompare <= 0;
                case BoundType.Exclude:
                    return lowCompare > 0 && highCompare < 0;
                case BoundType.IncludeExclude:
                    return lowCompare >= 0 && highCompare < 0;
                case BoundType.ExcludeInclude:
                    return lowCompare > 0 && highCompare <= 0;
                default:
                    throw new NotSupportedException($"Bound type '{boundType}' is unsupported.");
            }
        }
    }
}
