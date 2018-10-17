using Annex.Collections.Global;
using AutoFixture.AutoNSubstitute;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Test.Collections.Global
{
    public sealed class GlobalCollectionExtensions_AddToTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void NullThisThrowsArgumentNullException(ICollection<object> collection) =>
            Should.Throw<ArgumentNullException>(() => ((object)null).AddTo(collection));

        [Test, AutoDomainData]
        public void InvokesAdd([Substitute] ICollection<object> collection, object sut)
        {
            sut.AddTo(collection);

            collection.Received(1).Add(sut);
        }

        [Test, AutoDomainData]
        public void ReturnsItem([Substitute] ICollection<object> collection, object sut) =>
            sut.AddTo(collection).ShouldBe(sut);
    }
}
