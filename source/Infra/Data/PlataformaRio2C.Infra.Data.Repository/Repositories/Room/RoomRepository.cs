// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="RoomRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Room IQueryable Extensions

    /// <summary>
    /// RoomIQueryableExtensions
    /// </summary>
    internal static class RoomIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        internal static IQueryable<Room> FindByUid(this IQueryable<Room> query, Guid roomUid)
        {
            query = query.Where(r => r.Uid == roomUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="roomUids">The room uids.</param>
        /// <returns></returns>
        internal static IQueryable<Room> FindByUids(this IQueryable<Room> query, List<Guid> roomUids)
        {
            if (roomUids?.Any() == true)
            {
                query = query.Where(r => roomUids.Contains(r.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Room> FindByEditionId(this IQueryable<Room> query, bool showAllEditions, int editionId)
        {
            if (!showAllEditions)
            {
                query = query.Where(r => r.EditionId == editionId);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Room> IsNotDeleted(this IQueryable<Room> query)
        {
            query = query.Where(r => !r.IsDeleted);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Room> FindByKeywords(this IQueryable<Room> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Room>(false);
                var innerRoomNameWhere = PredicateBuilder.New<Room>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerRoomNameWhere = innerRoomNameWhere.And(r => r.RoomNames.Any(rn => rn.LanguageId == languageId
                                                                                               && !rn.IsDeleted
                                                                                               && rn.Value.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerRoomNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }
    }

    #endregion

    #region RoomJsonDto IQueryable Extensions

    /// <summary>
    /// RoomJsonDtoIQueryableExtensions
    /// </summary>
    internal static class RoomJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<RoomJsonDto>> ToListPagedAsync(this IQueryable<RoomJsonDto> query, int page, int pageSize)
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

    /// <summary>RoomRepository</summary>
    public class RoomRepository : Repository<PlataformaRio2CContext, Room>, IRoomRepository
    {
        public RoomRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Room> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<RoomDto> FindDtoAsync(Guid roomUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(roomUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(r => new RoomDto
                            {
                                Room = r,
                                RoomNameDtos = r.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                {
                                    RoomName = rn,
                                    LanguageDto = new LanguageDto
                                    {
                                        Id = rn.Language.Id,
                                        Uid = rn.Language.Uid,
                                        Code = rn.Language.Code
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<RoomDto> FindConferenceWidgetDtoAsync(Guid roomUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                    .FindByUid(roomUid)
                                    .FindByEditionId(false, editionId);

            return await query
                            .Select(r => new RoomDto
                            {
                                Room = r,
                                ConferenceDtos = r.Conferences.Where(c => !c.IsDeleted).Select(c => new ConferenceDto
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

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="roomUid">The room uid.</param>
        /// <returns></returns>
        public async Task<Room> FindByUidAsync(Guid roomUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(roomUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all dto by edition identifier asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<RoomDto>> FindAllDtoByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(r => new RoomDto
                            {
                                Room = r,
                                RoomNameDtos = r.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                {
                                    RoomName = rn,
                                    LanguageDto = new LanguageDto
                                    {
                                        Id = rn.Language.Id,
                                        Uid = rn.Language.Uid,
                                        Code = rn.Language.Code
                                    }
                                })
                            })
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="roomUids">The room uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<RoomJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> roomUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(false, editionId)
                                .FindByUids(roomUids);

            return await query
                            .DynamicOrder<Room>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "StartDate")
                            .Select(r => new RoomJsonDto
                            {
                                Id = r.Id,
                                Uid = r.Uid,
                                Name = r.RoomNames.FirstOrDefault(rn => !rn.IsDeleted && rn.LanguageId == languageId).Value
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