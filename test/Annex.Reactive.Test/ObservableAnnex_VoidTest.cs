namespace Annex.Reactive.Test;

public sealed class ObservableAnnex_VoidTest : ReactiveTest
{
    [Test]
    public void IsSingleton() =>
        ObservableAnnex.Void.ShouldBeSameAs(ObservableAnnex.Void);

    [Test]
    public void ReturnsCompletedSequence() =>
        new TestScheduler()
            .Start(() => ObservableAnnex.Void)
            .Messages
            .AssertEqual(
                OnNext(200, Unit.Default),
                OnCompleted<Unit>(200));
}
