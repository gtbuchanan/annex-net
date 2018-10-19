using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Linq
{
    public static partial class EnumerableExtensions
    {
        /// <summary>
        ///     Returns a specified number of random elements from the sequence using Durstenfield's variant of the
        ///     <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher–Yates shuffle</see> algorithm.
        /// </summary>
        /// <param name="this">The source sequence.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <param name="random">The random number generator.</param>
        /// <returns>The provided collection of objects in a random order.</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="this"/> or <paramref name="random"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="count"/> is less than or equal to zero.
        /// </exception>
        /// <remarks>
        ///     Requesting more than one element delegates to <see cref="Shuffle"/>, which
        ///     buffers the collection into memory.
        /// 
        ///     Note that you need to be careful about the instance of Random you use as:
        ///     * Creating two instances of Random at roughly the same time will yield the
        ///       same sequence of random numbers(when used in the same way)
        ///     * Random isn't thread-safe.
        ///     See <see href="http://csharpindepth.com/Articles/Chapter12/Random.aspx">C# in Depth</see> for details.
        /// </remarks>
        /// <see href="https://stackoverflow.com/a/648240/1409101">Adapted from StackOverflow</see>
        [MustUseReturnValue, NotNull]
        public static IEnumerable<T> TakeRandom<T>(
            [NotNull]this IEnumerable<T> @this, int count, [NotNull] Random random) =>
            count < 1
                ? throw new ArgumentException("Count must be greater than zero.", nameof(count))
                : @this.TakeRandomInternal(count, random);

        private static IEnumerable<T> TakeRandomInternal<T>(
            [NotNull]this IEnumerable<T> @this, int count, [NotNull] Random random)
        {
            // Shuffle buffers the collection into memory, so this
            // is more optimal if the user only needs one element
            if (count == 1)
                if (@this.TryRandomElement(random, out var element))
                    yield return element;
                else
                    yield break;
            else
                foreach (var element in @this.Shuffle(random).Take(count))
                    yield return element;
        }
    }
}
