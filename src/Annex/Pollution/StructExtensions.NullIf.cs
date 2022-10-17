namespace Annex.Pollution;

// TODO: Remove these overrides in favor of the unconstrained versions when there is a solution to
// the unconstrained nullable type problem.
// https://github.com/dotnet/csharplang/blob/main/meetings/2019/LDM-2019-11-25.md#solutions

/// <content />
public static partial class StructExtensions
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
    /// 3.NullIf(3, EqualityComparer&lt;T&gt;.Default); // null
    /// 3.NullIf(2, EqualityComparer&lt;T&gt;.Default); // 3
    /// </code>
    /// </example>
    public static T? NullIf<T>(this T @this, T value, IEqualityComparer<T> comparer)
        where T : struct =>
        comparer.Equals(@this, value) ? null : @this;

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
    /// 3.NullIf(3); // null
    /// 3.NullIf(2); // 3
    /// </code>
    /// </example>
    public static T? NullIf<T>(this T @this, T value)
        where T : struct =>
        @this.NullIf(value, EqualityComparer<T>.Default);
}
