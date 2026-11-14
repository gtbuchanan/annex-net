namespace Annex.Numerics;

/// <summary>
/// Represents a bounded interval according to mathematics.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
/// <seealso href="https://en.wikipedia.org/wiki/Interval_(mathematics)">
/// Wikipedia: Interval (mathematics)
/// </seealso>
[PublicAPI]
public interface IBoundedInterval<T> : IInterval<T>
{
    /// <inheritdoc cref="IInterval{T}.LeftEndpoint" />
    new IBoundedIntervalEndpoint<T> LeftEndpoint { get; }

    /// <inheritdoc cref="IInterval{T}.RightEndpoint" />
    new IBoundedIntervalEndpoint<T> RightEndpoint { get; }
}
