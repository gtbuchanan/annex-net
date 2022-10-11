using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Collections
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Adds the elements of the specified sequence to the end of the source collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source collection.</typeparam>
        /// <param name="this">The source collection.</param>
        /// <param name="collection">The sequence to add.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> or <paramref name="collection"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// public void Extend(ICollection&lt;int&gt; collection)
        /// {
        ///     collection.AddRange(Enumerable.Range(1, 10));
        /// }
        /// </code>
        /// </example>
        /// <seealso href="https://stackoverflow.com/a/26360010/1409101">Adapted from StackOverflow</seealso>
        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        public static void AddRange<T>(
            [JetBrains.Annotations.NotNull] this ICollection<T> @this,
            [JetBrains.Annotations.NotNull] IEnumerable<T> collection)
        {
            if (@this is List<T> list)
                list.AddRange(collection);
            else foreach (var value in collection)
                @this.Add(value);
        }
    }
}
