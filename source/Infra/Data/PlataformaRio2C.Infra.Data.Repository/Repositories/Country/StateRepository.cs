// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="StateRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
    #region State IQueryable Extensions

    /// <summary>
    /// StateIQueryableExtensions
    /// </summary>
    internal static class StateIQueryableExtensions
    {
        /// <summary>Finds the by country uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="countryUid">The country uid.</param>
        /// <returns></returns>
        internal static IQueryable<State> FindByCountryUid(this IQueryable<State> query, Guid countryUid)
        {
            query = query.Where(s => s.Country.Uid == countryUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<State> IsNotDeleted(this IQueryable<State> query)
        {
            query = query.Where(s => !s.IsDeleted);

            return query;
        }
    }

    #endregion

    #region StateBaseDto IQueryable Extensions

    /// <summary>
    /// StateDtoIQueryableExtensions
    /// </summary>
    internal static class StateDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<StateBaseDto>> ToListPagedAsync(this IQueryable<StateBaseDto> query, int page, int pageSize)
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

    /// <summary></summary>
    public class StateRepository : Repository<PlataformaRio2CContext, State>, IStateRepository
    {
        /// <summary>Initializes a new instance of the <see cref="StateRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public StateRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<State> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                  .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all base dtos by country uid asynchronous.</summary>
        /// <param name="countryUid">The country uid.</param>
        /// <returns></returns>
        public async Task<List<StateBaseDto>> FindAllBaseDtosByCountryUidAsync(Guid countryUid)
        {
            var query = this.GetBaseQuery()
                                .FindByCountryUid(countryUid);

            return await query
                            .OrderBy(s => s.Name)
                            .Select(s => new StateBaseDto
                            {
                                Id = s.Id,
                                Uid = s.Uid,
                                Name = s.Name,
                                Code = s.Code,
                                CreateDate = s.CreateDate,
                                CreateUserId = s.CreateUserId,
                                UpdateDate = s.UpdateDate,
                                UpdateUserId = s.UpdateUserId
                            }).ToListAsync();
        }
    }
}