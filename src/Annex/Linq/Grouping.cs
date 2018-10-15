using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    /// <inheritdoc />
    internal sealed class Grouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        /// <inheritdoc />
        public TKey Key { get; }

        private IEnumerable<TElement> Values { get; }

        /// <summary>
        ///     Creates a new instance of the <see cref="Grouping{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key for the grouping.</param>
        /// <param name="values">The values for the grouping.</param>
        internal Grouping([CanBeNull]TKey key, [CanBeNull]IEnumerable<TElement> values)
        {
            Key = key;
            Values = values ?? Enumerable.Empty<TElement>();
        }

        /// <inheritdoc />
        public IEnumerator<TElement> GetEnumerator() => Values.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    ///     Initialization helpers for <see cref="Grouping{TKey, TElement}"/>.
    /// </summary>
    public static class Grouping
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="Grouping{TKey, TElement}"/> class with no values.
        /// </summary>
        /// <typeparam name="TKey">The type of key for the grouping.</typeparam>
        /// <typeparam name="TElement">The type of element in the grouping.</typeparam>
        /// <param name="key">The key for the grouping.</param>
        /// <returns>An empty grouping with the specified key.</returns>
        public static IGrouping<TKey, TElement> Empty<TKey, TElement>([CanBeNull]TKey key = default) =>
            new Grouping<TKey, TElement>(key, null);

        /// <summary>
        ///     Creates a new instance of the <see cref="Grouping{TKey, TElement}"/> class.
        /// </summary>
        /// <typeparam name="TKey">The type of key for the grouping.</typeparam>
        /// <typeparam name="TElement">The type of element in the grouping.</typeparam>
        /// <param name="key">The key for the grouping.</param>
        /// <param name="values">The values for the grouping.</param>
        /// <returns>A grouping with the specified key and values.</returns>
        public static IGrouping<TKey, TElement> Create<TKey, TElement>(
            [CanBeNull]TKey key, [NotNull]IEnumerable<TElement> values) =>
            new Grouping<TKey, TElement>(key, values);
    }
}
