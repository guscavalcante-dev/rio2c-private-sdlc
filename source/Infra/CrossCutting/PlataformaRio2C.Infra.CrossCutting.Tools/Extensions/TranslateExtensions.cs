// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 02-12-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-12-2020
// ***********************************************************************
// <copyright file="TranslateExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>TranslateExtensions</summary>
    public static class TranslateExtensions
    {
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

            if (culture?.ToLowerInvariant() == "en-us" && splitName.Length == 2)
            {
                return splitName[1].Trim();
            }

            return splitName[0].Trim();
        }

        /// <summary>Gets the separator translation.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="property">The property.</param>
        /// <param name="culture">The culture.</param>
        /// <param name="separator">The separator.</param>
        /// <returns></returns>
        public static List<T> GetSeparatorTranslation<T>(this List<T> list, Expression<Func<T, string>> property, string culture, char separator)
        {
            if (!(property.Body is MemberExpression))
            {
                throw new ArgumentException("property", "Property isn't a MemberExpression");
            }

            var member = property.Body as MemberExpression;
            var type = typeof(T);

            var pInfo = type.GetProperty(member.Member.Name);

            foreach (var item in list)
            {
                pInfo?.SetValue(item, ((string)pInfo.GetValue(item)).GetSeparatorTranslation(culture, separator));
            }

            return list;
        }
    }
}