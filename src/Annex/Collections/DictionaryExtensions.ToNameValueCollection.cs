using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Annex.Collections
{
    public static partial class DictionaryExtensions
    {
        /// <summary>
        /// Converts the source dictionary into an equivalent <see cref="NameValueCollection"/>.
        /// </summary>
        /// <param name="this">The source dictionary.</param>
        /// <returns>The source dictionary as an equivalent <see cref="NameValueCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="this"/> is <c>null</c>.
        /// </exception>
        /// <example>
        /// <code language="csharp">
        /// var dict = new Dictionary&lt;string, IEnumerable&lt;string&gt;&gt;
        /// {
        ///     { "Key1", new [] { "Value1a", "Value1b" } }
        /// };
        ///
        /// var nvc = dict.ToNameValueCollection();
        ///
        /// nvc.GetValues("Key1"); // [ "Value1a", "Value1b" ]
        /// </code>
        /// </example>
        [Pure, NotNull]
        public static NameValueCollection ToNameValueCollection(
            [NotNull] this IDictionary<string, IEnumerable<string>> @this)
        {
            var nvc = new NameValueCollection(@this.Count);
            foreach (var kvp in @this)
                foreach (var value in kvp.Value)
                    nvc.Add(kvp.Key, value);
            return nvc;
        }
    }
}
