// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="EditionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Edition IQueryable Extensions

    /// <summary>
    /// EditionIQueryableExtensions
    /// </summary>
    internal static class EditionIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> FindByUid(this IQueryable<Edition> query, Guid editionUid)
        {
            query = query.Where(e => e.Uid == editionUid);

            return query;
        }

        /// <summary>
        /// Finds the by URL code.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="urlCode">The URL code.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> FindByUrlCode(this IQueryable<Edition> query, int urlCode)
        {
            query = query.Where(e => e.UrlCode == urlCode);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionUids">The edition event uids.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> FindByUids(this IQueryable<Edition> query, List<Guid> editionUids)
        {
            if (editionUids?.Any() == true)
            {
                query = query.Where(c => editionUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Determines whether the specified show inactive is active.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        internal static IQueryable<Edition> IsActive(this IQueryable<Edition> query, bool showInactive)
        {
            if (!showInactive)
            {
                query = query.Where(e => e.IsActive);
            }

            return query;
        }

        /// <summary>Determines whether this instance is current.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> IsCurrent(this IQueryable<Edition> query)
        {
            query = query.Where(e => e.IsCurrent);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> IsNotDeleted(this IQueryable<Edition> query)
        {
            query = query.Where(e => !e.IsDeleted);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> FindByKeywords(this IQueryable<Edition> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Edition>(false);
                var innerEditionNameWhere = PredicateBuilder.New<Edition>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerEditionNameWhere = innerEditionNameWhere.And(e => e.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerEditionNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }
    }

    #endregion

    #region EditionJsonDto IQueryable Extensions

    /// <summary>
    /// EditionJsonDtoIQueryableExtensions
    /// </summary>
    internal static class EditionJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<EditionJsonDto>> ToListPagedAsync(this IQueryable<EditionJsonDto> query, int page, int pageSize)
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

    /// <summary>EditionRepository</summary>
    public class EditionRepository : Repository<PlataformaRio2CContext, Edition>, IEditionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="EditionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public EditionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Edition> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public async Task<Edition> FindByUidAsync(Guid editionUid, bool showInactive)
        {
            var query = this.GetBaseQuery()
                                .IsActive(showInactive)
                                .FindByUid(editionUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the by is current asynchronous.</summary>
        /// <returns></returns>
        public async Task<Edition> FindByIsCurrentAsync()
        {
            var query = this.GetBaseQuery()
                                .IsCurrent();

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by is active.</summary>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public List<Edition> FindAllByIsActive(bool showInactive)
        {
            var query = this.GetBaseQuery()
                                .IsActive(showInactive);

            return query.ToList();
        }

        /// <summary>
        /// Finds the dto asynchronous.
        /// </summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<EditionDto> FindDtoAsync(Guid editionUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(editionUid);

            return await query
                            .Select(e => new EditionDto() 
                            { 
                                Edition = e
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the events widget dto asynchronous.
        /// </summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<EditionDto> FindEventsWidgetDtoAsync(Guid editionUid)
        {
            var query = this.GetBaseQuery()
                               .FindByUid(editionUid);

            return await query
                            .Select(e => new EditionDto()
                            {
                                Edition = e,
                                EditionEventDtos = e.EditionEvents.Select(ee => new EditionEventDto()
                                {
                                    EditionEvent = ee
                                }).ToList()
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="editionUids">The edition uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<EditionJsonDto>> FindAllByDataTable(
            int page, 
            int pageSize, 
            string keywords, 
            List<Tuple<string, string>> sortColumns, 
            List<Guid> editionUids, 
            int editionId, 
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByUids(editionUids);

            return await query
                            .DynamicOrder<Edition>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { nameof(Edition.Name), nameof(Edition.IsCurrent), nameof(Edition.IsActive), nameof(Edition.StartDate), nameof(Edition.EndDate), nameof(Edition.CreateDate), nameof(Edition.UpdateDate) },
                                nameof(Edition.StartDate))
                            .Select(c => new EditionJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                IsCurrent = c.IsCurrent,
                                IsActive = c.IsActive,
                                StartDate = c.StartDate,
                                EndDate = c.EndDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Counts all by data table.
        /// </summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, Guid editionUid)
        {
            var query = this.GetBaseQuery();

            return await query
                            .CountAsync();
        }

        /// <summary>
        /// Finds the by URL code.
        /// </summary>
        /// <param name="urlCode">The URL code.</param>
        /// <returns></returns>
        public Edition FindByUrlCode(int urlCode)
        {
            var query = this.GetBaseQuery()
                            .FindByUrlCode(urlCode);

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds all by is current.
        /// </summary>
        /// <returns></returns>
        public List<Edition> FindAllByIsCurrent()
        {
            var query = this.GetBaseQuery()
                            .IsCurrent();

            return query.ToList();
        }

        #region Old Methods

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Edition> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        #endregion
    }
}