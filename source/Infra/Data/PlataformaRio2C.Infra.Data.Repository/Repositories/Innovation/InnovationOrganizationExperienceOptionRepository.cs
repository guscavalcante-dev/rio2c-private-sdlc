// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationExperienceOptionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region InnovationOrganizationExperienceOption IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationExperienceOptionIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationExperienceOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationExperienceOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationExperienceOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationExperienceOption> FindByIds(this IQueryable<InnovationOrganizationExperienceOption> query, List<int?> InnovationOrganizationExperienceOptionsIds)
        {
            if (InnovationOrganizationExperienceOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationExperienceOptionsIds.Contains(io.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationExperienceOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationExperienceOption> FindByUids(this IQueryable<InnovationOrganizationExperienceOption> query, List<Guid?> InnovationOrganizationExperienceOptionsUids)
        {
            if (InnovationOrganizationExperienceOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationExperienceOptionsUids.Contains(io.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationExperienceOption> IsNotDeleted(this IQueryable<InnovationOrganizationExperienceOption> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationExperienceOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationExperienceOption> Order(this IQueryable<InnovationOrganizationExperienceOption> query)
        {
            query = query.OrderBy(io => io.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOrganizationExperienceOptionRepository</summary>
    public class InnovationOrganizationExperienceOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationExperienceOption>, IInnovationOrganizationExperienceOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationExperienceOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationExperienceOption> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionIds">The innovation option ids.</param>
        /// <returns>InnovationOrganizationExperienceOption.</returns>
        public InnovationOrganizationExperienceOption FindById(int InnovationOrganizationExperienceOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationExperienceOptionIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionUid">The innovation option uid.</param>
        /// <returns>InnovationOrganizationExperienceOption.</returns>
        public InnovationOrganizationExperienceOption FindByUid(Guid InnovationOrganizationExperienceOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationExperienceOptionUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationExperienceOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationExperienceOption> FindByIdAsync(int InnovationOrganizationExperienceOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationExperienceOptionIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationExperienceOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationExperienceOption> FindByUidAsync(Guid InnovationOrganizationExperienceOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationExperienceOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationExperienceOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationExperienceOption>> FindAllByIdsAsync(List<int?> InnovationOrganizationExperienceOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOrganizationExperienceOptionIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationExperienceOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationExperienceOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationExperienceOption>> FindAllByUidsAsync(List<Guid?> InnovationOrganizationExperienceOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOrganizationExperienceOptionUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOrganizationExperienceOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationExperienceOption>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}