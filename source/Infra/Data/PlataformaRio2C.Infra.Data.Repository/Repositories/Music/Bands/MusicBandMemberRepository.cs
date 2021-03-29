// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 03-24-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-24-2021
// ***********************************************************************
// <copyright file="MusicBandMemberRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region MusicBandMember IQueryable Extensions

    /// <summary>
    /// MusicBandMemberIQueryableExtensions
    /// </summary>
    internal static class MusicBandMemberIQueryableExtensions
    {
        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="bandMemberId">The band member identifier.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandMember> FindById(this IQueryable<MusicBandMember> query, int bandMemberId)
        {
            query = query.Where(mbm => mbm.Id == bandMemberId);

            return query;
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="bandMemberUid">The band member uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandMember> FindByUid(this IQueryable<MusicBandMember> query, Guid bandMemberUid)
        {
            query = query.Where(mbm => mbm.Uid == bandMemberUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicBandMember> IsNotDeleted(this IQueryable<MusicBandMember> query)
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
        internal static IQueryable<MusicBandMember> FindByBandId(this IQueryable<MusicBandMember> query, int musicBandId)
        {
            query = query.Where(mbm => mbm.MusicBandId == musicBandId);

            return query;
        }
    }

    #endregion

    #region MusicBandMemberDto IQueryable Extensions

    /// <summary>
    /// MusicBandMemberDtoIQueryableExtensions
    /// </summary>
    internal static class MusicBandMemberDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicBandMemberApiDto>> ToListPagedAsync(this IQueryable<MusicBandMemberApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MusicBandMemberJsonDto IQueryable Extensions

    ///// <summary>MusicBandMemberJsonDtoIQueryableExtensions</summary>
    //internal static class MusicBandMemberJsonDtoIQueryableExtensions
    //{
    //    /// <summary>
    //    /// To the list paged.
    //    /// </summary>
    //    /// <param name="query">The query.</param>
    //    /// <param name="page">The page.</param>
    //    /// <param name="pageSize">Size of the page.</param>
    //    /// <returns></returns>
    //    internal static async Task<IPagedList<MusicBandMemberJsonDto>> ToListPagedAsync(this IQueryable<MusicBandMemberJsonDto> query, int page, int pageSize)
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

    /// <summary>MusicBandMemberRepository</summary>
    public class MusicBandMemberRepository : Repository<Context.PlataformaRio2CContext, MusicBandMember>, IMusicBandMemberRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicBandMemberRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicBandMemberRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicBandMember> GetBaseQuery(bool @readonly = false)
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
        /// <param name="bandMemberId">The band member identifier.</param>
        /// <returns></returns>
        public async Task<MusicBandMember> FindByIdAsync(int bandMemberId)
        {
            var query = this.GetBaseQuery()
                               .IsNotDeleted()
                               .FindById(bandMemberId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="bandMemberUid">The band member uid.</param>
        /// <returns></returns>
        public async Task<MusicBandMember> FindByUidAsync(Guid bandMemberUid)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .FindByUid(bandMemberUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by band identifier asynchronous.
        /// </summary>
        /// <param name="bandId">The band identifier.</param>
        /// <returns></returns>
        public async Task<List<MusicBandMember>> FindAllByBandIdAsync(int bandId)
        {
            var query = this.GetBaseQuery()
                               .IsNotDeleted()
                               .FindByBandId(bandId);

            return await query
                            .ToListAsync();
        }

    }
}