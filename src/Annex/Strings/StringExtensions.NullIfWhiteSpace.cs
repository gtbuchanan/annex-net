using JetBrains.Annotations;

namespace Annex.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Returns null if the source string is only whitespace.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <returns><c>null</c> if the source string is only whitespace, otherwise <c>false</c>.</returns>
        public static string NullIfWhiteSpace([NotNull]this string @this) =>
            string.IsNullOrWhiteSpace(@this) ? null : @this;
    }
}
