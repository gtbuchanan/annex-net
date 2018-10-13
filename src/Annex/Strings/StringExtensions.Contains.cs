using JetBrains.Annotations;
using System;

namespace Annex.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Checks if the value contains the provided substring using the specified comparison type.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="value">The substring to check for.</param>
        /// <param name="comparisonType">The culture, case, and sort rules to be applied to the comparison.</param>
        /// <returns><c>true</c> if the value was found in the source string, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        /// <see href="http://stackoverflow.com/a/444818/1409101">Adapted from StackOverflow</see>
        [Pure]
        public static bool Contains(
            [NotNull]this string @this,
            [CanBeNull]string value,
            StringComparison comparisonType) =>
            @this.IndexOf(value, comparisonType) >= 0;
    }
}
