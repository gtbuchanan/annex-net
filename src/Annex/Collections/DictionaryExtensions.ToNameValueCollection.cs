using JetBrains.Annotations;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Annex.Collections
{
    public static partial class DictionaryExtensions
    {
        /// <summary>
        ///     Converts the dictionary into a <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="this">The source dictionary.</param>
        /// <returns>The equivalent <see cref="NameValueCollection"/>.</returns>
        [Pure, NotNull]
        public static NameValueCollection ToNameValueCollection(
            [NotNull]this IDictionary<string, IEnumerable<string>> @this)
        {
            var nvc = new NameValueCollection(@this.Count);
            foreach (var kvp in @this)
                foreach (var value in kvp.Value)
                    nvc.Add(kvp.Key, value);
            return nvc;
        }
    }
}
