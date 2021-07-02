// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationOptionRepository.cs" company="Softo">
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
    #region InnovationOrganizationOption IQueryable Extensions

    /// <summary>
    /// Class InnovationOrganizationOptionIQueryableExtensions.
    /// </summary>
    internal static class InnovationOrganizationOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationOption> FindByIds(this IQueryable<InnovationOrganizationOption> query, List<int?> innovationOrganizationOptionsIds)
        {
            if (innovationOrganizationOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOrganizationOptionsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationOption> FindByUids(this IQueryable<InnovationOrganizationOption> query, List<Guid?> innovationOrganizationOptionsUids)
        {
            if (innovationOrganizationOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => innovationOrganizationOptionsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationOption> IsNotDeleted(this IQueryable<InnovationOrganizationOption> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>
    /// Class InnovationOrganizationOptionRepository.
    /// Implements the <see cref="PlataformaRio2C.Infra.Data.Repository.Repository{PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, PlataformaRio2C.Domain.Entities.InnovationOrganizationOption}" />
    /// Implements the <see cref="PlataformaRio2C.Domain.Interfaces.IInnovationOrganizationOptionRepository" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Infra.Data.Repository.Repository{PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext, PlataformaRio2C.Domain.Entities.InnovationOrganizationOption}" />
    /// <seealso cref="PlataformaRio2C.Domain.Interfaces.IInnovationOrganizationOptionRepository" />
    public class InnovationOrganizationOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationOption>, IInnovationOrganizationOptionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationOrganizationOptionRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="innovationOrganizationOptionId">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationOption> FindByIdAsync(int innovationOrganizationOptionId)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { innovationOrganizationOptionId });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="innovationOrganizationOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationOption> FindByUidAsync(Guid innovationOrganizationOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { innovationOrganizationOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationOption>> FindAllByIdsAsync(List<int?> InnovationOrganizationOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOrganizationOptionIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationOption>> FindAllByUidsAsync(List<Guid?> InnovationOrganizationOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOrganizationOptionUids);

            return await query.ToListAsync();
        }
    }
}