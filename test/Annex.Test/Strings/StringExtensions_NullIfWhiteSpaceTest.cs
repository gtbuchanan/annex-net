using Annex.Strings;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test.Strings
{
    public sealed class StringExtensions_NullIfWhiteSpaceTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => default(string).NullIfWhiteSpace())
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void ReturnsThisIfNotEmpty(string sut) =>
            sut.NullIfWhiteSpace().ShouldBe(sut);

        [Test]
        public void ReturnsNullIfEmpty() =>
            string.Empty.NullIfWhiteSpace().ShouldBeNull();

        [Test, AutoDomainData]
        public void ReturnsNullIfWhiteSpace(int n) =>
            new string(' ', n).NullIfWhiteSpace().ShouldBeNull();
    }
}
