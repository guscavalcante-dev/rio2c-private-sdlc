// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-02-2021
// ***********************************************************************
// <copyright file="StringExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>StringExtensions</summary>
    public static class StringExtensions
    {
        /// <summary>Determines whether [is case insensitive equal to] [the specified other string].</summary>
        /// <param name="s">The s.</param>
        /// <param name="otherString">The other string.</param>
        /// <returns>
        ///   <c>true</c> if [is case insensitive equal to] [the specified other string]; otherwise, <c>false</c>.</returns>
        public static bool IsCaseInsensitiveEqualTo(this string s, string otherString)
        {
            return string.Compare(s?.Trim(), otherString?.Trim(), CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase) == 0;
        }

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

        /// <summary>Removes the filename invalid chars.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string RemoveFilenameInvalidChars(this string value)
        {
            return string.Concat(value.Split(Path.GetInvalidFileNameChars()));
        }

        /// <summary>Replaces the filename invalid chars.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ReplaceFilenameInvalidChars(this string value)
        {
            return string.Join("_", value.Split(Path.GetInvalidFileNameChars()));
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

        /// <summary>Gets the limit.</summary>
        /// <param name="s">The s.</param>
        /// <param name="limit">The limit.</param>
        /// <returns></returns>
        public static string GetLimit(this string s, int limit)
        {
            return (s?.Length ?? 0) <= limit ? s : 
                   s?.Substring(0, limit - 1);
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

            var splitString = s.Split(' ').Where(ss => !string.IsNullOrEmpty(ss)).ToList();
            if (splitString.Count == 1)
            {
                code = splitString[0].Substring(0, 2);
            }
            else
            {
                code = splitString[0].Substring(0, 1) + splitString[splitString.Count - 1].Substring(0, 1);
            }

            return code?.ToUpperInvariant();
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

        /// <summary>Converts to datetime.</summary>
        /// <param name="s">The s.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="truncateTime">The truncate time.</param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this string s, string dateFormat, bool? truncateTime = false)
        {
            DateTime? dateTime = null;

            if (string.IsNullOrEmpty(s))
            {
                return null;
            }

            if (DateTime.TryParseExact(s, dateFormat, null, DateTimeStyles.None, out DateTime dateTimeOut))
            {
                dateTime = dateTimeOut;
            }

            return dateTime;
        }

        /// <summary>Converts to listdatetime.</summary>
        /// <param name="s">The s.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="truncateTime">The truncate time.</param>
        /// <returns></returns>
        public static List<DateTime> ToListDateTime(this string s, char separator, string dateFormat, bool? truncateTime = false)
        {
            var list = new List<DateTime>();

            var splitted = s?.Split(separator);
            if (splitted == null || splitted.Length <= 0)
            {
                return list;
            }

            foreach (var split in splitted)
            {
                if (DateTime.TryParseExact(split, dateFormat, null, DateTimeStyles.None, out DateTime dateTime))
                {
                    list.Add(truncateTime != true ? dateTime.ToUtcTimeZone().DateTime : dateTime.ToUtcTimeZone().Date);
                }
            }

            return list;
        }

        /// <summary>Converts to listdatetimeoffset.</summary>
        /// <param name="s">The s.</param>
        /// <param name="separator">The separator.</param>
        /// <param name="dateFormat">The date format.</param>
        /// <param name="truncateTime">The truncate time.</param>
        /// <returns></returns>
        public static List<DateTimeOffset> ToListDateTimeOffset(this string s, char separator, string dateFormat, bool? truncateTime = false)
        {
            var list = new List<DateTimeOffset>();

            var splitted = s?.Split(separator);
            if (splitted == null || splitted.Length <= 0)
            {
                return list;
            }

            foreach (var split in splitted)
            {
                if (DateTime.TryParseExact(split, dateFormat, null, DateTimeStyles.None, out DateTime dateTime))
                {
                    list.Add(truncateTime != true ? dateTime.ToUtcTimeZone() : new DateTimeOffset(dateTime.ToUtcTimeZone().Date, TimeSpan.Zero));
                }
            }

            return list;
        }

        /// <summary>Converts to timespan.</summary>
        /// <param name="s">The string.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string s)
        {
            TimeSpan.TryParse(s, null, out var timeIntervalBetweenTurn);
            return timeIntervalBetweenTurn;
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

        #region Video

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
                   && (s.ToLowerInvariant().Contains("youtube.com")
                      || s.ToLowerInvariant().Contains("youtu.be"));
        }

        public static bool IsFacebookVideo(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && s.ToLowerInvariant().Contains("facebook.com")
                   && s.ToLowerInvariant().Contains("videos");
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
                return $"<iframe src='https://www.youtube.com/embed/{id}' class='embed-responsive-item' frameborder='0' allowFullScreen='true'></iframe>";
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
                return $"<iframe src='https://player.vimeo.com/video/{id}' class='embed-responsive-item' frameborder='0' allow='autoplay; fullscreen' allowFullScreen='true'></iframe>";
            }

            if (IsFacebookVideo(s))
            {
                var rgx = new Regex(@"facebook\.com\/([a-zA-Z0-9-_]+)\/(videos)\/([0-9]*)");
                var match = rgx.Match(s);

                if (!match.Success)
                {
                    return s;
                }

                //var id = match.Groups[1].Value;
                return $"<iframe src='https://www.facebook.com/plugins/video.php?href=https://www.{match.Value}&show_text=0' class='embed-responsive-item' scrolling='no' frameborder='0' allowTransparency='true' allowFullScreen='true'></iframe>";
            }

            //https://www.facebook.com/GilbertoCariocaMusic/videos/519439721771582/
            //

            return s;

            /*
             Use html like this:
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-8 col-md-8 col-lg-6">
                        <div class="embed-responsive embed-responsive-16by9 d-flex">
                            @Html.Raw(teaserLinkDto.ProjectTeaserLink.Value.ConvertVideoToEmbed())
                        </div>
                    </div>                                        
                </div>
             */
        }

        #endregion

        #region Music

        /// <summary>Determines whether [is spotify music].</summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is spotify music] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsSpotifyMusic(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && s.ToLowerInvariant().Contains("spotify.com");
        }

        /// <summary>Converts the music to embed.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ConvertMusicToEmbed(this string s)
        {
            if (IsSpotifyMusic(s))
            {
                var rgx = new Regex(@"open\.spotify\.com\/(track|album|artist)\/([a-zA-Z0-9-_]+)");
                var match = rgx.Match(s);

                if (!match.Success)
                {
                    return s;
                }

                var type = match.Groups[1].Value;
                var id = match.Groups[2].Value;
                return $"<iframe src='https://open.spotify.com/embed/{type}/{id}' class='embed-responsive-item' frameborder='0' allowTransparency='true' allow='encrypted-media'></iframe>";
            }

            return s;

            /*
             Use html like this:
                <div class="row mt-3 justify-content-center">
                    <div class="col-sm-8 col-md-8 col-lg-6">
                        <div class="embed-responsive embed-responsive-16by9 d-flex">
                            @Html.Raw(teaserLinkDto.ProjectTeaserLink.Value.ConvertVideoToEmbed())
                        </div>
                    </div>                                        
                </div>
             */
        }

        #endregion

        #region Pdf

        /// <summary>Determines whether this instance is PDF.</summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if the specified s is PDF; otherwise, <c>false</c>.</returns>
        public static bool IsPdf(this string s)
        {
            return !string.IsNullOrEmpty(s)
                   && (s.ToLowerInvariant().Contains(".pdf"));
        }

        /// <summary>Converts the PDF to embed.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string ConvertPdfToEmbed(this string s)
        {
            if (IsPdf(s))
            {
                return $"<embed src='https://drive.google.com/viewerng/viewer?embedded=true&url={s}' type='application/pdf'>";
            }

            return s;
        }

        #endregion

        #region File

        /// <summary>
        /// Gets the type of the base64 MIME.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetBase64MimeType(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return "application/octet-stream";

            var data = value.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                case "/9J/4":
                    return FileMimeType.Png;

                case "AAAAF":
                    return FileMimeType.Mp4;
                case "JVBER":
                    return FileMimeType.Pdf;

                default:
                    return "unknown";
            }
        }

        /// <summary>
        /// Gets the base64 file extension.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string GetBase64FileExtension(this string value)
        {
            if (String.IsNullOrEmpty(value))
                return "";

            var data = value.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                case "/9J/4":
                    return FileType.Png;

                case "AAAAF":
                    return FileType.Mp4;
                case "JVBER":
                    return FileType.Pdf;

                default:
                    return "Unknown";
            }
        }

        /// <summary>
        /// Determines whether [is base64 string] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns><c>true</c> if [is base64 string] [the specified s]; otherwise, <c>false</c>.</returns>
        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        #endregion

        /// <summary>Gets the URL with protocol.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetUrlWithProtocol(this string s)
        {
            if (!s.Contains("http"))
            {
                s = "http://" + s;
            }

            return s;
        }

        /// <summary>Gets the facebook URL.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetFacebookUrl(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (s.Contains("facebook.com"))
            {
                return s.GetUrlWithProtocol();
            }

            return $"https://www.facebook.com/{s}";
        }

        /// <summary>Gets the linkedin URL.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetLinkedinUrl(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (s.Contains("linkedin.com"))
            {
                return s.GetUrlWithProtocol();
            }

            return $"https://www.linkedin.com/in/{s}";
        }

        /// <summary>Gets the twitter URL.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetTwitterUrl(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (s.Contains("twitter.com"))
            {
                return s.GetUrlWithProtocol();
            }

            return $"https://twitter.com/{s}";
        }

        /// <summary>Gets the instagram URL.</summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetInstagramUrl(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            if (s.Contains("instagram.com"))
            {
                return s.GetUrlWithProtocol();
            }

            return $"https://www.instagram.com/{s}";
        }

        /// <summary>
        /// Gets the dash if null or empty.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static string GetDashIfNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) ? "-" : s;
        }

        /// <summary>
        /// Determines whether this instance has value.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns>
        ///   <c>true</c> if the specified array has value; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasValue(this string[] array)
        {
            return array?.Any(ctn => !string.IsNullOrEmpty(ctn)) == true;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this string[] array, string separator)
        {
            if (string.IsNullOrEmpty(separator))
                separator = ",";

            return string.Join(separator, array);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this List<string> list, string separator)
        {
            if (string.IsNullOrEmpty(separator))
                separator = ",";

            return string.Join(separator, list);
        }
    }
}
