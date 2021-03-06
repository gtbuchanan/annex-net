using JetBrains.Annotations;
using System;

namespace Annex.Time.Global
{
    /// <summary>
    /// Global extension methods for <see cref="DateTime"/>. Only recommended for unit testing.
    /// </summary>
    [PublicAPI]
    public static class GlobalDateTimeExtensions
    {
        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in January in the specified year.
        /// </summary>
        public static DateTime January(this int day, int year) => new DateTime(year, 1, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in February in the specified year.
        /// </summary>
        public static DateTime February(this int day, int year) => new DateTime(year, 2, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in March in the specified year.
        /// </summary>
        public static DateTime March(this int day, int year) => new DateTime(year, 3, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in April in the specified year.
        /// </summary>
        public static DateTime April(this int day, int year) => new DateTime(year, 4, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in May in the specified year.
        /// </summary>
        public static DateTime May(this int day, int year) => new DateTime(year, 5, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in June in the specified year.
        /// </summary>
        public static DateTime June(this int day, int year) => new DateTime(year, 6, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in July in the specified year.
        /// </summary>
        public static DateTime July(this int day, int year) => new DateTime(year, 7, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in August in the specified year.
        /// </summary>
        public static DateTime August(this int day, int year) => new DateTime(year, 8, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in September in the specified year.
        /// </summary>
        public static DateTime September(this int day, int year) => new DateTime(year, 9, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in October in the specified year.
        /// </summary>
        public static DateTime October(this int day, int year) => new DateTime(year, 10, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in November in the specified year.
        /// </summary>
        public static DateTime November(this int day, int year) => new DateTime(year, 11, day);

        /// <summary>
        /// Returns a <see cref="DateTime" /> representing the specified day in December in the specified year.
        /// </summary>
        public static DateTime December(this int day, int year) => new DateTime(year, 12, day);
    }
}
