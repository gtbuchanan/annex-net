namespace Annex.Test.Strings;

using Annex.Strings;

public sealed class StringExtensions_NullIfEmptyTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => default(string?)!.NullIfEmpty())
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void ReturnsThisWhenNotEmpty(string sut) =>
        sut.NullIfEmpty().ShouldBe(sut);

    [Test]
    public void ReturnsNullWhenEmpty() =>
        string.Empty.NullIfEmpty().ShouldBeNull();
}
