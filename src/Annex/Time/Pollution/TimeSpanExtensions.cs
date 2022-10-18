namespace Annex.Time.Pollution;

/// <summary>
/// Global extension methods for <see cref="TimeSpan"/>. Only recommended for unit testing.
/// </summary>
[PublicAPI]
public static class TimeSpanExtensions
{
    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of ticks.
    /// </summary>
    /// <param name="ticks">The tick value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Ticks(this int ticks) => TimeSpan.FromTicks(ticks);

    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of milliseconds.
    /// </summary>
    /// <param name="milliseconds">The millisecond value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Milliseconds(this int milliseconds) =>
        TimeSpan.FromMilliseconds(milliseconds);

    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of seconds.
    /// </summary>
    /// <param name="seconds">The second value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);

    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of minutes.
    /// </summary>
    /// <param name="minutes">The minute value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Minutes(this int minutes) => TimeSpan.FromMinutes(minutes);

    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of hours.
    /// </summary>
    /// <param name="hours">The hour value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Hours(this int hours) => TimeSpan.FromHours(hours);

    /// <summary>
    /// Returns a <see cref="TimeSpan"/> representing the specified number of days.
    /// </summary>
    /// <param name="days">The day value.</param>
    /// <returns>The <see cref="TimeSpan"/> representation.</returns>
    public static TimeSpan Days(this int days) => TimeSpan.FromDays(days);
}
