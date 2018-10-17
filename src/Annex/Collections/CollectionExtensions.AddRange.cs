using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Collections
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        ///     Adds the elements of the specified sequence to the end of the source collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source collection.</typeparam>
        /// <param name="this">The source collection.</param>
        /// <param name="collection">The sequence to add.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="this"/> or <paramref name="collection"/> is <c>null</c>.
        /// </exception>
        /// <see href="https://stackoverflow.com/a/26360010/1409101">Adapted from StackOverflow</see>
        [SuppressMessage("ReSharper", "ConvertIfStatementToSwitchStatement")]
        public static void AddRange<T>(
            [NotNull]this ICollection<T> @this,
            [NotNull]IEnumerable<T> collection)
        {
            if (@this is List<T> list)
                list.AddRange(collection);
            else foreach (var value in collection)
                @this.Add(value);
        }
    }
}
