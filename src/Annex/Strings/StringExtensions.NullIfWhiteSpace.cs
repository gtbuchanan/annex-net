using JetBrains.Annotations;
using System;

namespace Annex.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        /// Returns <c>null</c> if the source string contains only whitespace.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <returns><c>null</c> if the source string contains only whitespace, otherwise <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this" /> is <c>null</c>.</exception>
        /// <example>
        /// <code language="csharp">
        /// string.Empty.NullIfWhiteSpace(); // null
        /// new string(' ', 10).NullIfWhiteSpace(); // null
        /// </code>
        /// </example>
        public static string NullIfWhiteSpace([NotNull] this string @this) =>
            string.IsNullOrWhiteSpace(@this) ? null : @this;
    }
}
