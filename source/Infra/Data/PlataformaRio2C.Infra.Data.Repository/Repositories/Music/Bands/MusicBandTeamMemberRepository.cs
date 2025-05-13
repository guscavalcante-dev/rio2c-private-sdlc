// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="MusicBandTeamMemberRepository.cs" company="Softo">
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
    #region MusicBandTeamMember IQueryable Extensions

    /// <summary>
    /// MusicBandTeamMemberIQueryableExtensions
    /// </summary>
    internal static class MusicBandTeamMemberIQueryableExtensions
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="bandTeamMemberId">The band member identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandTeamMember> FindById(this IQueryable<MusicBandTeamMember> query, int bandTeamMemberId)
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
        internal static IQueryable<MusicBandTeamMember> FindByUid(this IQueryable<MusicBandTeamMember> query, Guid bandTeamMemberUid)
        {
            query = query.Where(mbm => mbm.Uid == bandTeamMemberUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandTeamMember> IsNotDeleted(this IQueryable<MusicBandTeamMember> query)
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
        internal static IQueryable<MusicBandTeamMember> FindByBandId(this IQueryable<MusicBandTeamMember> query, int musicBandId)
        {
            query = query.Where(mbm => mbm.MusicBandId == musicBandId);

            return query;
        }
    }

    #endregion

    #region MusicBandTeamMemberDto IQueryable Extensions

    /// <summary>
    /// MusicBandTeamMemberDtoIQueryableExtensions
    /// </summary>
    internal static class MusicBandTeamMemberDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicBandTeamMemberApiDto>> ToListPagedAsync(this IQueryable<MusicBandTeamMemberApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MusicBandTeamMemberJsonDto IQueryable Extensions

    ///// <summary>MusicBandTeamMemberJsonDtoIQueryableExtensions</summary>
    //internal static class MusicBandTeamMemberJsonDtoIQueryableExtensions
    //{
    //    /// <summary>
    //    /// To the list paged.
    //    /// </summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<MusicBandTeamMemberJsonDto>> ToListPagedAsync(this IQueryable<MusicBandTeamMemberJsonDto> query, int page, int pageSize)
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

    /// <summary>MusicBandTeamMemberRepository</summary>
    public class MusicBandTeamMemberRepository : Repository<Context.PlataformaRio2CContext, MusicBandTeamMember>, IMusicBandTeamMemberRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandTeamMemberRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBandTeamMemberRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBandTeamMember> GetBaseQuery(bool @readonly = false)
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
        public async Task<MusicBandTeamMember> FindByIdAsync(int bandTeamMemberId)
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
        public async Task<MusicBandTeamMember> FindByUidAsync(Guid bandTeamMemberUid)
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
        public async Task<List<MusicBandTeamMember>> FindAllByBandIdAsync(int bandId)
        {
            var query = this.GetBaseQuery()
                               .IsNotDeleted()
                               .FindByBandId(bandId);

            return await query
                            .ToListAsync();
        }
    }
}