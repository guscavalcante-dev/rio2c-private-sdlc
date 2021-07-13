// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionRepository.cs" company="Softo">
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
    /// InnovationOrganizationTrackOptionIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationTrackOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationTrackOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationTrackOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByIds(this IQueryable<InnovationOrganizationTrackOption> query, List<int?> InnovationOrganizationTrackOptionsIds)
        {
            if (InnovationOrganizationTrackOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationTrackOptionsIds.Contains(io.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOrganizationTrackOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByUids(this IQueryable<InnovationOrganizationTrackOption> query, List<Guid?> InnovationOrganizationTrackOptionsUids)
        {
            if (InnovationOrganizationTrackOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => InnovationOrganizationTrackOptionsUids.Contains(io.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> IsNotDeleted(this IQueryable<InnovationOrganizationTrackOption> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationTrackOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationTrackOption> Order(this IQueryable<InnovationOrganizationTrackOption> query)
        {
            query = query.OrderBy(io => io.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOrganizationTrackOptionRepository</summary>
    public class InnovationOrganizationTrackOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationTrackOption>, IInnovationOrganizationTrackOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationTrackOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationTrackOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation option ids.</param>
        /// <returns>InnovationOrganizationTrackOption.</returns>
        public InnovationOrganizationTrackOption FindById(int InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationTrackOptionIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUid">The innovation option uid.</param>
        /// <returns>InnovationOrganizationTrackOption.</returns>
        public InnovationOrganizationTrackOption FindByUid(Guid InnovationOrganizationTrackOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationTrackOptionUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationTrackOption> FindByIdAsync(int InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationTrackOptionIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationTrackOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationTrackOption> FindByUidAsync(Guid InnovationOrganizationTrackOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationTrackOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByIdsAsync(List<int?> InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOrganizationTrackOptionIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByUidsAsync(List<Guid?> InnovationOrganizationTrackOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOrganizationTrackOptionUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}