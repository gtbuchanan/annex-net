using EnumsNET;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Booleans.Global
{
    public static partial class GlobalBooleanExtensions
    {
        /// <summary>
        /// Determines if the value is within the specified range. Defaults to <see cref="BoundType.Include"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="bound1">The first bound for the range.</param>
        /// <param name="bound2">The second bound for the range.</param>
        /// <returns><c>true</c> if the value is within bounds, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/>, <paramref name="bound1"/>, or <paramref name="bound2"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// 10.InRange(1, 10); // true
        /// 10.InRange(1, 5); // false
        /// 10.InRange(20, 5); // true
        /// </code>
        /// </example>
        public static bool InRange<T>([NotNull] this T @this, [NotNull] T bound1, [NotNull] T bound2) =>
            @this.InRange(bound1, bound2, BoundType.Include);

        /// <summary>
        /// Determines if the value is within the specified range.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="bound1">The first bound for the range.</param>
        /// <param name="bound2">The second bound for the range.</param>
        /// <param name="boundType">The type of range bounds.</param>
        /// <returns><c>true</c> if the value is within bounds, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/>, <paramref name="bound1"/>, or <paramref name="bound2"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="boundType"/> is invalid.</exception>
        /// <example>
        /// <code language="csharp">
        /// 10.InRange(1, 10, BoundType.ExcludeInclude); // true
        /// 10.InRange(10, 1, BoundType.ExcludeInclude); // true
        /// 10.InRange(10, 1, BoundType.IncludeExclude); // false
        /// 10.InRange(1, 10, BoundType.Exclude); // false
        /// </code>
        /// </example>
        public static bool InRange<T>([NotNull] this T @this,
            [NotNull] T bound1, [NotNull] T bound2, BoundType boundType) =>
            @this.InRange(bound1, bound2, boundType, Comparer<T>.Default);

        /// <summary>
        /// Determines if the value is within the specified range. Defaults to <see cref="BoundType.Include"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="bound1">The first bound for the range.</param>
        /// <param name="bound2">The second bound for the range.</param>
        /// <param name="comparer">The comparer implementation to use when comparing values.</param>
        /// <returns><c>true</c> if the value is within bounds, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/>, <paramref name="bound1"/>, <paramref name="bound2"/>,
        /// or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// 10.InRange(1, 10, Comparer&lt;T&gt;.Default); // true
        /// </code>
        /// </example>
        public static bool InRange<T>([NotNull] this T @this, [NotNull] T bound1,
            [NotNull] T bound2, [NotNull] IComparer<T> comparer) =>
            @this.InRange(bound1, bound2, BoundType.Include, comparer);

        /// <summary>
        /// Determines if the value is within the specified range.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="bound1">The first bound for the range.</param>
        /// <param name="bound2">The second bound for the range.</param>
        /// <param name="boundType">The type of range bounds.</param>
        /// <param name="comparer">The comparer implementation to use when comparing values.</param>
        /// <returns><c>true</c> if the value is within bounds, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/>, <paramref name="bound1"/>, <paramref name="bound2"/>,
        /// or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="boundType"/> is invalid.</exception>
        /// <example>
        /// <code language="csharp">
        /// 10.InRange(1, 10, BoundType.Include, Comparer&lt;T&gt;.Default); // true
        /// </code>
        /// </example>
        public static bool InRange<T>([NotNull] this T @this, [NotNull] T bound1, [NotNull] T bound2,
            BoundType boundType, [NotNull] IComparer<T> comparer)
        {
            if (!boundType.IsDefined())
                throw new ArgumentOutOfRangeException(nameof(boundType), boundType, null);
            var boundCompare = comparer.Compare(bound1, bound2);
            var lowCompare = comparer.Compare(@this, boundCompare <= 0 ? bound1 : bound2);
            var highCompare = comparer.Compare(@this, boundCompare >= 0 ? bound1 : bound2);
            return (lowCompare != 0 || highCompare != 0 || !boundType.IsExclude())
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
