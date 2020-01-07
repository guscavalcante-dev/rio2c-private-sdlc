// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="PresentationFormatRepository.cs" company="Softo">
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
    #region Presentation Format IQueryable Extensions

    /// <summary>
    /// PresentationFormatIQueryableExtensions
    /// </summary>
    internal static class PresentationFormatIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="presentationFormatUid">The presentation format uid.</param>
        /// <returns></returns>
        internal static IQueryable<PresentationFormat> FindByUid(this IQueryable<PresentationFormat> query, Guid presentationFormatUid)
        {
            query = query.Where(pf => pf.Uid == presentationFormatUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="presentationFormatUids">The presentation format uids.</param>
        /// <returns></returns>
        internal static IQueryable<PresentationFormat> FindByUids(this IQueryable<PresentationFormat> query, List<Guid> presentationFormatUids)
        {
            if (presentationFormatUids?.Any() == true)
            {
                query = query.Where(pf => presentationFormatUids.Contains(pf.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<PresentationFormat> FindByEditionId(this IQueryable<PresentationFormat> query, bool showAllEditions, int editionId)
        {
            if (!showAllEditions)
            {
                query = query.Where(pf => pf.EditionId == editionId);
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<PresentationFormat> FindByKeywords(this IQueryable<PresentationFormat> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<PresentationFormat>(false);
                var innerPresentationFormatNameWhere = PredicateBuilder.New<PresentationFormat>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerPresentationFormatNameWhere = innerPresentationFormatNameWhere.And(pf => pf.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerPresentationFormatNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<PresentationFormat> IsNotDeleted(this IQueryable<PresentationFormat> query)
        {
            query = query.Where(pf => !pf.IsDeleted);

            return query;
        }

        ///// <summary>Orders the specified query.</summary>
        ///// <param name="query">The query.</param>
        ///// <returns></returns>
        //internal static IQueryable<PresentationFormat> Order(this IQueryable<PresentationFormat> query)
        //{
        //    query = query.OrderBy(pf => pf.DisplayOrder);

        //    return query;
        //}
    }

    #endregion

    #region PresentationFormatJsonDto IQueryable Extensions

    /// <summary>
    /// PresentationFormatJsonDtoIQueryableExtensions
    /// </summary>
    internal static class PresentationFormatJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<PresentationFormatJsonDto>> ToListPagedAsync(this IQueryable<PresentationFormatJsonDto> query, int page, int pageSize)
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

    /// <summary>PresentationFormatRepository</summary>
    public class PresentationFormatRepository : Repository<PlataformaRio2CContext, PresentationFormat>, IPresentationFormatRepository
    {
        /// <summary>Initializes a new instance of the <see cref="PresentationFormatRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public PresentationFormatRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<PresentationFormat> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="presentationFormatUid">The presentation format uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<PresentationFormatDto> FindDtoAsync(Guid presentationFormatUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(presentationFormatUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new PresentationFormatDto
                            {
                                PresentationFormat = vt
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="presentationFormatUid">The presentation format uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<PresentationFormatDto> FindConferenceWidgetDtoAsync(Guid presentationFormatUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(presentationFormatUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new PresentationFormatDto
                            {
                                PresentationFormat = vt,
                                ConferenceDtos = vt.ConferencePresentationFormats.Where(cvt => !cvt.IsDeleted && !cvt.Conference.IsDeleted).Select(cvt => new ConferenceDto
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

        /// <summary>Finds all asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<PresentationFormat>> FindAllAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="presentationFormatUids">The presentation format uids.</param>
        /// <returns></returns>
        public async Task<List<PresentationFormat>> FindAllByUidsAsync(List<Guid> presentationFormatUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(presentationFormatUids);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="presentationFormatUids">The presentation format uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<PresentationFormatJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> presentationFormatUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(false, editionId)
                                .FindByUids(presentationFormatUids);

            return await query
                            .DynamicOrder<PresentationFormat>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "CreateDate")
                            .Select(r => new PresentationFormatJsonDto
                            {
                                Id = r.Id,
                                Uid = r.Uid,
                                Name = r.Name
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