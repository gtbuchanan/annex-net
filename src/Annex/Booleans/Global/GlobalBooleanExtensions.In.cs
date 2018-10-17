using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Booleans.Global
{
    public static partial class GlobalBooleanExtensions
    {
        /// <summary>
        ///     Determines if the value is contained in the sequence of specified comparands.
        ///     For constant comparands, consider using a static readonly <see cref="HashSet{T}"/> instead.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TComparand">The type of the comparands.</typeparam>
        /// <param name="this">The value to check.</param>
        /// <param name="comparands">The comparands to compare the value to.</param>
        /// <returns>
        ///     <c>true</c> if the value is contained in the list of specified comparands,
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool In<TValue, TComparand>([NotNull] this TValue @this,
            [NotNull] params TComparand[] comparands) where TValue : TComparand =>
            @this.In(EqualityComparer<TComparand>.Default, comparands);

        /// <summary>
        ///     Determines if the value is contained in the sequence of specified comparands.
        ///     For constant comparands, consider using a static readonly <see cref="HashSet{T}"/> instead.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <typeparam name="TComparand">The type of the comparands.</typeparam>
        /// <param name="this">The value to check.</param>
        /// <param name="comparer">
        ///     The equality comparer implementation to use when comparing values.
        /// </param>
        /// <param name="comparands">The comparands to compare the value to.</param>
        /// <returns>
        ///     <c>true</c> if the value is contained in the list of specified comparands,
        ///     otherwise <c>false</c>.
        /// </returns>
        public static bool In<TValue, TComparand>([NotNull]this TValue @this,
            [NotNull]IEqualityComparer<TComparand> comparer,
            [NotNull]params TComparand[] comparands) where TValue : TComparand =>
            comparands.Contains(@this, comparer);
    }
}
