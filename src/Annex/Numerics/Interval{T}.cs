namespace Annex.Numerics;

using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Represents an interval according to mathematics.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
/// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)">
/// Wikipedia: Interval (mathematics)
/// </seealso>
[PublicAPI]
public sealed record Interval<T> : IFormattable
{
    /// <summary>
    /// The unbounded interval.
    /// </summary>
    public static readonly Interval<T> Unbounded =
        new(LeftIntervalEndpoint<T>.Unbounded, RightIntervalEndpoint<T>.Unbounded);

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}"/> class.
    /// </summary>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <param name="comparer">The comparer.</param>
    public Interval(
        LeftIntervalEndpoint<T> leftEndpoint,
        RightIntervalEndpoint<T> rightEndpoint,
        IComparer<T> comparer)
    {
        this.LeftEndpoint = leftEndpoint;
        this.RightEndpoint = rightEndpoint;
        this.Comparer = comparer;
        this.Classification = Interval.Classify(leftEndpoint, rightEndpoint, comparer);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}"/> class.
    /// </summary>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    public Interval(LeftIntervalEndpoint<T> leftEndpoint, RightIntervalEndpoint<T> rightEndpoint)
        : this(leftEndpoint, rightEndpoint, Comparer<T>.Default)
    {
    }

    /// <summary>
    /// Gets the left endpoint.
    /// </summary>
    public LeftIntervalEndpoint<T> LeftEndpoint { get; }

    /// <summary>
    /// Gets the right endpoint.
    /// </summary>
    public RightIntervalEndpoint<T> RightEndpoint { get; }

    /// <summary>
    /// Gets the comparer.
    /// </summary>
    public IComparer<T> Comparer { get; }

    /// <summary>
    /// Gets the classification.
    /// </summary>
    public IntervalClassification Classification { get; }

    /// <summary>
    /// Determines whether the interval contains the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// <c>true</c> if the interval contains the specified value;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool Contains(T value) => this.Classification switch
    {
        IntervalClassification.Empty => false,
        IntervalClassification.Unbounded => true,
        IntervalClassification.Degenerate =>
            this.LeftEndpoint.Contains(value, this.Comparer),
        _ => this.LeftEndpoint.Contains(value, this.Comparer) &&
            this.RightEndpoint.Contains(value, this.Comparer),
    };

    /// <inheritdoc />
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        var numberFormat = NumberFormatInfo.GetInstance(formatProvider);
        return string.Concat(
            this.LeftEndpoint.ToString(format, formatProvider),
            numberFormat.NumberDecimalSeparator == "," ? "; " : ", ",
            this.RightEndpoint.ToString(format, formatProvider));
    }

    /// <inheritdoc />
    /// <seealso href="https://en.wikipedia.org/wiki/ISO_31-11#Sets">
    /// Wikipedia: ISO 31-11 Sets
    /// </seealso>
    public override string ToString() => this.ToString(null, null);
}
