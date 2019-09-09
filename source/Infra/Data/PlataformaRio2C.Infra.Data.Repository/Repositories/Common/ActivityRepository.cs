// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-09-2019
// ***********************************************************************
// <copyright file="ActivityRepository.cs" company="Softo">
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
    #region Activity IQueryable Extensions

    /// <summary>
    /// ActivityIQueryableExtensions
    /// </summary>
    internal static class ActivityIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="activityUid">The activity uid.</param>
        /// <returns></returns>
        internal static IQueryable<Activity> FindByUid(this IQueryable<Activity> query, Guid activityUid)
        {
            query = query.Where(a => a.Uid == activityUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="activitiesUids">The activities uids.</param>
        /// <returns></returns>
        internal static IQueryable<Activity> FindByUids(this IQueryable<Activity> query, List<Guid> activitiesUids)
        {
            if (activitiesUids?.Any() == true)
            {
                query = query.Where(a => activitiesUids.Contains(a.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Activity> IsNotDeleted(this IQueryable<Activity> query)
        {
            query = query.Where(a => !a.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>ActivityRepository</summary>
    public class ActivityRepository : Repository<PlataformaRio2CContext, Activity>, IActivityRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ActivityRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ActivityRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Activity> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }


        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<Activity>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query.ToListAsync();
        }

        public async Task<List<Activity>> FindAllByUidsAsync(List<Guid> activitiesUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(activitiesUids);

            return await query.ToListAsync();
        }
    }
}