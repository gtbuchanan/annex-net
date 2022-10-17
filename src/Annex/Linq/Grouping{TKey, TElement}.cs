namespace Annex.Linq;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

/// <inheritdoc />
[SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1649:File name should match first type name",
    Justification = "False positive")]
[PublicAPI]
public sealed class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
{
    /// <summary>
    /// The empty grouping.
    /// </summary>
    public static readonly IGrouping<TKey?, TElement> Empty =
        new Grouping<TKey?, TElement>(default, Enumerable.Empty<TElement>());

    /// <summary>
    /// Initializes a new instance of the <see cref="Grouping{TKey, TElement}"/> class.
    /// </summary>
    /// <param name="key">The key for the grouping.</param>
    /// <param name="values">The values for the grouping.</param>
    internal Grouping(TKey key, IEnumerable<TElement> values)
    {
        this.Key = key;
        this.Values = values;
    }

    /// <inheritdoc />
    public TKey Key { get; }

    private IEnumerable<TElement> Values { get; }

    /// <inheritdoc />
    public IEnumerator<TElement> GetEnumerator() => this.Values.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
