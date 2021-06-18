// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 12-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>DateTimeExtensions</summary>
    public static class DateTimeExtensions
    {
        private const string UserTimeZone = "E. South America Standard Time";

        /// <summary>Converts to usertimezone.</summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime ToUserTimeZone(this DateTimeOffset dt)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UserTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dt.DateTime, userTimeZone);
        }

        /// <summary>Converts to usertimezone.</summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTime? ToUserTimeZone(this DateTimeOffset? dt)
        {
            if (!dt.HasValue)
                return null;

            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UserTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dt.Value.DateTime, userTimeZone);
        }

        /// <summary>Converts to usertimezone.</summary>
        /// <param name="dtUtc">The dt UTC.</param>
        /// <returns></returns>
        public static DateTime ToUserTimeZone(this DateTime dtUtc)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UserTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(dtUtc, userTimeZone);
        }

        /// <summary>Converts to utctimezone.</summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTimeOffset ToUtcTimeZone(this DateTime dt)
        {
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UserTimeZone);
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
        /// Converts to enddatetimeoffset.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static DateTimeOffset ToEndDateTimeOffset(this DateTime dt)
        {
            return dt.AddHours(23).AddMinutes(59).AddSeconds(59);
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
                dt = dt.AddSeconds(int.Parse(timeSplit[2]));
            }
            else if (!isStart)
            {
                dt = dt.AddSeconds(59);
            }

            // Add minutes
            if (timeSplit?.Length >= 2)
            {
                dt = dt.AddMinutes(int.Parse(timeSplit[1]));
            }

            // Add hours
            if (timeSplit?.Length >= 1)
            {
                dt = dt.AddHours(int.Parse(timeSplit[0]));
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
            return dt.ToString("dd/MM/yyyy hh:mm");
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

            return dt?.ToString("dd/MM/yyyy hh:mm");
        }

        /// <summary>
        /// Converts to stringhourminute.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        public static string ToStringHourMinute(this DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy hh:mm");
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

            return dt?.ToString("dd/MM/yyyy hh:mm");
        }
    }
}
