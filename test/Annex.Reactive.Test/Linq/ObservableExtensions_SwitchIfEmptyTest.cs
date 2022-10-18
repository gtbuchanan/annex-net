namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;

public sealed class ObservableExtensions_SwitchIfEmptyTest : ReactiveTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(IObservable<Unit> switchTo) =>
        Should.Throw<ArgumentNullException>(() =>
            ((IObservable<Unit>?)null)!.SwitchIfEmpty(switchTo));

    [Theory]
    [AutoDomainData]
    public void NullSwitchToThrowsArgumentNullException(IObservable<Unit> @this) =>
        Should.Throw<ArgumentNullException>(() => @this.SwitchIfEmpty(null!));

    [Theory]
    [AutoDomainData]
    public void ReturnsFirstSequenceIfNotEmpty(TestScheduler testScheduler) =>
        testScheduler
            .Start(() => Observable
                .Return(true, testScheduler)
                .SwitchIfEmpty(Observable.Empty<bool>()))
            .Messages
            .ShouldBe(
                OnNext(201, true),
                OnCompleted<bool>(201));

    [Theory]
    [AutoDomainData]
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

    [Theory]
    [AutoDomainData]
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
