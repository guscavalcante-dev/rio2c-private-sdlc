// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 12-13-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-16-2023
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>
    /// DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions
    {
        private const string BrazilTimeZone = "E. South America Standard Time";

        #region Brazil Timezone

        /// <summary>
        /// Gets the brazil UTC.
        /// </summary>
        /// <returns></returns>
        public static string GetBrazilUtc()
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(BrazilTimeZone);
            return $"UTC{userTimeZone.BaseUtcOffset.Hours}";
        }

        /// <summary>
        /// Converts to brazil time zone.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime ToBrazilTimeZone(this DateTimeOffset dt)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(BrazilTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dt.DateTime, userTimeZone);
        }

        /// <summary>
        /// Converts to brazil time zone.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime? ToBrazilTimeZone(this DateTimeOffset? dt)
        {
            if (!dt.HasValue)
                return null;

            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(BrazilTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dt.Value.DateTime, userTimeZone);
        }

        /// <summary>
        /// Converts to brazil time zone.
        /// </summary>
        /// <param name="dtUtc">The dt UTC.</param>
        /// <returns></returns>
        public static DateTime ToBrazilTimeZone(this DateTime dtUtc)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(BrazilTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dtUtc, userTimeZone);
        }

        #endregion

        /// <summary>Converts to utctimezone.</summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTimeOffset ToUtcTimeZone(this DateTime dt)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(BrazilTimeZone);
            return TimeZoneInfo.ConvertTimeToUtc(dt, userTimeZone);
        }

        /// <summary>
        /// Converts to enddatetime.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime ToEndDateTime(this DateTime dt)
        {
            return dt.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        /// <summary>
        /// Always use this function to save an EndDate at database!
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTimeOffset ToEndDateTimeOffset(this DateTime dt)
        {
            return dt.AddHours(23).AddMinutes(59).AddSeconds(59).ToUtcTimeZone();
        }

        /// <summary>Gets the day suffix.</summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string GetDaySuffix(this DateTime dt)
        {
            return (dt.Day % 10 == 1 && dt.Day != 11) ? "st" : 
                   (dt.Day % 10 == 2 && dt.Day != 12) ? "nd" : 
                   (dt.Day % 10 == 3 && dt.Day != 13) ? "rd" : 
                   "th";
        }

        /// <summary>Joins the date and time.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="time">The time.</param>
        /// <param name="isStart">if set to <c>true</c> [is start].</param>
        /// <returns></returns>
        public static DateTime JoinDateAndTime(this DateTime dt, string time, bool isStart)
        {
            var timeSplit = time?.Split(':');

            // Add seconds
            if (timeSplit?.Length == 3)
            {
                dt = dt.AddSeconds(timeSplit[2].ToIntNotNull());
            }
            else if (!isStart)
            {
                dt = dt.AddSeconds(59);
            }

            // Add minutes
            if (timeSplit?.Length >= 2)
            {
                dt = dt.AddMinutes(timeSplit[1].ToIntNotNull());
            }

            // Add hours
            if (timeSplit?.Length >= 1)
            {
                dt = dt.AddHours(timeSplit[0].ToIntNotNull());
            }

            return dt;
        }

        /// <summary>
        /// Converts to stringhourminute.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourMinute(this DateTimeOffset dt)
        {
            return dt.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Converts to stringhourminute.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourMinute(this DateTimeOffset? dt)
        {
            if (dt == null)
            {
                return "";
            }

            return dt?.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Converts to stringdatebraziltimezone.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringDateBrazilTimeZone(this DateTimeOffset dt)
        {
            return dt.ToBrazilTimeZone().ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Converts to stringdatebraziltimezone.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourBrazilTimeZone(this DateTimeOffset dt)
        {
            return dt.ToBrazilTimeZone().ToString("HH:mm");
        }

        /// <summary>
        /// Converts to stringhourminute.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourMinute(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Converts to stringhourminute.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourMinute(this DateTime? dt)
        {
            if(dt == null)
            {
                return "";
            }

            return dt?.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Converts to stringdaymonthyearextensive.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringDayMonthYearExtensive(this DateTime dt, string separator)
        {
            if (dt == null)
            {
                return "";
            }

            return dt.ToString($"dd '{separator}' MMMM '{separator}' yyyy");
        }

        /// <summary>
        /// Converts to stringdaymonthyearextensive.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringDayMonthYearExtensive(this DateTime? dt, string separator)
        {
            if (dt == null)
            {
                return "";
            }
            
            return dt?.ToString($"dd '{separator}' MMMM '{separator}' yyyy");
        }

        /// <summary>
        /// Converts to string filename timestamp (dd-MM-yyyy_HH-mm-ss).
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringFileNameTimestamp(this DateTime dt)
        {
            return dt.ToBrazilTimeZone().ToString("dd-MM-yyyy_HH-mm-ss");
        }

        /// <summary>
        /// Converts to shortdatestring.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTimeOffset dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Converts to shortdatestring.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTimeOffset? dt)
        {
            if (dt == null)
            {
                return "";
            }

            return dt?.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Converts to UTC date kind.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime? ToUtcDateKind(this DateTime? dt)
        {
            if (dt.Value.Kind == DateTimeKind.Unspecified)
            {
                // Specify the date kind when Unspecified because SQL set it automatically, resulting on different date from original
                dt = DateTime.SpecifyKind(dt.Value, DateTimeKind.Utc);
            }

            return dt;
        }
    }
}
