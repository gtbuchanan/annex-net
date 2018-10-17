namespace Annex.Numerics
{
    public static partial class FloatExtensions
    {
        /// <summary>
        ///     Converts the given float to a string representation of its exact decimal value.
        /// </summary>
        /// <param name="this">The float to convert.</param>
        /// <returns>A string representation of the float's exact decimal value.</returns>
        public static string ToExactString(this float @this) =>
            DoubleConverter.ToExactString(@this);
    }
}
