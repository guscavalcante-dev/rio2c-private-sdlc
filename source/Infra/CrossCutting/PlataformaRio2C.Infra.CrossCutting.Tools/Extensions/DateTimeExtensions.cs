// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 12-13-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-13-2019
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
    }
}
