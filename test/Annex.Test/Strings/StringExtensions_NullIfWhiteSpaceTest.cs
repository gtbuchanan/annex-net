namespace Annex.Test.Strings;

using Annex.Strings;

public sealed class StringExtensions_NullIfWhiteSpaceTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => default(string?)!.NullIfWhiteSpace())
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void ReturnsThisIfNotEmpty(string sut) =>
        sut.NullIfWhiteSpace().ShouldBe(sut);

    [Test]
    public void ReturnsNullIfEmpty() =>
        string.Empty.NullIfWhiteSpace().ShouldBeNull();

    [Theory]
    [AutoDomainData]
    public void ReturnsNullIfWhiteSpace(int n) =>
        new string(' ', n).NullIfWhiteSpace().ShouldBeNull();
}
