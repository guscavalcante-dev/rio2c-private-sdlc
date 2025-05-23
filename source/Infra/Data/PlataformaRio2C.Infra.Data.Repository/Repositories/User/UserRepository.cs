﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>Finds by userName.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        internal static IQueryable<User> FindByUserName(this IQueryable<User> query, string userName)
        {
            query = query.Where(u => u.UserName == userName);

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

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<User> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                    .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the by identifier asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<User> FindByIdAsync(int userId)
        {
            var query = this.GetBaseQuery()
                                .FindById(userId);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the by identifier asynchronous.</summary>
        /// <param name="userUid">The user identifier.</param>
        /// <returns></returns>
        public async Task<User> FindByUidAsync(Guid userUid)
        {
            var query = this.GetBaseQuery()
                                 .FindByUid(userUid);

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds by user name asynchronous.</summary>
        /// <param name="userName">The user name.</param>
        /// <returns></returns>
        public Task<User> FindByUserNameAsync(string userName)
        {
            var query = this.GetBaseQuery()
                                .FindByUserName(userName);

            return query
                .FirstOrDefaultAsync();
        }

        /// <summary>Finds the user dto by user e-mail and uid asynchronous.</summary>
        /// <param name="userEmail">The userEmai.</param>
        /// <param name="uid">The uid.</param>
        /// <returns></returns>
        public async Task<User> FindUserByEmailUidAsync(string userEmail, Guid uid)
        {
            var query = this.GetBaseQuery();
            return await query.Where(x => x.Email.Equals(userEmail) && x.Uid.Equals(uid) && !x.IsDeleted && x.Active).FirstOrDefaultAsync();
        }

        /// <summary>Finds the user dto by user e-mail asynchronous.</summary>
        /// <param name="userEmail">The userEmail.</param>
        /// <returns></returns>
        public async Task<User> FindUserByEmailAsync(string userEmail)
        {
            var query = this.GetBaseQuery();
            return await query.Where(x => x.Email.Equals(userEmail) && !x.IsDeleted && x.Active).FirstOrDefaultAsync();
        }

        /// <summary>Finds the user dto by user identifier asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<UserDto> FindUserDtoByUserIdAsync(int userId)
        {
            var query = this.GetBaseQuery()
                                .FindById(userId);

            return await query.Select(u => new UserDto
            {
                User = u,
                Collaborator = u.Collaborator
            })
            .FirstOrDefaultAsync();
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
                            EditionAttendeeCollaborators = u.Collaborator.AttendeeCollaborators.Where(ac => !ac.IsDeleted)
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
                            HasUnreadMessages = u.RecipientMessages.Any(rm => !rm.ReadDate.HasValue),

                            AudiovisualBusinessRoundProjectEvaluationsPendingCount = (int?)u.Collaborator.AttendeeCollaborators
                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                .Sum(ac => ac.AttendeeOrganizationCollaborators
                                                .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted)
                                                .Sum(aoc => aoc.AttendeeOrganization.ProjectBuyerEvaluations
                                                                .Count(pbe => !pbe.IsDeleted
                                                                                && !pbe.Project.IsDeleted
                                                                                && pbe.Project.FinishDate.HasValue
                                                                                && pbe.ProjectEvaluationStatus.Code == ProjectEvaluationStatus.UnderEvaluation.Code))) ?? 0,

                            MusicBusinessRoundProjectEvaluationsPendingCount = (int?)u.Collaborator.AttendeeCollaborators
                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                .Sum(ac => ac.AttendeeOrganizationCollaborators
                                                .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted)
                                                .Sum(aoc => aoc.AttendeeOrganization.MusicBusinessRoundProjectBuyerEvaluations
                                                                .Count(pbe => !pbe.IsDeleted
                                                                                && !pbe.MusicBusinessRoundProject.IsDeleted
                                                                                && pbe.MusicBusinessRoundProject.FinishDate.HasValue
                                                                                && pbe.ProjectEvaluationStatus.Code == ProjectEvaluationStatus.UnderEvaluation.Code))) ?? 0,

                            Collaborator = u.Collaborator,
                            EditionAttendeeCollaborator = u.Collaborator.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                            EditionAttendeeCollaborators = u.Collaborator.AttendeeCollaborators.Where(ac => !ac.IsDeleted),
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

        /// <summary>Finds the user language by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public UserLanguageDto FindUserLanguageByUserId(int userId)
        {
            var query = this.GetBaseQuery()
                                .FindById(userId)
                                .Where(u => u.UserInterfaceLanguageId != null);

            return query
                        .Select(u => new UserLanguageDto
                        {
                            Language = u.UserInterfaceLanguage
                        })
                        .FirstOrDefault();
        }

        /// <summary>Finds the user email settings dto by user identifier asynchronous.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<UserEmailSettingsDto> FindUserEmailSettingsDtoByUserIdAsync(int userId)
        {
            var query = this.GetBaseQuery()
                                .FindById(userId);

            return await query
                            .Select(u => new UserEmailSettingsDto
                            {
                                User = u,
                                UserUnsubscribedListDtos = u.UserUnsubscribedLists.Where(uul => !uul.IsDeleted).Select(uul => new UserUnsubscribedListDto
                                {
                                    UserUnsubscribedList = uul,
                                    User = u,
                                    SubscribeList = uul.SubscribeList
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        #region Old Methods

        /// <summary>Método que remove a entidade do Contexto</summary>
        /// <param name="entity">Entidade</param>
        public override void Delete(User entity)
        {
            entity.Roles.Clear();

            //if (entity.UserUseTerms != null && entity.UserUseTerms.Any())
            //{
            //    var terms = entity.UserUseTerms.ToList();

            //    foreach (var term in terms)
            //    {
            //        _context.Entry(term).State = EntityState.Deleted;
            //    }
            //}

            base.Delete(entity);
        }



        #endregion
    }
}