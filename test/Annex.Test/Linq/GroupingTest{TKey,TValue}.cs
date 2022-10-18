namespace Annex.Test.Linq;

using System.Linq;
using Annex.Linq;

[TestFixture(typeof(object), typeof(object))]
[TestFixture(typeof(int), typeof(object))]
[TestFixture(typeof(int?), typeof(object))]
[TestFixture(typeof(string), typeof(object))]
[TestFixture(typeof(Guid), typeof(object))]
[TestFixture(typeof(Guid?), typeof(object))]
public sealed class GroupingTest<TKey, TValue>
{
    [Test]
    public void Empty_HasDefaultKeyAndNoValues()
    {
        var sut = Grouping<TKey, TValue>.Empty;

        sut.ShouldSatisfyAllConditions(
            () => sut.Key.ShouldBe(default),
            () => sut.AsEnumerable().ShouldBeEmpty());
    }

    [Theory]
    [AutoDomainData]
    public void Constructor_NullKeyDoesNotThrow(IEnumerable<TValue> values) =>
        Should.NotThrow(() => new Grouping<TKey?, TValue>(default, values));

    [Theory]
    [AutoDomainData]
    public void Constructor_NullValuesThrowsArgumentNullException(TKey key) =>
        Should.Throw<ArgumentNullException>(() => new Grouping<TKey, TValue>(key, null!))
            .ParamName
            .ShouldBe("values");

    [Theory]
    [AutoDomainData]
    public void Key_ReturnsKey(TKey key, IEnumerable<TValue> values) =>
        new Grouping<TKey, TValue>(key, values).Key.ShouldBe(key);

    [Theory]
    [AutoDomainData]
    public void GetEnumerator_ReturnsValues(TKey key, IEnumerable<TValue> values) =>
        new Grouping<TKey, TValue>(key, values).AsEnumerable().ShouldBe(values);

    [Theory]
    [AutoDomainData]
    public void Create1_HasKeyWithNoValues(TKey key)
    {
        var sut = Grouping.Create<TKey, TValue>(key);

        var grouping = sut.ShouldBeOfType<Grouping<TKey, TValue>>();
        grouping.ShouldSatisfyAllConditions(
            () => grouping.Key.ShouldBe(key),
            () => grouping.AsEnumerable().ShouldBeEmpty());
    }

    [Theory]
    [AutoDomainData]
    public void Create2_NullKeyDoesNotThrow(IEnumerable<TValue> values) =>
        Should.NotThrow(() => Grouping.Create(default(TKey?), values));

    [Theory]
    [AutoDomainData]
    public void Create2_NullValuesThrowsArgumentNullException(TKey key) =>
        Should.Throw<ArgumentNullException>(() => Grouping.Create<TKey, TValue>(key, null!))
            .ParamName
            .ShouldBe("values");

    [Theory]
    [AutoDomainData]
    public void Create2_ReturnsGrouping(TKey key, IEnumerable<TValue> values)
    {
        var sut = Grouping.Create(key, values);

        var grouping = sut.ShouldBeOfType<Grouping<TKey, TValue>>();
        grouping.ShouldSatisfyAllConditions(
            () => grouping.Key.ShouldBe(key),
            () => grouping.AsEnumerable().ShouldBe(values));
    }
}
