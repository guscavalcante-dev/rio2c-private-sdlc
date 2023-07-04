// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-06-2023
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionRepository.cs" company="Softo">
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
using LinqKit;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region InnovationOrganizationTrackOption IQueryable Extensions

    /// <summary>
    /// InnovationOrganizationTrackOptionIQueryableExtensions
    /// </summary>
    internal static class InnovationOrganizationTrackOptionIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationTrackOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByIds(this IQueryable<InnovationOrganizationTrackOption> query, List<int?> innovationOrganizationTrackOptionsIds)
        {
            if (innovationOrganizationTrackOptionsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ioto => innovationOrganizationTrackOptionsIds.Contains(ioto.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByUids(this IQueryable<InnovationOrganizationTrackOption> query, List<Guid?> innovationOrganizationTrackOptionsUids)
        {
            if (innovationOrganizationTrackOptionsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ioto => innovationOrganizationTrackOptionsUids.Contains(ioto.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> IsNotDeleted(this IQueryable<InnovationOrganizationTrackOption> query)
        {
            query = query.Where(ioto => !ioto.IsDeleted);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is active.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> IsActive(this IQueryable<InnovationOrganizationTrackOption> query)
        {
            query = query.Where(ioto => ioto.IsActive);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;InnovationOrganizationTrackOption&gt;.</returns>
        internal static IQueryable<InnovationOrganizationTrackOption> Order(this IQueryable<InnovationOrganizationTrackOption> query)
        {
            query = query.OrderBy(ioto => ioto.DisplayOrder);

            return query;
        }

        /// <summary>
        /// Finds the by attendee collaborator identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByAttendeeCollaboratorId(this IQueryable<InnovationOrganizationTrackOption> query, int attendeeCollaboratorId)
        {
            query = query.Where(ioto => ioto.AttendeeCollaboratorInnovationOrganizationTracks
                                                .Where(aciot => !aciot.IsDeleted)
                                                .Any(aciot => aciot.AttendeeCollaboratorId == attendeeCollaboratorId));

            return query;
        }

        /// <summary>
        /// Finds the by groups uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionGroupsUids">The innovation organization track option groups uids.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByGroupsUids(this IQueryable<InnovationOrganizationTrackOption> query, IEnumerable<Guid?> innovationOrganizationTrackOptionGroupsUids)
        {
            if (innovationOrganizationTrackOptionGroupsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(ioto => innovationOrganizationTrackOptionGroupsUids.Contains(ioto.InnovationOrganizationTrackOptionGroup.Uid));
            }

            return query;
        }

        /// <summary>
        /// Finds the by keywords.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<InnovationOrganizationTrackOption> FindByKeywords(this IQueryable<InnovationOrganizationTrackOption> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<InnovationOrganizationTrackOption>(false);
                var innerNameWhere = PredicateBuilder.New<InnovationOrganizationTrackOption>(true);

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
    }

    #endregion

    /// <summary>InnovationOrganizationTrackOptionRepository</summary>
    public class InnovationOrganizationTrackOptionRepository : Repository<PlataformaRio2CContext, InnovationOrganizationTrackOption>, IInnovationOrganizationTrackOptionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public InnovationOrganizationTrackOptionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<InnovationOrganizationTrackOption> GetBaseQuery(bool @readonly = false)
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
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation option ids.</param>
        /// <returns>InnovationOrganizationTrackOption.</returns>
        public InnovationOrganizationTrackOption FindById(int InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationTrackOptionIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUid">The innovation option uid.</param>
        /// <returns>InnovationOrganizationTrackOption.</returns>
        public InnovationOrganizationTrackOption FindByUid(Guid InnovationOrganizationTrackOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationTrackOptionUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<InnovationOrganizationTrackOption> FindByIdAsync(int InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { InnovationOrganizationTrackOptionIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUid">The innovation organization uid.</param>
        /// <returns>Task&lt;InnovationOrganizationTrackOption&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<InnovationOrganizationTrackOption> FindByUidAsync(Guid InnovationOrganizationTrackOptionUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { InnovationOrganizationTrackOptionUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByIdsAsync(List<int?> InnovationOrganizationTrackOptionIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(InnovationOrganizationTrackOptionIds)
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;InnovationOrganizationTrackOption&gt;&gt;.</returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByUidsAsync(List<Guid?> InnovationOrganizationTrackOptionUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(InnovationOrganizationTrackOptionUids)
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Find all as an asynchronous operation.
        /// </summary>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllAsync()
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
        public async Task<List<InnovationOrganizationTrackOptionDto>> FindAllDtoAsync()
        {
            var query = this.GetBaseQuery()
                            .IsActive()
                            .Order()
                            .Select(ioto => new InnovationOrganizationTrackOptionDto
                            {
                                InnovationOrganizationTrackOption = ioto,
                                InnovationOrganizationTrackOptionGroup = ioto.InnovationOrganizationTrackOptionGroup
                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by attendee collaborator identifier asynchronous.
        /// </summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByAttendeeCollaboratorIdAsync(int attendeeCollaboratorId)
        {
            var query = this.GetBaseQuery()
                            .FindByAttendeeCollaboratorId(attendeeCollaboratorId)
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by group uids asynchronous.
        /// </summary>
        /// <param name="InnovationOrganizationTrackOptionGroupsUids">The innovation organization track option group uids.</param>
        /// <returns></returns>
        public async Task<List<InnovationOrganizationTrackOption>> FindAllByGroupsUidsAsync(IEnumerable<Guid?> InnovationOrganizationTrackOptionGroupsUids)
        {
            var query = this.GetBaseQuery()
                            .FindByGroupsUids(InnovationOrganizationTrackOptionGroupsUids)
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
        public async Task<IPagedList<InnovationOrganizationTrackOptionDto>> FindAllByDataTable(
            int page, 
            int pageSize, 
            string keywords, 
            List<Tuple<string, string>> sortColumns)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                            .FindByKeywords(keywords)
                            .IsActive();

            var innovationOrganizationTrackOptionDtos = await query
                             .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("GroupName", "InnovationOrganizationTrackOptionGroup.Name")
                                },
                                new List<string> { "Name", "InnovationOrganizationTrackOptionGroup.Name", "CreateDate", "UpdateDate" },
                                "Name")
                             .Select(ioto => new InnovationOrganizationTrackOptionDto
                             {
                                 Id = ioto.Id,
                                 Uid = ioto.Uid,
                                 Name = ioto.Name,
                                 GroupName = ioto.InnovationOrganizationTrackOptionGroup.Name,
                                 CreateDate = ioto.CreateDate,
                                 UpdateDate = ioto.UpdateDate
                             })
                            .ToPagedListAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return innovationOrganizationTrackOptionDtos;
        }

        /// <summary>
        /// Counts all by data table.
        /// </summary>
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