using JetBrains.Annotations;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace Annex.Reactive.Linq
{
    public static partial class ObservableExtensions
    {
        /// <summary>
        /// Repeats the source sequence after the specified delay.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="this">The source sequence.</param>
        /// <param name="delay">The time to wait until repeating.</param>
        /// <param name="scheduler">The scheduler for the delay.</param>
        /// <returns>A delayed, repeating sequence based on the source.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> or <paramref name="scheduler"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// Observable.Return(Unit.Default)
        ///     .RepeatAfterDelay(TimeSpan.FromSeconds(30), Scheduler.Default)
        ///     .Subscribe(_ => Console.WriteLine("Ping"));
        ///
        /// // 0s: Ping
        /// // 30s: Ping
        /// // 60s: Ping
        /// // (infinite)
        /// </code>
        /// </example>
        /// <seealso href="https://stackoverflow.com/a/32875852/1409101">Adapted from StackOverflow</seealso>
        [Pure, NotNull]
        public static IObservable<T> RepeatAfterDelay<T>(
            [NotNull] this IObservable<T> @this, TimeSpan delay,
            [NotNull] IScheduler scheduler)
        {
            var repeatSignal = Observable.Empty<T>().Delay(delay, scheduler);
            return @this.Concat(repeatSignal).Repeat();
        }
    }
}
