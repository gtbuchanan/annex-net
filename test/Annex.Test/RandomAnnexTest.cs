#if !NET6_0_OR_GREATER
namespace Annex.Test;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

[SuppressMessage(
    "Security",
    "CA5394:Do not use insecure randomness",
    Justification = "Intentional testing of Random polyfill.")]
public sealed class RandomAnnexTest
{
    [Test]
    public void Shared_IsSingleton()
    {
        RandomAnnex.Shared.ShouldNotBeNull();
        RandomAnnex.Shared.ShouldBeSameAs(RandomAnnex.Shared);
        Task.Run(() => RandomAnnex.Shared).Result.ShouldBeSameAs(RandomAnnex.Shared);
    }

    [Test]
    public void Shared_ParallelUsage()
    {
        using var barrier = new Barrier(2);
        Parallel.For(0, 2, _ =>
        {
            var buffer = new byte[1000];

            barrier.SignalAndWait();
            for (var i = 0; i < 1_000; i++)
            {
                RandomAnnex.Shared.Next().ShouldBeInRange(0, int.MaxValue - 1);
                RandomAnnex.Shared.Next(5).ShouldBeInRange(0, 4);
                RandomAnnex.Shared.Next(42, 50).ShouldBeInRange(42, 49);

                RandomAnnex.Shared.NextDouble().ShouldBeInRange(0.0, 1.0);

                Array.Clear(buffer, 0, buffer.Length);
                RandomAnnex.Shared.NextBytes(buffer);
                buffer.ShouldContain(b => b != 0);
            }
        });
    }
}
#endif
