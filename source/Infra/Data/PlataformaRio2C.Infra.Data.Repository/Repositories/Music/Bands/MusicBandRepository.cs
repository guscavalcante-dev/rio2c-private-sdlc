// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-23-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-23-2021
// ***********************************************************************
// <copyright file="MusicBandRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region MusicBand IQueryable Extensions

    /// <summary>
    /// MusicBandIQueryableExtensions
    /// </summary>
    internal static class MusicBandIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBand> FindById(this IQueryable<MusicBand> query, int musicBandId)
        {
            query = query.Where(mb => mb.Id == musicBandId);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandUid">The music project uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBand> FindByUid(this IQueryable<MusicBand> query, Guid musicBandUid)
        {
            query = query.Where(mb => mb.Uid == musicBandUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBand> IsNotDeleted(this IQueryable<MusicBand> query)
        {
            query = query.Where(mb => !mb.IsDeleted);

            return query;
        }
    }

    #endregion

    #region MusicBandDto IQueryable Extensions

    /// <summary>
    /// MusicBandDtoIQueryableExtensions
    /// </summary>
    internal static class MusicBandDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicBandApiDto>> ToListPagedAsync(this IQueryable<MusicBandApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>MusicBandRepository</summary>
    public class MusicBandRepository : Repository<Context.PlataformaRio2CContext, MusicBand>, IMusicBandRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBandRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBand> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <returns></returns>
        public async Task<MusicBand> FindByIdAsync(int musicBandId)
        {
            var query = this.GetBaseQuery()
                               .FindById(musicBandId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets the music band by uid asynchronous.
        /// </summary>
        /// <param name="musicBandUid">The music band uid.</param>
        /// <returns></returns>
        public async Task<MusicBand> FindByUidAsync(Guid musicBandUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(musicBandUid);

            return await query
                            .FirstOrDefaultAsync();
        }
    }
}