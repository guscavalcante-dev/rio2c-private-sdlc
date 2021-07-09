// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-09-2021
// ***********************************************************************
// <copyright file="AdministratorsController.cs" company="Softo">
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
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Role IQueryable Extensions

    /// <summary>
    /// RoleIQueryableExtensions
    /// </summary>
    internal static class RoleIQueryableExtensions
    {
        /// <summary>
        /// Finds the by uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="roleUid">The role uid.</param>
        /// <returns></returns>
        internal static IQueryable<Role> FindByUid(this IQueryable<Role> query, Guid roleUid)
        {
            query = query.Where(r => r.Uid == roleUid);

            return query;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal static IQueryable<Role> FindByName(this IQueryable<Role> query, string name)
        {
            query = query.Where(r => r.Name == name);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is admin.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Role> IsAdmin(this IQueryable<Role> query)
        {
            query = query.Where(r => Constants.Role.AnyAdminArray.Contains(r.Name));

            return query;
        }
    }

    #endregion

    /// <summary>
    /// RoleRepository
    /// </summary>
    public class RoleRepository : Repository<PlataformaRio2CContext, Role>, IRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public RoleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the base query.
        /// </summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Role> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>
        /// Finds all admin roles asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<List<Role>> FindAllAdminRolesAsync()
        {
            var query = this.GetBaseQuery()
                           .IsAdmin();

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all admin roles.
        /// </summary>
        /// <returns></returns>
        public List<Role> FindAllAdminRoles()
        {
            var query = this.GetBaseQuery()
                           .IsAdmin();

            return query.ToList();
        }

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        public async Task<List<Role>> FindByNameAsync(string roleName)
        {
            var query = this.GetBaseQuery()
                           .FindByName(roleName);

            return await query
                            .ToListAsync();
        }
    }
}
