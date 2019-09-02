// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SentEmailRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Sent Email IQueryable Extensions

    /// <summary>
    /// SentEmailIQueryableExtensions
    /// </summary>
    internal static class SentEmailIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sentEmailUid">The sent email uid.</param>
        /// <returns></returns>
        internal static IQueryable<SentEmail> FindByUid(this IQueryable<SentEmail> query, Guid sentEmailUid)
        {
            query = query.Where(se => se.Uid == sentEmailUid);

            return query;
        }
    }

    #endregion

    /// <summary>SentEmailRepository</summary>
    public class SentEmailRepository : Repository<PlataformaRio2CContext, SentEmail>, ISentEmailRepository
    {
        /// <summary>Initializes a new instance of the <see cref="SentEmailRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public SentEmailRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<SentEmail> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}