namespace Annex.Test.Collections;

using System.Collections.Specialized;
using Annex.Collections;

public sealed class NameValueCollectionExtensions_GetValuesSafeTest
{
    [Theory]
    [AutoDomainData]
    public static void NullThisThrowsArgumentNullException(string key) =>
        Should.Throw<ArgumentNullException>(() => ((NameValueCollection?)null)!.GetValuesSafe(key))
            .ParamName
            .ShouldBe("this");

    [Theory]
    [AutoDomainData]
    public static void NullValueReturnsEmptyEnumerable(string key) =>
        new NameValueCollection { { key, null } }
            .GetValuesSafe(key)
            .ShouldBeEmpty();

    [Theory]
    [AutoDomainData]
    public static void NonNullValueReturnsValue(string key, string value, string value2)
    {
        var sut = new NameValueCollection { { key, value }, { key, value2 } };

        sut.GetValuesSafe(key).ShouldBe(sut.GetValues(key));
    }
}
