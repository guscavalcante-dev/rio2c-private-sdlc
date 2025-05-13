// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="CreatorProjectRepository.cs" company="Softo">
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
    #region CreatorProject IQueryable Extensions

    /// <summary>
    /// CreatorProjectIQueryableExtensions
    /// </summary>
    internal static class CreatorProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="creatorProjectIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;CreatorProject&gt;.</returns>
        internal static IQueryable<CreatorProject> FindByIds(this IQueryable<CreatorProject> query, List<int?> creatorProjectIds)
        {
            if (creatorProjectIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => creatorProjectIds.Contains(io.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="creatorProjectUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<CreatorProject> FindByUids(this IQueryable<CreatorProject> query, List<Guid?> creatorProjectUids)
        {
            if (creatorProjectUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => creatorProjectUids.Contains(io.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CreatorProject> IsNotDeleted(this IQueryable<CreatorProject> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by title.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        internal static IQueryable<CreatorProject> FindByTitle(this IQueryable<CreatorProject> query, string title)
        {
            return query.Where(cp => cp.Title == title);
        }
    }

    #endregion

    /// <summary>CreatorProjectRepository</summary>
    public class CreatorProjectRepository : Repository<PlataformaRio2CContext, CreatorProject>, ICreatorProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CreatorProjectRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the base query.
        /// </summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<CreatorProject> GetBaseQuery(bool @readonly = false)
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
        /// <param name="creatorProjectIds">The creator project ids.</param>
        /// <returns></returns>
        public CreatorProjectDto FindDtoById(int creatorProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { creatorProjectIds })
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="creatorProjectUid">The creator project uid.</param>
        /// <returns></returns>
        public CreatorProjectDto FindDtoByUid(Guid creatorProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { creatorProjectUid })
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="creatorProjectIds">The creator project ids.</param>
        /// <returns></returns>
        public async Task<CreatorProjectDto> FindDtoByIdAsync(int creatorProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { creatorProjectIds })
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="creatorProjectUid">The creator project uid.</param>
        /// <returns></returns>
        public async Task<CreatorProjectDto> FindDtoByUidAsync(Guid creatorProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { creatorProjectUid })
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by ids asynchronous.
        /// </summary>
        /// <param name="creatorProjectIds">The creator project ids.</param>
        /// <returns></returns>
        public async Task<List<CreatorProjectDto>> FindAllByIdsAsync(List<int?> creatorProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(creatorProjectIds)
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by uids asynchronous.
        /// </summary>
        /// <param name="creatorProjectUids">The creator project uids.</param>
        /// <returns></returns>
        public async Task<List<CreatorProjectDto>> FindAllByUidsAsync(List<Guid?> creatorProjectUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(creatorProjectUids)
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<CreatorProjectDto>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the by title asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public async Task<CreatorProjectDto> FindDtoByTitleAsync(string title)
        {
            var query = this.GetBaseQuery()
                            .FindByTitle(title)
                            .Select(cp => new CreatorProjectDto
                            {

                            });

            return await query.FirstOrDefaultAsync();
        }
    }
}