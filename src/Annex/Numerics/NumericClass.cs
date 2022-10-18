namespace Annex.Numerics;

/// <summary>
/// Represents a class of numeric types.
/// </summary>
[PublicAPI]
[Flags]
public enum NumericClass
{
    /// <summary>
    /// No numeric class.
    /// </summary>
    None = 0,

    /// <summary>
    /// Represents numbers without fractional parts.
    /// </summary>
    Integral = 1,

    /// <summary>
    /// Represents numbers with both integer and fractional parts.
    /// </summary>
    FloatingPoint = 2,

    /// <summary>
    /// Represents numbers with a real number part and an imaginary number part.
    /// </summary>
    Complex = 4,

    /// <summary>
    /// Represents a quantity having direction as well as magnitude, especially
    /// as determining the position of one point in space relative to another.
    /// </summary>
    Vector = 8,

    /// <summary>
    /// All numeric types.
    /// </summary>
    All = Integral | FloatingPoint | Complex | Vector,
}
