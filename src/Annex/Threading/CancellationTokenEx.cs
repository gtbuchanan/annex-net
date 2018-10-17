using JetBrains.Annotations;
using System.Threading;

namespace Annex.Threading
{
    /// <summary>
    ///     Provides an additional set of static members for <see cref="CancellationToken"/>.
    /// </summary>
    [PublicAPI]
    public static class CancellationTokenEx
    {
        /// <summary>
        ///     Returns an already canceled <see cref="CancellationToken"/> value.
        /// </summary>
        public static CancellationToken Canceled { get; } = new CancellationToken(true);
    }
}
