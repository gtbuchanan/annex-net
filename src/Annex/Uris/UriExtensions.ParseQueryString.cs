using JetBrains.Annotations;
using System;
using System.Linq;

namespace Annex.Uris
{
    public static partial class UriExtensions
    {
        /// <summary>
        /// Parses the query string for the <see cref="Uri"/>.
        /// </summary>
        /// <param name="this">The <see cref="Uri"/> for which to parse the query string.</param>
        /// <returns>The parsed query string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
        /// <example>
        /// <code language="csharp">
        /// var uri = new Uri("https://mydomain.com?value=123&amp;value=456");
        /// var queryString = uri.ParseQueryString();
        /// queryString["value"]; // [ "123", "456" ]
        /// queryString["otherValue"]; // []
        /// </code>
        /// </example>
        /// <seealso href="https://stackoverflow.com/a/20134983/1409101">Adapted from StackOverflow</seealso>
        [Pure, NotNull]
        public static ILookup<string, string> ParseQueryString([NotNull] this Uri @this) =>
            @this.Query
                .TrimStart('?')
                .Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(q => q.Split(new[] { '=' }))
                .ToLookup(
                    arr => arr[0],
                    arr => arr.Length > 1
                        ? Uri.UnescapeDataString(string.Join("=", arr.Skip(1)).Replace('+', ' '))
                        : string.Empty,
#if NETSTANDARD1_4
                    StringComparer.OrdinalIgnoreCase
#else
                    StringComparer.InvariantCultureIgnoreCase
#endif
                    );
    }
}
