using JetBrains.Annotations;
using System.Collections.Generic;

namespace Annex.Global
{
    public static partial class GlobalStructExtensions
    {
        /// <summary>
        ///     Returns null if the source value equals the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the source value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns>
        ///     <c>null</c> if the source value equals the provided value, otherwise <c>false</c>.
        /// </returns>
        public static T? NullIf<T>(this T @this, T value) where T : struct =>
            @this.NullIf(value, EqualityComparer<T>.Default);

        /// <summary>
        ///     Returns null if the source value equals the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the source value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <param name="comparer">The comparer to use to check equality.</param>
        /// <returns>
        ///     <c>null</c> if the source value equals the provided value, otherwise <c>false</c>.
        /// </returns>
        public static T? NullIf<T>(this T @this, T value,
            [NotNull]IEqualityComparer<T> comparer) where T : struct =>
            comparer.Equals(@this, value) ? (T?)null : @this;
    }
}
