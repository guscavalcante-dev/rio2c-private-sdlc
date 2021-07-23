// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 09-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-22-2021
// ***********************************************************************
// <copyright file="CollaboratorTypeRepository.cs" company="Softo">
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
    #region Collaborator Type IQueryable Extensions

    /// <summary>
    /// CollaboratorTypeIQueryableExtensions
    /// </summary>
    internal static class CollaboratorTypeIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorType> FindByUid(this IQueryable<CollaboratorType> query, Guid collaboratorTypeUid)
        {
            query = query.Where(ct => ct.Uid == collaboratorTypeUid);

            return query;
        }

        /// <summary>
        /// Finds the name of the by.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorType> FindByName(this IQueryable<CollaboratorType> query, string collaboratorTypeName)
        {
            query = query.Where(ct => ct.Name == collaboratorTypeName);

            return query;
        }

        /// <summary>
        /// Finds the by names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorType> FindByNames(this IQueryable<CollaboratorType> query, string[] collaboratorTypeNames)
        {
            if (collaboratorTypeNames == null)
            {
                collaboratorTypeNames = new string[] {};
            }

            query = query.Where(ct => collaboratorTypeNames.Contains(ct.Name));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorType> IsNotDeleted(this IQueryable<CollaboratorType> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>
        /// Determines whether this instance is admin.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<CollaboratorType> IsAdmin(this IQueryable<CollaboratorType> query)
        {
            query = query.Where(ct => Constants.Role.AnyAdminArray.Contains(ct.Role.Name));

            return query;
        }
    }

    #endregion

    /// <summary> CollaboratorTypeRepository </summary>
    public class CollaboratorTypeRepository : Repository<PlataformaRio2CContext, CollaboratorType>, ICollaboratorTypeRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CollaboratorTypeRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<CollaboratorType> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by name asunc.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        public async Task<CollaboratorType> FindByNameAsync(string collaboratorTypeName)
        {
            var query = this.GetBaseQuery()
                                .FindByName(collaboratorTypeName);

            return await query
                            .Include(ct => ct.Role)
                            .FirstOrDefaultAsync();
        }

        public async Task<List<CollaboratorType>> FindAllByNamesAsync(string[] collaboratorTypeNames)
        {
            var query = this.GetBaseQuery()
                                .FindByNames(collaboratorTypeNames);

            return await query
                            .Include(ct => ct.Role)
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all admin collaborator types asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<CollaboratorType>> FindAllAdminsAsync()
        {
            var query = this.GetBaseQuery()
                            .IsAdmin();

            return await query
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all admin collaborator types.
        /// </summary>
        /// <returns></returns>
        public List<CollaboratorType> FindAllAdmins()
        {
            var query = this.GetBaseQuery()
                            .IsAdmin();

            return query.ToList();
        }
    }
}