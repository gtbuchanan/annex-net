namespace Annex.Strings;

using Annex.Pollution;

/// <content />
public static partial class StringExtensions
{
    /// <summary>
    /// Returns <c>null</c> if the source string is empty.
    /// </summary>
    /// <param name="this">The source string.</param>
    /// <returns><c>null</c> if the source string is empty, otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
    /// <example>
    /// <code language="csharp">
    /// string.Empty.NullIfEmpty(); // null
    /// </code>
    /// </example>
    public static string? NullIfEmpty(this string @this) =>
        @this.NullIf(string.Empty);
}
