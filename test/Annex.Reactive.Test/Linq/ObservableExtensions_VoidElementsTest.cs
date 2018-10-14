using Annex.Reactive.Linq;
using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reactive;

namespace Annex.Reactive.Test.Linq
{
    public sealed class ObservableExtensions_VoidElementsTest : ReactiveTest
    {
        [Test]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        [SuppressMessage("ReSharper", "ReturnValueOfPureMethodIsNotUsed")]
        public void NullThisThrowsArgumentNullException() =>
            Should.Throw<ArgumentNullException>(() => ((IObservable<Unit>)null).VoidElements());

        [Test, AutoDomainData]
        public void ReturnsUnitForEachElement(TestScheduler testScheduler)
        {
            var source = testScheduler.CreateColdObservable(
                OnNext(100, 1),
                OnNext(200, 2),
                OnNext(300, 3),
                OnCompleted<int>(400));

            testScheduler
                .Start(() => source.VoidElements())
                .Messages
                .ShouldBe(
                    OnNext(300, Unit.Default),
                    OnNext(400, Unit.Default),
                    OnNext(500, Unit.Default),
                    OnCompleted<Unit>(600));
        }
    }
}
