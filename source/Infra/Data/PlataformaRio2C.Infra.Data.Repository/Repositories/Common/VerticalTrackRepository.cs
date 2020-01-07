// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="VerticalTrackRepository.cs" company="Softo">
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
    #region Vertical Track IQueryable Extensions

    /// <summary>
    /// VerticalTrackIQueryableExtensions
    /// </summary>
    internal static class VerticalTrackIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> FindByUid(this IQueryable<VerticalTrack> query, Guid verticalTrackUid)
        {
            query = query.Where(vt => vt.Uid == verticalTrackUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="verticalTrackUids">The vertical track uids.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> FindByUids(this IQueryable<VerticalTrack> query, List<Guid> verticalTrackUids)
        {
            if (verticalTrackUids?.Any() == true)
            {
                query = query.Where(vt => verticalTrackUids.Contains(vt.Uid));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> FindByKeywords(this IQueryable<VerticalTrack> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<VerticalTrack>(false);
                var innerVerticalTrackNameWhere = PredicateBuilder.New<VerticalTrack>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerVerticalTrackNameWhere = innerVerticalTrackNameWhere.And(vt => vt.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerVerticalTrackNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> IsNotDeleted(this IQueryable<VerticalTrack> query)
        {
            query = query.Where(vt => !vt.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> Order(this IQueryable<VerticalTrack> query)
        {
            query = query.OrderBy(vt => vt.DisplayOrder);

            return query;
        }
    }

    #endregion

    #region VerticalTrackJsonDto IQueryable Extensions

    /// <summary>
    /// VerticalTrackJsonDtoIQueryableExtensions
    /// </summary>
    internal static class VerticalTrackJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<VerticalTrackJsonDto>> ToListPagedAsync(this IQueryable<VerticalTrackJsonDto> query, int page, int pageSize)
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

    /// <summary>VerticalTrackRepository</summary>
    public class VerticalTrackRepository : Repository<PlataformaRio2CContext, VerticalTrack>, IVerticalTrackRepository
    {
        /// <summary>Initializes a new instance of the <see cref="VerticalTrackRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VerticalTrackRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<VerticalTrack> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<VerticalTrackDto> FindDtoAsync(Guid verticalTrackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(verticalTrackUid);
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new VerticalTrackDto
                            {
                                VerticalTrack = vt
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<VerticalTrackDto> FindConferenceWidgetDtoAsync(Guid verticalTrackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(verticalTrackUid);
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new VerticalTrackDto
                            {
                                VerticalTrack = vt,
                                ConferenceDtos = vt.ConferenceVerticalTracks.Where(cvt => !cvt.IsDeleted && !cvt.Conference.IsDeleted).Select(cvt => new ConferenceDto
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
        /// <returns></returns>
        public async Task<List<VerticalTrack>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="verticalTrackUids">The vertical track uids.</param>
        /// <returns></returns>
        public async Task<List<VerticalTrack>> FindAllByUidsAsync(List<Guid> verticalTrackUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(verticalTrackUids);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="verticalTrackUids">The vertical track uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<VerticalTrackJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> verticalTrackUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                //.FindByEditionId(false, editionId)
                                .FindByUids(verticalTrackUids);

            return await query
                            .DynamicOrder<VerticalTrack>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "CreateDate")
                            .Select(r => new VerticalTrackJsonDto
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
            var query = this.GetBaseQuery();
                                //.FindByEditionId(showAllEditions, editionId);

            return await query
                            .CountAsync();
        }
    }
}