// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="InnovationOptionRepository.cs" company="Softo">
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
    #region InnovationOption IQueryable Extensions

    /// <summary>
    /// InnovationOptionIQueryableExtensions
    /// </summary>
    internal static class InnovationOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOption&gt;.</returns>
        internal static IQueryable<InnovationOption> FindByIds(this IQueryable<InnovationOption> query, List<int?> innovationOptionsIds)
        {
            if (innovationOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOptionsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOption> FindByUids(this IQueryable<InnovationOption> query, List<Guid?> innovationOptionsUids)
        {
            if (innovationOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOptionsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>
        /// Finds the by innovation option group ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOptionGroupsIds">The innovation option groups ids.</param>
        /// <returns>IQueryable&lt;InnovationOption&gt;.</returns>
        internal static IQueryable<InnovationOption> FindByInnovationOptionGroupIds(this IQueryable<InnovationOption> query, List<int> innovationOptionGroupsIds)
        {
            if (innovationOptionGroupsIds?.Any() == true)
            {
                query = query.Where(ao => innovationOptionGroupsIds.Contains(ao.InnovationOptionGroupId));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOption> IsNotDeleted(this IQueryable<InnovationOption> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOption&gt;.</returns>
        internal static IQueryable<InnovationOption> Order(this IQueryable<InnovationOption> query)
        {
            query = query.OrderBy(wd => wd.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOptionRepository</summary>
    public class InnovationOptionRepository : Repository<PlataformaRio2CContext, InnovationOption>, IInnovationOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="InnovationOptionIds">The innovation option ids.</param>
        /// <returns>InnovationOption.</returns>
        public InnovationOption FindById(int InnovationOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOptionIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="InnovationOptionUid">The innovation option uid.</param>
        /// <returns>InnovationOption.</returns>
        public InnovationOption FindByUid(Guid InnovationOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOptionUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOption&gt;&gt;.</returns>
        public async Task<InnovationOption> FindByIdAsync(int InnovationOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOptionIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOption> FindByUidAsync(Guid InnovationOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOption>> FindAllByIdsAsync(List<int?> InnovationOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOptionIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOption>> FindAllByUidsAsync(List<Guid?> InnovationOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOptionUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOption>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all by group uid as an asynchronous operation.
        /// </summary>
        /// <param name="innovationOptionGroupId">The innovation option group identifier.</param>
        /// <returns>Task&lt;List&lt;InnovationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOption>> FindAllByGroupUidAsync(int innovationOptionGroupId)
        {
            var query = this.GetBaseQuery()
                            .FindByInnovationOptionGroupIds(new List<int>() { innovationOptionGroupId })
                            .Order();

            return await query.ToListAsync();
        }
    }
}