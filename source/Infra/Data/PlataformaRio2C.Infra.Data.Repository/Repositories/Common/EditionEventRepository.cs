// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-05-2020
// ***********************************************************************
// <copyright file="EditionEventRepository.cs" company="Softo">
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
    #region Edition Event IQueryable Extensions

    /// <summary>
    /// EditionEventIQueryableExtensions
    /// </summary>
    internal static class EditionEventIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByUid(this IQueryable<EditionEvent> query, Guid editionEventUid)
        {
            query = query.Where(ee => ee.Uid == editionEventUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> FindByEditionId(this IQueryable<EditionEvent> query, int editionId)
        {
            query = query.Where(ee => ee.Edition.Id == editionId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<EditionEvent> IsNotDeleted(this IQueryable<EditionEvent> query)
        {
            query = query.Where(ee => !ee.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>EditionEventRepository</summary>
    public class EditionEventRepository : Repository<PlataformaRio2CContext, EditionEvent>, IEditionEventRepository
    {
        /// <summary>Initializes a new instance of the <see cref="EditionEventRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public EditionEventRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<EditionEvent> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="editionEventUid">The edition event uid.</param>
        /// <returns></returns>
        public async Task<EditionEvent> FindByUidAsync(Guid editionEventUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(editionEventUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by edition identifier asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<EditionEvent>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }
    }
}