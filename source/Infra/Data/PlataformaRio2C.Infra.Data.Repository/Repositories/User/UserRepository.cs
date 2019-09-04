// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-04-2019
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region User IQueryable Extensions

    /// <summary>
    /// UserIQueryableExtensions
    /// </summary>
    internal static class UserIQueryableExtensions
    {
        /// <summary>Finds the by identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal static IQueryable<User> FindById(this IQueryable<User> query, int userId)
        {
            query = query.Where(u => u.Id == userId);

            return query;
        }
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userUid">The user uid.</param>
        /// <returns></returns>
        internal static IQueryable<User> FindByUid(this IQueryable<User> query, Guid userUid)
        {
            query = query.Where(u => u.Uid == userUid);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<User> IsNotDeleted(this IQueryable<User> query)
        {
            query = query.Where(u => !u.IsDeleted);

            return query;
        }
    }

    #endregion

    /// <summary>UserRepository</summary>
    public class UserRepository : Repository<PlataformaRio2CContext, User>, IUserRepository
    {
        /// <summary>Initializes a new instance of the <see cref="UserRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public UserRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        private IQueryable<User> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Método que remove a entidade do Contexto</summary>
        /// <param name="entity">Entidade</param>
        public override void Delete(User entity)
        {
            entity.Roles.Clear();

            if (entity.UserUseTerms != null && entity.UserUseTerms.Any())
            {
                var terms = entity.UserUseTerms.ToList();

                foreach (var term in terms)
                {
                    _context.Entry(term).State = EntityState.Deleted;
                }
            }

            base.Delete(entity);
        }

        /// <summary>Finds the access control dto by user identifier and by edition identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public UserAccessControlDto FindAccessControlDtoByUserIdAndByEditionId(int userId, int editionId)
        {
            var query = this.GetBaseQuery()
                                    .FindById(userId);

            return query
                        .Select(u => new UserAccessControlDto
                        {
                            User = u,
                            Roles = u.Roles,
                            Collaborator = u.Collaborator,
                            Language = u.UserInterfaceLanguage,
                            EditionAttendeeCollaborator = u.Collaborator.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                            EditionAttendeeOrganizations = u.Collaborator.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                                                        .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted)
                                                                                        .Select(aoc => aoc.AttendeeOrganization)),
                            EditionAttendeeCollaboratorTickets = u.Collaborator.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeCollaboratorTickets
                                                                                        .Where(act => !act.IsDeleted)),
                            EditionUserTicketTypes = u.Collaborator.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeCollaboratorTickets
                                                                                        .Where(act => !act.IsDeleted 
                                                                                                      && !act.AttendeeSalesPlatformTicketType.IsDeleted 
                                                                                                      && !act.AttendeeSalesPlatformTicketType.TicketType.IsDeleted)
                                                                                        .Select(act => act.AttendeeSalesPlatformTicketType.TicketType))
                        })
                        .FirstOrDefault();
        }
    }
}