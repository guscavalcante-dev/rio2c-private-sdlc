// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="ProjectEvaluationRefuseReasonRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project Evaluation Refuse Reason IQueryable Extensions

    /// <summary>ProjectEvaluationStatusIQueryableExtensions</summary>
    internal static class ProjectEvaluationRefuseReasonIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectEvaluationRefuseReasonUid">The project evaluation refuse reason uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationRefuseReason> FindByUid(this IQueryable<ProjectEvaluationRefuseReason> query, Guid projectEvaluationRefuseReasonUid)
        {
            query = query.Where(perr => perr.Uid == projectEvaluationRefuseReasonUid);

            return query;
        }

        /// <summary>Finds the by project type uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectTypeUid">The project type uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationRefuseReason> FindByProjectTypeUid(this IQueryable<ProjectEvaluationRefuseReason> query, Guid projectTypeUid)
        {
            query = query.Where(perr => perr.ProjectType.Uid == projectTypeUid);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationRefuseReason> Order(this IQueryable<ProjectEvaluationRefuseReason> query)
        {
            query = query.OrderBy(perr => perr.DisplayOrder);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationRefuseReason> IsNotDeleted(this IQueryable<ProjectEvaluationRefuseReason> query)
        {
            query = query.Where(perr => !perr.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>ProjectEvaluationRefuseReasonRepository</summary>
    public class ProjectEvaluationRefuseReasonRepository : Repository<Context.PlataformaRio2CContext, ProjectEvaluationRefuseReason>, IProjectEvaluationRefuseReasonRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationRefuseReasonRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectEvaluationRefuseReasonRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ProjectEvaluationRefuseReason> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="projectEvaluationRefuseReasonUid">The project evaluation refuse reason uid.</param>
        /// <returns></returns>
        public async Task<ProjectEvaluationRefuseReason> FindByUidAsync(Guid projectEvaluationRefuseReasonUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(projectEvaluationRefuseReasonUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<ProjectEvaluationRefuseReason>> FindAllByProjectTypeUidAsync(Guid projectTypeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByProjectTypeUid(projectTypeUid);

            return await query
                            .Order()
                            .ToListAsync();
        }
    }
}