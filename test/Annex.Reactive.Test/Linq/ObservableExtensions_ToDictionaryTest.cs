namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;

public sealed class ObservableExtensions_ToDictionaryTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() =>
            ((IObservable<(object, object)>?)null)!.ToDictionary());

    [Theory]
    [AutoDomainData]
    public void ShouldCreateMatchingDictionary(IDictionary<string, string> dict) =>
        dict.ToObservable()
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToDictionary()
            .Wait()
            .ShouldBe(dict, true);
}
