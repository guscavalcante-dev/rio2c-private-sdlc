// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-04-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroupRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region InnovationOrganizationTrackOptionGroup IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationTrackOptionGroupIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationTrackOptionGroupIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> IsNotDeleted(this IQueryable<InnovationOrganizationTrackOptionGroup> query)
        {
            query = query.Where(iotog => !iotog.IsDeleted);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is active.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> IsActive(this IQueryable<InnovationOrganizationTrackOptionGroup> query)
        {
            query = query.Where(iotog => iotog.IsActive);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationTrackOptionGroup&gt;.</returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> Order(this IQueryable<InnovationOrganizationTrackOptionGroup> query)
        {
            query = query.OrderBy(iotog => iotog.DisplayOrder);

            return query;
        }

        /// <summary>
        /// Finds the by attendee collaborator identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> FindByAttendeeCollaboratorId(this IQueryable<InnovationOrganizationTrackOptionGroup> query, int attendeeCollaboratorId)
        {
            query = query.Where(iotog => iotog.InnovationOrganizationTrackOptions
                                                .Any(ioto => ioto.AttendeeCollaboratorInnovationOrganizationTracks
                                                                    .Where(aciot => !aciot.IsDeleted)
                                                                    .Any(aciot => aciot.AttendeeCollaboratorId == attendeeCollaboratorId)));

            return query;
        }
    }

    #endregion

    /// <summary>InnovationOrganizationTrackOptionGroupRepository</summary>
    public class InnovationOrganizationTrackOptionGroupRepository : Repository<PlataformaRio2CContext, InnovationOrganizationTrackOptionGroup>, IInnovationOrganizationTrackOptionGroupRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationTrackOptionGroupRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationTrackOptionGroup> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Find all as an asynchronous operation.
        /// </summary>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOptionGroup>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .IsActive()
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all dto asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOptionGroupDto>> FindAllDtoAsync()
        {
            var query = this.GetBaseQuery()
                            .IsActive()
                            .Order()
                            .Select(iotog => new InnovationOrganizationTrackOptionGroupDto
                            {
                                InnovationOrganizationTrackOptionGroup = iotog,
                                InnovationOrganizationTrackOptions = iotog.InnovationOrganizationTrackOptions
                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by attendee collaborator identifier asynchronous.
        /// </summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOptionGroup>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId)
        {
            var query = this.GetBaseQuery()
                            .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                            .Order();

            return await query.ToListAsync();
        }
    }
}