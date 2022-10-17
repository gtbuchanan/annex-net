namespace Annex.Reactive.Linq;

/// <content />
public static partial class ObservableExtensions
{
    /// <summary>
    /// Creates a dictionary from an observable sequence contain tuple key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
    /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <returns>A dictionary created from the source sequence.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// var dict = await Observable.Return(("Key1", "Value1")).ToDictionary();
    ///
    /// dict["Key1"]; // Value1
    /// </code>
    /// </example>
    [Pure]
    public static IObservable<IDictionary<TKey, TValue>> ToDictionary<TKey, TValue>(
        this IObservable<(TKey Key, TValue Value)> @this)
        where TKey : notnull =>
        @this.ToDictionary(t => t.Key, t => t.Value);
}
