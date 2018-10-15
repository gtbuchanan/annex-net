using AutoFixture;
using NUnit.Framework;
using Shouldly;
using System.Collections.Specialized;

namespace Annex.Test.Customizations
{
    public sealed class NameValueCollectionCustomizationTest
    {
        [Test]
        public void CreatesNonEmptyNameValueCollection() =>
            new Fixture().Customize(new NameValueCollectionCustomization())
                .Create<NameValueCollection>()
                .Count.ShouldBeGreaterThan(0);
    }
}
