// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="MessageRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Message IQueryable Extensions

    /// <summary>
    /// MessageIQueryableExtensions
    /// </summary>
    internal static class MessageIQueryableExtensions
    {
        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByEditionId(this IQueryable<Message> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(m => m.EditionId == editionId && !m.Edition.IsDeleted);
            }

            return query;
        }

        /// <summary>Finds the by user identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByUserId(this IQueryable<Message> query, int userId)
        {
            query = query.Where(m => m.SenderId == userId || m.RecipientId == userId);

            return query;
        }

        /// <summary>Finds the by sender identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindBySenderId(this IQueryable<Message> query, int senderId)
        {
            query = query.Where(m => m.SenderId == senderId);

            return query;
        }

        /// <summary>Finds the by recipient identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByRecipientId(this IQueryable<Message> query, int recipientId)
        {
            query = query.Where(m => m.RecipientId == recipientId);

            return query;
        }

        /// <summary>Finds the by recipient uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByRecipientUid(this IQueryable<Message> query, Guid recipientUid)
        {
            query = query.Where(m => m.Recipient.Uid == recipientUid);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByKeywords(this IQueryable<Message> query, int userId, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Message>(false);
                var innerMessageTextWhere = PredicateBuilder.New<Message>(true);
                var innerOtherUserNameWhere = PredicateBuilder.New<Message>(true);
                var innerOtherUserJobTitleWhere = PredicateBuilder.New<Message>(true);
                var innerOtherUserOrganizationWhere = PredicateBuilder.New<Message>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerMessageTextWhere = innerMessageTextWhere.And(m => m.Text.Contains(keyword));
                        innerOtherUserNameWhere = innerOtherUserNameWhere.And(m => m.SenderId == userId ?
                                                                                        m.Recipient.Name.Contains(keyword) :
                                                                                        m.Sender.Name.Contains(keyword));
                        innerOtherUserJobTitleWhere = innerOtherUserJobTitleWhere.And(m => m.SenderId == userId ?
                                                                                        m.Recipient.Collaborator.JobTitles.Any(jb =>
                                                                                            !jb.IsDeleted
                                                                                            && jb.Value.Contains(keyword)) :
                                                                                        m.Sender.Collaborator.JobTitles.Any(jb =>
                                                                                            !jb.IsDeleted
                                                                                            && jb.Value.Contains(keyword)));
                        innerOtherUserOrganizationWhere = innerOtherUserOrganizationWhere.And(m => m.SenderId == userId ?
                                                                                        m.Recipient.Collaborator.AttendeeCollaborators.Any(ac =>
                                                                                            !ac.IsDeleted
                                                                                            && ac.AttendeeOrganizationCollaborators.Any(aoc =>
                                                                                                !aoc.IsDeleted
                                                                                                && !aoc.AttendeeOrganization.IsDeleted
                                                                                                && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                && aoc.AttendeeOrganization.Organization.TradeName.Contains(keyword))) :
                                                                                        m.Sender.Collaborator.AttendeeCollaborators.Any(ac =>
                                                                                            !ac.IsDeleted
                                                                                            && ac.AttendeeOrganizationCollaborators.Any(aoc =>
                                                                                                !aoc.IsDeleted
                                                                                                && !aoc.AttendeeOrganization.IsDeleted
                                                                                                && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                && aoc.AttendeeOrganization.Organization.TradeName.Contains(keyword))));
                    }
                }

                outerWhere = outerWhere.Or(innerMessageTextWhere);
                outerWhere = outerWhere.Or(innerOtherUserNameWhere);
                outerWhere = outerWhere.Or(innerOtherUserJobTitleWhere);
                outerWhere = outerWhere.Or(innerOtherUserOrganizationWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [is not read].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Message> IsNotRead(this IQueryable<Message> query)
        {
            query = query.Where(m => !m.ReadDate.HasValue);

            return query;
        }

        /// <summary>Determines whether [is notification email not sent].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Message> IsNotificationEmailNotSent(this IQueryable<Message> query)
        {
            var date = DateTime.UtcNow.AddMinutes(-10);

            query = query.Where(m => !m.NotificationEmailSendDate.HasValue
                                     && m.SendDate < date);

            return query;
        }

        /// <summary>Determines whether [is recipient subscribed].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Message> IsRecipientSubscribed(this IQueryable<Message> query)
        {
            query = query.Where(m => !m.Recipient.UserUnsubscribedLists.Any(uul => !uul.IsDeleted
                                                                                   && uul.SubscribeList.Code == SubscribeList.UnreadConversationEmail.Code));

            return query;
        }
    }

    #endregion

    /// <summary>MessageRepository</summary>
    public class MessageRepository : Repository<PlataformaRio2CContext, Message>, IMessageRepository
    {
        /// <summary>Initializes a new instance of the <see cref="MessageRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public MessageRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Message> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet;
            //.IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the new conversations dto by edition identifier and by other user uid.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="otherUserUid">The other user uid.</param>
        /// <returns></returns>
        public async Task<ConversationDto> FindNewConversationsDtoByEditionIdAndByOtherUserUid(int editionId, Guid otherUserUid)
        {
            var query = this._context.Users
                                        .IsNotDeleted()
                                        .FindByUid(otherUserUid);

            return await query
                        .Select(u => new ConversationDto
                        {
                            OtherUser = u,
                            OtherAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                            {
                                AttendeeCollaborator = u.Collaborator.AttendeeCollaborators
                                                                            .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                                Collaborator = u.Collaborator,
                                JobTitlesDtos = u.Collaborator.JobTitles
                                                                    .Where(jb => !jb.IsDeleted)
                                                                    .Select(jb => new CollaboratorJobTitleBaseDto
                                                                    {
                                                                        Id = jb.Id,
                                                                        Uid = jb.Uid,
                                                                        Value = jb.Value,
                                                                        LanguageDto = new LanguageBaseDto
                                                                        {
                                                                            Id = jb.Language.Id,
                                                                            Uid = jb.Language.Uid,
                                                                            Name = jb.Language.Name,
                                                                            Code = jb.Language.Code
                                                                        }
                                                                    }),
                                AttendeeOrganizationsDtos = u.Collaborator.AttendeeCollaborators
                                                                                .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .AttendeeOrganizationCollaborators
                                                                                .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                .Select(aoc => new AttendeeOrganizationDto
                                                                                {
                                                                                    AttendeeOrganization = aoc.AttendeeOrganization,
                                                                                    Organization = aoc.AttendeeOrganization.Organization
                                                                                })
                            },
                            LastMessageDate = null,
                            UnreadMessagesCount = 0
                        })
                        .FirstOrDefaultAsync();
        }

        /// <summary>Finds all conversations dtos by edition identifier and by user identifier and by search keywords.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <returns></returns>
        public async Task<List<ConversationDto>> FindAllConversationsDtosByEditionIdAndByUserIdAndBySearchKeywords(int editionId, int userId, string searchKeywords)
        {
            var query = this.GetBaseQuery()
                                .FindByUserId(userId)
                                .FindByKeywords(userId, searchKeywords);

            return await query
                            .GroupBy(m => new
                            {
                                User = m.SenderId == userId ? m.Recipient : m.Sender,
                            })
                            .Select(g => new ConversationDto
                            {
                                OtherUser = g.Key.User,
                                OtherAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    AttendeeCollaborator = g.Key.User.Collaborator.AttendeeCollaborators
                                                                                        .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                                    Collaborator = g.Key.User.Collaborator,
                                    JobTitlesDtos = g.Key.User.Collaborator.JobTitles
                                                                                .Where(jb => !jb.IsDeleted)
                                                                                .Select(jb => new CollaboratorJobTitleBaseDto
                                                                                {
                                                                                    Id = jb.Id,
                                                                                    Uid = jb.Uid,
                                                                                    Value = jb.Value,
                                                                                    LanguageDto = new LanguageBaseDto
                                                                                    {
                                                                                        Id = jb.Language.Id,
                                                                                        Uid = jb.Language.Uid,
                                                                                        Name = jb.Language.Name,
                                                                                        Code = jb.Language.Code
                                                                                    }
                                                                                }),
                                    AttendeeOrganizationsDtos = g.Key.User.Collaborator.AttendeeCollaborators
                                                                                            .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                            .AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationDto
                                                                                            {
                                                                                                AttendeeOrganization = aoc.AttendeeOrganization,
                                                                                                Organization = aoc.AttendeeOrganization.Organization
                                                                                            })
                                },
                                LastMessageDate = g.Max(m => m.SendDate),
                                UnreadMessagesCount = g.Count(m => m.RecipientId == userId && !m.ReadDate.HasValue)
                            })
                            .OrderByDescending(cd => cd.UnreadMessagesCount > 0 ? 1 : 0)
                            .ThenByDescending(cd => cd.LastMessageDate)
                            .ThenBy(cd => cd.OtherAttendeeCollaboratorDto.AttendeeCollaborator.Collaborator.Badge)
                            .ToListAsync();
        }

        /// <summary>Finds all messages dtos by edition identifier and by user identifier and by recipient identifier and by recipient uid.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <returns></returns>
        public async Task<List<MessageDto>> FindAllMessagesDtosByEditionIdAndByUserIdAndByRecipientIdAndByRecipientUid(int editionId, int userId, int recipientId, Guid recipientUid)
        {
            var query = this.GetBaseQuery()
                                .FindByUserId(userId)
                                .FindByUserId(recipientId);

            return await query
                            .Select(m => new MessageDto
                            {
                                SenderUser = m.Sender,
                                SenderCollaborator = m.Sender.Collaborator,
                                RecipientUser = m.Recipient,
                                RecipientCollaborator = m.Recipient.Collaborator,
                                Message = m
                            })
                            .OrderBy(md => md.Message.SendDate)
                            .ToListAsync();
        }

        /// <summary>Finds all not read by sender identifier and by recipient identifier.</summary>
        /// <param name="senderId">The sender identifier.</param>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <returns></returns>
        public async Task<List<Message>> FindAllNotReadBySenderIdAndByRecipientId(int senderId, int recipientId)
        {
            var query = this.GetBaseQuery()
                                .FindBySenderId(senderId)
                                .FindByRecipientId(recipientId);

            return await query
                            .ToListAsync();
        }

        /// <summary>Finds all notification email conversations dtos by edition identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<NotificationEmailConversationDto>> FindAllNotificationEmailConversationsDtosByEditionId(int editionId)
        {
            var query = this.GetBaseQuery()
                                    .IsNotRead()
                                    .IsNotificationEmailNotSent()
                                    .IsRecipientSubscribed();

            return await query
                            .GroupBy(m => new
                            {
                                m.Recipient,
                                m.Sender
                            })
                            .Select(g => new NotificationEmailConversationDto
                            {
                                RecipientUser = g.Key.Recipient,
                                RecipientCollaborator = g.Key.Recipient.Collaborator,
                                RecipientLanguage = g.Key.Recipient.UserInterfaceLanguage,
                                OtherUser = g.Key.Sender,
                                OtherAttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                {
                                    AttendeeCollaborator = g.Key.Sender.Collaborator.AttendeeCollaborators
                                                                                        .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                                    Collaborator = g.Key.Sender.Collaborator,
                                    JobTitlesDtos = g.Key.Sender.Collaborator.JobTitles
                                                                                .Where(jb => !jb.IsDeleted)
                                                                                .Select(jb => new CollaboratorJobTitleBaseDto
                                                                                {
                                                                                    Id = jb.Id,
                                                                                    Uid = jb.Uid,
                                                                                    Value = jb.Value,
                                                                                    LanguageDto = new LanguageBaseDto
                                                                                    {
                                                                                        Id = jb.Language.Id,
                                                                                        Uid = jb.Language.Uid,
                                                                                        Name = jb.Language.Name,
                                                                                        Code = jb.Language.Code
                                                                                    }
                                                                                }),
                                    AttendeeOrganizationsDtos = g.Key.Sender.Collaborator.AttendeeCollaborators
                                                                                            .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                            .AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationDto
                                                                                            {
                                                                                                AttendeeOrganization = aoc.AttendeeOrganization,
                                                                                                Organization = aoc.AttendeeOrganization.Organization
                                                                                            })
                                },
                                Messages = g,
                                LastMessageDate = g.Max(m => m.SendDate),
                                UnreadMessagesCount = g.Count(m => !m.ReadDate.HasValue)
                            })
                            .OrderByDescending(cd => cd.UnreadMessagesCount > 0 ? 1 : 0)
                            .ThenByDescending(cd => cd.LastMessageDate)
                            .ThenBy(cd => cd.OtherAttendeeCollaboratorDto.AttendeeCollaborator.Collaborator.Badge)
                            .ToListAsync();
        }

        #region Old

        public override IQueryable<Message> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Sender)
                                .Include(i => i.Recipient);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        #endregion
    }
}