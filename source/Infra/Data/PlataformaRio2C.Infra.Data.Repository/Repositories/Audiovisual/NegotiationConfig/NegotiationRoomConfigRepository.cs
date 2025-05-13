// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-05-2020
// ***********************************************************************
// <copyright file="NegotiationRoomConfigRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region NegotiationRoomConfig IQueryable Extensions

    /// <summary>
    /// NegotiationRoomConfigIQueryableExtensions
    /// </summary>
    internal static class NegotiationRoomConfigIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="negotiationConfigUid">The negotiation configuration uid.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationRoomConfig> FindByUid(this IQueryable<NegotiationRoomConfig> query, Guid negotiationConfigUid)
        {
            query = query.Where(nrc => nrc.Uid == negotiationConfigUid);

            return query;
        }

        internal static IQueryable<NegotiationRoomConfig> FindByProjectTypeId(this IQueryable<NegotiationRoomConfig> query, int projectTypeId)
        {
            query = query.Where(nc => nc.NegotiationConfig.ProjectTypeId == projectTypeId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<NegotiationRoomConfig> IsNotDeleted(this IQueryable<NegotiationRoomConfig> query)
        {
            query = query.Where(nrc => !nrc.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>NegotiationRoomConfigRepository</summary>
    public class NegotiationRoomConfigRepository : Repository<PlataformaRio2CContext, NegotiationRoomConfig>, INegotiationRoomConfigRepository
    {
        /// <summary>Initializes a new instance of the <see cref="NegotiationRoomConfigRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public NegotiationRoomConfigRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<NegotiationRoomConfig> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="negotiationRoomConfigUid">The negotiation room configuration uid.</param>
        /// <param name="projectTypeId">The Project Type Identifier.</param>
        /// <returns></returns>
        public async Task<NegotiationRoomConfigDto> FindMainInformationWidgetDtoAsync(Guid negotiationRoomConfigUid, int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(negotiationRoomConfigUid)
                                .FindByProjectTypeId(projectTypeId)
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
                                    },
                                    NegotiationConfig = nrc.NegotiationConfig
                                });

            return await query
                            .FirstOrDefaultAsync();
        }
    }
}