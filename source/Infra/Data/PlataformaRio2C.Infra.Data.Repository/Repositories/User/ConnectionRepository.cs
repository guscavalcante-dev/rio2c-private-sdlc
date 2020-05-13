// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 05-13-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 05-13-2020
// ***********************************************************************
// <copyright file="ConnectionRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Connection IQueryable Extensions

    /// <summary>ConnectionIQueryableExtensions</summary>
    internal static class ConnectionIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="connectionId">The connection identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Connection> FindByUid(this IQueryable<Connection> query, Guid connectionId)
        {
            query = query.Where(c => c.Uid == connectionId);

            return query;
        }

        /// <summary>Finds the name of the by user.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        internal static IQueryable<Connection> FindByUserName(this IQueryable<Connection> query, string userName)
        {
            query = query.Where(c => c.User.UserName == userName);

            return query;
        }
    }

    #endregion

    /// <summary>ConnectionRepository</summary>
    public class ConnectionRepository : Repository<PlataformaRio2CContext, Connection>, IConnectionRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ConnectionRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ConnectionRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Connection> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet;

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all connected by user name asynchronous.</summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public Task<List<Connection>> FindAllConnectedByUserNameAsync(string userName)
        {
            var query = this.GetBaseQuery()
                .FindByUserName(userName);

            return query
                .ToListAsync();
        }
    }
}