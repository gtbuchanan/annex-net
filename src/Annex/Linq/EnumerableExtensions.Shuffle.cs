namespace Annex.Linq;

using System.Diagnostics.CodeAnalysis;

/// <content />
public static partial class EnumerableExtensions
{
    /// <summary>
    /// Randomly shuffles a collection of objects using Durstenfield's variant of the
    /// <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher–Yates shuffle</see> algorithm.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <param name="random">The random number generator.</param>
    /// <returns>The provided collection of objects in a random order.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="random"/> is <c>null</c>.
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
    /// <seealso href="http://stackoverflow.com/a/1287572/1409101">Adapted from StackOverflow</seealso>
    [MustUseReturnValue]
    [SuppressMessage(
        "Security",
        "CA5394:Do not use insecure randomness",
        Justification = "The caller can choose their own source of randomness.")]
    public static IEnumerable<T> Shuffle<T>(
        this IEnumerable<T> @this, Random random)
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
