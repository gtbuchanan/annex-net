namespace Annex.Numerics
{
    /// <summary>
    ///     Provides an additional set of static members for <see cref="double"/>.
    /// </summary>
    public static class DoubleEx
    {
        /// <summary>
        ///     The smallest positive normal value of <see cref="double"/>. Comparable to Java's Double.MIN_NORMAL.
        /// </summary>
        public const double MinNormal = (1L << 52) * double.Epsilon;
    }
}
