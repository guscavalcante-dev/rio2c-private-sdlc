// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="PillarRepository.cs" company="Softo">
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
    #region Pillar IQueryable Extensions

    /// <summary>
    /// PillarIQueryableExtensions
    /// </summary>
    internal static class PillarIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="pillarUid">The pillar uid.</param>
        /// <returns></returns>
        internal static IQueryable<Pillar> FindByUid(this IQueryable<Pillar> query, Guid pillarUid)
        {
            query = query.Where(t => t.Uid == pillarUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="pillarUids">The pillar uids.</param>
        /// <returns></returns>
        internal static IQueryable<Pillar> FindByUids(this IQueryable<Pillar> query, List<Guid> pillarUids)
        {
            if (pillarUids?.Any() == true)
            {
                query = query.Where(t => pillarUids.Contains(t.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Pillar> FindByEditionId(this IQueryable<Pillar> query, bool showAllEditions, int editionId)
        {
            if (!showAllEditions)
            {
                query = query.Where(t => t.EditionId == editionId);
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Pillar> FindByKeywords(this IQueryable<Pillar> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Pillar>(false);
                var innerPillarNameWhere = PredicateBuilder.New<Pillar>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerPillarNameWhere = innerPillarNameWhere.And(t => t.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerPillarNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Pillar> IsNotDeleted(this IQueryable<Pillar> query)
        {
            query = query.Where(t => !t.IsDeleted);

            return query;
        }

        ///// <summary>Orders the specified query.</summary>
        ///// <param name="query">The query.</param>
        ///// <returns></returns>
        //internal static IQueryable<Pillar> Order(this IQueryable<Pillar> query)
        //{
        //    query = query.OrderBy(t => t.DisplayOrder);

        //    return query;
        //}
    }

    #endregion

    #region PillarJsonDto IQueryable Extensions

    /// <summary>
    /// PillarJsonDtoIQueryableExtensions
    /// </summary>
    internal static class PillarJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<PillarJsonDto>> ToListPagedAsync(this IQueryable<PillarJsonDto> query, int page, int pageSize)
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

    /// <summary>PillarRepository</summary>
    public class PillarRepository : Repository<PlataformaRio2CContext, Pillar>, IPillarRepository
    {
        /// <summary>Initializes a new instance of the <see cref="PillarRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public PillarRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Pillar> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="pillarUid">The pillar uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<PillarDto> FindDtoAsync(Guid pillarUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(pillarUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new PillarDto
                            {
                                Pillar = vt
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="pillarUid">The pillar uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<PillarDto> FindConferenceWidgetDtoAsync(Guid pillarUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(pillarUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new PillarDto
                            {
                                Pillar = vt,
                                ConferenceDtos = vt.ConferencePillars.Where(cvt => !cvt.IsDeleted && !cvt.Conference.IsDeleted).Select(cvt => new ConferenceDto
                                {
                                    Conference = cvt.Conference,
                                    ConferenceTitleDtos = cvt.Conference.ConferenceTitles.Where(ct => !ct.IsDeleted).Select(ct => new ConferenceTitleDto
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
        public async Task<List<Pillar>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="pillarUids">The pillar uids.</param>
        /// <returns></returns>
        public async Task<List<Pillar>> FindAllByUidsAsync(List<Guid> pillarUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(pillarUids);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="pillarUids">The pillar uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<PillarJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> pillarUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(false, editionId)
                                .FindByUids(pillarUids);

            return await query
                            .DynamicOrder<Pillar>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "CreateDate")
                            .Select(r => new PillarJsonDto
                            {
                                Id = r.Id,
                                Uid = r.Uid,
                                Name = r.Name,
                                Color = r.Color,
                                CreateDate = r.CreateDate,
                                UpdateDate = r.UpdateDate
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
    }
}