using JetBrains.Annotations;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Annex.Reactive
{
    public static partial class ObservableEx
    {
        /// <summary>
        /// Constructs an observable sequence that depends on a resource object,
        /// whose lifetime is tied to the resulting observable sequence's lifetime.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the produced sequence.</typeparam>
        /// <typeparam name="TResource">
        /// The type of resource used during the generation of the resulting sequence.
        /// Needs to implement <see cref="IDisposable"/>.
        /// </typeparam>
        /// <param name="resourceFactory">Factory function to obtain a resource object</param>
        /// <param name="observableFactory">Factory function to obtain an observable sequence that depends on the obtained resource.</param>
        /// <returns>An observable sequence whose lifetime controls the lifetime of the dependent resource object.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="resourceFactory"/> or <paramref name="observableFactory"/> is <c>null</c>.
        /// </exception>
        /// <seealso href="https://stackoverflow.com/a/49616041/1409101">Adapted from StackOverflow</seealso>
        [Pure]
        [NotNull]
        public static IObservable<TSource> Using<TSource, TResource>(
            [NotNull] Func<CancellationToken, Task<TResource>> resourceFactory,
            [NotNull] Func<TResource, IObservable<TSource>> observableFactory)
            where TResource : IDisposable =>
            Observable.FromAsync(resourceFactory).SelectMany(
                resource => Observable.Using(() => resource, observableFactory));
    }
}
