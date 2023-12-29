// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-28-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionGroupRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
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

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> FindByKeywords(this IQueryable<InnovationOrganizationTrackOptionGroup> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<InnovationOrganizationTrackOptionGroup>(false);
                var innerNameWhere = PredicateBuilder.New<InnovationOrganizationTrackOptionGroup>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerNameWhere = innerNameWhere.And(t => t.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOptionGroup> FindByUid(this IQueryable<InnovationOrganizationTrackOptionGroup> query, Guid uid)
        {
            query = query.Where(iotog => iotog.Uid == uid);

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
        /// Finds the main information widget dto asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<InnovationOrganizationTrackOptionGroupDto> FindMainInformationWidgetDtoAsync(Guid innovationOrganizationTrackOptionGroupUid)
        {
            var query = this.GetBaseQuery()
                            .IsActive()
                            .Order()
                            .FindByUid(innovationOrganizationTrackOptionGroupUid)
                            .Select(iotog => new InnovationOrganizationTrackOptionGroupDto
                            {
                                Id = iotog.Id,
                                Uid = iotog.Uid,
                                GroupName = iotog.Name,
                                CreateDate = iotog.CreateDate,
                                UpdateDate = iotog.UpdateDate
                            });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the track options widget dto asynchronous.
        /// </summary>
        /// <param name="innovationOrganizationTrackOptionGroupUid">The innovation organization track option group uid.</param>
        /// <returns></returns>
        public async Task<InnovationOrganizationTrackOptionGroupDto> FindTrackOptionsWidgetDtoAsync(Guid innovationOrganizationTrackOptionGroupUid)
        {
            var query = this.GetBaseQuery()
                            .IsActive()
                            .Order()
                            .FindByUid(innovationOrganizationTrackOptionGroupUid)
                            .Select(iotog => new InnovationOrganizationTrackOptionGroupDto
                            {
                                Id = iotog.Id,
                                Uid = iotog.Uid,
                                GroupName = iotog.Name,
                                InnovationOrganizationTrackOptionDtos = iotog.InnovationOrganizationTrackOptions
                                                                                .Where(ioto => !ioto.IsDeleted)
                                                                                .Select(ioto => new InnovationOrganizationTrackOptionDto
                                                                                {
                                                                                    Id = ioto.Id,
                                                                                    Uid = ioto.Uid,
                                                                                    Name = ioto.Name,
                                                                                    HasAdditionalInfo = ioto.HasAdditionalInfo,
                                                                                    CreateDate = ioto.CreateDate,
                                                                                    UpdateDate = ioto.UpdateDate
                                                                                })
                            });

            return await query.FirstOrDefaultAsync();
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
                                Id = iotog.Id,
                                Uid = iotog.Uid,
                                GroupName = iotog.Name,
                                InnovationOrganizationTrackOptionGroup = iotog,
                                InnovationOrganizationTrackOptions = iotog.InnovationOrganizationTrackOptions.Where(ioto => !ioto.IsDeleted),
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

        /// <summary>
        /// Finds all by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <returns></returns>
        public async Task<IPagedList<InnovationOrganizationTrackOptionGroupDto>> FindAllByDataTable(
            int page, 
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                            .FindByKeywords(keywords)
                            .IsActive();

            var innovationOrganizationTrackOptionGroupDtos = await query
                             .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("GroupName", "Name")
                                },
                                new List<string> { "Name", "CreateDate", "UpdateDate" },
                                "Name")
                             .Select(iotog => new InnovationOrganizationTrackOptionGroupDto
                             {
                                 Id = iotog.Id,
                                 Uid = iotog.Uid,
                                 GroupName = iotog.Name,
                                 CreateDate = iotog.CreateDate,
                                 UpdateDate = iotog.UpdateDate,
                                 InnovationOrganizationTrackOptionNames = iotog.InnovationOrganizationTrackOptions.Where(ioto => !ioto.IsDeleted).Select(ioto => ioto.Name)
                             })
                            .ToPagedListAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return innovationOrganizationTrackOptionGroupDtos;
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, int editionId)
        {
            var query = this.GetBaseQuery();
                                //.FindByEditionId(showAllEditions, editionId); TODO: This table doesn't have EditionId column. If necessary, implement this!

            return await query
                            .CountAsync();
        }
    }
}