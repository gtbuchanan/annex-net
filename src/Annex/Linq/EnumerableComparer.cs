using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    /// <inheritdoc />
    /// <see href="https://stackoverflow.com/a/14675741/1409101">Adapted from StackOverflow</see>
    [PublicAPI]
    public sealed class EnumerableComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        /// <summary>
        /// Gets the default <see cref="EnumerableComparer{T}"/>.
        /// </summary>
        public static EnumerableComparer<T> Default { get; } = new EnumerableComparer<T>();

        /// <inheritdoc />
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y) =>
            ReferenceEquals(x, y) || x != null && y != null && x.SequenceEqual(y);

        /// <inheritdoc />
        public int GetHashCode(IEnumerable<T> obj)
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
