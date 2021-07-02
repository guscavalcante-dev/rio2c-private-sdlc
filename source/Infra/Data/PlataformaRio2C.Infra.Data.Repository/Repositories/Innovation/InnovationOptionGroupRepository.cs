// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="InnovationOptionGroupRepository.cs" company="Softo">
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
    #region InnovationOptionGroup IQueryable Extensions

    /// <summary>
    /// InnovationOptionGroupIQueryableExtensions
    /// </summary>
    internal static class InnovationOptionGroupIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOptionGroupsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOptionGroup&gt;.</returns>
        internal static IQueryable<InnovationOptionGroup> FindByIds(this IQueryable<InnovationOptionGroup> query, List<int?> InnovationOptionGroupsIds)
        {
            if (InnovationOptionGroupsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => InnovationOptionGroupsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="InnovationOptionGroupsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOptionGroup> FindByUids(this IQueryable<InnovationOptionGroup> query, List<Guid?> InnovationOptionGroupsUids)
        {
            if (InnovationOptionGroupsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => InnovationOptionGroupsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOptionGroup> IsNotDeleted(this IQueryable<InnovationOptionGroup> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOptionGroup&gt;.</returns>
        internal static IQueryable<InnovationOptionGroup> Order(this IQueryable<InnovationOptionGroup> query)
        {
            query = query.OrderBy(wd => wd.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOptionGroupRepository</summary>
    public class InnovationOptionGroupRepository : Repository<PlataformaRio2CContext, InnovationOptionGroup>, IInnovationOptionGroupRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOptionGroupRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOptionGroup> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionGroupIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOptionGroup&gt;&gt;.</returns>
        public async Task<InnovationOptionGroup> FindByIdAsync(int InnovationOptionGroupIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOptionGroupIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOptionGroupUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOptionGroup&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOptionGroup> FindByUidAsync(Guid InnovationOptionGroupUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOptionGroupUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionGroupIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOptionGroup&gt;&gt;.</returns>
        public async Task<List<InnovationOptionGroup>> FindAllByIdsAsync(List<int?> InnovationOptionGroupIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOptionGroupIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOptionGroupUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOptionGroup&gt;&gt;.</returns>
        public async Task<List<InnovationOptionGroup>> FindAllByUidsAsync(List<Guid?> InnovationOptionGroupUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOptionGroupUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;InnovationOptionGroup&gt;&gt;.</returns>
        public async Task<List<InnovationOptionGroup>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}