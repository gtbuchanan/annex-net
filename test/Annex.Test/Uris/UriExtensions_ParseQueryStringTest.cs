namespace Annex.Test.Uris;

using Annex.Linq;
using Annex.Uris;

public sealed class UriExtensions_ParseQueryStringTest
{
    public static readonly object?[] UriCases =
    {
        new object?[]
        {
            "http://test/test.html",
            new Dictionary<string, IEnumerable<string>>(),
        },
        new object?[]
        {
            "http://test/test.html?",
            new Dictionary<string, IEnumerable<string>>(),
        },
        new object?[]
        {
            "http://test/test.html?key=bla/blub.xml",
            new Dictionary<string, IEnumerable<string>> { { "key", new[] { "bla/blub.xml" } } },
        },
        new object?[]
        {
            "http://test/test.html?eins=1&zwei=2",
            new Dictionary<string, IEnumerable<string>>
            {
                { "eins", new[] { "1" } },
                { "zwei", new[] { "2" } },
            },
        },
        new object?[]
        {
            "http://test/test.html?empty",
            new Dictionary<string, IEnumerable<string>> { { "empty", new[] { string.Empty } } },
        },
        new object?[]
        {
            "http://test/test.html?empty=",
            new Dictionary<string, IEnumerable<string>> { { "empty", new[] { string.Empty } } },
        },
        new object?[]
        {
            "http://test/test.html?key=1&",
            new Dictionary<string, IEnumerable<string>> { { "key", new[] { "1" } } },
        },
        new object?[]
        {
            "http://test/test.html?key=value?&b=c",
            new Dictionary<string, IEnumerable<string>>
            {
                { "key", new[] { "value?" } },
                { "b", new[] { "c" } },
            },
        },
        new object?[]
        {
            "http://test/test.html?key=value=what",
            new Dictionary<string, IEnumerable<string>> { { "key", new[] { "value=what" } } },
        },
        new object?[]
        {
            "http://www.google.com/search?q=energy+edge&rls=com.microsoft:en-au" +
            "&ie=UTF-8&oe=UTF-8&startIndex=&startPage=1%22",
            new Dictionary<string, IEnumerable<string>>
            {
                { "q", new[] { "energy edge" } },
                { "rls", new[] { "com.microsoft:en-au" } },
                { "ie", new[] { "UTF-8" } },
                { "oe", new[] { "UTF-8" } },
                { "startIndex", new[] { string.Empty } },
                { "startPage", new[] { "1\"" } },
            },
        },
        new object?[]
        {
            "http://test/test.html?key=value&key=anotherValue",
            new Dictionary<string, IEnumerable<string>>
            {
                { "key", new[] { "value", "anotherValue" } },
            },
        },
        new object?[]
        {
            "http://test/test.html?key=lowerCase&KEY=upperCase",
            new Dictionary<string, IEnumerable<string>>
            {
                { "key", new[] { "lowerCase", "upperCase" } },
            },
        },
        new object?[]
        {
            "http://test/test.html?key=j,lajsdfn==",
            new Dictionary<string, IEnumerable<string>> { { "key", new[] { "j,lajsdfn==" } } },
        },
    };

    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((Uri?)null)!.ParseQueryString());

    [Theory]
    [TestCaseSource(nameof(UriCases))]
    public void ParseQueryString(string url, IDictionary<string, IEnumerable<string>> expected)
    {
        var sut = new Uri(url).ParseQueryString().ToDictionary();

        Assert.That(sut, Is.EqualTo(expected));
    }
}
