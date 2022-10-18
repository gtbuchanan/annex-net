#pragma warning disable MEN010 // The numeric literal must be replaced with a named constant
// Justification: The names of the methods are descriptive enough.
namespace Annex.Time.Pollution;

/// <summary>
/// Global extension methods for <see cref="DateTime"/>. Only recommended for unit testing.
/// </summary>
[PublicAPI]
public static class DateTimeExtensions
{
    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in January in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime January(this int day, int year) => new(year, 1, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in February in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime February(this int day, int year) => new(year, 2, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in March in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime March(this int day, int year) => new(year, 3, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in April in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime April(this int day, int year) => new(year, 4, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in May in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime May(this int day, int year) => new(year, 5, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in June in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime June(this int day, int year) => new(year, 6, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in July in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime July(this int day, int year) => new(year, 7, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in August in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime August(this int day, int year) => new(year, 8, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in September in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime September(this int day, int year) => new(year, 9, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in October in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime October(this int day, int year) => new(year, 10, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in November in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime November(this int day, int year) => new(year, 11, day);

    /// <summary>
    /// Returns a <see cref="DateTime" /> representing the specified day in December in the specified year.
    /// </summary>
    /// <param name="day">The day value.</param>
    /// <param name="year">The year value.</param>
    /// <returns>The <see cref="DateTime"/> representation.</returns>
    public static DateTime December(this int day, int year) => new(year, 12, day);
}
