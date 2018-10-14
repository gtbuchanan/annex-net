using JetBrains.Annotations;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        ///     Switches to the provided sequence if the source sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of elements in the source sequence.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <param name="switchTo">The sequence to switch to if the source sequence is empty.</param>
        /// <returns>The source sequence if its not empty, otherwise the secondary sequence.</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="this"/> or <paramref name="switchTo"/> are <c>null</c>.
        /// </exception>
        /// <see href="https://stackoverflow.com/a/51795229/1409101">Adapted from StackOverflow</see>
        [Pure, NotNull]
        public static IObservable<T> SwitchIfEmpty<T>(
            [NotNull]this IObservable<T> @this,
            [NotNull]IObservable<T> switchTo) =>
            Observable.Create<T>(obs =>
            {
                var source = @this.Replay(1);
                var switched = source.Any().SelectMany(any => any ? Observable.Empty<T>() : switchTo);
                return new CompositeDisposable(source.Concat(switched).Subscribe(obs), source.Connect());
            });
    }
}
