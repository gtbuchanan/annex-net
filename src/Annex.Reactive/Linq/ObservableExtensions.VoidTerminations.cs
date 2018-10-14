using JetBrains.Annotations;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        ///     Materializes only the termination notifications of the source
        ///     sequence and voids all resulting elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <returns>
        ///     An observable sequence whose elements are voided termination
        ///     notifications of the source sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="this"/> is <c>null</c>.
        /// </exception>
        [Pure, NotNull]
        public static IObservable<Unit> VoidTerminations<T>(
            [NotNull]this IObservable<T> @this) =>
            @this.IgnoreElements().Materialize().VoidElements();
    }
}
