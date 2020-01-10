// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleRepository.cs" company="Softo">
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
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using X.PagedList;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region ConferenceParticipantRole IQueryable Extensions

    /// <summary>
    /// ConferenceParticipantRoleIQueryableExtensions
    /// </summary>
    internal static class ConferenceParticipantRoleIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> FindByUid(this IQueryable<ConferenceParticipantRole> query, Guid conferenceParticipantRoleUid)
        {
            query = query.Where(cpr => cpr.Uid == conferenceParticipantRoleUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="conferenceParticipantRoleUids">The conferenceParticipantRole uids.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> FindByUids(this IQueryable<ConferenceParticipantRole> query, List<Guid> conferenceParticipantRoleUids)
        {
            if (conferenceParticipantRoleUids?.Any() == true)
            {
                query = query.Where(cpr => conferenceParticipantRoleUids.Contains(cpr.Uid));
            }

            return query;
        }

        ///// <summary>Finds the by edition identifier.</summary>
        ///// <param name="query">The query.</param>
        ///// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        ///// <param name="editionId">The edition identifier.</param>
        ///// <returns></returns>
        //internal static IQueryable<ConferenceParticipantRole> FindByEditionId(this IQueryable<ConferenceParticipantRole> query, bool showAllEditions, int editionId)
        //{
        //    if (!showAllEditions)
        //    {
        //        query = query.Where(cpr => cpr.EditionId == editionId);
        //    }

        //    return query;
        //}

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> IsNotDeleted(this IQueryable<ConferenceParticipantRole> query)
        {
            query = query.Where(cpr => !cpr.IsDeleted);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ConferenceParticipantRole> FindByKeywords(this IQueryable<ConferenceParticipantRole> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<ConferenceParticipantRole>(false);
                var innerConferenceParticipantRoleTitleWhere = PredicateBuilder.New<ConferenceParticipantRole>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerConferenceParticipantRoleTitleWhere = innerConferenceParticipantRoleTitleWhere.And(cpr => cpr.ConferenceParticipantRoleTitles.Any(cprt => cprt.LanguageId == languageId
                                                                                                                      && !cprt.IsDeleted
                                                                                                                      && cprt.Value.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerConferenceParticipantRoleTitleWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }
    }

    #endregion

    #region ConferenceParticipantRoleJsonDto IQueryable Extensions

    /// <summary>
    /// ConferenceParticipantRoleJsonDtoIQueryableExtensions
    /// </summary>
    internal static class ConferenceParticipantRoleJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ConferenceParticipantRoleJsonDto>> ToListPagedAsync(this IQueryable<ConferenceParticipantRoleJsonDto> query, int page, int pageSize)
        {
            page++;

            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>ConferenceParticipantRoleRepository</summary>
    public class ConferenceParticipantRoleRepository : Repository<PlataformaRio2CContext, ConferenceParticipantRole>, IConferenceParticipantRoleRepository
    {
        public ConferenceParticipantRoleRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ConferenceParticipantRole> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceParticipantRoleDto> FindDtoAsync(Guid conferenceParticipantRoleUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceParticipantRoleUid);
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(cpr => new ConferenceParticipantRoleDto
                            {
                                ConferenceParticipantRole = cpr,
                                ConferenceParticipantRoleTitleDtos = cpr.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                {
                                    ConferenceParticipantRoleTitle = cprt,
                                    LanguageDto = new LanguageDto
                                    {
                                        Id = cprt.Language.Id,
                                        Uid = cprt.Language.Uid,
                                        Code = cprt.Language.Code
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceParticipantRoleDto> FindConferenceWidgetDtoAsync(Guid conferenceParticipantRoleUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceParticipantRoleUid);
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(cpr => new ConferenceParticipantRoleDto
                            {
                                ConferenceParticipantRole = cpr,
                                ConferenceDtos = cpr.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted).Select(cp => cp.Conference).Distinct().Select(c => new ConferenceDto
                                {
                                    Conference = c,
                                    ConferenceTitleDtos = c.ConferenceTitles.Where(ct => !ct.IsDeleted).Select(ct => new ConferenceTitleDto
                                    {
                                        ConferenceTitle = ct,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = ct.Language.Id,
                                            Uid = ct.Language.Uid,
                                            Name = ct.Language.Name,
                                            Code = ct.Language.Code
                                        }
                                    })
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the participants widget dto asynchronous.</summary>
        /// <param name="conferenceParticipantRoleUid">The conference participant role uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceParticipantRoleDto> FindParticipantsWidgetDtoAsync(Guid conferenceParticipantRoleUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceParticipantRoleUid);
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(cpr => new ConferenceParticipantRoleDto
                            {
                                ConferenceParticipantRole = cpr,
                                ConferenceParticipantDtos = cpr.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted).Select(cp => new ConferenceParticipantDto
                                {
                                    ConferenceParticipant = cp,
                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = cp.AttendeeCollaborator,
                                        Collaborator = cp.AttendeeCollaborator.Collaborator
                                    },
                                    ConferenceDto = new ConferenceDto
                                    {
                                        Conference = cp.Conference,
                                        ConferenceTitleDtos = cp.Conference.ConferenceTitles.Where(ct => !ct.IsDeleted).Select(ct => new ConferenceTitleDto
                                        {
                                            ConferenceTitle = ct,
                                            LanguageDto = new LanguageBaseDto
                                            {
                                                Id = ct.Language.Id,
                                                Uid = ct.Language.Uid,
                                                Name = ct.Language.Name,
                                                Code = ct.Language.Code
                                            }
                                        })
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the by uid asynchronous.</summary>
        /// <param name="conferenceParticipantRoleUid">The conferenceParticipantRole uid.</param>
        /// <returns></returns>
        public async Task<ConferenceParticipantRole> FindByUidAsync(Guid conferenceParticipantRoleUid)
        {
            var query = this.GetBaseQuery()
                .FindByUid(conferenceParticipantRoleUid);

            return await query
                .FirstOrDefaultAsync();
        }

        /// <summary>Finds all dto by edition identifier asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<ConferenceParticipantRoleDto>> FindAllDtoByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery();
                                //.FindByEditionId(false, editionId);

            return await query
                            .Select(cpr => new ConferenceParticipantRoleDto
                            {
                                ConferenceParticipantRole = cpr,
                                ConferenceParticipantRoleTitleDtos = cpr.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                {
                                    ConferenceParticipantRoleTitle = cprt,
                                    LanguageDto = new LanguageDto
                                    {
                                        Id = cprt.Language.Id,
                                        Uid = cprt.Language.Uid,
                                        Code = cprt.Language.Code
                                    }
                                })
                            })
                            .ToListAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="conferenceParticipantRoleUids">The conferenceParticipantRole uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<ConferenceParticipantRoleJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> conferenceParticipantRoleUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                //.FindByEditionId(false, editionId)
                                .FindByUids(conferenceParticipantRoleUids);

            return await query
                            .DynamicOrder<ConferenceParticipantRole>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    //new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "CreateDate", "UpdateDate" },
                                "StartDate")
                            .Select(r => new ConferenceParticipantRoleJsonDto
                            {
                                Id = r.Id,
                                Uid = r.Uid,
                                Title = r.ConferenceParticipantRoleTitles.FirstOrDefault(cprt => !cprt.IsDeleted && cprt.LanguageId == languageId).Value
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, int editionId)
        {
            var query = this.GetBaseQuery();
                                //.FindByEditionId(showAllEditions, editionId);

            return await query
                            .CountAsync();
        }
    }
}