// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationObjectivesOptionRepository.cs" company="Softo">
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
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region InnovationOrganizationObjectivesOption IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationObjectivesOptionIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationObjectivesOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationObjectivesOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationObjectivesOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationObjectivesOption> FindByIds(this IQueryable<InnovationOrganizationObjectivesOption> query, List<int?> InnovationOrganizationObjectivesOptionsIds)
        {
            if (InnovationOrganizationObjectivesOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationObjectivesOptionsIds.Contains(io.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationObjectivesOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationObjectivesOption> FindByUids(this IQueryable<InnovationOrganizationObjectivesOption> query, List<Guid?> InnovationOrganizationObjectivesOptionsUids)
        {
            if (InnovationOrganizationObjectivesOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationObjectivesOptionsUids.Contains(io.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationObjectivesOption> IsNotDeleted(this IQueryable<InnovationOrganizationObjectivesOption> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationObjectivesOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationObjectivesOption> Order(this IQueryable<InnovationOrganizationObjectivesOption> query)
        {
            query = query.OrderBy(io => io.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOrganizationObjectivesOptionRepository</summary>
    public class InnovationOrganizationObjectivesOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationObjectivesOption>, IInnovationOrganizationObjectivesOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationObjectivesOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationObjectivesOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="InnovationOrganizationObjectivesOptionIds">The innovation option ids.</param>
        /// <returns>InnovationOrganizationObjectivesOption.</returns>
        public InnovationOrganizationObjectivesOption FindById(int InnovationOrganizationObjectivesOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationObjectivesOptionIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="InnovationOrganizationObjectivesOptionUid">The innovation option uid.</param>
        /// <returns>InnovationOrganizationObjectivesOption.</returns>
        public InnovationOrganizationObjectivesOption FindByUid(Guid InnovationOrganizationObjectivesOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationObjectivesOptionUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationObjectivesOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationObjectivesOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationObjectivesOption> FindByIdAsync(int InnovationOrganizationObjectivesOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationObjectivesOptionIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOrganizationObjectivesOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationObjectivesOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationObjectivesOption> FindByUidAsync(Guid InnovationOrganizationObjectivesOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationObjectivesOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationObjectivesOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationObjectivesOption>> FindAllByIdsAsync(List<int?> InnovationOrganizationObjectivesOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOrganizationObjectivesOptionIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationObjectivesOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationObjectivesOption>> FindAllByUidsAsync(List<Guid?> InnovationOrganizationObjectivesOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOrganizationObjectivesOptionUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOrganizationObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationObjectivesOption>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}