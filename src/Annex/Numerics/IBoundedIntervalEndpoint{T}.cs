namespace Annex.Numerics;

/// <summary>
/// Represents a bounded endpoint for an interval.
/// </summary>
/// <typeparam name="T">The type of endpoint value.</typeparam>
[PublicAPI]
public interface IBoundedIntervalEndpoint<T> : IIntervalEndpoint<T>
{
    /// <summary>
    /// Gets the endpoint value.
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Gets a value indicating whether the endpoint is included.
    /// </summary>
    bool Include { get; }
}
