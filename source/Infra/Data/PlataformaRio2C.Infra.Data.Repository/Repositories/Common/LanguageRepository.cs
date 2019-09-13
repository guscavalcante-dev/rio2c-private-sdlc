// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="LanguageRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Language IQueryable Extensions

    /// <summary>
    /// LanguageIQueryableExtensions
    /// </summary>
    internal static class LanguageIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Language> IsNotDeleted(this IQueryable<Language> query)
        {
            query = query.Where(h => !h.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Language> Order(this IQueryable<Language> query)
        {
            query = query.OrderByDescending(l => l.IsDefault).ThenBy(l => l.Name);

            return query;
        }
    }

    #endregion

    #region LanguageDto IQueryable Extensions

    /// <summary>
    /// LanguageIQueryableExtensions
    /// </summary>
    internal static class LanguageDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LanguageDto>> ToListPagedAsync(this IQueryable<LanguageDto> query, int page, int pageSize)
        {
            page++;

            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>LanguageRepository</summary>
    public class LanguageRepository : Repository<PlataformaRio2CContext, Language>, ILanguageRepository
    {
        public LanguageRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Language> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<LanguageDto>> FindAllDtosAsync()
        {
            var query = this.GetBaseQuery()
                                .Order();

            return await query.Select(l => new LanguageDto
            {
                Id = l.Id,
                Uid = l.Uid,
                Name = l.Name,
                Code = l.Code,
                CreateDate = l.CreateDate,
                CreateUserId = l.CreateUserId,
                UpdateDate = l.UpdateDate,
                UpdateUserId = l.UpdateUserId,
                Language = l
            }).ToListAsync();
        }
    }
}