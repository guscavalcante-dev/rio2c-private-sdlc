// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 07-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-03-2019
// ***********************************************************************
// <copyright file="ListExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    /// <summary>ListExtensions</summary>
    public static class ListExtensions
    {
        /// <summary>Determines whether the specified o has element.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="o">The o.</param>
        /// <returns>
        ///   <c>true</c> if the specified o has element; otherwise, <c>false</c>.</returns>
        public static bool HasElement<T>(this IList<T> list, T o)
        {
            return list?.Any(e => e.Equals(o)) ?? false;
        }
    }
}
