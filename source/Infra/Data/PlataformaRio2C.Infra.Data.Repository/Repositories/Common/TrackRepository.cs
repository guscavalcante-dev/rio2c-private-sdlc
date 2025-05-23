﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="TrackRepository.cs" company="Softo">
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
    #region Track IQueryable Extensions

    /// <summary>
    /// TrackIQueryableExtensions
    /// </summary>
    internal static class TrackIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="trackUid">The track uid.</param>
        /// <returns></returns>
        internal static IQueryable<Track> FindByUid(this IQueryable<Track> query, Guid trackUid)
        {
            query = query.Where(t => t.Uid == trackUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="trackUids">The track uids.</param>
        /// <returns></returns>
        internal static IQueryable<Track> FindByUids(this IQueryable<Track> query, List<Guid> trackUids)
        {
            if (trackUids?.Any() == true)
            {
                query = query.Where(t => trackUids.Contains(t.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Track> FindByEditionId(this IQueryable<Track> query, bool showAllEditions, int editionId)
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
        internal static IQueryable<Track> FindByKeywords(this IQueryable<Track> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Track>(false);
                var innerTrackNameWhere = PredicateBuilder.New<Track>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerTrackNameWhere = innerTrackNameWhere.And(t => t.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerTrackNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Track> IsNotDeleted(this IQueryable<Track> query)
        {
            query = query.Where(t => !t.IsDeleted);

            return query;
        }

        ///// <summary>Orders the specified query.</summary>
        ///// <param name="query">The query.</param>
        ///// <returns></returns>
        //internal static IQueryable<Track> Order(this IQueryable<Track> query)
        //{
        //    query = query.OrderBy(t => t.DisplayOrder);

        //    return query;
        //}
    }

    #endregion

    #region TrackJsonDto IQueryable Extensions

    /// <summary>
    /// TrackJsonDtoIQueryableExtensions
    /// </summary>
    internal static class TrackJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<TrackJsonDto>> ToListPagedAsync(this IQueryable<TrackJsonDto> query, int page, int pageSize)
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

    /// <summary>TrackRepository</summary>
    public class TrackRepository : Repository<PlataformaRio2CContext, Track>, ITrackRepository
    {
        /// <summary>Initializes a new instance of the <see cref="TrackRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public TrackRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Track> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="trackUid">The track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<TrackDto> FindDtoAsync(Guid trackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(trackUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new TrackDto
                            {
                                Track = vt
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="trackUid">The track uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<TrackDto> FindConferenceWidgetDtoAsync(Guid trackUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(trackUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(vt => new TrackDto
                            {
                                Track = vt,
                                ConferenceDtos = vt.ConferenceTracks.Where(cvt => !cvt.IsDeleted && !cvt.Conference.IsDeleted).Select(cvt => new ConferenceDto
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
        public async Task<List<Track>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="trackUids">The track uids.</param>
        /// <returns></returns>
        public async Task<List<Track>> FindAllByUidsAsync(List<Guid> trackUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(trackUids);

            return await query
                            //.Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="trackUids">The track uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<TrackJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> trackUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(false, editionId)
                                .FindByUids(trackUids);

            return await query
                            .DynamicOrder<Track>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "CreateDate")
                            .Select(t => new TrackJsonDto
                            {
                                Id = t.Id,
                                Uid = t.Uid,
                                Name = t.Name,
                                Color = t.Color,
                                CreateDate = t.CreateDate,
                                UpdateDate = t.UpdateDate
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