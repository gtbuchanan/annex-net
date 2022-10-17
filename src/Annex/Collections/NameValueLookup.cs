namespace Annex.Collections;

using System.Collections;
using System.Collections.Specialized;
using Annex.Linq;

internal sealed class NameValueLookup : ILookup<string?, string>
{
    public NameValueLookup(NameValueCollection nvc) => this.Nvc = nvc;

    public int Count => this.Nvc.Count;

    internal NameValueCollection Nvc { get; }

    public IEnumerable<string> this[string? key] => this.Nvc.GetValuesSafe(key);

    /// <inheritdoc />
    /// <seealso href="https://stackoverflow.com/a/9869070/1409101">Adapted from StackOverflow</seealso>
    public bool Contains(string? key) =>
        this.Nvc.Get(key) != null || this.Nvc.AllKeys.Contains(key);

    public IEnumerator<IGrouping<string?, string>> GetEnumerator() =>
        this.Nvc.AllKeys
            .Select(key => Grouping.Create(key, this.Nvc.GetValuesSafe(key)))
            .GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
