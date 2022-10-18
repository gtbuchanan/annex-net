namespace Annex.Numerics;

/// <summary>
/// Helpers for <see cref="RightIntervalEndpoint{T}" />.
/// </summary>
[PublicAPI]
public static class RightIntervalEndpoint
{
    internal const string InclusionNotation = "]";

    internal const string ExclusionNotation = ")";

    internal const string ExclusionNotationUnambiguous = "[";

    /// <summary>
    /// Creates a bounded right endpoint.
    /// </summary>
    /// <typeparam name="T">The type of endpoint.</typeparam>
    /// <param name="value">The bounded value.</param>
    /// <param name="include">
    /// Specifies if the bounded value is included in the interval.
    /// </param>
    /// <returns>The bounded right endpoint.</returns>
    public static RightIntervalEndpoint<T> Create<T>(T value, bool include) =>
        new RightIntervalEndpoint<T>.Bounded(value, include);

    /// <summary>
    /// Creates a right endpoint from the specified <see cref="float"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="include">
    /// Specifies whether the right endpoint is included. This value is ignored
    /// if <paramref name="value"/> is <c>null</c> or <see cref="float.PositiveInfinity"/>.
    /// </param>
    /// <returns>The right endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or negative infinity.
    /// </exception>
    public static RightIntervalEndpoint<float> FromFloat(float? value, bool include)
    {
        if (value == null)
            return RightIntervalEndpoint<float>.Unbounded;
        if (float.IsNaN(value.Value))
            throw new ArgumentException("Must not be NaN.", nameof(value));
        if (float.IsNegativeInfinity(value.Value))
            throw new ArgumentException("Must not be negative infinity.", nameof(value));
        return float.IsPositiveInfinity(value.Value)
            ? RightIntervalEndpoint<float>.Unbounded
            : Create(value.Value, include);
    }

    /// <summary>
    /// Creates a closed right endpoint from the specified <see cref="float"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The right endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or negative infinity.
    /// </exception>
    public static RightIntervalEndpoint<float> FromFloat(float value) =>
        FromFloat(value, true);

    /// <summary>
    /// Creates a right endpoint from the specified <see cref="double"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="include">
    /// Specifies whether the right endpoint is included. This value is ignored
    /// if <paramref name="value"/> is <c>null</c> or <see cref="double.PositiveInfinity"/>.
    /// </param>
    /// <returns>The right endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or negative infinity.
    /// </exception>
    public static RightIntervalEndpoint<double> FromDouble(double value, bool include)
    {
        if (double.IsNaN(value))
            throw new ArgumentException("Must not be NaN.", nameof(value));
        if (double.IsNegativeInfinity(value))
            throw new ArgumentException("Must not be positive infinity.", nameof(value));
        return double.IsPositiveInfinity(value)
            ? RightIntervalEndpoint<double>.Unbounded
            : Create(value, include);
    }

    /// <summary>
    /// Creates a closed right endpoint from the specified <see cref="double"/>.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The right endpoint.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is NaN or negative infinity.
    /// </exception>
    public static RightIntervalEndpoint<double> FromDouble(double value) =>
        FromDouble(value, true);
}
