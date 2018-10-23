using JetBrains.Annotations;
using System;

namespace Annex.Time.Global
{
    /// <summary>
    /// Global extension methods for <see cref="TimeSpan"/>. Only recommended for unit testing.
    /// </summary>
    [PublicAPI]
    public static class GlobalTimeSpanExtensions
    {
        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of ticks.
        /// </summary>
        public static TimeSpan Ticks(this int ticks) => TimeSpan.FromTicks(ticks);

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of milliseconds.
        /// </summary>
        public static TimeSpan Milliseconds(this int milliseconds) =>
            TimeSpan.FromMilliseconds(milliseconds);

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of seconds.
        /// </summary>
        public static TimeSpan Seconds(this int seconds) => TimeSpan.FromSeconds(seconds);

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of minutes.
        /// </summary>
        public static TimeSpan Minutes(this int minutes) => TimeSpan.FromMinutes(minutes);

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of hours.
        /// </summary>
        public static TimeSpan Hours(this int hours) => TimeSpan.FromHours(hours);

        /// <summary>
        /// Returns a <see cref="TimeSpan"/> representing the specified number of days.
        /// </summary>
        public static TimeSpan Days(this int days) => TimeSpan.FromDays(days);
    }
}
