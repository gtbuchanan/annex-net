namespace Annex.Test.Pollution;

using Annex.Pollution;
using AutoFixture.AutoNSubstitute;

public sealed class Extensions_AddToTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(ICollection<object> collection) =>
        Should.Throw<ArgumentNullException>(() => ((object?)null)!.AddTo(collection));

    [Theory]
    [AutoDomainData]
    public void InvokesAdd([Substitute] ICollection<object> collection, object sut)
    {
        sut.AddTo(collection);

        collection.Received(1).Add(sut);
    }

    [Theory]
    [AutoDomainData]
    public void ReturnsItem([Substitute] ICollection<object> collection, object sut) =>
        sut.AddTo(collection).ShouldBe(sut);
}
