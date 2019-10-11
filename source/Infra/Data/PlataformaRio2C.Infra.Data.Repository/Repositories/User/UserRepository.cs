// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-10-2019
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

        /// <summary>Finds the admin access control dto by user identifier and by edition identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public AdminAccessControlDto FindAdminAccessControlDtoByUserIdAndByEditionId(int userId, int editionId)
        {
            var query = this.GetBaseQuery()
                                 .FindById(userId);

            return query
                        .Select(u => new AdminAccessControlDto
                        {
                            User = u,
                            Roles = u.Roles,
                            Language = u.UserInterfaceLanguage,
                            Collaborator = u.Collaborator,
                            EditionCollaboratorTypes = u.Collaborator.AttendeeCollaborators
                                                                            .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                            .SelectMany(ac => ac.AttendeeCollaboratorTypes
                                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                                        .Select(act => act.CollaboratorType)),
                        })
                        .FirstOrDefault();
        }

        /// <summary>Finds the user access control dto by user identifier and by edition identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public UserAccessControlDto FindUserAccessControlDtoByUserIdAndByEditionId(int userId, int editionId)
        {
            var query = this.GetBaseQuery()
                                    .FindById(userId);

            return query
                        .Select(u => new UserAccessControlDto
                        {
                            User = u,
                            Roles = u.Roles,
                            Language = u.UserInterfaceLanguage,
                            Collaborator = u.Collaborator,
                            EditionAttendeeCollaborator = u.Collaborator.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                            EditionCollaboratorTypes = u.Collaborator.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                        .Select(act => act.CollaboratorType)),
                            EditionAttendeeOrganizations = u.Collaborator.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                                                        .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted)
                                                                                        .Select(aoc => aoc.AttendeeOrganization)).OrderBy(ao => ao.CreateDate),
                            JobTitlesDtos = u.Collaborator.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
                            {
                                Id = d.Id,
                                Uid = d.Uid,
                                Value = d.Value,
                                LanguageDto = new LanguageBaseDto
                                {
                                    Id = d.Language.Id,
                                    Uid = d.Language.Uid,
                                    Name = d.Language.Name,
                                    Code = d.Language.Code
                                }
                            }),
                            EditionAttendeeCollaboratorTickets = u.Collaborator.AttendeeCollaborators
                                                                    .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                    .SelectMany(ac => ac.AttendeeCollaboratorTickets
                                                                                        .Where(act => !act.IsDeleted && !act.AttendeeSalesPlatformTicketType.IsDeleted)
                                                                                        .Select(act => new AttendeeCollaboratorTicketDto
                                                                                        {
                                                                                            AttendeeCollaboratorTicket = act,
                                                                                            AttendeeSalesPlatformTicketType = act.AttendeeSalesPlatformTicketType
                                                                                        }))
                        })
                        .FirstOrDefault();
        }

        /// <summary>Finds the admin access control dto by user identifier and by edition identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public UserLanguageDto FindUserLanguageByUserId(int userId)
        {
            var query = this.GetBaseQuery()
                                 .FindById(userId);

            return query
                        .Select(u => new UserLanguageDto
                        {
                            User = u,
                            Language = u.UserInterfaceLanguage
                        })
                        .FirstOrDefault();
        }
    }
}