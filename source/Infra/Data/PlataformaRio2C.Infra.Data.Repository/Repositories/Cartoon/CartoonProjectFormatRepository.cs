// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-25-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-25-2022
// ***********************************************************************
// <copyright file="CartoonProjectFormatRepository.cs" company="Softo">
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
    #region CartoonProjectFormat IQueryable Extensions

    /// <summary>
    /// CartoonProjectFormatIQueryableExtensions
    /// </summary>
    internal static class CartoonProjectFormatIQueryableExtensions
    {
        /// <summary>
        /// Finds the by ids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="cartoonProjectFormatsIds">The innovation organizations ids.</param>
        /// <returns>IQueryable&lt;CartoonProjectFormat&gt;.</returns>
        internal static IQueryable<CartoonProjectFormat> FindByIds(this IQueryable<CartoonProjectFormat> query, List<int?> cartoonProjectFormatsIds)
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
        internal static IQueryable<CartoonProjectFormat> FindByUids(this IQueryable<CartoonProjectFormat> query, List<Guid?> cartoonProjectFormatsUids)
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
        internal static IQueryable<CartoonProjectFormat> IsNotDeleted(this IQueryable<CartoonProjectFormat> query)
        {
            query = query.Where(io => !io.IsDeleted);

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>IQueryable&lt;CartoonProjectFormat&gt;.</returns>
        internal static IQueryable<CartoonProjectFormat> Order(this IQueryable<CartoonProjectFormat> query)
        {
            query = query.OrderBy(io => io.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>CartoonProjectFormatRepository</summary>
    public class CartoonProjectFormatRepository : Repository<PlataformaRio2CContext, CartoonProjectFormat>, ICartoonProjectFormatRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CartoonProjectFormatRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<CartoonProjectFormat> GetBaseQuery(bool @readonly = false)
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
        /// <param name="CartoonProjectFormatIds">The innovation option ids.</param>
        /// <returns>CartoonProjectFormat.</returns>
        public CartoonProjectFormat FindById(int CartoonProjectFormatIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { CartoonProjectFormatIds });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="CartoonProjectFormatUid">The innovation option uid.</param>
        /// <returns>CartoonProjectFormat.</returns>
        public CartoonProjectFormat FindByUid(Guid CartoonProjectFormatUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { CartoonProjectFormatUid });

            return query.FirstOrDefault();
        }

        /// <summary>
        /// find by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="CartoonProjectFormatIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;CartoonProjectFormat&gt;&gt;.</returns>
        public async Task<CartoonProjectFormat> FindByIdAsync(int CartoonProjectFormatIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(new List<int?> { CartoonProjectFormatIds });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="cartoonProjectFormatUid">The innovation organization uid.</param>
        /// <returns>Task&lt;CartoonProjectFormat&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CartoonProjectFormat> FindByUidAsync(Guid cartoonProjectFormatUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(new List<Guid?> { cartoonProjectFormatUid });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="cartoonProjectFormatIds">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;CartoonProjectFormat&gt;&gt;.</returns>
        public async Task<List<CartoonProjectFormat>> FindAllByIdsAsync(List<int?> cartoonProjectFormatIds)
        {
            var query = this.GetBaseQuery()
                            .FindByIds(cartoonProjectFormatIds);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find by ids as an asynchronous operation.
        /// </summary>
        /// <param name="cartoonProjectFormatUids">The innovation organization ids.</param>
        /// <returns>Task&lt;List&lt;CartoonProjectFormat&gt;&gt;.</returns>
        public async Task<List<CartoonProjectFormat>> FindAllByUidsAsync(List<Guid?> cartoonProjectFormatUids)
        {
            var query = this.GetBaseQuery()
                            .FindByUids(cartoonProjectFormatUids);

            return await query.ToListAsync();
        }

        /// <summary>
        /// find all as an asynchronous operation.
        /// </summary>
        /// <returns>Task&lt;List&lt;CartoonProjectFormat&gt;&gt;.</returns>
        public async Task<List<CartoonProjectFormat>> FindAllAsync()
        {
            var query = this.GetBaseQuery()
                            .Order();

            return await query.ToListAsync();
        }
    }
}