// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 06-30-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationRepository.cs" company="Softo">
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
    #region AttendeeInnovationOrganization IQueryable Extensions

    /// <summary>
    /// AttendeeInnovationOrganizationIQueryableExtensions
    /// </summary>
    internal static class AttendeeInnovationOrganizationIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeInnovationOrganizationsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;AttendeeInnovationOrganization&gt;.</returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByIds(this IQueryable<AttendeeInnovationOrganization> query, List<int?> attendeeInnovationOrganizationsIds)
        {
            if (attendeeInnovationOrganizationsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ao => attendeeInnovationOrganizationsIds.Contains(ao.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeInnovationOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByUids(this IQueryable<AttendeeInnovationOrganization> query, List<Guid?> attendeeInnovationOrganizationsUids)
        {
            if (attendeeInnovationOrganizationsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeInnovationOrganizationsUids.Contains(aio.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeInnovationOrganization> IsNotDeleted(this IQueryable<AttendeeInnovationOrganization> query)
        {
            query = query.Where(aio => !aio.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by document.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>IQueryable&lt;AttendeeInnovationOrganization&gt;.</returns>
        internal static IQueryable<AttendeeInnovationOrganization> FindByDocument(this IQueryable<AttendeeInnovationOrganization> query, string document, int editionId)
        {
            query = query.Where(aio => aio.InnovationOrganization.Document == document 
                                        && aio.EditionId == editionId);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeInnovationOrganizationRepository</summary>
    public class AttendeeInnovationOrganizationRepository : Repository<PlataformaRio2CContext, AttendeeInnovationOrganization>, IAttendeeInnovationOrganizationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeInnovationOrganizationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeInnovationOrganization> GetBaseQuery(bool @readonly = false)
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
        /// <param name="AttendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<AttendeeInnovationOrganization> FindByIdAsync(int AttendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { AttendeeInnovationOrganizationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganization&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeInnovationOrganization> FindByUidAsync(Guid AttendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { AttendeeInnovationOrganizationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByIdsAsync(List<int?> AttendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(AttendeeInnovationOrganizationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="AttendeeInnovationOrganizationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeInnovationOrganization&gt;&gt;.</returns>
        public async Task<List<AttendeeInnovationOrganization>> FindAllByUidsAsync(List<Guid?> AttendeeInnovationOrganizationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(AttendeeInnovationOrganizationUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by document and edition identifier as an asynchronous operation.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;AttendeeInnovationOrganization&gt;.</returns>
        public async Task<AttendeeInnovationOrganization> FindByDocumentAndEditionIdAsync(string document, int editionId)
        {
            var query = this.GetBaseQuery()
                           .FindByDocument(document, editionId);

            return await query.FirstOrDefaultAsync();
        }
    }
}