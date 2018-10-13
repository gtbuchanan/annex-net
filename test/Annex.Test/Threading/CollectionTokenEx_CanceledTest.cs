using Annex.Threading;
using NUnit.Framework;
using Shouldly;

namespace Annex.Test.Threading
{
    public sealed class CollectionTokenEx_CanceledTest
    {
        [Test]
        public void IsCanceled() => CancellationTokenEx.Canceled.IsCancellationRequested.ShouldBeTrue();
    }
}
