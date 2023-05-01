// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="AttendeeNegotiationCollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region AttendeeNegotiationCollaborator IQueryable Extensions

    /// <summary>
    /// AttendeeNegotiationCollaboratorIQueryableExtensions
    /// </summary>
    internal static class AttendeeNegotiationCollaboratorIQueryableExtensions
    {
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeNegotiationCollaborator> IsNotDeleted(this IQueryable<AttendeeNegotiationCollaborator> query)
        {
            query = query.Where(n => !n.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeNegotiationCollaboratorRepository</summary>
    public class AttendeeNegotiationCollaboratorRepository : Repository<PlataformaRio2CContext, AttendeeNegotiationCollaborator>, IAttendeeNegotiationCollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeNegotiationCollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeNegotiationCollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeNegotiationCollaborator> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}