// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="VerticalTrackRepository.cs" company="Softo">
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
    #region Vertical Track IQueryable Extensions

    /// <summary>
    /// VerticalTrackIQueryableExtensions
    /// </summary>
    internal static class VerticalTrackIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="verticalTrackUid">The vertical track uid.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> FindByUid(this IQueryable<VerticalTrack> query, Guid verticalTrackUid)
        {
            query = query.Where(vt => vt.Uid == verticalTrackUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="verticalTrackUids">The vertical track uids.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> FindByUids(this IQueryable<VerticalTrack> query, List<Guid> verticalTrackUids)
        {
            if (verticalTrackUids?.Any() == true)
            {
                query = query.Where(vt => verticalTrackUids.Contains(vt.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> IsNotDeleted(this IQueryable<VerticalTrack> query)
        {
            query = query.Where(vt => !vt.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<VerticalTrack> Order(this IQueryable<VerticalTrack> query)
        {
            query = query.OrderBy(vt => vt.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>VerticalTrackRepository</summary>
    public class VerticalTrackRepository : Repository<PlataformaRio2CContext, VerticalTrack>, IVerticalTrackRepository
    {
        /// <summary>Initializes a new instance of the <see cref="VerticalTrackRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public VerticalTrackRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<VerticalTrack> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }


        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<VerticalTrack>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="verticalTrackUids">The vertical track uids.</param>
        /// <returns></returns>
        public async Task<List<VerticalTrack>> FindAllByUidsAsync(List<Guid> verticalTrackUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(verticalTrackUids);

            return await query
                            .Order()
                            .ToListAsync();
        }
    }
}