namespace Annex.Reactive;

/// <summary>
/// Provides an additional set of static methods for writing in-memory queries over observable sequences.
/// </summary>
[PublicAPI]
public static partial class ObservableAnnex
{
    /// <summary>
    /// Gets a void <see cref="IObservable{T}"/>. Equivalent to <c>Observable.Return(Unit.Default)</c>.
    /// </summary>
    public static readonly IObservable<Unit> Void = Observable.Return(Unit.Default);
}
