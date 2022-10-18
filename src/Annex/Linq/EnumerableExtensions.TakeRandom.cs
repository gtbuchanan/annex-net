namespace Annex.Linq;

/// <content />
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Returns a specified number of random elements from the sequence using Durstenfield's variant of the
    /// <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher–Yates shuffle</see> algorithm.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <param name="count">The number of elements to return.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The provided collection of objects in a random order.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="random"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="count"/> is less than or equal to zero.
    /// </exception>
    /// <remarks>
    /// Requesting more than one element delegates to <see cref="Shuffle"/>, which buffers the collection into memory.
    /// <br /><br />
    /// Note that you need to be careful about the instance of Random you use as:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Creating two instances of Random at roughly the same time will yield the
    /// same sequence of random numbers (when used in the same way)
    /// </description>
    /// </item>
    /// <item>
    /// <description>Random isn't thread-safe.</description>
    /// </item>
    /// </list>
    /// See <see href="http://csharpindepth.com/Articles/Chapter12/Random.aspx">C# in Depth Chapter 12: Random</see> for details.
    /// </remarks>
    /// <seealso href="https://stackoverflow.com/a/648240/1409101">Adapted from StackOverflow</seealso>
    [MustUseReturnValue]
    public static IEnumerable<T> TakeRandom<T>(
        this IEnumerable<T> @this, int count, Random random) =>
        count < 1
            ? throw new ArgumentException("Count must be greater than zero.", nameof(count))
            : @this.TakeRandomInternal(count, random);

    [MustUseReturnValue]
    private static IEnumerable<T> TakeRandomInternal<T>(
        this IEnumerable<T> @this, int count, Random random)
    {
        if (count != 1)
        {
            foreach (var element in @this.Shuffle(random).Take(count))
                yield return element;
        }
        else if (@this.TryRandomElement(random, out var element))
        {
            yield return element;
        }
        else
        {
            // No elements
            yield break;
        }
    }
}
