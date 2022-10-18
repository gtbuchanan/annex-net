namespace Annex.Numerics;

using System.Collections.Generic;

/// <summary>
/// Represents the right endpoint in an interval.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
[PublicAPI]
public abstract record RightIntervalEndpoint<T> : IIntervalEndpoint<T>
{
    /// <summary>
    /// The unbounded right endpoint.
    /// </summary>
    public static readonly RightIntervalEndpoint<T> Unbounded =
        PositiveInfinity.Instance;

    private RightIntervalEndpoint()
    {
    }

    /// <summary>
    /// Performs an implicit conversion from <typeparamref name="T"/> to
    /// <see cref="RightIntervalEndpoint{T}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator RightIntervalEndpoint<T>(T? value) =>
        value is null ? Unbounded : new Bounded(value, true);

    /// <inheritdoc />
    public abstract bool Contains(T value, IComparer<T> comparer);

    /// <inheritdoc />
    public abstract string ToString(string? format, IFormatProvider? formatProvider);

    /// <summary>
    /// Represents a bounded right endpoint.
    /// </summary>
    /// <param name="Value">The bounded value.</param>
    /// <param name="Include">
    /// Specifies if the bounded value is included in the interval.
    /// </param>
    internal sealed record Bounded(T Value, bool Include)
        : RightIntervalEndpoint<T>, IBoundedIntervalEndpoint<T>
    {
        /// <inheritdoc/>
        public override bool Contains(T value, IComparer<T> comparer)
        {
            var comparison = comparer.Compare(this.Value, value);
            return this.Include ? comparison <= 0 : comparison < 0;
        }

        /// <inheritdoc/>
        public override string ToString(string? format, IFormatProvider? formatProvider)
        {
            var formattable = this.Value as IFormattable;
            var formats = format?.Split(':') ?? new[] { IntervalFormatSpecifier.General };
            var value = formattable?.ToString(
                formats.Length > 1 ? formats[1] : null,
                formatProvider)
                ?? this.Value?.ToString();
            return formats[0] switch
            {
                IntervalFormatSpecifier.General => value + this.GetNotation(),
                IntervalFormatSpecifier.Unambiguous => value + this.GetNotation(true),
                _ => throw new FormatException(
                    $"The '{formats[0]}' format string is not supported"),
            };
        }

        /// <inheritdoc/>
        public override string ToString() => this.ToString(null, null);

        private string GetNotation(bool unambiguous = false)
        {
            if (this.Include)
                return RightIntervalEndpoint.InclusionNotation;
            return unambiguous
                ? RightIntervalEndpoint.ExclusionNotationUnambiguous
                : RightIntervalEndpoint.ExclusionNotation;
        }
    }

    /// <summary>
    /// Represents an unbounded right endpoint.
    /// </summary>
    private sealed record PositiveInfinity : RightIntervalEndpoint<T>
    {
        private const string StringValue = "+∞";

        /// <summary>
        /// The unbounded right endpoint.
        /// </summary>
        public static readonly PositiveInfinity Instance = new();

        private PositiveInfinity()
        {
        }

        /// <inheritdoc/>
        public override bool Contains(T value, IComparer<T> comparer) => true;

        /// <inheritdoc/>
        public override string ToString(string? format, IFormatProvider? formatProvider)
        {
            var formats = format?.Split(':') ?? new[] { IntervalFormatSpecifier.General };
            return formats[0] switch
            {
                IntervalFormatSpecifier.General => StringValue + RightIntervalEndpoint.ExclusionNotation,
                IntervalFormatSpecifier.Unambiguous =>
                    StringValue + RightIntervalEndpoint.ExclusionNotationUnambiguous,
                _ => throw new FormatException(
                    $"The '{formats[0]}' format string is not supported"),
            };
        }

        /// <inheritdoc/>
        public override string ToString() => this.ToString(null, null);
    }
}
