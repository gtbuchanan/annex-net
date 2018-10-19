using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        ///     Randomly shuffles a collection of objects using Durstenfield's variant of the
        ///     <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher–Yates shuffle</see> algorithm.
        /// </summary>
        /// <param name="this">The source sequence.</param>
        /// <param name="random">The random number generator.</param>
        /// <returns>The provided collection of objects in a random order.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="this"/> or <paramref name="random"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        ///     Note that you need to be careful about the instance of Random you use as:
        ///     * Creating two instances of Random at roughly the same time will yield the
        ///       same sequence of random numbers(when used in the same way)
        ///     * Random isn't thread-safe.
        ///     See <see href="http://csharpindepth.com/Articles/Chapter12/Random.aspx">C# in Depth</see> for details.
        /// </remarks>
        /// <see href="http://stackoverflow.com/a/1287572/1409101">Adapted from StackOverflow</see>
        [MustUseReturnValue, NotNull]
        public static IEnumerable<T> Shuffle<T>(
            [NotNull]this IEnumerable<T> @this, [NotNull]Random random)
        {
            var elements = @this.ToArray();
            for (var i = elements.Length - 1; i >= 0; i--)
            {
                var swapIndex = random.Next(i + 1);
                yield return elements[swapIndex];
                elements[swapIndex] = elements[i];
            }
        }
    }
}
