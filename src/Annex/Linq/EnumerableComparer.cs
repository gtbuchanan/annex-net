using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    /// <summary>
    /// Defines methods to support the comparison of <see cref="IEnumerable{T}"/> for equality.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sequence.</typeparam>
    /// <seealso href="https://stackoverflow.com/a/14675741/1409101">Adapted from StackOverflow</seealso>
    [PublicAPI]
    public sealed class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        /// <summary>
        /// Gets the default <see cref="EnumerableComparer{T}"/>.
        /// </summary>
        [NotNull]
        public static EnumerableComparer<T> Default { get; } = new EnumerableComparer<T>();

        private EnumerableComparer() { }

        /// <inheritdoc />
        public bool Equals([CanBeNull]IEnumerable<T> x, [CanBeNull]IEnumerable<T> y) =>
            ReferenceEquals(x, y) || x != null && y != null && x.SequenceEqual(y);

        /// <inheritdoc />
        public int GetHashCode([NotNull]IEnumerable<T> obj)
        {
            unchecked
            {
                return obj
                    .Where(e => e != null)
                    .Select(e => e.GetHashCode())
                    .Aggregate(17, (a, b) => 23 * a + b);
            }
        }
    }
}
