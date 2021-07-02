// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationRepository.cs" company="Softo">
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
    #region InnovationOrganization IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganization&gt;.</returns>
        internal static IQueryable<InnovationOrganization> FindByIds(this IQueryable<InnovationOrganization> query, List<int?> innovationOrganizationsIds)
        {
            if (innovationOrganizationsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOrganizationsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganization> FindByUids(this IQueryable<InnovationOrganization> query, List<Guid?> innovationOrganizationsUids)
        {
            if (innovationOrganizationsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOrganizationsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganization> IsNotDeleted(this IQueryable<InnovationOrganization> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOrganizationRepository</summary>
    public class InnovationOrganizationRepository : Repository<PlataformaRio2CContext, InnovationOrganization>, IInnovationOrganizationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganization> GetBaseQuery(bool @readonly = false)
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
        /// <param name="innovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganization&gt;&gt;.</returns>
        public async Task<InnovationOrganization> FindByIdAsync(int innovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { innovationOrganizationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="innovationOrganizationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganization&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganization> FindByUidAsync(Guid innovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { innovationOrganizationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="innovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganization&gt;&gt;.</returns>
        public async Task<List<InnovationOrganization>> FindAllByIdsAsync(List<int?> innovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(innovationOrganizationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="innovationOrganizationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganization&gt;&gt;.</returns>
        public async Task<List<InnovationOrganization>> FindAllByUidsAsync(List<Guid?> innovationOrganizationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(innovationOrganizationUids);

            return await query.ToListAsync();
        }

    }
}