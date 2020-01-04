// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="HorizontalTrackRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Horizontal Track IQueryable Extensions

    /// <summary>
    /// HorizontalTrackIQueryableExtensions
    /// </summary>
    internal static class HorizontalTrackIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="horizontalTrackUid">The horizontal track uid.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> FindByUid(this IQueryable<HorizontalTrack> query, Guid horizontalTrackUid)
        {
            query = query.Where(ht => ht.Uid == horizontalTrackUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="horizontalTrackUids">The horizontal track uids.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> FindByUids(this IQueryable<HorizontalTrack> query, List<Guid> horizontalTrackUids)
        {
            if (horizontalTrackUids?.Any() == true)
            {
                query = query.Where(ht => horizontalTrackUids.Contains(ht.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> IsNotDeleted(this IQueryable<HorizontalTrack> query)
        {
            query = query.Where(ht => !ht.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<HorizontalTrack> Order(this IQueryable<HorizontalTrack> query)
        {
            query = query.OrderBy(ht => ht.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>HorizontalTrackRepository</summary>
    public class HorizontalTrackRepository : Repository<PlataformaRio2CContext, HorizontalTrack>, IHorizontalTrackRepository
    {
        /// <summary>Initializes a new instance of the <see cref="HorizontalTrackRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public HorizontalTrackRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<HorizontalTrack> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }


        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<HorizontalTrack>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="horizontalTrackUids">The horizontal track uids.</param>
        /// <returns></returns>
        public async Task<List<HorizontalTrack>> FindAllByUidsAsync(List<Guid> horizontalTrackUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(horizontalTrackUids);

            return await query
                            .Order()
                            .ToListAsync();
        }
    }
}