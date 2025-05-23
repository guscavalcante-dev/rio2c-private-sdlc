﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-03-2024
// ***********************************************************************
// <copyright file="NegotiationConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using Z.EntityFramework.Plus;

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
        internal static IQueryable<NegotiationConfig> FindByProjectTypeId(this IQueryable<NegotiationConfig> query, int projectTypeId)
        {
            query = query.Where(nc => nc.ProjectTypeId == projectTypeId);

            return query;
        }

        /// <summary>Finds the by negotiation room configu uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> FindByNegotiationRoomConfiguUid(this IQueryable<NegotiationConfig> query, Guid negotiationRoomConfigUid)
        {
            query = query.Where(nc => nc.NegotiationRoomConfigs.Any(nrc => !nrc.IsDeleted && nrc.Uid == negotiationRoomConfigUid));

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

        /// <summary>Determines whether [has rooms configured].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> HasRoomsConfigured(this IQueryable<NegotiationConfig> query)
        {
            query = query.Where(nc => nc.NegotiationRoomConfigs.Any(nrc => !nrc.IsDeleted && !nrc.Room.IsDeleted));

            return query;
        }

        /// <summary>
        /// Determines whether [has virtual room configured].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> HasVirtualRoomConfigured(this IQueryable<NegotiationConfig> query)
        {
            query = query.Where(nc => nc.NegotiationRoomConfigs.Any(nrc => !nrc.IsDeleted &&
                                                                            !nrc.Room.IsDeleted &&
                                                                            nrc.Room.IsVirtualMeeting));

            return query;
        }

        /// <summary>
        /// Determines whether [has virtual room configured].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> HasPresentialRoomConfigured(this IQueryable<NegotiationConfig> query)
        {
            query = query.Where(nc => nc.NegotiationRoomConfigs.Any(nrc => !nrc.IsDeleted &&
                                                                            !nrc.Room.IsDeleted &&
                                                                            !nrc.Room.IsVirtualMeeting));

            return query;
        }

        /// <summary>
        /// Finds the by custom filter.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationConfig> FindByCustomFilter(this IQueryable<NegotiationConfig> query, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting)
        {
            if (!string.IsNullOrEmpty(customFilter))
            {
                if (customFilter == "HasManualTables")
                {
                    query = query
                                .HasRoomsConfigured()
                                .Where(n => n.NegotiationRoomConfigs.Any(nrc => !nrc.IsDeleted
                                                                                && (nrc.CountManualTables > 0 || nrc.CountAutomaticTables > 0)
                                                                                && nrc.Room.IsVirtualMeeting == buyerAttendeeOrganizationAcceptsVirtualMeeting));
                }
            }

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

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        public async Task<NegotiationConfigDto> FindMainInformationWidgetDtoAsync(Guid negotiationConfigUid, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(negotiationConfigUid)
                                .FindByProjectTypeId(projectTypeId)
                                .Select(nc => new NegotiationConfigDto
                                {
                                    NegotiationConfig = nc
                                });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the rooms widget dto asynchronous.</summary>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<NegotiationConfigDto> FindRoomsWidgetDtoAsync(Guid negotiationConfigUid, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(negotiationConfigUid)
                                .FindByProjectTypeId(projectTypeId)
                                .Select(nc => new NegotiationConfigDto
                                {
                                    NegotiationConfig = nc,
                                    NegotiationRoomConfigDtos = nc.NegotiationRoomConfigs
                                                                    .Where(nrc => !nrc.IsDeleted && !nrc.Room.IsDeleted)
                                                                    .Select(nrc => new NegotiationRoomConfigDto
                                                                    {
                                                                        NegotiationRoomConfig = nrc,
                                                                        RoomDto = new RoomDto
                                                                        {
                                                                            Room = nrc.Room,
                                                                            RoomNameDtos = nrc.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                                                            {
                                                                                RoomName = rn,
                                                                                LanguageDto = new LanguageDto
                                                                                {
                                                                                    Id = rn.Language.Id,
                                                                                    Uid = rn.Language.Uid,
                                                                                    Code = rn.Language.Code
                                                                                }
                                                                            })
                                                                        }
                                                                    })
                                });

            return await query
                            .FirstOrDefaultAsync();
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
            int editionId,
            int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByProjectTypeId(projectTypeId)
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
                                    RoundFirstTurn = nc.RoundFirstTurn,
                                    RoundSecondTurn = nc.RoundSecondTurn,
                                    TimeIntervalBetweenTurn = nc.TimeIntervalBetweenTurn,
                                    TimeOfEachRound = nc.TimeOfEachRound,
                                    TimeIntervalBetweenRound = nc.TimeIntervalBetweenRound,
                                    CreateDate = nc.CreateDate,
                                    UpdateDate = nc.UpdateDate
                                });

            return await query
                           .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountAsync(int editionId, int projectTypeId, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .FindByProjectTypeId(projectTypeId);

            return await query.CountAsync();
        }

        /// <summary>Finds all for generate negotiations asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<NegotiationConfig>> FindAllForGenerateNegotiationsAsync(int editionId, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                    .FindByEditionId(editionId, false)
                                    .FindByProjectTypeId(projectTypeId)
                                    .HasRoomsConfigured()
                                    .IncludeFilter(nc => nc.NegotiationRoomConfigs.Where(nrc => !nrc.IsDeleted && !nrc.Room.IsDeleted))
                                    .IncludeFilter(nc => nc.NegotiationRoomConfigs.Where(nrc => !nrc.IsDeleted && !nrc.Room.IsDeleted).Select(nrc => nrc.Room));

            return await query
                            .OrderBy(nc => nc.StartDate)
                            .ToListAsync();
        }

        /// <summary>Finds all dates dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<List<NegotiationConfigDto>> FindAllDatesDtosAsync(int editionId, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectTypeId(projectTypeId)
                                .FindByCustomFilter(customFilter, buyerAttendeeOrganizationAcceptsVirtualMeeting);

            return await query
                            .Select(nc => new NegotiationConfigDto
                            {
                                NegotiationConfig = nc,
                                NegotiationRoomConfigDtos = nc.NegotiationRoomConfigs.Select(nrc => new NegotiationRoomConfigDto()
                                {
                                    NegotiationConfig = nc,
                                    NegotiationRoomConfig = nrc,
                                    RoomDto = new RoomDto()
                                    {
                                        Room = nrc.Room
                                    }
                                })
                            })
                            .OrderBy(ncd => ncd.NegotiationConfig.StartDate)
                            .ToListAsync();
        }

        /// <summary>Finds all rooms dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<List<NegotiationConfigDto>> FindAllRoomsDtosAsync(int editionId, Guid negotiationConfigUid, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByProjectTypeId(projectTypeId)
                                .FindByUid(negotiationConfigUid)
                                .FindByCustomFilter(customFilter, buyerAttendeeOrganizationAcceptsVirtualMeeting);

            return await query
                            .Select(nc => new NegotiationConfigDto
                            {
                                NegotiationConfig = nc,
                                NegotiationRoomConfigDtos = nc.NegotiationRoomConfigs
                                                                    .Where(nrc => !nrc.IsDeleted
                                                                                    && !nrc.Room.IsDeleted
                                                                                    && (nrc.CountManualTables > 0 || nrc.CountAutomaticTables > 0)
                                                                                    && nrc.Room.IsVirtualMeeting == buyerAttendeeOrganizationAcceptsVirtualMeeting)
                                                                    .Select(nrc => new NegotiationRoomConfigDto
                                                                    {
                                                                        NegotiationRoomConfig = nrc,
                                                                        RoomDto = new RoomDto
                                                                        {
                                                                            Room = nrc.Room,
                                                                            RoomNameDtos = nrc.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                                                            {
                                                                                RoomName = rn,
                                                                                LanguageDto = new LanguageDto
                                                                                {
                                                                                    Id = rn.Language.Id,
                                                                                    Uid = rn.Language.Uid,
                                                                                    Code = rn.Language.Code
                                                                                }
                                                                            })
                                                                        }
                                                                    })
                            })
                            .OrderBy(ncd => ncd.NegotiationConfig.StartDate)
                            .ToListAsync();
        }

        /// <summary>Finds all times dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<NegotiationConfigDto> FindAllTimesDtosAsync(int editionId, Guid negotiationRoomConfigUid, string customFilter, bool buyerAttendeeOrganizationAcceptsVirtualMeeting, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByNegotiationRoomConfiguUid(negotiationRoomConfigUid)
                                .FindByProjectTypeId(projectTypeId)
                                .FindByCustomFilter(customFilter, buyerAttendeeOrganizationAcceptsVirtualMeeting);

            return await query
                            .Select(nc => new NegotiationConfigDto
                            {
                                NegotiationConfig = nc,
                                NegotiationRoomConfigDtos = nc.NegotiationRoomConfigs
                                                                    .Where(nrc => !nrc.IsDeleted && !nrc.Room.IsDeleted && (nrc.CountManualTables > 0 || nrc.CountAutomaticTables > 0) && nrc.Uid == negotiationRoomConfigUid)
                                                                    .Select(nrc => new NegotiationRoomConfigDto
                                                                    {
                                                                        NegotiationRoomConfig = nrc,
                                                                        RoomDto = new RoomDto
                                                                        {
                                                                            Room = nrc.Room,
                                                                            RoomNameDtos = nrc.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                                                            {
                                                                                RoomName = rn,
                                                                                LanguageDto = new LanguageDto
                                                                                {
                                                                                    Id = rn.Language.Id,
                                                                                    Uid = rn.Language.Uid,
                                                                                    Code = rn.Language.Code
                                                                                }
                                                                            })
                                                                        }
                                                                    })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all times dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <returns></returns>
        public async Task<List<NegotiationConfigDto>> FindAllByEditionIdAsync(int editionId, int projectTypeId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByEditionId(editionId, false)
                                .FindByProjectTypeId(projectTypeId)
                                .Select(nc => new NegotiationConfigDto
                                {
                                    NegotiationConfig = nc,
                                    NegotiationRoomConfigDtos = nc.NegotiationRoomConfigs
                                                                    .Where(nrc => !nrc.IsDeleted)
                                                                    .Select(nrc => new NegotiationRoomConfigDto
                                                                    {
                                                                        NegotiationRoomConfig = nrc,
                                                                    })
                                });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Counts the negotiation configs with virtual room configured asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountNegotiationConfigsWithVirtualRoomConfiguredAsync(int editionId, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectTypeId(projectTypeId)
                                .HasVirtualRoomConfigured();

            return await query.CountAsync();
        }

        /// <summary>
        /// Counts the negotiation configs with presential room configured asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountNegotiationConfigsWithPresentialRoomConfiguredAsync(int editionId, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectTypeId(projectTypeId)
                                .HasPresentialRoomConfigured();

            return await query.CountAsync();
        }
    }
}