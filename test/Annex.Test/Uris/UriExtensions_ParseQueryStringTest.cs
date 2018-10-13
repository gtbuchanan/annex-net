using Annex.Linq;
using Annex.Uris;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test.Uris
{
    public sealed class UriExtensions_ParseQueryStringTest
    {
        public static readonly TestCaseData[] UriCases =
        {
            new TestCaseData("http://test/test.html").Returns(
                new Dictionary<string, IEnumerable<string>>()),
            new TestCaseData("http://test/test.html?").Returns(
                new Dictionary<string, IEnumerable<string>>()),
            new TestCaseData("http://test/test.html?key=bla/blub.xml").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "bla/blub.xml" } } }),
            new TestCaseData("http://test/test.html?eins=1&zwei=2").Returns(
                new Dictionary<string, IEnumerable<string>> { { "eins", new [] { "1" } }, { "zwei", new [] { "2" } } }),
            new TestCaseData("http://test/test.html?empty").Returns(
                new Dictionary<string, IEnumerable<string>> { { "empty", new [] { string.Empty } } }),
            new TestCaseData("http://test/test.html?empty=").Returns(
                new Dictionary<string, IEnumerable<string>> { { "empty", new[] { string.Empty } } }),
            new TestCaseData("http://test/test.html?key=1&").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "1" } } }),
            new TestCaseData("http://test/test.html?key=value?&b=c").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "value?" } }, { "b", new [] { "c" } } }),
            new TestCaseData("http://test/test.html?key=value=what").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "value=what" } } }),
            new TestCaseData("http://www.google.com/search?q=energy+edge&rls=com.microsoft:en-au&ie=UTF-8&oe=UTF-8&startIndex=&startPage=1%22").Returns(
                new Dictionary<string, IEnumerable<string>>
                {
                    { "q", new [] { "energy edge" } },
                    { "rls", new [] { "com.microsoft:en-au" } },
                    { "ie", new [] { "UTF-8" } },
                    { "oe", new [] { "UTF-8" } },
                    { "startIndex", new [] { "" } },
                    { "startPage", new [] { "1\"" } },
                }),
            new TestCaseData("http://test/test.html?key=value&key=anotherValue").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "value", "anotherValue" } } }),
            new TestCaseData("http://test/test.html?key=lowerCase&KEY=upperCase").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "lowerCase", "upperCase" } } }),
            new TestCaseData("http://test/test.html?key=j;lajsdfn==").Returns(
                new Dictionary<string, IEnumerable<string>> { { "key", new [] { "j;lajsdfn==" } } })
        };

        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((Uri)null).ParseQueryString());

        [TestCaseSource(nameof(UriCases))]
        public IDictionary<string, IEnumerable<string>> ParseQueryString(string url) =>
            new Uri(url).ParseQueryString().ToDictionary();
    }
}
