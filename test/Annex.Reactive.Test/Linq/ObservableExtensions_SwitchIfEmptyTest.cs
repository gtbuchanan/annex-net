using Annex.Reactive.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Linq;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_SwitchIfEmptyTest : ReactiveTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(IObservable<Unit> switchTo) =>
            Should.Throw<ArgumentNullException>(() =>
                ((IObservable<Unit>)null).SwitchIfEmpty(switchTo));

        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullSwitchToThrowsArgumentNullException(IObservable<Unit> @this) =>
            Should.Throw<ArgumentNullException>(() => @this.SwitchIfEmpty(null));

        [Test, AutoDomainData]
        public void ReturnsFirstSequenceIfNotEmpty(TestScheduler testScheduler) =>
            testScheduler
                .Start(() => Observable
                    .Return(true, testScheduler)
                    .SwitchIfEmpty(Observable.Empty<bool>()))
                .Messages
                .ShouldBe(
                    OnNext(201, true),
                    OnCompleted<bool>(201));

        [Test, AutoDomainData]
        public void DoesNotSubscribeToSecondSequenceIfFirstIsNotEmpty(TestScheduler testScheduler)
        {
            var switchTo = testScheduler.CreateColdObservable(
                OnNext(300, true),
                OnCompleted<bool>(300));

            testScheduler.Start(() => Observable
                .Return(true, testScheduler)
                .SwitchIfEmpty(switchTo));

            switchTo.Subscriptions.ShouldBeEmpty();
        }

        [Test, AutoDomainData]
        public void SwitchesToSecondSequenceIfFirstIsEmpty(TestScheduler testScheduler)
        {
            var switchTo = testScheduler.CreateColdObservable(
                OnNext(100, true),
                OnCompleted<bool>(100));

            testScheduler
                .Start(() => Observable
                    .Empty<bool>()
                    .SwitchIfEmpty(switchTo))
                .Messages
                .ShouldBe(
                    OnNext(300, true),
                    OnCompleted<bool>(300));
        }
    }
}
