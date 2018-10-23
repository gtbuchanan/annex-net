using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    public static partial class LookupExtensions
    {
        /// <summary>
        /// Converts the <see cref="ILookup{TKey,TElement}"/> to an equivalent <see cref="IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the lookup.</typeparam>
        /// <typeparam name="TElement">The type of elements in the lookup.</typeparam>
        /// <param name="this">The source lookup.</param>
        /// <returns>An equivalent dictionary representation of the lookup.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
        /// <seealso href="https://stackoverflow.com/a/11383405/1409101">Adapted from StackOverflow</seealso>
        [Pure, NotNull]
        public static IDictionary<TKey, IEnumerable<TElement>> ToDictionary<TKey, TElement>(
            [NotNull] this ILookup<TKey, TElement> @this) =>
            @this.ToDictionary(g => g.Key, g => g.AsEnumerable());
    }
}
