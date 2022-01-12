// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository.cs" company="Softo">
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
    #region InnovationOrganizationTrackOption IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationSustainableDevelopmentObjectivesOptionRepositoryIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationSustainableDevelopmentObjectivesOptionRepositoryIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="ids">Ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByIds(this IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> query, List<int?> ids)
        {
            if (ids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ioto => ids.Contains(ioto.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="uids">The uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByUids(this IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> query, List<Guid?> uids)
        {
            if (uids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ioto => uids.Contains(ioto.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> IsNotDeleted(this IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> query)
        {
            query = query.Where(ioto => !ioto.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> Order(this IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> query)
        {
            query = query.OrderBy(ioto => ioto.DisplayOrder);

            return query;
        }        
    }

    #endregion

    /// <summary>InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository</summary>
    public class InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationSustainableDevelopmentObjectivesOption>, IInnovationOrganizationSustainableDevelopmentObjectivesOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationSustainableDevelopmentObjectivesOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationSustainableDevelopmentObjectivesOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="ids">The innovation option ids.</param>
        /// <returns>InnovationOrganizationSustainableDevelopmentObjectivesOption.</returns>
        public InnovationOrganizationSustainableDevelopmentObjectivesOption FindById(int ids)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { ids });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="uid">The innovation option uid.</param>
        /// <returns>InnovationOrganizationSustainableDevelopmentObjectivesOption.</returns>
        public InnovationOrganizationSustainableDevelopmentObjectivesOption FindByUid(Guid uid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { uid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="ids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByIdAsync(int ids)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { ids });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="uid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationSustainableDevelopmentObjectivesOption> FindByUidAsync(Guid uid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { uid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="ids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllByIdsAsync(List<int?> ids)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(ids)
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="uids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllByUidsAsync(List<Guid?> uids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(uids)
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOrganizationSustainableDevelopmentObjectivesOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationSustainableDevelopmentObjectivesOption>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}