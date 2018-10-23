namespace Annex.Numerics
{
    public static partial class FloatExtensions
    {
        /// <summary>
        /// Converts the given <see cref="float"/> to the string representation of its exact decimal value.
        /// </summary>
        /// <param name="this">The source <see cref="float"/>.</param>
        /// <returns>The string representation of the float's exact decimal value.</returns>
        public static string ToExactString(this float @this) =>
            DoubleConverter.ToExactString(@this);
    }
}
