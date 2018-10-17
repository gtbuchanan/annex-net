using Annex.Global;
using JetBrains.Annotations;

namespace Annex.Strings
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     Returns null if the source string is empty.
        /// </summary>
        /// <param name="this">The source string.</param>
        /// <returns><c>null</c> if the source string is empty, otherwise <c>false</c>.</returns>
        public static string NullIfEmpty([NotNull] this string @this) =>
            @this.NullIf(string.Empty);
    }
}
