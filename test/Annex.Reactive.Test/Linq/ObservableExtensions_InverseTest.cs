using Annex.Reactive.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_InverseTest : ReactiveTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((IObservable<bool>) null).Inverse());

        [Test]
        [InlineAutoDomainData(true)]
        [InlineAutoDomainData(false)]
        public void ReturnsInverse(bool value, TestScheduler testScheduler) =>
            testScheduler
                .Start(() => Observable
                    .Return(value, testScheduler)
                    .Inverse())
                .Messages
                .ShouldBe(
                    OnNext(201, !value),
                    OnCompleted<bool>(201));
    }
}
