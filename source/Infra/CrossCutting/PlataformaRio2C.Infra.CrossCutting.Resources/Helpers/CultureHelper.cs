﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-01-2019
// ***********************************************************************
// <copyright file="CultureHelper.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Infra.CrossCutting.Resources.Helpers
{
    /// <summary>CultureHelper</summary>
    public class CultureHelper
    {
        // Valid cultures
        private static readonly List<string> _validCultures = new List<string> { "af", "af-ZA", "sq", "sq-AL", "gsw-FR", "am-ET", "ar", "ar-DZ", "ar-BH", "ar-EG", "ar-IQ", "ar-JO", "ar-KW", "ar-LB", "ar-LY", "ar-MA", "ar-OM", "ar-QA", "ar-SA", "ar-SY", "ar-TN", "ar-AE", "ar-YE", "hy", "hy-AM", "as-IN", "az", "az-Cyrl-AZ", "az-Latn-AZ", "ba-RU", "eu", "eu-ES", "be", "be-BY", "bn-BD", "bn-IN", "bs-Cyrl-BA", "bs-Latn-BA", "br-FR", "bg", "bg-BG", "ca", "ca-ES", "zh-HK", "zh-MO", "zh-CN", "zh-Hans", "zh-SG", "zh-TW", "zh-Hant", "co-FR", "hr", "hr-HR", "hr-BA", "cs", "cs-CZ", "da", "da-DK", "prs-AF", "div", "div-MV", "nl", "nl-BE", "nl-NL", "en", "en-AU", "en-BZ", "en-CA", "en-029", "en-IN", "en-IE", "en-JM", "en-MY", "en-NZ", "en-PH", "en-SG", "en-ZA", "en-TT", "en-GB", "en-US", "en-ZW", "et", "et-EE", "fo", "fo-FO", "fil-PH", "fi", "fi-FI", "fr", "fr-BE", "fr-CA", "fr-FR", "fr-LU", "fr-MC", "fr-CH", "fy-NL", "gl", "gl-ES", "ka", "ka-GE", "de", "de-AT", "de-DE", "de-LI", "de-LU", "de-CH", "el", "el-GR", "kl-GL", "gu", "gu-IN", "ha-Latn-NG", "he", "he-IL", "hi", "hi-IN", "hu", "hu-HU", "is", "is-IS", "ig-NG", "id", "id-ID", "iu-Latn-CA", "iu-Cans-CA", "ga-IE", "xh-ZA", "zu-ZA", "it", "it-IT", "it-CH", "ja", "ja-JP", "kn", "kn-IN", "kk", "kk-KZ", "km-KH", "qut-GT", "rw-RW", "sw", "sw-KE", "kok", "kok-IN", "ko", "ko-KR", "ky", "ky-KG", "lo-LA", "lv", "lv-LV", "lt", "lt-LT", "wee-DE", "lb-LU", "mk", "mk-MK", "ms", "ms-BN", "ms-MY", "ml-IN", "mt-MT", "mi-NZ", "arn-CL", "mr", "mr-IN", "moh-CA", "mn", "mn-MN", "mn-Mong-CN", "ne-NP", "no", "nb-NO", "nn-NO", "oc-FR", "or-IN", "ps-AF", "fa", "fa-IR", "pl", "pl-PL", "pt", "pt-BR", "pt-PT", "pa", "pa-IN", "quz-BO", "quz-EC", "quz-PE", "ro", "ro-RO", "rm-CH", "ru", "ru-RU", "smn-FI", "smj-NO", "smj-SE", "se-FI", "se-NO", "se-SE", "sms-FI", "sma-NO", "sma-SE", "sa", "sa-IN", "sr", "sr-Cyrl-BA", "sr-Cyrl-SP", "sr-Latn-BA", "sr-Latn-SP", "nso-ZA", "tn-ZA", "si-LK", "sk", "sk-SK", "sl", "sl-SI", "es", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-DO", "es-EC", "es-SV", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-ES", "es-US", "es-UY", "es-VE", "sv", "sv-FI", "sv-SE", "syr", "syr-SY", "tg-Cyrl-TJ", "tzm-Latn-DZ", "ta", "ta-IN", "tt", "tt-RU", "te", "te-IN", "th", "th-TH", "bo-CN", "tr", "tr-TR", "tk-TM", "ug-CN", "uk", "uk-UA", "wen-DE", "ur", "ur-PK", "uz", "uz-Cyrl-UZ", "uz-Latn-UZ", "vi", "vi-VN", "cy-GB", "wo-SN", "sah-RU", "ii-CN", "yo-NG" };

        // Include ONLY cultures you are implementing
        public static readonly List<string> Cultures = new List<string> {
            "pt-BR", // first culture is the DEFAULT
            "en-US",
        };

        /// <summary>Returns true if the language is a right-to-left language. Otherwise, false.</summary>
        /// <returns>
        ///   <c>true</c> if [is righ to left]; otherwise, <c>false</c>.</returns>
        public static bool IsRighToLeft()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;
        }

        /// <summary>Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US".</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetImplementedCulture(string name)
        {
            // Make sure it's not null
            if (string.IsNullOrEmpty(name))
            {
                return GetDefaultCulture(); // return Default culture
            }

            // Make sure it is a valid culture first
            if (!IsValidCulture(name))
            {
                return GetDefaultCulture(); // return Default culture if it is invalid
            }

            // If it is implemented, accept it
            if (IsImplementedCulture(name))
            {
                return name; // accept it
                             // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
                             // the function will return closes match that is "en-US" because at least the language is the same (ie English)  
            }

            var n = GetNeutralCulture(name);
            foreach (var c in Cultures)
            {
                if (c.StartsWith(n))
                {
                    return c;
                }
            }

            // else 
            // It is not implemented
            return GetDefaultCulture(); // return Default culture as no match found
        }

        /// <summary>Determines whether [is valid culture] [the specified name].</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is valid culture] [the specified name]; otherwise, <c>false</c>.</returns>
        public static bool IsValidCulture(string name)
        {
            return _validCultures.Any(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>Determines whether [is implemented culture] [the specified name].</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if [is implemented culture] [the specified name]; otherwise, <c>false</c>.</returns>
        public static bool IsImplementedCulture(string name)
        {
            return Cultures.Any(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>Returns default culture name which is the first name decalared (e.g. en-US).</summary>
        /// <returns></returns>
        public static string GetDefaultCulture()
        {
            return Cultures[0]; // return Default culture
        }

        /// <summary>Gets the current culture.</summary>
        /// <returns></returns>
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        /// <summary>Gets the current neutral culture.</summary>
        /// <returns></returns>
        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        /// <summary>Gets the neutral culture.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-"))
            {
                return name;
            }

            return name.Split('-')[0]; // Read first part only. E.g. "en", "es"
        }

        /// <summary>Gets the culture flag image.</summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string GetCultureFlagImage(string name)
        {
            var nameInvariant = name.ToLowerInvariant();

            if (nameInvariant == "en-us")
            {
                return "020-flag.svg";
            }

            return "011-brazil.svg";
        }
    }
}
