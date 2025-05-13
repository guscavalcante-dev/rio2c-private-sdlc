// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="LanguageRepository.cs" company="Softo">
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
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region CollaboratorGender IQueryable Extensions

    /// <summary>
    /// LanguageIQueryableExtensions
    /// </summary>
    internal static class CollaboratorGenderIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorGender> IsNotDeleted(this IQueryable<CollaboratorGender> query)
        {
            query = query.Where(l => !l.IsDeleted);

            return query;
        }

        /// <summary>Orders the specified query.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorGender> Order(this IQueryable<CollaboratorGender> query)
        {
            query = query.OrderBy(l => l.Name);

            return query;
        }
    }

    #endregion

    /// <summary>LanguageRepository</summary>
    public class CollaboratorGenderRepository : Repository<PlataformaRio2CContext, CollaboratorGender>, ICollaboratorGenderRepository
    {
        public CollaboratorGenderRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<CollaboratorGender> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<CollaboratorGender>> FindAllAsync(bool? showInactive = false)
        {
            var query = this.GetBaseQuery()
                                .IsNotDeleted()
                                .Order();

            return await query.ToListAsync();
        }
    }
}