namespace Annex.Numerics;

/// <summary>
/// Helpers for <see cref="LeftIntervalEndpoint{T}" />.
/// </summary>
[PublicAPI]
public static class LeftIntervalEndpoint
{
    internal const string InclusionNotation = "[";

    internal const string ExclusionNotation = "(";

    internal const string ExclusionNotationUnambiguous = "]";

    /// <summary>
    /// Creates a bounded left endpoint.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="value">The bounded value.</param>
    /// <param name="include">
    /// Specifies if the bounded value is included in the interval.
    /// </param>
    /// <returns>The bounded left endpoint.</returns>
    public static LeftIntervalEndpoint<T> Create<T>(T value, bool include) =>
        new LeftIntervalEndpoint<T>.Bounded(value, include);

    /// <summary>
    /// Creates a left endpoint from the specified <see cref="float"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="include">
    /// Specifies whether the left endpoint is included. This value is ignored
    /// if <paramref name="value"/> is <c>null</c> or <see cref="float.NegativeInfinity"/>.
    /// </param>
    /// <returns>The left endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or positive infinity.
    /// </exception>
    public static LeftIntervalEndpoint<float> FromFloat(float? value, bool include)
    {
        if (value == null)
            return LeftIntervalEndpoint<float>.Unbounded;
        if (float.IsNaN(value.Value))
            throw new ArgumentException("Must not be NaN.", nameof(value));
        if (float.IsPositiveInfinity(value.Value))
            throw new ArgumentException("Must not be positive infinity.", nameof(value));
        return float.IsNegativeInfinity(value.Value)
            ? LeftIntervalEndpoint<float>.Unbounded
            : Create(value.Value, include);
    }

    /// <summary>
    /// Creates a closed left endpoint from the specified <see cref="float"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The left endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or positive infinity.
    /// </exception>
    public static LeftIntervalEndpoint<float> FromFloat(float? value) =>
        FromFloat(value, true);

    /// <summary>
    /// Creates a left endpoint from the specified <see cref="double"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="include">
    /// Specifies whether the left endpoint is included. This value is ignored
    /// if <paramref name="value"/> is <c>null</c> or <see cref="double.PositiveInfinity"/>.
    /// </param>
    /// <returns>The left endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or positive infinity.
    /// </exception>
    public static LeftIntervalEndpoint<double> FromDouble(double? value, bool include)
    {
        if (value == null)
            return LeftIntervalEndpoint<double>.Unbounded;
        if (double.IsNaN(value.Value))
            throw new ArgumentException("Must not be NaN.", nameof(value));
        if (double.IsPositiveInfinity(value.Value))
            throw new ArgumentException("Must not be positive infinity.", nameof(value));
        return double.IsNegativeInfinity(value.Value)
            ? LeftIntervalEndpoint<double>.Unbounded
            : Create(value.Value, include);
    }

    /// <summary>
    /// Creates a closed left endpoint from the specified <see cref="double"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The left endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or positive infinity.
    /// </exception>
    public static LeftIntervalEndpoint<double> FromDouble(double? value) =>
        FromDouble(value, true);
}
