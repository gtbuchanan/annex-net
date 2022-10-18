namespace Annex.Test.Collections;

using Annex.Collections;
using AutoFixture.AutoNSubstitute;

public sealed class CollectionExtensions_AddRangeTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(IEnumerable<object> expectedValues) =>
        Should.Throw<ArgumentNullException>(
            () => ((ICollection<object>?)null!).AddRange(expectedValues));

    [Theory]
    [AutoDomainData]
    public void NullCollectionThrowsArgumentNullException([Substitute] ICollection<object> sut) =>
        Should.Throw<ArgumentNullException>(() => sut.AddRange(null!));

    [Theory]
    [AutoDomainData]
    public void CollectionInvokesAdd(
        [Substitute] ICollection<object> sut,
        IEnumerable<object> expectedValues)
    {
        var values = new List<object>();
        sut.Add(Arg.Do<object>(values.Add));

        sut.AddRange(expectedValues);

        values.ShouldBe(expectedValues);
    }

    [Theory]
    [AutoDomainData]
    public void ListInvokesAddRange(IEnumerable<object> expectedValues)
    {
        var values = Substitute.For<List<object>, ICollection<object>>();
        var sut = (ICollection<object>)values;

        sut.AddRange(expectedValues);

        // Since List.AddRange isn't virtual, this is the best we can do
        sut.DidNotReceive().Add(Arg.Any<object>());

        values.ToArray().ShouldBe(expectedValues);
    }
}
