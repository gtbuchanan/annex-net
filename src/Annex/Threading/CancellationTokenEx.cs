using JetBrains.Annotations;
using System.Threading;

namespace Annex.Threading
{
    /// <summary>
    ///     Propogates notification that operations should be canceled.
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
