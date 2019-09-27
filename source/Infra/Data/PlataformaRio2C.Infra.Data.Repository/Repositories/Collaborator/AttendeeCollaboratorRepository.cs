// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorRepository.cs" company="Softo">
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
    #region Attendee Collaborator IQueryable Extensions

    /// <summary>
    /// AttendeeeCollaboratorIQueryableExtensions
    /// </summary>
    internal static class AttendeeeCollaboratorIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByUid(this IQueryable<AttendeeCollaborator> query, Guid attendeeCollaboratorUid)
        {
            query = query.Where(ac => ac.Uid == attendeeCollaboratorUid);

            return query;
        }

        /// <summary>Finds the by edition uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByEditionUid(this IQueryable<AttendeeCollaborator> query, Guid editionUid)
        {
            query = query.Where(ac => ac.Edition.Uid == editionUid);

            return query;
        }

        /// <summary>Finds the by user identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByUserId(this IQueryable<AttendeeCollaborator> query, int userId)
        {
            query = query.Where(ac => ac.Collaborator.User.Id == userId);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> IsNotDeleted(this IQueryable<AttendeeCollaborator> query)
        {
            query = query.Where(ac => !ac.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>AttendeeCollaboratorRepository</summary>
    public class AttendeeCollaboratorRepository : Repository<PlataformaRio2CContext, AttendeeCollaborator>, IAttendeeCollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeCollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeCollaborator> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }
    }
}