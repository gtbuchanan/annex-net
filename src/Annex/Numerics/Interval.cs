namespace Annex.Numerics;

using System.Collections.Generic;

/// <summary>
/// Helper methods or <see cref="Interval{T}"/>.
/// </summary>
[PublicAPI]
public static class Interval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns>The interval.</returns>
    public static Interval<T> Create<T>(
        LeftIntervalEndpoint<T> leftEndpoint,
        RightIntervalEndpoint<T> rightEndpoint,
        IComparer<T> comparer) =>
        new(leftEndpoint, rightEndpoint, comparer);

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <returns>The interval.</returns>
    public static Interval<T> Create<T>(
        LeftIntervalEndpoint<T> leftEndpoint, RightIntervalEndpoint<T> rightEndpoint) =>
        new(leftEndpoint, rightEndpoint);

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <returns>The interval.</returns>
    public static Interval<T> Create<T>(
        T leftEndpoint, RightIntervalEndpoint<T> rightEndpoint) =>
        new(leftEndpoint, rightEndpoint);

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <returns>The interval.</returns>
    public static Interval<T> Create<T>(
        LeftIntervalEndpoint<T> leftEndpoint, T rightEndpoint) =>
        new(leftEndpoint, rightEndpoint);

    /// <summary>
    /// Initializes a new instance of the <see cref="Interval{T}" /> class.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <returns>The interval.</returns>
    public static Interval<T> Create<T>(T leftEndpoint, T rightEndpoint) =>
        new(leftEndpoint, rightEndpoint);

    /// <summary>
    /// Classifies an interval from the specified left and right endpoints.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="leftEndpoint">The left endpoint.</param>
    /// <param name="rightEndpoint">The right endpoint.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns>The interval classification.</returns>
    /// <exception cref="InvalidOperationException">
    /// Unreachable. If this is thrown, there is a bug.
    /// </exception>
    /// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)#Classification_of_intervals">
    /// Wikipedia: Classification of intervals
    /// </seealso>
    internal static IntervalClassification Classify<T>(
        LeftIntervalEndpoint<T> leftEndpoint,
        RightIntervalEndpoint<T> rightEndpoint,
        IComparer<T> comparer)
    {
        var leftBounded = leftEndpoint as LeftIntervalEndpoint<T>.Bounded;
        var rightBounded = rightEndpoint as RightIntervalEndpoint<T>.Bounded;

        if (leftBounded != null && rightBounded != null)
            return ClassifyProper(leftBounded, rightBounded, comparer);
        if (leftBounded != null)
            return ClassifyLeftBounded(leftBounded);
        if (rightBounded != null)
            return ClassifyRightBounded(rightBounded);
        return IntervalClassification.Unbounded;
    }

    private static IntervalClassification ClassifyProper<T>(
        LeftIntervalEndpoint<T>.Bounded leftBounded,
        RightIntervalEndpoint<T>.Bounded rightBounded,
        IComparer<T> comparer)
    {
        var comparison = comparer.Compare(leftBounded.Value, rightBounded.Value);

        if (leftBounded.Include && rightBounded.Include)
        {
            return comparison == 0
                ? IntervalClassification.Degenerate
                : IntervalClassification.Closed;
        }

        if (comparison >= 0)
            return IntervalClassification.Empty;
        if (leftBounded.Include)
            return IntervalClassification.LeftClosedRightOpen;
        return rightBounded.Include
            ? IntervalClassification.LeftOpenRightClosed
            : IntervalClassification.Open;
    }

    private static IntervalClassification ClassifyLeftBounded<T>(
        LeftIntervalEndpoint<T>.Bounded leftBounded) =>
        leftBounded.Include
            ? IntervalClassification.LeftClosed
            : IntervalClassification.LeftOpen;

    private static IntervalClassification ClassifyRightBounded<T>(
        RightIntervalEndpoint<T>.Bounded rightBounded) =>
        rightBounded.Include
            ? IntervalClassification.RightClosed
            : IntervalClassification.RightOpen;
}
