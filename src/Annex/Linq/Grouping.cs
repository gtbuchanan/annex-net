namespace Annex.Linq;

/// <summary>
/// Initialization helpers for <see cref="Grouping{TKey, TElement}"/>.
/// </summary>
public static class Grouping
{
    /// <summary>
    /// Create a new instance of the <see cref="Grouping{TKey, TElement}"/> class with no values.
    /// </summary>
    /// <typeparam name="TKey">The type of key for the grouping.</typeparam>
    /// <typeparam name="TElement">The type of element in the grouping.</typeparam>
    /// <param name="key">The key for the grouping.</param>
    /// <returns>An empty grouping with the specified key.</returns>
    public static IGrouping<TKey, TElement> Create<TKey, TElement>(TKey key) =>
        new Grouping<TKey, TElement>(key, Enumerable.Empty<TElement>());

    /// <summary>
    /// Create a new instance of the <see cref="Grouping{TKey, TElement}"/> class.
    /// </summary>
    /// <typeparam name="TKey">The type of key for the grouping.</typeparam>
    /// <typeparam name="TElement">The type of element in the grouping.</typeparam>
    /// <param name="key">The key for the grouping.</param>
    /// <param name="values">The values for the grouping.</param>
    /// <returns>A grouping with the specified key and values.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="values"/> is <c>null</c>.</exception>
    public static IGrouping<TKey, TElement> Create<TKey, TElement>(
        TKey key, IEnumerable<TElement> values) =>
        new Grouping<TKey, TElement>(key, values);
}
