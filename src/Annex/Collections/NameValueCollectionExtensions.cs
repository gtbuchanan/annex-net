namespace Annex.Collections;

using System.Collections.Specialized;

/// <summary>
/// Extension methods for <see cref="NameValueCollection"/>.
/// </summary>
[PublicAPI]
public static partial class NameValueCollectionExtensions
{
    [Pure]
    internal static IEnumerable<string> GetValuesSafe(
        this NameValueCollection @this, string? key) =>
        @this.GetValues(key) ?? Enumerable.Empty<string>();
}
