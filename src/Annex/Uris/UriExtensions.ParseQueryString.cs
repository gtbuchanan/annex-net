using JetBrains.Annotations;
using System;
using System.Linq;

namespace Annex.Uris
{
    public static partial class UriExtensions
    {
        /// <summary>
        ///     Parses the query string for the <see cref="Uri"/>.
        /// </summary>
        /// <param name="this">The <see cref="Uri"/> for which to parse the query string.</param>
        /// <returns>The parsed query string.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        /// <see href="https://stackoverflow.com/a/20134983/1409101">Adapted from StackOverflow</see>
        [Pure, NotNull]
        public static ILookup<string, string> ParseQueryString([NotNull]this Uri @this) =>
            @this.Query
                .TrimStart('?')
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(q => q.Split(new[] { '=' }))
                .ToLookup(
                    arr => arr[0],
                    arr => arr.Length > 1
                        ? Uri.UnescapeDataString(string.Join("=", arr.Skip(1)).Replace('+', ' '))
                        : string.Empty,
                    StringComparer.InvariantCultureIgnoreCase);
    }
}
