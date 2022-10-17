namespace Annex.Reactive.Linq;

/// <content />
public static partial class ObservableExtensions
{
    /// <summary>
    /// Materializes only the termination notifications of the source
    /// sequence and voids all resulting elements.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="this">The source sequence.</param>
    /// <returns>
    /// An observable sequence whose elements are voided termination
    /// notifications of the source sequence.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// await Observable.Return("Value").VoidTerminations(); // Unit
    /// await Observable.Empty&lt;string&gt;().VoidTerminations(); // Unit
    /// await Observable.Throw&lt;string&gt;(new Exception()).VoidTerminations(); // Unit
    /// </code>
    /// </example>
    [Pure]
    public static IObservable<Unit> VoidTerminations<T>(
        this IObservable<T> @this) =>
        @this.IgnoreElements().Materialize().VoidElements();
}
