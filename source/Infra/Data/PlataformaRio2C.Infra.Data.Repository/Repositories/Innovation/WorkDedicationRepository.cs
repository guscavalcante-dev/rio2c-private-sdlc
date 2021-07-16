// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="WorkDedicationRepository.cs" company="Softo">
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
    #region WorkDedication IQueryable Extensions

    /// <summary>
    /// WorkDedicationIQueryableExtensions
    /// </summary>
    internal static class WorkDedicationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="workDedicationsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;WorkDedication&gt;.</returns>
        internal static IQueryable<WorkDedication> FindByIds(this IQueryable<WorkDedication> query, List<int?> workDedicationsIds)
        {
            if (workDedicationsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => workDedicationsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="workDedicationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<WorkDedication> FindByUids(this IQueryable<WorkDedication> query, List<Guid?> workDedicationsUids)
        {
            if (workDedicationsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => workDedicationsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<WorkDedication> IsNotDeleted(this IQueryable<WorkDedication> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;WorkDedication&gt;.</returns>
        internal static IQueryable<WorkDedication> Order(this IQueryable<WorkDedication> query)
        {
            query = query.OrderBy(wd => wd.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>WorkDedicationRepository</summary>
    public class WorkDedicationRepository : Repository<PlataformaRio2CContext, WorkDedication>, IWorkDedicationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public WorkDedicationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<WorkDedication> GetBaseQuery(bool @readonly = false)
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
        /// <param name="workDedicationId">The work dedication identifier.</param>
        /// <returns></returns>
        public WorkDedication FindById(int workDedicationId)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { workDedicationId });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="workDedicationUid">The work dedication uid.</param>
        /// <returns></returns>
        public WorkDedication FindByUid(Guid workDedicationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { workDedicationUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="workDedicationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;WorkDedication&gt;&gt;.</returns>
        public async Task<WorkDedication> FindByIdAsync(int workDedicationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { workDedicationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="workDedicationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;WorkDedication&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<WorkDedication> FindByUidAsync(Guid workDedicationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { workDedicationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="workDedicationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;WorkDedication&gt;&gt;.</returns>
        public async Task<List<WorkDedication>> FindAllByIdsAsync(List<int?> workDedicationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(workDedicationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="workDedicationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;WorkDedication&gt;&gt;.</returns>
        public async Task<List<WorkDedication>> FindAllByUidsAsync(List<Guid?> workDedicationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(workDedicationUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;WorkDedication&gt;&gt;.</returns>
        public async Task<List<WorkDedication>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}