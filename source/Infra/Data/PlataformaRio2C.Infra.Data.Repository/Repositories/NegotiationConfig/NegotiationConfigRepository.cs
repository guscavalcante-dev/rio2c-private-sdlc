// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-04-2020
// ***********************************************************************
// <copyright file="NegotiationConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region NegotiationConfig IQueryable Extensions

    /// <summary>
    /// NegotiationConfigIQueryableExtensions
    /// </summary>
    internal static class NegotiationConfigIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> FindByUid(this IQueryable<NegotiationConfig> query, Guid negotiationConfigUid)
        {
            query = query.Where(nc => nc.Uid == negotiationConfigUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> FindByEditionId(this IQueryable<NegotiationConfig> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(nc => (showAllEditions || nc.EditionId == editionId));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> IsNotDeleted(this IQueryable<NegotiationConfig> query)
        {
            query = query.Where(nc => !nc.IsDeleted);

            return query;
        }
    }

    #endregion

    #region NegotiationConfigJsonDto IQueryable Extensions

    /// <summary>NegotiationConfigJsonDtoIQueryableExtensions</summary>
    internal static class NegotiationConfigJsonDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<NegotiationConfigJsonDto>> ToListPagedAsync(this IQueryable<NegotiationConfigJsonDto> query, int page, int pageSize)
        {
            // Page the list
            page++;

            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion 

    /// <summary>NegotiationConfigRepository</summary>
    public class NegotiationConfigRepository : Repository<PlataformaRio2CContext, NegotiationConfig>, INegotiationConfigRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationConfigRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public NegotiationConfigRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<NegotiationConfig> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all json dtos paged asynchronous.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<NegotiationConfigJsonDto>> FindAllJsonDtosPagedAsync(
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns,
            string keywords,
            Guid? musicGenreUid,
            Guid? evaluationStatusUid,
            string languageCode,
            int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .DynamicOrder<NegotiationConfig>(
                                    sortColumns,
                                    null,
                                    new List<string> { "StartDate", "EndDate", "CreateDate", "UpdateDate" }, "StartDate")
                                .Select(nc => new NegotiationConfigJsonDto
                                {
                                    NegotiationConfigId = nc.Id,
                                    NegotiationConfigUid = nc.Uid,
                                    StartDate = nc.StartDate,
                                    EndDate = nc.EndDate,
                                    CreateDate = nc.CreateDate,
                                    UpdateDate = nc.UpdateDate
                                });

            return await query
                           .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts the asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountAsync(int editionId, bool showAllEditions = false)

        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions);

            return await query.CountAsync();
        }

        //public override IQueryable<NegotiationConfig> GetAll(bool @readonly = false)
        //{
        //    var consult = this.dbSet
        //                        .Include(i => i.Rooms)
        //                        .Include(i => i.Rooms.Select(e => e.Room));
        //                        //.Include(i => i.Rooms.Select(e => e.Room.Names))
        //                        //.Include(i => i.Rooms.Select(e => e.Room.Names.Select(r => r.Language)));

        //    return @readonly
        //      ? consult.AsNoTracking()
        //      : consult;

        //}


        //public override void Delete(NegotiationConfig entity)
        //{
        //    if (entity.Rooms != null && entity.Rooms.Any())
        //    {
        //        foreach (var item in entity.Rooms.ToList())
        //        {
        //            _context.Entry(item).State = EntityState.Deleted;
        //        }

        //        entity.Rooms.Clear();
        //    }

        //    base.Delete(entity);
        //}

        //public override void DeleteAll(IEnumerable<NegotiationConfig> entities)
        //{
        //    foreach (var item in entities)
        //    {
        //        Delete(item);
        //    }
        //}
    }
}