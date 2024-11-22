// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Renan Valentim
// Created          : 11-11-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-11-2024
// ***********************************************************************
// <copyright file="DoubleExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Globalization;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>DoubleExtensions</summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// Converts to stringinvariantculture.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public static string ToStringInvariantCulture(this double number)
        {
            return number.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}