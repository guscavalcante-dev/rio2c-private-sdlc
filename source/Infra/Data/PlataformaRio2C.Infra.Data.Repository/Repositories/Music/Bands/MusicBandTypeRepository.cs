// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-25-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2024
// ***********************************************************************
// <copyright file="MusicBandTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region MusicBandType IQueryable Extensions

    /// <summary>
    /// MusicBandTypeIQueryableExtensions
    /// </summary>
    internal static class MusicBandTypeIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="MusicBandTypeUid">The music project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandType> FindByUid(this IQueryable<MusicBandType> query, Guid MusicBandTypeUid)
        {
            query = query.Where(mb => mb.Uid == MusicBandTypeUid);

            return query;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandTypeId">The music band type identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandType> FindById(this IQueryable<MusicBandType> query, int musicBandTypeId)
        {
            query = query.Where(mbt => mbt.Id == musicBandTypeId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandType> IsNotDeleted(this IQueryable<MusicBandType> query)
        {
            query = query.Where(mbt => !mbt.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>MusicBandTypeRepository</summary>
    public class MusicBandTypeRepository : Repository<Context.PlataformaRio2CContext, MusicBandType>, IMusicBandTypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandTypeRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBandTypeRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBandType> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Gets the music band by uid asynchronous.
        /// </summary>
        /// <param name="MusicBandTypeUid">The music band uid.</param>
        /// <returns></returns>
        public async Task<MusicBandType> FindByIdAsync(int musicBandTypeId)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindById(musicBandTypeId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="musicBandTypeUid">The music band type uid.</param>
        /// <returns></returns>
        public async Task<MusicBandType> FindByUidAsync(Guid musicBandTypeUid)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindByUid(musicBandTypeUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<MusicBandType>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .ToListAsync();
        }
    }
}