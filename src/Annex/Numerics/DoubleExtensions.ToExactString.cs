namespace Annex.Numerics
{
    public static partial class DoubleExtensions
    {
        /// <summary>
        ///     Converts the given double to a string representation of its exact decimal value.
        /// </summary>
        /// <param name="this">The double to convert.</param>
        /// <returns>A string representation of the double's exact decimal value.</returns>
        public static string ToExactString(this double @this) =>
            DoubleConverter.ToExactString(@this);
    }
}
