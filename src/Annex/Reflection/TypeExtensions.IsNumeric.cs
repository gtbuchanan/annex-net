namespace Annex.Reflection;

using Annex.Numerics;
using EnumsNET;

/// <content />
public static partial class TypeExtensions
{
    /// <summary>
    /// Gets a value indicating if the type is a numeric type.
    /// </summary>
    /// <param name="this">The source type.</param>
    /// <returns><c>true</c> if the type is numeric, otherwise <c>false</c>.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
    /// <example>
    /// <code language="csharp">
    /// typeof(int).IsNumeric(); // true
    /// typeof(double).IsNumeric(); // true
    /// typeof(decimal).IsNumeric(); // true
    /// typeof(BigInteger).IsNumeric(); // true
    /// typeof(Complex).IsNumeric(); // true
    /// typeof(Matrix3x2).IsNumeric(); // true
    /// typeof(object).IsNumeric(); // false
    /// </code>
    /// </example>
    public static bool IsNumeric(this Type @this) => @this.IsNumeric(NumericClass.All);

    /// <summary>
    /// Gets a value indicating if the type is a numeric type in any of the specified numeric classes.
    /// </summary>
    /// <param name="this">The source type.</param>
    /// <param name="numericClass">The classes of numeric types to check.</param>
    /// <returns>
    /// <c>true</c> if the type is a numeric type in any of the specified numeric classes, otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException"><paramref name="this"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="numericClass"/> is invalid.</exception>
    /// <example>
    /// <code language="csharp">
    /// typeof(int).IsNumeric(NumericClass.FloatingPoint); // false
    /// typeof(int).IsNumeric(NumericClass.Integral | NumericClass.FloatingPoint); // true
    /// typeof(BigInteger).IsNumeric(NumericClass.Integral); // true
    /// typeof(Matrix3x2).IsNumeric(NumericClass.Vector); // true
    /// </code>
    /// </example>
    public static bool IsNumeric(this Type @this, NumericClass numericClass)
    {
        if (!numericClass.IsValid())
            throw new ArgumentOutOfRangeException(nameof(numericClass), numericClass, null);

        var type = @this.IsGenericType ? @this.GetGenericTypeDefinition() : @this;
        return NumericAnnex.TypeMap.TryGetValue(type, out var actualClass) &&
            numericClass.HasAnyFlags(actualClass);
    }
}
