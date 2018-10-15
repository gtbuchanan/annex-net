using Annex.Linq;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Annex.Collections
{
    internal sealed class NameValueLookup : ILookup<string, string>
    {
        internal NameValueCollection Nvc { get; }

        public IEnumerable<string> this[string key] => Nvc.GetValuesSafe(key);

        public int Count => Nvc.Count;

        public NameValueLookup([NotNull]NameValueCollection nvc) => Nvc = nvc;

        /// <see href="https://stackoverflow.com/a/9869070/1409101">Adapted from StackOverflow</see>
        public bool Contains(string key) =>
            Nvc.Get(key) != null || Nvc.AllKeys.Contains(key);

        public IEnumerator<IGrouping<string, string>> GetEnumerator() =>
            Nvc.AllKeys
                .Select(key => Grouping.Create(key, Nvc.GetValuesSafe(key)))
                .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
