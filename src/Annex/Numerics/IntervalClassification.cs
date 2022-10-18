namespace Annex.Numerics;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the classification of an interval.
/// </summary>
/// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)#Classification_of_intervals">
/// Wikipedia: Classification of intervals
/// </seealso>
[SuppressMessage(
    "Design",
    "CA1027:Mark enums with FlagsAttribute",
    Justification = "Intentionally not flags to prevent invalid combinations.")]
[SuppressMessage(
    "Roslynator",
    "RCS1130:Bitwise operation on enum without Flags attribute",
    Justification = "Intentionally not flags to prevent invalid combinations.")]
[PublicAPI]
public enum IntervalClassification
{
    /// <summary>
    /// The interval represents no value.
    /// </summary>
    Empty = 0,

    /// <summary>
    /// The interval represents a single value.
    /// </summary>
    Degenerate = 1,

    /// <summary>
    /// The interval is left-open and right-unbounded.
    /// </summary>
    LeftOpen = 2,

    /// <summary>
    /// The interval is left-closed and right-unbounded.
    /// </summary>
    LeftClosed = 4,

    /// <summary>
    /// The interval is left-unbounded and right-open.
    /// </summary>
    RightOpen = 8,

    /// <summary>
    /// The interval is proper and both endpoints are open.
    /// </summary>
    Open = LeftOpen | RightOpen,

    /// <summary>
    /// The interval is left-closed and right-open.
    /// </summary>
    LeftClosedRightOpen = LeftClosed | RightOpen,

    /// <summary>
    /// The interval is left-unbounded and right-closed.
    /// </summary>
    RightClosed = 16,

    /// <summary>
    /// The interval is left-open and right-closed.
    /// </summary>
    LeftOpenRightClosed = LeftOpen | RightClosed,

    /// <summary>
    /// The interval is proper and both endpoints are closed.
    /// </summary>
    Closed = LeftClosed | RightClosed,

    /// <summary>
    /// The interval is unbounded at both endpoints (both open and closed).
    /// </summary>
    Unbounded = Open | Closed,
}
