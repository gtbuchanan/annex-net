// Shim for https://github.com/dotnet/corefx/pull/16605
#if NETSTANDARD || NETFRAMEWORK
namespace Annex.Collections;

using System.Diagnostics.CodeAnalysis;

/// <content />
public static partial class DictionaryExtensions
{
    /// <summary>
    /// Gets the value associated with the specified key, or the type's default
    /// value if the key is not found.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="this">The source dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>
    /// The value associated with the specified key, or the type's default value
    /// if the key was not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="key"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// var dict = new Dictionary&lt;string, string&gt; { { "Key1", "Value1" } };
    ///
    /// dict.GetValueOrDefault("Key1"); // Value1
    /// dict.GetValueOrDefault("Key2"); // null
    /// </code>
    /// </example>
    /// <seealso href="https://github.com/dotnet/corefx/pull/16605">CoreFX Addition</seealso>
    [Pure]
    [return: MaybeNull]
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> @this, TKey key)
        where TKey : notnull =>
        @this!.GetValueOrDefault(key, default);

    /// <summary>
    /// Gets the value associated with the specified key, or the specified default
    /// value if the key is not found.
    /// </summary>
    /// <typeparam name="TKey">The type of key.</typeparam>
    /// <typeparam name="TValue">The type of value.</typeparam>
    /// <param name="this">The source dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="defaultValue">The default value to use if the key is not found.</param>
    /// <returns>
    /// The value associated with the specified key, or the specified default value
    /// if the key was not found.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="key"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// var dict = new Dictionary&lt;string, string&gt; { { "Key1", "Value1" } };
    ///
    /// dict.GetValueOrDefault("Key1", string.Empty); // Value1
    /// dict.GetValueOrDefault("Key2", "Value2"); // Value2
    /// </code>
    /// </example>
    /// <seealso href="https://github.com/dotnet/corefx/pull/16605">CoreFX Addition</seealso>
    [Pure]
    [return: MaybeNull]
    [return: NotNullIfNotNull("defaultValue")]
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> @this,
        TKey key,
        [AllowNull] TValue defaultValue)
        where TKey : notnull =>
        @this.TryGetValue(key, out var value) ? value : defaultValue;
}
#endif
