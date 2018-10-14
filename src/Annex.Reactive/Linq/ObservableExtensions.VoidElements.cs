using JetBrains.Annotations;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        ///     Voids all elements in the sequence. Equivalent to <c>@this.Select(_ => Unit.Default)</c>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <returns>The source sequence with its elements voided.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="this"/> is null.
        /// </exception>
        [Pure, NotNull]
        public static IObservable<Unit> VoidElements<T>(
            [NotNull]this IObservable<T> @this) =>
            @this.Select(_ => Unit.Default);
    }
}
