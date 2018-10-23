using JetBrains.Annotations;

namespace Annex.Numerics
{
    /// <summary>
    /// Provides an additional set of static members for <see cref="float"/>.
    /// </summary>
    [PublicAPI]
    public static class FloatEx
    {
        /// <summary>
        /// The smallest positive normal value of <see cref="float"/>. Comparable to Java's
        /// <see href="https://docs.oracle.com/javase/7/docs/api/java/lang/Float.html#MIN_NORMAL">Float.MIN_NORMAL</see>.
        /// </summary>
        public const float MinNormal = (1 << 23) * float.Epsilon;
    }
}
