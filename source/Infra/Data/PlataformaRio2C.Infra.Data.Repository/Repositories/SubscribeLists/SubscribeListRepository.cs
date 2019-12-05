// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-05-2019
// ***********************************************************************
// <copyright file="SubscribeListRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Subscribe List IQueryable Extensions

    /// <summary>
    /// SubscribeListIQueryableExtensions
    /// </summary>
    internal static class SubscribeListIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<SubscribeList> IsNotDeleted(this IQueryable<SubscribeList> query)
        {
            query = query.Where(o => !o.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>SubscribeListRepository</summary>
    public class SubscribeListRepository : Repository<PlataformaRio2CContext, SubscribeList>, ISubscribeListRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SubscribeListRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SubscribeListRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<SubscribeList> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all subscribe lists asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<SubscribeList>> FindAllSubscribeListsAsync()
        {
            var query = this.GetBaseQuery();

            return await query.ToListAsync();
        }
    }
}