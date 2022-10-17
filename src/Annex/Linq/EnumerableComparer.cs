namespace Annex.Linq;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Defines methods to support the comparison of <see cref="IEnumerable{T}"/> for equality.
/// </summary>
/// <typeparam name="T">The type of elements in the sequence.</typeparam>
/// <seealso href="https://stackoverflow.com/a/14675741/1409101">Adapted from StackOverflow</seealso>
[PublicAPI]
public sealed class EnumerableComparer<T>
    : IEqualityComparer<IEnumerable<T>>, IEqualityComparer
{
    private EnumerableComparer()
    {
    }

    /// <summary>
    /// Gets the default <see cref="EnumerableComparer{T}"/>.
    /// </summary>
    [SuppressMessage(
        "Design",
        "CA1000:Do not declare static members on generic types",
        Justification = "Follows the same pattern as EqualityComparer<T>.")]
    public static EnumerableComparer<T> Default { get; } = new();

    /// <inheritdoc />
    public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y) =>
        ReferenceEquals(x, y) || (x != null && y != null && x.SequenceEqual(y));

    /// <inheritdoc />
    public int GetHashCode(IEnumerable<T>? obj)
    {
        if (obj == null) return 0;
        var hashCode = default(HashCode);
        foreach (var item in obj)
            hashCode.Add(item);
        return hashCode.ToHashCode();
    }

    /// <inheritdoc cref="IEqualityComparer" />
    public new bool Equals(object? x, object? y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        if (x is IEnumerable<T> a && y is IEnumerable<T> b)
            return this.Equals(a, b);

        throw new ArgumentException(string.Empty, nameof(x));
    }

    /// <inheritdoc />
    bool IEqualityComparer.Equals(object? x, object? y) =>
        this.Equals(x, y);

    /// <inheritdoc />
    public int GetHashCode(object? obj)
    {
        if (obj == null) return 0;
        if (obj is IEnumerable<T> x) return this.GetHashCode(x);
        throw new ArgumentException(string.Empty, nameof(obj));
    }
}
