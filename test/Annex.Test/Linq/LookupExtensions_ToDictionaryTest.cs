namespace Annex.Test.Linq;

using Annex.Linq;

public sealed class LookupExtensions_ToDictionaryTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((ILookup<object, object>?)null)!.ToDictionary());

    [Theory]
    [AutoDomainData]
    public void ReturnsMatchingDictionary(IDictionary<object, IEnumerable<object>> expected)
    {
        var lookup = expected
            .SelectMany(kvp => kvp.Value, Tuple.Create)
            .ToLookup(x => x.Item1.Key, x => x.Item2);

        var sut = lookup.ToDictionary();

        // TODO: Use Shouldly when it compares dictionaries correctly
        Assert.That(sut, Is.EqualTo(expected));
    }
}
