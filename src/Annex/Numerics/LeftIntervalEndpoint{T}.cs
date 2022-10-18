namespace Annex.Numerics;

using System.Collections.Generic;

/// <summary>
/// Represents the left endpoint in an interval.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
[PublicAPI]
public abstract record LeftIntervalEndpoint<T> : IIntervalEndpoint<T>
{
    /// <summary>
    /// The unbounded left endpoint.
    /// </summary>
    public static readonly LeftIntervalEndpoint<T> Unbounded =
        NegativeInfinity.Instance;

    private LeftIntervalEndpoint()
    {
    }

    /// <summary>
    /// Performs an implicit conversion from <typeparamref name="T"/>
    /// to <see cref="LeftIntervalEndpoint{T}"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator LeftIntervalEndpoint<T>(T? value) =>
        value is null ? Unbounded : new Bounded(value, true);

    /// <inheritdoc />
    public abstract bool Contains(T value, IComparer<T> comparer);

    /// <inheritdoc />
    public abstract string ToString(string? format, IFormatProvider? formatProvider);

    /// <summary>
    /// Represents a bounded left endpoint.
    /// </summary>
    /// <param name="Value">The bounded value.</param>
    /// <param name="Include">
    /// Specifies if the bounded value is included in the interval.
    /// </param>
    internal sealed record Bounded(T Value, bool Include)
        : LeftIntervalEndpoint<T>, IBoundedIntervalEndpoint<T>
    {
        /// <inheritdoc/>
        public override bool Contains(T value, IComparer<T> comparer)
        {
            var comparison = comparer.Compare(this.Value, value);
            return this.Include ? comparison >= 0 : comparison > 0;
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
                IntervalFormatSpecifier.General => this.GetNotation() + value,
                IntervalFormatSpecifier.Unambiguous => this.GetNotation(true) + value,
                _ => throw new FormatException(
                    $"The '{formats[0]}' format string is not supported"),
            };
        }

        /// <inheritdoc/>
        public override string ToString() => this.ToString(null, null);

        private string GetNotation(bool unambiguous = false)
        {
            if (this.Include)
                return LeftIntervalEndpoint.InclusionNotation;
            return unambiguous
                ? LeftIntervalEndpoint.ExclusionNotationUnambiguous
                : LeftIntervalEndpoint.ExclusionNotation;
        }
    }

    /// <summary>
    /// Represents an unbounded left endpoint.
    /// </summary>
    private sealed record NegativeInfinity : LeftIntervalEndpoint<T>
    {
        private const string StringValue = "-∞";

        /// <summary>
        /// The unbounded left endpoint.
        /// </summary>
        public static readonly NegativeInfinity Instance = new();

        private NegativeInfinity()
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
                IntervalFormatSpecifier.General => LeftIntervalEndpoint.ExclusionNotation + StringValue,
                IntervalFormatSpecifier.Unambiguous =>
                    LeftIntervalEndpoint.ExclusionNotationUnambiguous + StringValue,
                _ => throw new FormatException(
                    $"The '{formats[0]}' format string is not supported"),
            };
        }

        /// <inheritdoc/>
        public override string ToString() => this.ToString(null, null);
    }
}
