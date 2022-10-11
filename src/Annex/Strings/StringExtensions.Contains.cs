#if !NET6_0_OR_GREATER
using EnumsNET;
using JetBrains.Annotations;
using System;

namespace Annex.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Determines if the value contains the provided substring using the specified comparison type.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <param name="value">The substring to check for.</param>
        /// <param name="comparisonType">The culture, case, and sort rules to be applied to the comparison.</param>
        /// <returns><c>true</c> if the value was found in the source string, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> or <paramref name="value"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="comparisonType"/> is invalid.</exception>
        /// <example>
        /// <code language="csharp">
        /// "This string contains SOME values".Contains("some", StringComparison.InvariantCultureIgnoreCase); // true
        /// </code>
        /// </example>
        /// <seealso href="http://stackoverflow.com/a/444818/1409101">Adapted from StackOverflow</seealso>
        [Pure]
        public static bool Contains([NotNull] this string @this,
            [NotNull] string value, StringComparison comparisonType)
        {
            if (!comparisonType.IsValid())
                throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
            return @this.IndexOf(value, comparisonType) >= 0;
        }
    }
}
#endif
