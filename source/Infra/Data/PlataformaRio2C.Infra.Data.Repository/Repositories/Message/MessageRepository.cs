// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-25-2019
// ***********************************************************************
// <copyright file="MessageRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;

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

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Message> FindByKeywords(this IQueryable<Message> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<Message>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(m => m.Text.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        ///// <summary>Determines whether [is not deleted].</summary>
        ///// <param name="query">The query.</param>
        ///// <returns></returns>
        //internal static IQueryable<Message> IsNotDeleted(this IQueryable<Message> query)
        //{
        //    query = query.Where(m => !m.IsDeleted);

        //    return query;
        //}
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

        /// <summary>Finds all conversations dtos by edition identifier and by user identifier.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<ConversationDto>> FindAllConversationsDtosByEditionIdAndByUserId(int editionId, int userId)
        {
            var query = this.GetBaseQuery()
                                .FindByUserId(userId);

            return await query
                            .GroupBy(m => new
                            {
                                User = m.SenderId == userId ? m.Recipient : m.Sender,
                            })
                            .Select(g => new ConversationDto
                            {
                                User = g.Key.User,
                                AttendeeCollaboratorDto = new AttendeeCollaboratorDto
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
                                UnreadMessagesCount = g.Count(m => !m.ReadDate.HasValue)
                            })
                            .OrderByDescending(cd => cd.UnreadMessagesCount > 0 ? 1 : 0)
                            .ThenByDescending(cd => cd.LastMessageDate)
                            .ThenBy(cd => cd.User.Name)
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