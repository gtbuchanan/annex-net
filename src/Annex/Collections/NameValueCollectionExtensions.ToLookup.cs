namespace Annex.Collections;

using System.Collections.Specialized;

/// <content />
public static partial class NameValueCollectionExtensions
{
    /// <summary>
    /// Converts the source <see cref="NameValueCollection"/> to an <see cref="ILookup{TKey, TElement}"/>.
    /// </summary>
    /// <param name="this">The source collection.</param>
    /// <returns>The equivalent <see cref="ILookup{TKey, TElement}"/>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
    /// <example>
    /// <code language="csharp">
    /// var nvc = new NameValueCollection { { "Key1", "Value1a" }, { "Key1", "Value1b" } };
    ///
    /// var lookup = nvc.ToLookup();
    ///
    /// lookup["Key1"]; // [ "Value1a", "Value1b" ]
    /// </code>
    /// </example>
    [Pure]
    public static ILookup<string?, string> ToLookup(
        this NameValueCollection @this) =>
        new NameValueLookup(@this);
}
