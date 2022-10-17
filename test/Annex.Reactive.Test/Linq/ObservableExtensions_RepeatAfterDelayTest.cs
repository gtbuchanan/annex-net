namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;
using Annex.Time.Pollution;

public sealed class ObservableExtensions_RepeatAfterDelayTest : ReactiveTest
{
    [Theory]
    [AutoDomainData]
    public void NullThisThrowsArgumentNullException(TimeSpan delay, IScheduler scheduler) =>
        Should.Throw<ArgumentNullException>(() =>
            ((IObservable<Unit>?)null)!.RepeatAfterDelay(delay, scheduler));

    [Theory]
    [AutoDomainData]
    public void ReturnsRepeatingSequence(TestScheduler testScheduler) =>
        testScheduler
            .Start(() => ObservableAnnex.Void
                .RepeatAfterDelay(300.Ticks(), testScheduler))
            .Messages
            .ShouldBe(
                OnNext(200, Unit.Default),
                OnNext(500, Unit.Default),
                OnNext(800, Unit.Default));
}
