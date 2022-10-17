namespace Annex.Reactive.Test;

using System.Runtime.CompilerServices;
using Annex.Pollution;
using Microsoft.Reactive.Testing;

public sealed class ObservableAnnex_UsingTest : ReactiveTest
{
    [Theory]
    [AutoDomainData]
    public void NullResourceFactoryThrowsArgumentNullException(
        Func<CancellationTokenSource, IObservable<Unit>> observableFactory) =>
        Should.Throw<ArgumentNullException>(() => ObservableAnnex.Using(null!, observableFactory));

    [Theory]
    [AutoDomainData]
    public void NullObservableFactoryThrowsArgumentNullException(
        Func<CancellationToken, Task<CancellationTokenSource>> resourceFactory) =>
        Should.Throw<ArgumentNullException>(() => ObservableAnnex
            .Using<Unit, CancellationTokenSource>(resourceFactory, null!));

    [Theory]
    [AutoDomainData]
    public void DisposesResourceOnCompleted(IDisposable disposable)
    {
        const long Delay = 1;
        const long Completed = Subscribed + Delay;

        var scheduler = new TestScheduler();

        scheduler.ScheduleAbsolute(Completed, () => disposable.DidNotReceive().Dispose());

        var observable = scheduler.CreateColdObservable(
            OnNext(Delay, Unit.Default),
            OnCompleted<Unit>(Delay));

        var results = scheduler.Start(() => ObservableAnnex.Using(
            _ => Task.FromResult(disposable),
            _ => observable));

        results.Messages.AssertEqual(
            OnNext(Completed, Unit.Default),
            OnCompleted<Unit>(Completed));

        disposable.Received(1).Dispose();
    }
}
