using Annex.Strings;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test.Strings
{
    public sealed class StringExtensions_NullIfEmptyTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => default(string).NullIfEmpty())
                .ParamName.ShouldBe("this");

        [Test, AutoDomainData]
        public void ReturnsThisWhenNotEmpty(string sut) =>
            sut.NullIfEmpty().ShouldBe(sut);

        [Test]
        public void ReturnsNullWhenEmpty() =>
            string.Empty.NullIfEmpty().ShouldBeNull();
    }
}
