// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-16-2021
// ***********************************************************************
// <copyright file="OrganizationTypeRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region OrganizationType IQueryable Extensions

    /// <summary>
    /// OrganizationTypeIQueryableExtensions
    /// </summary>
    internal static class OrganizationTypeIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        internal static IQueryable<OrganizationType> FindByUid(this IQueryable<OrganizationType> query, Guid organizationTypeUid)
        {
            query = query.Where(o => o.Uid == organizationTypeUid);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<OrganizationType> FindByKeywords(this IQueryable<OrganizationType> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<OrganizationType>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(h => h.Name.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<OrganizationType> IsNotDeleted(this IQueryable<OrganizationType> query)
        {
            query = query.Where(ot => !ot.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeName">Name of the organization type.</param>
        /// <returns></returns>
        internal static IQueryable<OrganizationType> FindByName(this IQueryable<OrganizationType> query, string organizationTypeName)
        {
            query = query.Where(ct => ct.Name == organizationTypeName);

            return query;
        }
    }

    #endregion

    /// <summary>OrganizationTypeRepository</summary>
    public class OrganizationTypeRepository : Repository<PlataformaRio2CContext, OrganizationType>, IOrganizationTypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationTypeRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public OrganizationTypeRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<OrganizationType> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();

            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        public async Task<OrganizationType> FindByUidAsync(Guid organizationTypeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(organizationTypeUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="organizationTypeName">Name of the organization type.</param>
        /// <returns></returns>
        public async Task<OrganizationType> FindByNameAsync(string organizationTypeName)
        {
            var query = this.GetBaseQuery()
                                .FindByName(organizationTypeName);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<OrganizationType> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;
            //.Include(i => i.Descriptions)
            //.Include(i => i.Descriptions.Select(t => t.Language));

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}