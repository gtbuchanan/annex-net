using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Global
{
    public static partial class GlobalStructExtensions
    {
        /// <summary>
        /// Returns <c>null</c> if the source value is equal to the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns>
        /// <c>null</c> if the source value is equal to the provided value, otherwise <c>false</c>.
        /// </returns>
        /// <example>
        /// <code language="csharp">
        /// 10.NullIf(10); // null
        /// 5.NullIf(10); // 5
        /// </code>
        /// </example>
        public static T? NullIf<T>(this T @this, T value) where T : struct =>
            @this.NullIf(value, EqualityComparer<T>.Default);

        /// <summary>
        /// Returns <c>null</c> if the source value is equal to the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <param name="comparer">The comparer implementation to use to check for equality.</param>
        /// <returns>
        /// <c>null</c> if the source value equals the provided value, otherwise <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="comparer"/> is <c>null</c>.</exception>
        /// <example>
        /// <code language="csharp">
        /// 10.NullIf(10, EqualityComparer&lt;T&gt;.Default); // null
        /// 5.NullIf(10, EqualityComparer&lt;T&gt;.Default); // 5
        /// </code>
        /// </example>
        public static T? NullIf<T>(this T @this, T value,
            [NotNull]IEqualityComparer<T> comparer) where T : struct =>
            comparer.Equals(@this, value) ? (T?)null : @this;
    }
}
