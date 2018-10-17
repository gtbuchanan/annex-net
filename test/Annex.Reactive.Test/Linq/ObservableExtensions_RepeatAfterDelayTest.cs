using Annex.Reactive.Linq;
using Annex.Time.Global;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;
using System.Reactive.Concurrency;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_RepeatAfterDelayTest : ReactiveTest
    {
        [Test, AutoDomainData]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException(TimeSpan delay, IScheduler scheduler) =>
            Should.Throw<ArgumentNullException>(() =>
                ((IObservable<Unit>)null).RepeatAfterDelay(delay, scheduler));

        [Test, AutoDomainData]
        public void ReturnsRepeatingSequence(TestScheduler testScheduler) =>
            testScheduler
                .Start(() => ObservableEx.Void
                    .RepeatAfterDelay(300.Ticks(), testScheduler))
                .Messages
                .ShouldBe(
                    OnNext(200, Unit.Default),
                    OnNext(500, Unit.Default),
                    OnNext(800, Unit.Default));
    }
}
