using Microsoft.Reactive.Testing;
using NUnit.Framework;
using Shouldly;
using System.Reactive;

namespace Annex.Reactive.Test
{
    public sealed class ObservableEx_VoidTest : ReactiveTest
    {
        [Test]
        public void IsSingleton() => ObservableEx.Void.ShouldBeSameAs(ObservableEx.Void);

        [Test]
        public void ReturnsCompletedSequence() =>
            new TestScheduler()
                .Start(() => ObservableEx.Void)
                .Messages
                .AssertEqual(
                    OnNext(200, Unit.Default),
                    OnCompleted<Unit>(200));
    }
}
