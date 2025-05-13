// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="EditionEventRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Edition Event IQueryable Extensions

    /// <summary>
    /// EditionEventIQueryableExtensions
    /// </summary>
    internal static class EditionEventIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByUid(this IQueryable<EditionEvent> query, Guid editionEventUid)
        {
            query = query.Where(ee => ee.Uid == editionEventUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionEventUids">The edition event uids.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByUids(this IQueryable<EditionEvent> query, List<Guid> editionEventUids)
        {
            if (editionEventUids?.Any() == true)
            {
                query = query.Where(c => editionEventUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByEditionId(this IQueryable<EditionEvent> query, bool showAllEditions, int editionId)
        {
            if (!showAllEditions)
            {
                query = query.Where(ee => ee.EditionId == editionId);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> IsNotDeleted(this IQueryable<EditionEvent> query)
        {
            query = query.Where(ee => !ee.IsDeleted);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByKeywords(this IQueryable<EditionEvent> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<EditionEvent>(false);
                var innerEditionEventNameWhere = PredicateBuilder.New<EditionEvent>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerEditionEventNameWhere = innerEditionEventNameWhere.And(ee => ee.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerEditionEventNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }
    }

    #endregion

    #region EditionEventJsonDto IQueryable Extensions

    /// <summary>
    /// EditionEventJsonDtoIQueryableExtensions
    /// </summary>
    internal static class EditionEventJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<EditionEventJsonDto>> ToListPagedAsync(this IQueryable<EditionEventJsonDto> query, int page, int pageSize)
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

    /// <summary>EditionEventRepository</summary>
    public class EditionEventRepository : Repository<PlataformaRio2CContext, EditionEvent>, IEditionEventRepository
    {
        /// <summary>Initializes a new instance of the <see cref="EditionEventRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public EditionEventRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<EditionEvent> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        public async Task<EditionEvent> FindByUidAsync(Guid editionEventUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(editionEventUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<EditionEventDto> FindDtoAsync(Guid editionEventUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(editionEventUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(ee => new EditionEventDto
                            {
                                EditionEvent = ee
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<EditionEventDto> FindConferenceWidgetDtoAsync(Guid editionEventUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(editionEventUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(ee => new EditionEventDto
                            {
                                EditionEvent = ee,
                                ConferenceDtos = ee.Conferences.Where(c => !c.IsDeleted).Select(c => new ConferenceDto
                                {
                                    Conference = c,
                                    ConferenceTitleDtos = c.ConferenceTitles.Where(ct => !ct.IsDeleted).Select(ct => new ConferenceTitleDto
                                    {
                                        ConferenceTitle = ct,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = ct.Language.Id,
                                            Uid = ct.Language.Uid,
                                            Name = ct.Language.Name,
                                            Code = ct.Language.Code
                                        }
                                    })
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by edition identifier asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<EditionEvent>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId);

            return await query
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="editionEventUids">The edition event uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<EditionEventJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> editionEventUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByEditionId(false, editionId)
                                .FindByUids(editionEventUids);

            return await query
                            .DynamicOrder<EditionEvent>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "Name", "StartDate", "EndDate", "CreateDate", "UpdateDate" },
                                "StartDate")
                            .Select(c => new EditionEventJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                Name = c.Name,
                                StartDate = c.StartDate,
                                EndDate = c.EndDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(showAllEditions, editionId);

            return await query
                            .CountAsync();
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="conferenceUid">The edition event uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<EditionEventDto> FindByConferenceUidAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                .Where(
                    ev => ev.Conferences.Any(
                        c => c.Uid == conferenceUid
                    )
                )
                .FindByEditionId(false, editionId);
            return await query
                .Select(ee => new EditionEventDto
                {
                    EditionEvent = ee
                })
                .FirstOrDefaultAsync();
        }
    }
}