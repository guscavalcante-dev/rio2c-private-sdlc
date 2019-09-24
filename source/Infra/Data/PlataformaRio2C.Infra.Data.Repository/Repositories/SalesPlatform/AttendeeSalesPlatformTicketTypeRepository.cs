// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 09-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-24-2019
// ***********************************************************************
// <copyright file="AttendeeSalesPlatformTicketTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Attendee Sales Platform Ticket Type IQueryable Extensions

    /// <summary>
    /// AttendeeSalesPlatformTicketTypeIQueryableExtensions
    /// </summary>
    internal static class AttendeeSalesPlatformTicketTypeIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeSalesPlatformTicketType> IsNotDeleted(this IQueryable<AttendeeSalesPlatformTicketType> query)
        {
            query = query.Where(asp => !asp.IsDeleted);

            return query;
        }
    }

    #endregion

    //#region SalesPlatformBaseDto IQueryable Extensions

    ///// <summary>
    ///// SalesPlatformBaseDtoIQueryableExtensions
    ///// </summary>
    //internal static class SalesPlatformBaseDtoIQueryableExtensions
    //{
    //    /// <summary>
    //    /// To the list paged.
    //    /// </summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<SalesPlatformDto>> ToListPagedAsync(this IQueryable<SalesPlatformDto> query, int page, int pageSize)
    //    {
    //        page++;

    //        // Page the list
    //        var pagedList = await query.ToPagedListAsync(page, pageSize);
    //        if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
    //            pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

    //        return pagedList;
    //    }
    //}

    //#endregion

    /// <summary>AttendeeSalesPlatformTicketTypeRepository</summary>
    public class AttendeeSalesPlatformTicketTypeRepository : Repository<PlataformaRio2CContext, AttendeeSalesPlatformTicketType>, IAttendeeSalesPlatformTicketTypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeSalesPlatformTicketTypeRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeSalesPlatformTicketTypeRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeSalesPlatformTicketType> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<AttendeeSalesPlatformTicketType>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .ToListAsync();
        }
    }
}