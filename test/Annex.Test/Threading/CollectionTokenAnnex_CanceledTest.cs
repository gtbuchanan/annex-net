namespace Annex.Test.Threading;

using Annex.Threading;

public sealed class CollectionTokenAnnex_CanceledTest
{
    [Test]
    public void IsCanceled() =>
        CancellationTokenAnnex
            .Canceled
            .ShouldBe(new CancellationToken(true));
}
