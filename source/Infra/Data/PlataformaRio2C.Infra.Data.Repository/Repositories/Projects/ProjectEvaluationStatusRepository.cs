// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="ProjectEvaluationStatusRepository.cs" company="Softo">
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
    #region Project Evaluation Status IQueryable Extensions

    /// <summary>ProjectEvaluationStatusIQueryableExtensions</summary>
    internal static class ProjectEvaluationStatusIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectEvaluationStatusUid">The project evaluation status uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationStatus> FindByUid(this IQueryable<ProjectEvaluationStatus> query, Guid projectEvaluationStatusUid)
        {
            query = query.Where(pes => pes.Uid == projectEvaluationStatusUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectEvaluationStatus> IsNotDeleted(this IQueryable<ProjectEvaluationStatus> query)
        {
            query = query.Where(p => !p.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>ProjectEvaluationStatusRepository</summary>
    public class ProjectEvaluationStatusRepository : Repository<Context.PlataformaRio2CContext, ProjectEvaluationStatus>, IProjectEvaluationStatusRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectEvaluationStatusRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectEvaluationStatusRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ProjectEvaluationStatus> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<ProjectEvaluationStatus>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .ToListAsync();
        }
    }
}