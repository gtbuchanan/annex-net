namespace Annex.Numerics;

/// <summary>
/// Represents an endpoint in an interval.
/// </summary>
/// <typeparam name="T">The type of endpoint.</typeparam>
[PublicAPI]
public interface IIntervalEndpoint<T> : IFormattable
{
    /// <summary>
    /// Determines whether the endpoint contains the value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="comparer">The comparer.</param>
    /// <returns>
    /// <c>true</c> if the endpoint contains the specified value;
    /// otherwise, <c>false</c>.
    /// </returns>
    bool Contains(T value, IComparer<T> comparer);
}
