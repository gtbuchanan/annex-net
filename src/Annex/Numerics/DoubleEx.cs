using JetBrains.Annotations;

namespace Annex.Numerics
{
    /// <summary>
    /// Provides an additional set of static members for <see cref="double"/>.
    /// </summary>
    [PublicAPI]
    public static class DoubleEx
    {
        /// <summary>
        /// The smallest positive normal value of <see cref="double"/>. Comparable to Java's
        /// <see href="https://docs.oracle.com/javase/7/docs/api/java/lang/Double.html#MIN_NORMAL">Double.MIN_NORMAL</see>.
        /// </summary>
        public const double MinNormal = (1L << 52) * double.Epsilon;
    }
}
