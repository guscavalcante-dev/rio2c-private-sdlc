// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="CityRepository.cs" company="Softo">
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
    #region City IQueryable Extensions

    /// <summary>
    /// CityIQueryableExtensions
    /// </summary>
    internal static class CityIQueryableExtensions
    {
        /// <summary>Finds the by state uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="stateUid">The state uid.</param>
        /// <returns></returns>
        internal static IQueryable<City> FindByStateUid(this IQueryable<City> query, Guid stateUid)
        {
            query = query.Where(c => c.State.Uid == stateUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<City> IsNotDeleted(this IQueryable<City> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion

    #region CityBaseDto IQueryable Extensions

    /// <summary>
    /// CityDtoIQueryableExtensions
    /// </summary>
    internal static class CityDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<CityBaseDto>> ToListPagedAsync(this IQueryable<CityBaseDto> query, int page, int pageSize)
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

    /// <summary>CityRepository</summary>
    public class CityRepository : Repository<PlataformaRio2CContext, City>, ICityRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CityRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CityRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<City> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all base dtos by state uid asynchronous.</summary>
        /// <param name="stateUid">The state uid.</param>
        /// <returns></returns>
        public async Task<List<CityBaseDto>> FindAllBaseDtosByStateUidAsync(Guid stateUid)
        {
            var query = this.GetBaseQuery()
                                .FindByStateUid(stateUid);

            return await query
                .OrderBy(s => s.Name)
                .Select(s => new CityBaseDto
                {
                    Id = s.Id,
                    Uid = s.Uid,
                    Name = s.Name,
                    CreateDate = s.CreateDate,
                    CreateUserId = s.CreateUserId,
                    UpdateDate = s.UpdateDate,
                    UpdateUserId = s.UpdateUserId
                }).ToListAsync();
        }
    }
}