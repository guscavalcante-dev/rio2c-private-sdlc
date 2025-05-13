// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="ReleasedMusicProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
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
    #region ReleasedMusicProject IQueryable Extensions

    /// <summary>
    /// ReleasedMusicProjectIQueryableExtensions
    /// </summary>
    internal static class ReleasedMusicProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="bandTeamMemberId">The band member identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ReleasedMusicProject> FindById(this IQueryable<ReleasedMusicProject> query, int bandTeamMemberId)
        {
            query = query.Where(mbm => mbm.Id == bandTeamMemberId);

            return query;
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="bandTeamMemberUid">The band member uid.</param>
        /// <returns></returns>
        internal static IQueryable<ReleasedMusicProject> FindByUid(this IQueryable<ReleasedMusicProject> query, Guid bandTeamMemberUid)
        {
            query = query.Where(mbm => mbm.Uid == bandTeamMemberUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ReleasedMusicProject> IsNotDeleted(this IQueryable<ReleasedMusicProject> query)
        {
            query = query.Where(mbm => !mbm.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by band identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="musicBandId">The music band identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ReleasedMusicProject> FindByBandId(this IQueryable<ReleasedMusicProject> query, int musicBandId)
        {
            query = query.Where(mbm => mbm.MusicBandId == musicBandId);

            return query;
        }
    }

    #endregion

    #region ReleasedMusicProjectDto IQueryable Extensions

    /// <summary>
    /// ReleasedMusicProjectDtoIQueryableExtensions
    /// </summary>
    internal static class ReleasedMusicProjectDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ReleasedMusicProjectApiDto>> ToListPagedAsync(this IQueryable<ReleasedMusicProjectApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region ReleasedMusicProjectJsonDto IQueryable Extensions

    ///// <summary>ReleasedMusicProjectJsonDtoIQueryableExtensions</summary>
    //internal static class ReleasedMusicProjectJsonDtoIQueryableExtensions
    //{
    //    /// <summary>
    //    /// To the list paged.
    //    /// </summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<ReleasedMusicProjectJsonDto>> ToListPagedAsync(this IQueryable<ReleasedMusicProjectJsonDto> query, int page, int pageSize)
    //    {
    //        // Page the list
    //        page++;

    //        var pagedList = await query.ToPagedListAsync(page, pageSize);
    //        if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
    //            pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

    //        return pagedList;
    //    }
    //}

    #endregion 

    /// <summary>ReleasedMusicProjectRepository</summary>
    public class ReleasedMusicProjectRepository : Repository<Context.PlataformaRio2CContext, ReleasedMusicProject>, IReleasedMusicProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ReleasedMusicProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ReleasedMusicProjectRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ReleasedMusicProject> GetBaseQuery(bool @readonly = false)
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
        /// <param name="bandTeamMemberId">The band member identifier.</param>
        /// <returns></returns>
        public async Task<ReleasedMusicProject> FindByIdAsync(int bandTeamMemberId)
        {
            var query = this.GetBaseQuery()
                               .IsNotDeleted()
                               .FindById(bandTeamMemberId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="bandTeamMemberUid">The band member uid.</param>
        /// <returns></returns>
        public async Task<ReleasedMusicProject> FindByUidAsync(Guid bandTeamMemberUid)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindByUid(bandTeamMemberUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by band identifier asynchronous.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <returns></returns>
        public async Task<List<ReleasedMusicProject>> FindAllByBandIdAsync(int bandId)
        {
            var query = this.GetBaseQuery()
                               .IsNotDeleted()
                               .FindByBandId(bandId);

            return await query
                            .ToListAsync();
        }
    }
}