using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        ///     Creates a dictionary from an observable sequence contain tuple key/value pairs.
        /// </summary>
        /// <typeparam name="TKey">The type of the dictionary key.</typeparam>
        /// <typeparam name="TValue">The type of the dictionary value.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <returns>A dictionary created from the source sequence.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="this"/> is <c>null</c>.
        /// </exception>
        [Pure, NotNull]
        public static IObservable<IDictionary<TKey, TValue>> ToDictionary<TKey, TValue>(
            [NotNull]this IObservable<(TKey Key, TValue Value)> @this) =>
            @this.ToDictionary(t => t.Key, t => t.Value);
    }
}
