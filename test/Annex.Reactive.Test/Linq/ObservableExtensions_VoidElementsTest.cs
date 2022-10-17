namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;

public sealed class ObservableExtensions_VoidElementsTest : ReactiveTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((IObservable<Unit>?)null)!.VoidElements());

    [Theory]
    [AutoDomainData]
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
