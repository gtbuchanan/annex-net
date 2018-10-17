namespace Annex.Numerics
{
    /// <summary>
    ///     Provides an additional set of static members for <see cref="float"/>.
    /// </summary>
    public static class FloatEx
    {
        /// <summary>
        ///     The smallest positive normal value of <see cref="float"/>. Comparable to Java's Float.MIN_NORMAL.
        /// </summary>
        public const float MinNormal = (1 << 23) * float.Epsilon;
    }
}
