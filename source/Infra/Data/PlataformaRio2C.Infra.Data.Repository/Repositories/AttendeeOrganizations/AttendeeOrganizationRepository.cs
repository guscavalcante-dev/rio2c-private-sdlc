// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-28-2019
// ***********************************************************************
// <copyright file="AttendeeOrganizationRepository.cs" company="Softo">
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
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Attendee Organization IQueryable Extensions

    /// <summary>
    /// AttendeeOrganizationIQueryableExtensions
    /// </summary>
    internal static class AttendeeeOrganizationIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByUid(this IQueryable<AttendeeOrganization> query, Guid attendeeOrganizationUid)
        {
            query = query.Where(ao => ao.Uid == attendeeOrganizationUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByUids(this IQueryable<AttendeeOrganization> query, List<Guid> attendeeOrganizationsUids)
        {
            if (attendeeOrganizationsUids?.Any() == true)
            {
                query = query.Where(ao => attendeeOrganizationsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByEditionId(this IQueryable<AttendeeOrganization> query, int editionId, bool showAllEditions)
        {
            query = query.Where(ao => (showAllEditions || ao.EditionId == editionId)
                                      && !ao.IsDeleted
                                      && !ao.Edition.IsDeleted
                                      && !ao.Organization.IsDeleted);
            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> IsNotDeleted(this IQueryable<AttendeeOrganization> query)
        {
            query = query.Where(o => !o.IsDeleted);

            return query;
        }
    }

    #endregion

    #region Attendee OrganizationBaseDto IQueryable Extensions

    /// <summary>
    /// AttendeeOrganizationBaseDtoIQueryableExtensions
    /// </summary>
    internal static class AttendeeOrganizationBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<AttendeeOrganizationBaseDto>> ToListPagedAsync(this IQueryable<AttendeeOrganizationBaseDto> query, int page, int pageSize)
        {
            page++;

            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>AttendeeOrganizationRepository</summary>
    public class AttendeeOrganizationRepository : Repository<PlataformaRio2CContext, AttendeeOrganization>, IAttendeeOrganizationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeOrganizationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeOrganization> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all base dtos by edition uid asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganizationBaseDto>> FindAllBaseDtosByEditionUidAsync(int editionId, bool showAllEditions)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions);

            return await query
                            .Select(ao => new AttendeeOrganizationBaseDto
                            {
                                Id = ao.Id,
                                Uid = ao.Uid,
                                OrganizationBaseDto = new OrganizationBaseDto
                                {
                                    Id = ao.Organization.Id,
                                    Uid = ao.Organization.Uid,
                                    Name = ao.Organization.Name,
                                    HoldingBaseDto = ao.Organization.Holding == null ? null : new HoldingBaseDto
                                    {
                                        Id = ao.Organization.Holding.Id,
                                        Uid = ao.Organization.Holding.Uid,
                                        Name = ao.Organization.Holding.Name
                                    }
                                },
                                CreateDate = ao.CreateDate,
                                UpdateDate = ao.UpdateDate,
                            })
                            .OrderBy(ao => ao.OrganizationBaseDto.HoldingBaseDto.Name)
                            .ThenBy(ao => ao.OrganizationBaseDto.Name)
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="attendeeOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganization>> FindAllByUidsAsync(List<Guid> attendeeOrganizationsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(attendeeOrganizationsUids);

            return await query.ToListAsync();
        }
    }
}