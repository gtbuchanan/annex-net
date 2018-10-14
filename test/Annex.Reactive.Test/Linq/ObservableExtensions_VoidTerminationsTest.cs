using Annex.Reactive.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_VoidTerminationsTest : ReactiveTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((IObservable<Unit>)null).VoidTerminations());

        [Test, AutoDomainData]
        public void MaterializesException(int value, TestScheduler testScheduler, Exception ex)
        {
            var source = testScheduler.CreateColdObservable(
                OnNext(100, value),
                OnError<int>(200, ex));

            testScheduler
                .Start(() => source.VoidTerminations())
                .Messages
                .ShouldBe(
                    OnNext(400, Unit.Default),
                    OnCompleted<Unit>(400));
        }

        [Test, AutoDomainData]
        public void MaterializesCompletion(int value, TestScheduler testScheduler)
        {
            var source = testScheduler.CreateColdObservable(
                OnNext(100, value),
                OnCompleted<int>(200));

            testScheduler
                .Start(() => source.VoidTerminations())
                .Messages
                .ShouldBe(
                    OnNext(400, Unit.Default),
                    OnCompleted<Unit>(400));
        }
    }
}
