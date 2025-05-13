// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectRepository.cs" company="Softo">
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
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region CartoonProject IQueryable Extensions

    /// <summary>
    /// CartoonProjectIQueryableExtensions
    /// </summary>
    internal static class CartoonProjectIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cartoonProjectFormatsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;CartoonProject&gt;.</returns>
        internal static IQueryable<CartoonProject> FindByIds(this IQueryable<CartoonProject> query, List<int?> cartoonProjectFormatsIds)
        {
            if (cartoonProjectFormatsIds?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => cartoonProjectFormatsIds.Contains(io.Id));
            }

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="cartoonProjectFormatsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<CartoonProject> FindByUids(this IQueryable<CartoonProject> query, List<Guid?> cartoonProjectFormatsUids)
        {
            if (cartoonProjectFormatsUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(io => cartoonProjectFormatsUids.Contains(io.Uid));
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CartoonProject> IsNotDeleted(this IQueryable<CartoonProject> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;CartoonProject&gt;.</returns>
        internal static IQueryable<CartoonProject> Order(this IQueryable<CartoonProject> query)
        {
            //query = query.OrderBy(io => io.DisplayOrder);

            return query;
        }

        /// <summary>
        /// Finds the by title.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        internal static IQueryable<CartoonProject> FindByTitle(this IQueryable<CartoonProject> query, string title)
        {
            return query.Where(cp => cp.Title == title);
        }
    }

    #endregion

    /// <summary>CartoonProjectRepository</summary>
    public class CartoonProjectRepository : Repository<PlataformaRio2CContext, CartoonProject>, ICartoonProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CartoonProjectRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<CartoonProject> GetBaseQuery(bool @readonly = false)
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
        /// <param name="CartoonProjectIds">The cartoon project ids.</param>
        /// <returns></returns>
        public CartoonProject FindById(int CartoonProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { CartoonProjectIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="CartoonProjectUid">The cartoon project uid.</param>
        /// <returns></returns>
        public CartoonProject FindByUid(Guid CartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { CartoonProjectUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="CartoonProjectIds">The cartoon project ids.</param>
        /// <returns></returns>
        public async Task<CartoonProject> FindByIdAsync(int CartoonProjectIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { CartoonProjectIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="cartoonProjectFormatUid">The cartoon project format uid.</param>
        /// <returns></returns>
        public async Task<CartoonProject> FindByUidAsync(Guid cartoonProjectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { cartoonProjectUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds all by ids asynchronous.
        /// </summary>
        /// <param name="cartoonProjectFormatIds">The cartoon project format ids.</param>
        /// <returns></returns>
        public async Task<List<CartoonProject>> FindAllByIdsAsync(List<int?> cartoonProjectFormatIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(cartoonProjectFormatIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all by uids asynchronous.
        /// </summary>
        /// <param name="cartoonProjectFormatUids">The cartoon project format uids.</param>
        /// <returns></returns>
        public async Task<List<CartoonProject>> FindAllByUidsAsync(List<Guid?> cartoonProjectFormatUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(cartoonProjectFormatUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartoonProject>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the by title asynchronous.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public async Task<CartoonProject> FindByTitleAsync(string title)
        {
            var query = this.GetBaseQuery()
                            .FindByTitle(title);

            return await query.FirstOrDefaultAsync();
        }
    }
}