// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>StringExtensions</summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Método de extensão feito para capturar valores do arquivo de configuração
        /// Web.config ou App.config
        /// </summary>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <param name="propertyName">valor da key do parametro no arquivo de configuração</param>
        /// <returns></returns>
        public static T GetConfigurationProperty<T>(this string propertyName)
        {
            var value = ConfigurationManager.AppSettings[propertyName];
            return string.IsNullOrEmpty(value) ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// Método de extensão feito para capturar valores do arquivo de configuração
        /// Web.config ou App.config
        /// </summary>
        /// <typeparam name="T">Tipo do retorno</typeparam>
        /// <param name="propertyName">valor da key de um enum que equivale à um parametro no arquivo de configuração</param>
        /// <returns></returns>
        public static T GetConfigurationProperty<T>(this Enum propertyName)
        {
            var value = ConfigurationManager.AppSettings[propertyName.ToString()];
            return string.IsNullOrEmpty(value) ? default(T) : (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>Reversers the length of the string with.</summary>
        /// <param name="value">The value.</param>
        /// <param name="lenght">The lenght.</param>
        /// <returns></returns>
        public static string ReverserStringWithLength(this string value, int lenght)
        {
            int interactions = lenght > 0 ? lenght - 1 : value.Length - 1;
            string result = "";
            for (int i = interactions; i >= 0; i--)
            {
                result += value[i];
            }
            return result;
        }

        /// <summary>Removes the accents.</summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        /// <summary>Uppercases the first.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        /// <summary>Uppercases the first of each word.</summary>
        /// <param name="s">The s.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public static string UppercaseFirstOfEachWord(this string s, string culture)
        {
            if (string.IsNullOrEmpty(culture))
            {
                return s;
            }

            var textInfo = new CultureInfo(culture, false).TextInfo;
            return textInfo.ToTitleCase(s.ToLowerInvariant());
        }

        /// <summary>Gets the first word.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetFirstWord(this string s)
        {
            return s?.Split(' ')?[0];
        }

        /// <summary>Gets the first character.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetFirstChar(this string s)
        {
            return s?.Substring(0, 1);
        }

        /// <summary>Converts the string representation of a number to an integer.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static int? ToInt(this string s)
        {
            int output;
            if (!int.TryParse(s, NumberStyles.None, null, out output))
            {
                return null;
            }

            return output;
        }

        /// <summary>Gets the two letter code.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetTwoLetterCode(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            string code = null;

            var splitString = s.Split(' ');
            if (splitString.Length == 1)
            {
                code = splitString[0].Substring(0, 2);
            }
            else if (splitString.Length >= 3)
            {
                code = splitString[0].Substring(0, 1) + splitString[2].Substring(0, 1);
            }
            else
            {
                code = splitString[0].Substring(0, 1) + splitString[1].Substring(0, 1);
            }

            return code?.ToUpperInvariant();
        }

        /// <summary>Gets the separator translation.</summary>
        /// <param name="s">The s.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static string GetSeparatorTranslation(this string s, string culture, char separator)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            var splitName = s.Split(separator);

            if (culture?.ToLowerInvariant() == "pt-br")
            {
                return splitName[0].Trim();
            }

            if (splitName.Length > 1)
            {
                return splitName[1].Trim();
            }

            return splitName[0].Trim();
        }

        /// <summary>Gets the splitted word.</summary>
        /// <param name="s">The s.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="position">The position.</param>
        /// <returns></returns>
        public static string GetSplittedWord(this string s, char separator, int position)
        {
            var splitName = s.Split(separator);

            if (position > splitName.Length)
            {
                return splitName[splitName.Length];
            }

            return splitName[position];
        }

        /// <summary>Converts to listguid.</summary>
        /// <param name="s">The s.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static List<Guid> ToListGuid(this string s, char separator)
        {
            var list = new List<Guid>();

            var splitted = s?.Split(separator);
            if (splitted == null || splitted.Length <= 0)
            {
                return list;
            }

            foreach (var split in splitted)
            {
                if (Guid.TryParse(split, out Guid guid))
                {
                    list.Add(guid);
                }
            }

            return list;
        }

        /// <summary>Determines whether this instance is image.</summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if the specified s is image; otherwise, <c>false</c>.</returns>
        public static bool IsImage(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && (s.ToLowerInvariant().EndsWith(".jpg")
                       || s.ToLowerInvariant().EndsWith(".jpeg")
                       || s.ToLowerInvariant().EndsWith(".png")
                       || s.ToLowerInvariant().EndsWith(".gif"));
        }

        /// <summary>Determines whether [is vimeo video].</summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is vimeo video] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsVimeoVideo(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && s.ToLowerInvariant().Contains("vimeo.com");
        }

        /// <summary>Determines whether [is youtube video].</summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is youtube video] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsYoutubeVideo(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && s.ToLowerInvariant().Contains("youtube.com");
        }

        /// <summary>Converts the video to embed.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ConvertVideoToEmbed(this string s)
        {
            if (IsYoutubeVideo(s))
            {
                var rgx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
                var match = rgx.Match(s);

                if (!match.Success)
                {
                    return s;
                }

                var id = match.Groups[1].Value;
                return $"<iframe title='YouTube video player' width='480' height='270' src='https://www.youtube.com/embed/{id}' frameborder='0' allowfullscreen='1'></iframe>";
            }

            if (IsVimeoVideo(s))
            {
                var rgx = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)");
                var match = rgx.Match(s);

                if (!match.Success)
                {
                    return s;
                }

                var id = match.Groups[1].Value;
                return $"<iframe src='https://player.vimeo.com/video/{id}' width='480' height='270' frameborder='0' allow='autoplay; fullscreen' allowfullscreen></iframe>";
            }

            return s;
        }
    }
}
