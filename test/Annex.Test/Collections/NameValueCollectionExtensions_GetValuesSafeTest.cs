using Annex.Collections;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Specialized;

namespace Annex.Test.Collections
{
    internal sealed class NameValueCollectionExtensions_GetValuesSafeTest
    {
        [Test, AutoDomainData]
        public static void NullThisThrowsArgumentNullException(string key) =>
            Should.Throw<ArgumentNullException>(() => ((NameValueCollection)null).GetValuesSafe(key))
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public static void NullValueReturnsEmptyEnumerable(string key) =>
            new NameValueCollection { { key, null } }
                .GetValuesSafe(key).ShouldBeEmpty();

        [Test, AutoDomainData]
        public static void NonNullValueReturnsValue(string key, string value, string value2)
        {
            var sut = new NameValueCollection { { key, value }, { key, value2 } };

            sut.GetValuesSafe(key).ShouldBe(sut.GetValues(key));
        }
    }
}
