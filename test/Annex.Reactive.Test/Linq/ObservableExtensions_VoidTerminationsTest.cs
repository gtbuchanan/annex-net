namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;

public sealed class ObservableExtensions_VoidTerminationsTest : ReactiveTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((IObservable<Unit>?)null)!.VoidTerminations());

    [Theory]
    [AutoDomainData]
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

    [Theory]
    [AutoDomainData]
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
