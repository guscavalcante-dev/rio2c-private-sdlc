// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-28-2020
// ***********************************************************************
// <copyright file="MusicGenreRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Music Genre IQueryable Extensions

    /// <summary>MusicGenreIQueryableExtensions</summary>
    internal static class MusicGenreIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="musicGenreUid">The music genre uid.</param>
        /// <returns></returns>
        internal static IQueryable<MusicGenre> FindByUid(this IQueryable<MusicGenre> query, Guid musicGenreUid)
        {
            query = query.Where(mg => mg.Uid == musicGenreUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicGenre> IsNotDeleted(this IQueryable<MusicGenre> query)
        {
            query = query.Where(mg => !mg.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<MusicGenre> Order(this IQueryable<MusicGenre> query)
        {
            query = query.OrderBy(mg => mg.DisplayOrder);

            return query;
        }
    }

    #endregion

    /// <summary>MusicGenreRepository</summary>
    public class MusicGenreRepository : Repository<Context.PlataformaRio2CContext, MusicGenre>, IMusicGenreRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MusicGenreRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MusicGenreRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<MusicGenre> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<MusicGenre>> FindAllAsync()
        {
            var query = this.GetBaseQuery();

            return await query
                            .Order()
                            .ToListAsync();
        }
    }
}