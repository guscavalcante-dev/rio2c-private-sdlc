// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
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
using PlataformaRio2C.Domain.Dtos;

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

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Room> FindByEditionId(this IQueryable<Room> query, int editionId)
        {
            query = query.Where(r => r.Edition.Id == editionId);

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
                                .FindByEditionId(editionId);

            return await query
                            .Select(r => new RoomDto
                            {
                                Room = r,
                                RoomNameBaseDtos = r.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameBaseDto
                                {
                                    Id = rn.Id,
                                    Uid = rn.Uid,
                                    Value = rn.Value,
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
    }
}
