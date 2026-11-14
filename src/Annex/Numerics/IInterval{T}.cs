namespace Annex.Numerics;

/// <summary>
/// Represents an interval according to mathematics.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
/// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)">
/// Wikipedia: Interval (mathematics)
/// </seealso>
[PublicAPI]
public interface IInterval<T> : IFormattable
{
    /// <summary>
    /// Gets the interval's left endpoint.
    /// </summary>
    IIntervalEndpoint<T> LeftEndpoint { get; }

    /// <summary>
    /// Gets the interval's right endpoint.
    /// </summary>
    IIntervalEndpoint<T> RightEndpoint { get; }

    /// <summary>
    /// Gets the interval's classification.
    /// </summary>
    /// <seealso cref="IntervalClassification" />
    /// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)#Classification_of_intervals">
    /// Wikipedia: Classification of intervals
    /// </seealso>
    IntervalClassification Classification { get; }

    /// <summary>
    /// Determines whether the interval contains the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// <c>true</c> if the interval contains the specified value;
    /// otherwise, <c>false</c>.
    /// </returns>
    bool Contains(T value);
}
