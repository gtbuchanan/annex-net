using JetBrains.Annotations;
using System;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        ///     Inverts the source sequence. Equivalent to <c>@this.Select(@bool => !@bool)</c>.
        /// </summary>
        /// <param name="this">The source sequence.</param>
        /// <returns>The inverse of the source sequence.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="this"/> is <c>null</c>.
        /// </exception>
        [Pure, NotNull]
        public static IObservable<bool> Inverse(
            [NotNull]this IObservable<bool> @this) =>
            @this.Select(@bool => !@bool);
    }
}
