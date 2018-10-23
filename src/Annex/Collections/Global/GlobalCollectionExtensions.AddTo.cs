using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace Annex.Collections.Global
{
    public static partial class GlobalCollectionExtensions
    {
        /// <summary>
        /// Adds the value to the specified collection.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <typeparam name="TElement">The type of elements in the collection.</typeparam>
        /// <param name="this">The source value to add.</param>
        /// <param name="collection">The collection to add to.</param>
        /// <returns>The source value.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> or <paramref name="collection"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// var disposables = new CompositeDisposable();
        /// Observable.Interval(...)
        ///     .Subscribe(...)
        ///     .AddTo(disposables);
        /// </code>
        /// </example>
        [NotNull]
        public static T AddTo<T, TElement>(
            [NotNull] this T @this, [NotNull] ICollection<TElement> collection)
            where T : TElement
        {
            collection.Add(@this);
            return @this;
        }
    }
}
