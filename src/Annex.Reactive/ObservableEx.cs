using JetBrains.Annotations;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Annex.Reactive
{
    /// <summary>
    /// Provides an additional set of static methods for writing in-memory queries over observable sequences.
    /// </summary>
    [PublicAPI]
    public static partial class ObservableEx
    {
        /// <summary>
        /// A void <see cref="IObservable{T}"/>. Equivalent to <c>Observable.Return(Unit.Default)</c>.
        /// </summary>
        [NotNull]
        public static IObservable<Unit> Void { get; } = Observable.Return(Unit.Default);
    }
}
