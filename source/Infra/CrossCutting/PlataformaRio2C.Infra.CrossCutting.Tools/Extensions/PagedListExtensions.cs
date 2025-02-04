// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.Tools
// Author           : Rafael Dantas Ruiz
// Created          : 11-11-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-11-2019
// ***********************************************************************
// <copyright file="PagedListExtensions.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Extensions
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static async Task<IPagedList<T>> ToGenericPagedListAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            try
            {
                page++;
                // Page the list
                var pagedList = await query.ToPagedListAsync(page, pageSize);
                if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                    pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

                return pagedList;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}