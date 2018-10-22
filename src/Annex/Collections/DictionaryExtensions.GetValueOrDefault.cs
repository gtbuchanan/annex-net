// Shim for https://github.com/dotnet/corefx/pull/16605
#if NETSTANDARD || NETFRAMEWORK
using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Collections
{
    public static partial class DictionaryExtensions
    {
        /// <summary>
        ///     Gets the value associated with the specified key,
        ///     or the type's default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="this">The source dictionary.</param>
        /// <param name="key">The key.</param>
        /// <returns>
        ///     The value associated with the specified key,
        ///     or the type's default value if the key was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="this"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static TValue GetValueOrDefault<TKey, TValue>(
            [NotNull]this IDictionary<TKey, TValue> @this, [NotNull]TKey key) =>
            @this.GetValueOrDefault(key, default);

        /// <summary>
        ///     Gets the value associated with the specified key,
        ///     or the default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <typeparam name="TValue">The type of value.</typeparam>
        /// <param name="this">The source dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value to use if the key is not found.</param>
        /// <returns>
        ///     The value associated with the specified key,
        ///     or the default value if the key was not found.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="this"/> or <paramref name="key"/> is <c>null</c>.
        /// </exception>
        public static TValue GetValueOrDefault<TKey, TValue>(
            [NotNull]this IDictionary<TKey, TValue> @this,
            [NotNull]TKey key, TValue defaultValue) =>
            @this.TryGetValue(key, out var value) ? value : defaultValue;
    }
}
#endif
