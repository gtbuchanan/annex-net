namespace Annex.Test.Pollution;

using Annex.Pollution;
using Annex.Strings;

public sealed class ClassExtensions_NullIfTest
{
    public static readonly object[] NullCases =
    {
        new object[] { string.Empty },
        new object[] { Enumerable.Empty<string>() },
        new object[] { new object() },
    };

    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(string value) =>
        Should.Throw<ArgumentNullException>(() => default(string?)!.NullIf(value))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public void NullValueThrowsArgumentNullException(string sut) =>
        Should.Throw<ArgumentNullException>(() => sut.NullIf(null!))
            .ParamName
            .ShouldBe("value");

    [Theory]
    [AutoDomainData]
    public void ReturnsThisIfDifferentValue(object sut, object value) =>
        sut.NullIf(value).ShouldBe(sut);

    [Theory]
    [TestCaseSource(nameof(NullCases))]
    public void ReturnsNullIfSameValue(object sut)
    {
        object result = NullIf((dynamic)sut, (dynamic)sut);

        result.ShouldBeNull();
    }

    [Theory]
    [AutoDomainData]
    public void UsesComparer(object sut, IEqualityComparer<object> comparer)
    {
        comparer.Equals(Arg.Any<object>(), Arg.Any<object>()).Returns(false);

        sut.NullIf(sut, comparer).ShouldBe(sut);
    }

    [Theory]
    [AutoDomainData]
    public void NullIfTest(object obj, string s, string s2, int n)
    {
        obj.NullIf(obj).ShouldBeNull();
        s.NullIf(s).ShouldBeNull();
        s.NullIf(s2).ShouldBe(s);
        n.NullIf(n).ShouldBeNull();
        string.Empty.NullIfEmpty().ShouldBeNull();
        new string(' ', 10).NullIfWhiteSpace();
        Guid.Empty.NullIfDefault().ShouldBeNull();
        default(int).NullIfDefault().ShouldBeNull();
    }

    private static T? NullIf<T>(T sut, T value)
        where T : class =>
        sut.NullIf(value);
}
