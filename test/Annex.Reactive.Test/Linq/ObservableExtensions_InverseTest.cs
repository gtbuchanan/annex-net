namespace Annex.Reactive.Test.Linq;

using Annex.Reactive.Linq;

public sealed class ObservableExtensions_InverseTest : ReactiveTest
{
    [Test]
    public void NullThisThrowsArgumentNullException() =>
        Should.Throw<ArgumentNullException>(() => ((IObservable<bool>?)null)!.Inverse());

    [Theory]
    [InlineAutoDomainData(true)]
    [InlineAutoDomainData(false)]
    public void ReturnsInverse(bool value, TestScheduler testScheduler) =>
        testScheduler
            .Start(() => Observable
                .Return(value, testScheduler)
                .Inverse())
            .Messages
            .ShouldBe(
                OnNext(201, !value),
                OnCompleted<bool>(201));
}
