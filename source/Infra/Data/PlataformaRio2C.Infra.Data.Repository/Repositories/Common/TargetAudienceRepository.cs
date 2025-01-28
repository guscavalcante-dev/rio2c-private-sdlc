// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="TargetAudienceRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Target Audience IQueryable Extensions

    /// <summary>
    /// TargetAudienceIQueryableExtensions
    /// </summary>
    internal static class TargetAudienceIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        internal static IQueryable<TargetAudience> FindByUid(this IQueryable<TargetAudience> query, Guid targetAudienceUid)
        {
            query = query.Where(ta => ta.Uid == targetAudienceUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <returns></returns>
        internal static IQueryable<TargetAudience> FindByUids(this IQueryable<TargetAudience> query, List<Guid> targetAudiencesUids)
        {
            if (targetAudiencesUids?.Any() == true)
            {
                query = query.Where(ta => targetAudiencesUids.Contains(ta.Uid));
            }

            return query;
        }

        /// <summary>
        /// Finds the by project type identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        internal static IQueryable<TargetAudience> FindByProjectTypeId(this IQueryable<TargetAudience> query, int projectTypeId)
        {
            query = query.Where(ta => ta.ProjectTypeId == projectTypeId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<TargetAudience> IsNotDeleted(this IQueryable<TargetAudience> query)
        {
            query = query.Where(ta => !ta.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<TargetAudience> Order(this IQueryable<TargetAudience> query)
        {
            query = query.OrderBy(ta => ta.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>TargetAudienceRepository</summary>
    public class TargetAudienceRepository : Repository<PlataformaRio2CContext, TargetAudience>, ITargetAudienceRepository
    {
        /// <summary>Initializes a new instance of the <see cref="TargetAudienceRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public TargetAudienceRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<TargetAudience> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <returns></returns>
        public async Task<List<TargetAudience>> FindAllByUidsAsync(List<Guid> targetAudiencesUids)
        {
            var query = this.GetBaseQuery()
                                    .FindByUids(targetAudiencesUids);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all by project type identifier asynchronous.
        /// </summary>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<List<TargetAudience>> FindAllByProjectTypeIdAsync(int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                   .FindByProjectTypeId(projectTypeId);

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all dtos by project type identifier asynchronous.
        /// </summary>
        /// <param name="projectTypeId">The project type identifier.</param>
        /// <returns></returns>
        public async Task<List<TargetAudienceDto>> FindAllDtosByProjectTypeIdAsync(int projectTypeId)
        {
            var query = this.GetBaseQuery()
                                   .FindByProjectTypeId(projectTypeId);

            return await query
                            .Order()
                            .Select(ta => new TargetAudienceDto
                            {
                                Uid = ta.Uid,
                                Name = ta.Name
                            })
                            .ToListAsync();
        }
    }
}