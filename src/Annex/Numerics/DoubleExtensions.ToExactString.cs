namespace Annex.Numerics;

/// <content />
public static partial class DoubleExtensions
{
    /// <summary>
    /// Converts the given <see cref="double"/> to the string representation of its exact decimal value.
    /// </summary>
    /// <param name="this">The source <see cref="double"/>.</param>
    /// <returns>The string representation of the double's exact decimal value.</returns>
    public static string ToExactString(this double @this) =>
        DoubleConverter.ToExactString(@this);
}
