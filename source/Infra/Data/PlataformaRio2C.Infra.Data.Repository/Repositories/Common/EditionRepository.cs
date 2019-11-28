// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="EditionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Edition IQueryable Extensions

    /// <summary>
    /// EditionIQueryableExtensions
    /// </summary>
    internal static class EditionIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> FindByUid(this IQueryable<Edition> query, Guid editionUid)
        {
            query = query.Where(e => e.Uid == editionUid);

            return query;
        }

        /// <summary>Determines whether the specified show inactive is active.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        internal static IQueryable<Edition> IsActive(this IQueryable<Edition> query, bool showInactive)
        {
            if (!showInactive)
            {
                query = query.Where(e => e.IsActive);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Edition> IsNotDeleted(this IQueryable<Edition> query)
        {
            query = query.Where(e => !e.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>EditionRepository</summary>
    public class EditionRepository : Repository<PlataformaRio2CContext, Edition>, IEditionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="EditionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public EditionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Edition> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public async Task<Edition> FindByUidAsync(Guid editionUid, bool showInactive)
        {
            var query = this.GetBaseQuery()
                                .IsActive(showInactive)
                                .FindByUid(editionUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by is active.</summary>
        /// <param name="showInactive">if set to <c>true</c> [show inactive].</param>
        /// <returns></returns>
        public List<Edition> FindAllByIsActive(bool showInactive)
        {
            var query = this.GetBaseQuery()
                                .IsActive(showInactive);

            return query.ToList();
        }

        #region Old Methods

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Edition> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        #endregion
    }
}