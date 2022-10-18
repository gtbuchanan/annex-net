namespace Annex.Linq;

using System.Diagnostics.CodeAnalysis;

/// <content />
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Returns a random element from the sequence using Durstenfield's variant of the
    /// <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher–Yates shuffle</see> algorithm.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The provided collection of objects in a random order.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="random"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="this"/> contains no elements.
    /// </exception>
    /// <remarks>
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
    public static T RandomElement<T>(
        this IEnumerable<T> @this, Random random) =>
        @this.TryRandomElement(random, out var element)
            ? element
            : throw new InvalidOperationException(@"Sequence contains no elements");

    [MustUseReturnValue]
    [SuppressMessage(
        "Security",
        "CA5394:Do not use insecure randomness",
        Justification = "The caller can choose their own source of randomness.")]
    internal static bool TryRandomElement<T>(
        this IEnumerable<T> @this,
        Random random,
        [NotNullWhen(true)] out T? element)
    {
        element = default;
        var count = 0;
        foreach (var item in @this)
        {
            if (random.Next(++count) == 0) // Maintain uniform probability distribution
                element = item;
        }

        return count != 0;
    }
}
