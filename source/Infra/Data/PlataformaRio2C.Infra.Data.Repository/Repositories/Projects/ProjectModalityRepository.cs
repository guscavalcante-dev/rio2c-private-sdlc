// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Gilson Oliveira
// Created          : 10-23-2024
//
// Last Modified By : Gilson Oliveira
// Last Modified On : 10-23-2024
// ***********************************************************************
// <copyright file="ProjectModalityRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project Modality IQueryable Extensions

    /// <summary>
    /// ProjectModalityIQueryableExtensions
    /// </summary>
    internal static class ProjectModalityIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectModality> IsNotDeleted(this IQueryable<ProjectModality> query)
        {
            query = query.Where(l => !l.IsDeleted);

            return query;
        }

        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectModalityUid">The project type uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectModality> FindByUid(this IQueryable<ProjectModality> query, Guid projectModalityUid)
        {
            query = query.Where(pt => pt.Uid == projectModalityUid);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectModality> FindByKeywords(this IQueryable<ProjectModality> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<ProjectModality>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(pt => pt.Name.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }
    }

    #endregion

    /// <summary>ProjectModalityRepository</summary>
    public class ProjectModalityRepository : Repository<PlataformaRio2CContext, ProjectModality>, IProjectModalityRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectModalityRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectModalityRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ProjectModality> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<ProjectModalityDto>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                .IsNotDeleted()
                .Select(pm => new ProjectModalityDto
                {
                    Uid = pm.Uid,
                    Name = pm.Name
                });
            return await query.ToListAsync();
        }
    }
}