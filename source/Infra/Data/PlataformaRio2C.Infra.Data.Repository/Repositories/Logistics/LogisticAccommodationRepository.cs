// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Arthur Souza
// Created          : 01-20-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-10-2020
// ***********************************************************************
// <copyright file="LogisticAccommodationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Logistic Acommodation IQueryable Extensions

    /// <summary>
    /// LogisticAccommodationIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> IsNotDeleted(this IQueryable<LogisticAccommodation> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(e => e.Uid == uid);
        }

        /// <summary>Finds the by logistics uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<LogisticAccommodation> FindByLogisticsUid(this IQueryable<LogisticAccommodation> query, Guid uid)
        {
            return query.Where(e => e.Logistic.Uid == uid);
        }
    }

    #endregion

    #region LogisticAccommodationDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class LogisticAccommodationDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<LogisticAccommodationDto>> ToListPagedAsync(this IQueryable<LogisticAccommodationDto> query, int page, int pageSize)
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

    /// <summary>LogisticAccommodationRepository</summary>
    public class LogisticAccommodationRepository : Repository<PlataformaRio2CContext, LogisticAccommodation>, ILogisticAccommodationRepository
    {
        public LogisticAccommodationRepository(PlataformaRio2CContext context)
            : base(context)
        {
            _context = context;
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<LogisticAccommodation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Finds all dtos paged.</summary>
        /// <param name="logisticsUid">The logistics uid.</param>
        /// <returns></returns>
        public Task<List<LogisticAccommodationDto>> FindAllDtosPaged(Guid logisticsUid)
        {
            return this.GetBaseQuery()
                .FindByLogisticsUid(logisticsUid)
                .Select(e => new LogisticAccommodationDto()
                {
                    Id = e.Id,
                    Uid = e.Uid,
                    AdditionalInfo = e.AdditionalInfo,
                    AttendeePlace = e.AttendeePlace.Place.Name,
                    CheckOutDate = e.CheckOutDate,
                    CheckInDate = e.CheckInDate,
                    CreateDate = e.CreateDate,
                    UpdateDate = e.UpdateDate
                })
                .OrderBy(e => e.CreateDate)
                .ToListAsync();
        }
    }
}