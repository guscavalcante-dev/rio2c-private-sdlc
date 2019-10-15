// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Wilian Almado
// Created          : 10-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-11-2019
// ***********************************************************************
// <copyright file="ApplicationCookieControl.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Web;

namespace PlataformaRio2C.Application.Common
{
    /// <summary>ApplicationCookieControl</summary>
    public static class ApplicationCookieControl
    {
        /// <summary>Sets the cookie.</summary>
        /// <param name="culture">The culture.</param>
        /// <param name="cookie">The cookie.</param>
        /// <param name="cookieName">Name of the cookie.</param>
        /// <returns></returns>
        public static HttpCookie SetCookie(string culture, HttpCookie cookie, string cookieName)
        {
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie(cookieName);
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }

            return cookie;
        }
    }
}
