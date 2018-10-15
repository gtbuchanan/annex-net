using JetBrains.Annotations;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Annex.Collections
{
    /// <summary>
    ///     Extensions for <see cref="NameValueCollection"/>.
    /// </summary>
    [PublicAPI]
    public static partial class NameValueCollectionExtensions
    {
        [Pure, NotNull]
        internal static IEnumerable<string> GetValuesSafe(
            [NotNull]this NameValueCollection @this, [NotNull]string key) =>
            @this.GetValues(key) ?? Enumerable.Empty<string>(); 
    }
}
