// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="HorizontalTrackRepository.cs" company="Softo">
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
    #region Horizontal Track IQueryable Extensions

    /// <summary>
    /// HorizontalTrackIQueryableExtensions
    /// </summary>
    internal static class HorizontalTrackIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> FindByUid(this IQueryable<HorizontalTrack> query, Guid horizontalTrackUid)
        {
            query = query.Where(ht => ht.Uid == horizontalTrackUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="horizontalTrackUids">The horizontal track uids.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> FindByUids(this IQueryable<HorizontalTrack> query, List<Guid> horizontalTrackUids)
        {
            if (horizontalTrackUids?.Any() == true)
            {
                query = query.Where(ht => horizontalTrackUids.Contains(ht.Uid));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> FindByKeywords(this IQueryable<HorizontalTrack> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<HorizontalTrack>(false);
                var innerHorizontalTrackNameWhere = PredicateBuilder.New<HorizontalTrack>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerHorizontalTrackNameWhere = innerHorizontalTrackNameWhere.And(ht => ht.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerHorizontalTrackNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> IsNotDeleted(this IQueryable<HorizontalTrack> query)
        {
            query = query.Where(ht => !ht.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> Order(this IQueryable<HorizontalTrack> query)
        {
            query = query.OrderBy(ht => ht.DisplayOrder);

            return query;
        }
    }

    #endregion

    #region HorizontalTrackJsonDto IQueryable Extensions

    /// <summary>
    /// HorizontalTrackJsonDtoIQueryableExtensions
    /// </summary>
    internal static class HorizontalTrackJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<HorizontalTrackJsonDto>> ToListPagedAsync(this IQueryable<HorizontalTrackJsonDto> query, int page, int pageSize)
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

    /// <summary>HorizontalTrackRepository</summary>
    public class HorizontalTrackRepository : Repository<PlataformaRio2CContext, HorizontalTrack>, IHorizontalTrackRepository
    {
        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public HorizontalTrackRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<HorizontalTrack> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<HorizontalTrackDto> FindDtoAsync(Guid horizontalTrackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                .FindByUid(horizontalTrackUid);
            //.FindByEditionId(false, editionId);

            return await query
                .Select(vt => new HorizontalTrackDto
                {
                    HorizontalTrack = vt
                })
                .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<HorizontalTrackDto> FindConferenceWidgetDtoAsync(Guid horizontalTrackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                .FindByUid(horizontalTrackUid);
            //.FindByEditionId(false, editionId);

            return await query
                .Select(vt => new HorizontalTrackDto
                {
                    HorizontalTrack = vt,
                    ConferenceDtos = vt.ConferenceHorizontalTracks.Where(cvt => !cvt.IsDeleted && !cvt.Conference.IsDeleted).Select(cvt => new ConferenceDto
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
        public async Task<List<HorizontalTrack>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                .Order()
                .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="horizontalTrackUids">The horizontal track uids.</param>
        /// <returns></returns>
        public async Task<List<HorizontalTrack>> FindAllByUidsAsync(List<Guid> horizontalTrackUids)
        {
            var query = this.GetBaseQuery()
                .FindByUids(horizontalTrackUids);

            return await query
                .Order()
                .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="horizontalTrackUids">The horizontal track uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<HorizontalTrackJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> horizontalTrackUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                .FindByKeywords(keywords, languageId)
                //.FindByEditionId(false, editionId)
                .FindByUids(horizontalTrackUids);

            return await query
                .DynamicOrder<HorizontalTrack>(
                    sortColumns,
                    new List<Tuple<string, string>>
                    {
                        //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                        //new Tuple<string, string>("Email", "User.Email"),
                    },
                    new List<string> { "CreateDate", "UpdateDate" },
                    "CreateDate")
                .Select(r => new HorizontalTrackJsonDto
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