// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-30-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="AttendeeCartoonProjectRepository.cs" company="Softo">
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
    #region AttendeeCartoonProject IQueryable Extensions

    /// <summary>
    /// AttendeeCartoonProjectIQueryableExtensions
    /// </summary>
    internal static class AttendeeCartoonProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCartoonProjectIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;AttendeeCartoonProject&gt;.</returns>
        internal static IQueryable<AttendeeCartoonProject> FindByIds(this IQueryable<AttendeeCartoonProject> query, List<int?> attendeeCartoonProjectIds)
        {
            if (attendeeCartoonProjectIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeCartoonProjectIds.Contains(aio.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCartoonProjectUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByUids(this IQueryable<AttendeeCartoonProject> query, List<Guid?> attendeeCartoonProjectUids)
        {
            if (attendeeCartoonProjectUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(aio => attendeeCartoonProjectUids.Contains(aio.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> IsNotDeleted(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.Where(acp => !acp.CartoonProject.IsDeleted && !acp.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by document.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="title">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>IQueryable&lt;AttendeeCartoonProject&gt;.</returns>
        internal static IQueryable<AttendeeCartoonProject> FindByTitleAndEditionId(this IQueryable<AttendeeCartoonProject> query, string title, int editionId)
        {
            query = query.Where(acp => acp.CartoonProject.Title == title
                                        && acp.EditionId == editionId);

            return query;
        }

        /// <summary>
        /// Finds the by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByEditionId(this IQueryable<AttendeeCartoonProject> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(aio => (showAllEditions || aio.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByKeywords(this IQueryable<AttendeeCartoonProject> query, string keywords)
        {
            //if (!string.IsNullOrEmpty(keywords))
            //{
            //    var outerWhere = PredicateBuilder.New<AttendeeCartoonProject>(false);
            //    var innerInnovationOrganizationNameWhere = PredicateBuilder.New<AttendeeCartoonProject>(true);
            //    var innerInnovationOrganizationServiceNameWhere = PredicateBuilder.New<AttendeeCartoonProject>(true);

            //    foreach (var keyword in keywords.Split(' '))
            //    {
            //        if (!string.IsNullOrEmpty(keyword))
            //        {
            //            innerInnovationOrganizationNameWhere = innerInnovationOrganizationNameWhere.Or(aio => aio.InnovationOrganization.Name.Contains(keyword));
            //            innerInnovationOrganizationServiceNameWhere = innerInnovationOrganizationServiceNameWhere.Or(aio => aio.InnovationOrganization.ServiceName.Contains(keyword));
            //        }
            //    }

            //    outerWhere = outerWhere.Or(innerInnovationOrganizationNameWhere);
            //    outerWhere = outerWhere.Or(innerInnovationOrganizationServiceNameWhere);
            //    query = query.Where(outerWhere);
            //}

            return query;
        }

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> FindByIsEvaluated(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.Where(aio => aio.Grade != null);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCartoonProject> Order(this IQueryable<AttendeeCartoonProject> query)
        {
            query = query.OrderBy(mp => mp.CreateDate);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeCartoonProjectRepository</summary>
    public class AttendeeCartoonProjectRepository : Repository<PlataformaRio2CContext, AttendeeCartoonProject>, IAttendeeCartoonProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeCartoonProjectRepository(
            PlataformaRio2CContext context,
            IEditionRepository editionRepository
            )
            : base(context)
        {
            this.editioRepo = editionRepository;
        }

        #region Private Methods

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeCartoonProject> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all attendee innovation organizations asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="innovationOrganizationTrackOptionUid">The innovation organization track option uid.</param>
        /// <returns></returns>
        private async Task<List<AttendeeCartoonProject>> FindAllAttendeeCartoonProjectsAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> innovationOrganizationTrackOptionUids)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId);
                                //.FindByKeywords(searchKeywords)
                                //.FindByInnovationOrganizationTrackOptionUids(innovationOrganizationTrackOptionUids);

            return await query
                            .Order()
                            .ToListAsync();
        }

        #endregion

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeCartoonProject&gt;&gt;.</returns>
        public async Task<AttendeeCartoonProject> FindByIdAsync(int attendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { attendeeInnovationOrganizationIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUid">The innovation organization uid.</param>
        /// <returns>Task&lt;AttendeeCartoonProject&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AttendeeCartoonProject> FindByUidAsync(Guid attendeeInnovationOrganizationUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { attendeeInnovationOrganizationUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeCartoonProject&gt;&gt;.</returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByIdsAsync(List<int?> attendeeInnovationOrganizationIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(attendeeInnovationOrganizationIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="attendeeInnovationOrganizationUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;AttendeeCartoonProject&gt;&gt;.</returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByUidsAsync(List<Guid?> attendeeInnovationOrganizationUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(attendeeInnovationOrganizationUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by document and edition identifier as an asynchronous operation.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns>Task&lt;AttendeeCartoonProject&gt;.</returns>
        public async Task<AttendeeCartoonProject> FindByTitleAndEditionIdAsync(string document, int editionId)
        {
            var query = this.GetBaseQuery()
                           .FindByTitleAndEditionId(document, editionId);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all approved attendee innovation organizations ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedAttendeeCartoonProjectsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(aio => aio.Grade)
                            .Take(edition.InnovationCommissionMaximumApprovedCompaniesCount)
                            .Select(aio => aio.Id)
                            .ToArrayAsync();
        }

        /// <summary>Counts the asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountAsync(int editionId, bool showAllEditions = false)

        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions);

            return await query.CountAsync();
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCartoonProject>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }
    }
}