using JetBrains.Annotations;
using System;
using System.Reactive;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        /// Voids all elements in the sequence. Equivalent to <c>@this.Select(_ => Unit.Default)</c>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <returns>The source sequence with its elements voided.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// Observable.Interval(TimeSpan.FromMinutes(5))
        ///     .VoidElements()
        ///     .Subscribe(Console.WriteLine);
        ///
        /// // 5m: Unit
        /// // 10m: Unit
        /// // etc.
        /// </code>
        /// </example>
        [Pure, NotNull]
        public static IObservable<Unit> VoidElements<T>(
            [NotNull] this IObservable<T> @this) =>
            @this.Select(_ => Unit.Default);
    }
}
