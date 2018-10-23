using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Global
{
    public static partial class GlobalClassExtensions
    {
        /// <summary>
        /// Returns <c>null</c> if the source value is equal to the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <returns><c>null</c> if the source value is equal to the provided value, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> or <paramref name="value"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// "UnwantedValue".NullIf("UnwantedValue"); // null
        /// "WantedValue".NullIf("UnwantedValue"); // WantedValue
        /// </code>
        /// </example>
        public static T NullIf<T>([NotNull] this T @this, [NotNull] T value) where T : class =>
            @this.NullIf(value, EqualityComparer<T>.Default);

        /// <summary>
        /// Returns <c>null</c> if the source value is equal to the provided value.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <param name="value">The value to compare.</param>
        /// <param name="comparer">The comparer implementation to use to check for equality.</param>
        /// <returns><c>null</c> if the source value is equal to the provided value, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/>, <paramref name="value"/>, or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// "UnwantedValue".NullIf("UnwantedValue", EqualityComparer&lt;T&gt;.Default); // null
        /// "WantedValue".NullIf("UnwantedValue", EqualityComparer&lt;T&gt;.Default); // WantedValue
        /// </code>
        /// </example>
        public static T NullIf<T>([NotNull] this T @this, [NotNull] T value,
            [NotNull] IEqualityComparer<T> comparer) where T : class =>
            comparer.Equals(@this, value) ? null : @this;
    }
}
