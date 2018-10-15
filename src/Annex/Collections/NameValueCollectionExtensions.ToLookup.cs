using JetBrains.Annotations;
using System.Collections.Specialized;
using System.Linq;

namespace Annex.Collections
{
    public static partial class NameValueCollectionExtensions
    {
        /// <summary>
        ///     Converts the <see cref="NameValueCollection"/> to an <see cref="ILookup{TKey, TElement}"/>.
        /// </summary>
        /// <param name="this">The source collection.</param>
        /// <returns>The equivalent <see cref="ILookup{TKey, TElement}"/>.</returns>
        [Pure, NotNull]
        public static ILookup<string, string> ToLookup(
            [NotNull]this NameValueCollection @this) =>
            new NameValueLookup(@this);
    }
}
