// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCollaboratorRepository.cs" company="Softo">
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
    #region AttendeeInnovationOrganizationCollaborator IQueryable Extensions

    /// <summary>
    /// AttendeeInnovationOrganizationCollaboratorIQueryableExtensions
    /// </summary>
    internal static class AttendeeInnovationOrganizationCollaboratorIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="AttendeeInnovationOrganizationCollaboratorsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;AttendeeInnovationOrganizationCollaborator&gt;.</returns>
        internal static IQueryable<AttendeeInnovationOrganizationCollaborator> FindByIds(this IQueryable<AttendeeInnovationOrganizationCollaborator> query, List<int?> AttendeeInnovationOrganizationCollaboratorsIds)
        {
            if (AttendeeInnovationOrganizationCollaboratorsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => AttendeeInnovationOrganizationCollaboratorsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="AttendeeInnovationOrganizationCollaboratorsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganizationCollaborator> FindByUids(this IQueryable<AttendeeInnovationOrganizationCollaborator> query, List<Guid?> AttendeeInnovationOrganizationCollaboratorsUids)
        {
            if (AttendeeInnovationOrganizationCollaboratorsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => AttendeeInnovationOrganizationCollaboratorsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganizationCollaborator> IsNotDeleted(this IQueryable<AttendeeInnovationOrganizationCollaborator> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeInnovationOrganizationCollaboratorRepository</summary>
    public class AttendeeInnovationOrganizationCollaboratorRepository : Repository<PlataformaRio2CContext, AttendeeInnovationOrganizationCollaborator>, IAttendeeInnovationOrganizationCollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeInnovationOrganizationCollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeInnovationOrganizationCollaborator> GetBaseQuery(bool @readonly = false)
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
        /// <param name="AttendeeInnovationOrganizationCollaboratorIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganizationCollaborator&gt;&gt;.</returns>
        public async Task<AttendeeInnovationOrganizationCollaborator> FindByIdAsync(int AttendeeInnovationOrganizationCollaboratorIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { AttendeeInnovationOrganizationCollaboratorIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationCollaboratorUid">The innovation organization uid.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganizationCollaborator&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganizationCollaborator> FindByUidAsync(Guid AttendeeInnovationOrganizationCollaboratorUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { AttendeeInnovationOrganizationCollaboratorUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationCollaboratorIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganizationCollaborator&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganizationCollaborator>> FindAllByIdsAsync(List<int?> AttendeeInnovationOrganizationCollaboratorIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(AttendeeInnovationOrganizationCollaboratorIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationCollaboratorUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganizationCollaborator&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganizationCollaborator>> FindAllByUidsAsync(List<Guid?> AttendeeInnovationOrganizationCollaboratorUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(AttendeeInnovationOrganizationCollaboratorUids);

            return await query.ToListAsync();
        }

    }
}