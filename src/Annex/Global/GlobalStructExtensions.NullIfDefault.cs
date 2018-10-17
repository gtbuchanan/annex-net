namespace Annex.Global
{
    public static partial class GlobalStructExtensions
    {
        /// <summary>
        ///     Returns null if the source value equals the default value for the type.
        /// </summary>
        /// <typeparam name="T">The type of the source value.</typeparam>
        /// <param name="this">The source value.</param>
        /// <returns>
        ///     <c>null</c> if the source value equals the default value for the type, otherwise <c>false</c>.
        /// </returns>
        public static T? NullIfDefault<T>(this T @this) where T : struct =>
            @this.NullIf(default);
    }
}
