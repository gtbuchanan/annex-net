namespace Annex.Pollution;

/// <content />
public static partial class ClassExtensions
{
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
    public static T? NullIf<T>(this T @this, T value, IEqualityComparer<T> comparer)
        where T : class =>
        comparer.Equals(@this, value) ? default : @this;

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
    public static T? NullIf<T>(this T @this, T value)
        where T : class =>
        @this.NullIf(value, EqualityComparer<T>.Default);
}
