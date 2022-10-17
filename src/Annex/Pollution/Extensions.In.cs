namespace Annex.Pollution;

/// <content />
public static partial class Extensions
{
    /// <summary>
    /// Determines if the value is contained in the sequence of specified comparands
    /// using the specified <see cref="IEqualityComparer{T}"/>. For constant comparands,
    /// consider using a static readonly <see cref="HashSet{T}"/> instead.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TComparand">The type of the comparands.</typeparam>
    /// <param name="this">The value to check.</param>
    /// <param name="comparer">
    /// The equality comparer implementation to use when comparing values.
    /// </param>
    /// <param name="comparands">The comparands to compare the value to.</param>
    /// <returns>
    /// <c>true</c> if the value is contained in the list of specified comparands,
    /// otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/>, <paramref name="comparer"/>, or
    /// <paramref name="comparands"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// int num = 0, possibleNum1 = 1, possibleNum2 = 5; // Preferably not constant values
    ///
    /// num.In(possibleNum1, possibleNum2, EqualityComparer&lt;int&gt;.Default); // false
    /// </code>
    /// </example>
    /// <remarks>
    /// For constant comparands, consider using a static readonly <see cref="HashSet{T}"/>
    /// instead to avoid initializing a new array every time the method is called.
    /// </remarks>
    /// <seealso cref="HashSet{T}.Contains"/>
    public static bool In<TValue, TComparand>(
        this TValue @this,
        IEqualityComparer<TComparand> comparer,
        params TComparand[] comparands)
        where TValue : TComparand =>
        comparands.Contains(@this, comparer);

    /// <summary>
    /// Determines if the value is contained in the sequence of specified comparands.
    /// For constant comparands, consider using a static readonly <see cref="HashSet{T}"/>
    /// instead.
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <typeparam name="TComparand">The type of the comparands.</typeparam>
    /// <param name="this">The source value.</param>
    /// <param name="comparands">The comparands to compare the value to.</param>
    /// <returns>
    /// <c>true</c> if the value is contained in the list of specified comparands,
    /// otherwise <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="this"/> or <paramref name="comparands"/> is <c>null</c>.
    /// </exception>
    /// <example>
    /// <code language="csharp">
    /// int num = 0, possibleNum1 = 1, possibleNum2 = 5; // Preferably not constant values
    ///
    /// num.In(possibleNum1, possibleNum2); // false
    /// </code>
    /// </example>
    /// <remarks>
    /// Elements are compared to the specified value by using the default equality comparer,
    /// <see cref="EqualityComparer{T}.Default">Default</see>.
    /// <br/><br/>
    /// For constant comparands, consider using a static readonly <see cref="HashSet{T}"/>
    /// instead to avoid initializing a new array every time the method is called.
    /// </remarks>
    /// <seealso cref="HashSet{T}.Contains"/>
    public static bool In<TValue, TComparand>(
        this TValue @this,
        params TComparand[] comparands)
        where TValue : notnull, TComparand =>
        @this.In(EqualityComparer<TComparand>.Default, comparands);
}
