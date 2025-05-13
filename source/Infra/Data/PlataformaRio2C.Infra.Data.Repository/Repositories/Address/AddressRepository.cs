// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-08-2019
// ***********************************************************************
// <copyright file="AddressRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Address IQueryable Extensions

    /// <summary>AddressIQueryableExtensions</summary>
    internal static class AddressIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="addressUid">The address uid.</param>
        /// <returns></returns>
        internal static IQueryable<Address> FindByUid(this IQueryable<Address> query, Guid addressUid)
        {
            query = query.Where(a => a.Uid == addressUid);

            return query;
        }

        /// <summary>Finds the by organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Address> FindByOrganizationUid(this IQueryable<Address> query, Guid organizationUid)
        {
            query = query.Where(a => a.Organizations.Any(o => o.Uid == organizationUid));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Address> IsNotDeleted(this IQueryable<Address> query)
        {
            query = query.Where(a => !a.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AddressRepository</summary>
    public class AddressRepository : Repository<PlataformaRio2CContext, Address>, IAddressRepository
    {
        public AddressRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Address> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}
