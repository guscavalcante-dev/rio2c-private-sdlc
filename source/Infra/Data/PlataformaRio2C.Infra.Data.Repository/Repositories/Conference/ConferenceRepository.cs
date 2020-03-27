// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-27-2020
// ***********************************************************************
// <copyright file="ConferenceRepository.cs" company="Softo">
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
    #region Conference IQueryable Extensions

    /// <summary>
    /// ConferenceIQueryableExtensions
    /// </summary>
    internal static class ConferenceIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByUid(this IQueryable<Conference> query, Guid conferenceUid)
        {
            query = query.Where(c => c.Uid == conferenceUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="conferencesUids">The conferences uids.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByUids(this IQueryable<Conference> query, List<Guid> conferencesUids)
        {
            if (conferencesUids?.Any() == true)
            {
                query = query.Where(c => conferencesUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByEditionId(this IQueryable<Conference> query, bool showAllEditions, int editionId)
        {
            if (!showAllEditions)
            {
                query = query.Where(c => c.EditionEvent.EditionId == editionId);
            }

            return query;
        }

        /// <summary>Finds the by edition uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByEditionUid(this IQueryable<Conference> query, bool showAllEditions, Guid editionUid)
        {
            if (!showAllEditions)
            {
                query = query.Where(c => c.EditionEvent.Edition.Uid == editionUid);
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByKeywords(this IQueryable<Conference> query, string keywords, int languageId)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Conference>(false);
                var innerConferenceTitleWhere = PredicateBuilder.New<Conference>(true);
                var innerRoomNameWhere = PredicateBuilder.New<Conference>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerConferenceTitleWhere = innerConferenceTitleWhere.And(c => c.ConferenceTitles.Any(ct => !ct.IsDeleted
                                                                                                                    && ct.LanguageId == languageId
                                                                                                                    && ct.Value.Contains(keyword)));
                        innerRoomNameWhere = innerRoomNameWhere.And(c => c.Room.RoomNames.Any(rn => !rn.IsDeleted
                                                                                                    && rn.LanguageId == languageId
                                                                                                    && rn.Value.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerConferenceTitleWhere);
                outerWhere = outerWhere.Or(innerRoomNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by API filters.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionDates">The edition dates.</param>
        /// <param name="editionEventsUids">The edition events uids.</param>
        /// <param name="roomsUids">The rooms uids.</param>
        /// <param name="tracksUids">The tracks uids.</param>
        /// <param name="pillarsUids">The pillars uids.</param>
        /// <param name="presentationFormatsUids">The presentation formats uids.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByApiFilters(this IQueryable<Conference> query, List<DateTimeOffset> editionDates, List<Guid> editionEventsUids, List<Guid> roomsUids, List<Guid> tracksUids, List<Guid> pillarsUids, List<Guid> presentationFormatsUids)
        {
            if (editionDates?.Any() == true || editionEventsUids?.Any() == true || roomsUids?.Any() == true || pillarsUids?.Any() == true || tracksUids?.Any() == true || presentationFormatsUids?.Any() == true)
            {
                var outerWhere = PredicateBuilder.New<Conference>(false);
                var innerEditionDatesWhere = PredicateBuilder.New<Conference>(true);
                var innerEditionEventsUidsWhere = PredicateBuilder.New<Conference>(true);
                var innerRoomsUidsWhere = PredicateBuilder.New<Conference>(true);
                var innerTracksUidsWhere = PredicateBuilder.New<Conference>(true);
                var innerPillarsUidsWhere = PredicateBuilder.New<Conference>(true);
                var innerPresentationFormatsUidsWhere = PredicateBuilder.New<Conference>(true);

                if (editionDates?.Any() == true)
                {
                    innerEditionDatesWhere = innerEditionDatesWhere.Or(c => editionDates.Contains(DbFunctions.TruncateTime(c.StartDate).Value));
                }

                if (editionEventsUids?.Any() == true)
                {
                    innerEditionEventsUidsWhere = innerEditionEventsUidsWhere.Or(c => editionEventsUids.Contains(c.EditionEvent.Uid));
                }

                if (roomsUids?.Any() == true)
                {
                    innerRoomsUidsWhere = innerRoomsUidsWhere.Or(c => roomsUids.Contains(c.Room.Uid));
                }

                if (tracksUids?.Any() == true)
                {
                    innerTracksUidsWhere = innerTracksUidsWhere.Or(c => c.ConferenceTracks.Any(ct => tracksUids.Contains(ct.Track.Uid) && !ct.IsDeleted));
                }

                if (pillarsUids?.Any() == true)
                {
                    innerPillarsUidsWhere = innerPillarsUidsWhere.Or(c => c.ConferencePillars.Any(ct => pillarsUids.Contains(ct.Pillar.Uid) && !ct.IsDeleted));
                }

                if (presentationFormatsUids?.Any() == true)
                {
                    innerPresentationFormatsUidsWhere = innerPresentationFormatsUidsWhere.Or(c => c.ConferencePresentationFormats.Any(cpf => presentationFormatsUids.Contains(cpf.PresentationFormat.Uid) && !cpf.IsDeleted));
                }

                outerWhere = outerWhere.And(innerEditionDatesWhere);
                outerWhere = outerWhere.And(innerEditionEventsUidsWhere);
                outerWhere = outerWhere.And(innerRoomsUidsWhere);
                outerWhere = outerWhere.And(innerTracksUidsWhere);
                outerWhere = outerWhere.And(innerPillarsUidsWhere);
                outerWhere = outerWhere.And(innerPresentationFormatsUidsWhere);
                query = query.Where(outerWhere);
                //query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        /// <summary>Finds the by date range.</summary>
        /// <param name="query">The query.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> FindByDateRange(this IQueryable<Conference> query, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

            query = query.Where(c => (c.StartDate >= startDate && c.StartDate <= endDate)
                                     || (c.EndDate >= startDate && c.EndDate <= endDate));

            return query;
        }

        /// <summary>Determines whether this instance has participants.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> HasParticipants(this IQueryable<Conference> query)
        {
            query = query.Where(c => c.ConferenceParticipants.Any(cp => !cp.IsDeleted 
                                                                        && !cp.AttendeeCollaborator.IsDeleted
                                                                        && !cp.AttendeeCollaborator.Collaborator.IsDeleted));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Conference> IsNotDeleted(this IQueryable<Conference> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }
    }

    #endregion

    #region ConferenceJsonDto IQueryable Extensions

    /// <summary>
    /// ConferenceJsonDtoIQueryableExtensions
    /// </summary>
    internal static class ConferenceJsonDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ConferenceJsonDto>> ToListPagedAsync(this IQueryable<ConferenceJsonDto> query, int page, int pageSize)
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

    #region ConferenceDto IQueryable Extensions

    /// <summary>
    /// ConferenceDtoIQueryableExtensions
    /// </summary>
    internal static class ConferenceDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ConferenceDto>> ToListPagedAsync(this IQueryable<ConferenceDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>ConferenceRepository</summary>
    public class ConferenceRepository : Repository<PlataformaRio2CContext, Conference>, IConferenceRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ConferenceRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ConferenceRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Conference> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds the dto asynchronous.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceDto> FindDtoAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(c => new ConferenceDto
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
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the main information widget dto asynchronous.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceDto> FindMainInformationWidgetDtoAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                    .FindByUid(conferenceUid)
                                    .FindByEditionId(false, editionId);

            return await query
                            .Select(c => new ConferenceDto
                            {
                                Conference = c,
                                EditionEvent = c.EditionEvent,
                                RoomDto = new RoomDto
                                {
                                    Room = c.Room,
                                    RoomNameDtos = c.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                    {
                                        RoomName = rn,
                                        LanguageDto = new LanguageDto
                                        {
                                            Id = rn.Language.Id,
                                            Uid = rn.Language.Uid,
                                            Code = rn.Language.Code
                                        }
                                    })
                                },
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
                                }),
                                ConferenceSynopsisDtos = c.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
                                {
                                    ConferenceSynopsis = cs,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = cs.Language.Id,
                                        Uid = cs.Language.Uid,
                                        Name = cs.Language.Name,
                                        Code = cs.Language.Code
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the tracks and presentation formats widget dto asynchronous.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceDto> FindTracksAndPresentationFormatsWidgetDtoAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(c => new ConferenceDto
                            {
                                Conference = c,
                                ConferenceTrackDtos = c.ConferenceTracks.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferenceTrackDto
                                {
                                    ConferenceTrack = cvt,
                                    Track = cvt.Track
                                }),
                                ConferencePillarDtos = c.ConferencePillars.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferencePillarDto()
                                {
                                    ConferencePillar = cvt,
                                    Pillar = cvt.Pillar
                                }),
                                ConferencePresentationFormatDtos = c.ConferencePresentationFormats.Where(cht => !cht.IsDeleted).Select(cht => new ConferencePresentationFormatDto
                                {
                                    ConferencePresentationFormat = cht,
                                    PresentationFormat = cht.PresentationFormat
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the participants widget dto asynchronous.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceDto> FindParticipantsWidgetDtoAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(c => new ConferenceDto
                            {
                                Conference = c,
                                ConferenceParticipantDtos = c.ConferenceParticipants.Where(cp => !cp.IsDeleted).Select(cp => new ConferenceParticipantDto
                                {
                                    ConferenceParticipant = cp,
                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = cp.AttendeeCollaborator,
                                        Collaborator = cp.AttendeeCollaborator.Collaborator,
                                        JobTitlesDtos = cp.AttendeeCollaborator.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                        AttendeeOrganizationsDtos = cp.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                            .Select(aoc => new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = aoc.AttendeeOrganization,
                                                Organization = aoc.AttendeeOrganization.Organization
                                            })
                                    },
                                    ConferenceParticipantRoleDto = new ConferenceParticipantRoleDto
                                    {
                                        ConferenceParticipantRole = cp.ConferenceParticipantRole,
                                        ConferenceParticipantRoleTitleDtos = cp.ConferenceParticipantRole.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                        {
                                            ConferenceParticipantRoleTitle = cprt,
                                            LanguageDto = new LanguageBaseDto
                                            {
                                                Id = cprt.Language.Id,
                                                Uid = cprt.Language.Uid,
                                                Name = cprt.Language.Name,
                                                Code = cprt.Language.Code
                                            }
                                        })
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="conferencesUids">The conferences uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="languageId">The language identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<ConferenceJsonDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> conferencesUids,
            int editionId,
            int languageId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, languageId)
                                .FindByEditionId(false, editionId)
                                .FindByUids(conferencesUids);
                                //.FindByHighlights(collaboratorTypeName, showHighlights);

            return await query
                            .DynamicOrder<Conference>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("EditionEventJsonDto", "EditionEvent.Name"),
                                    //new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "EditionEvent.Name", "StartDate", "EndDate", "CreateDate", "UpdateDate" },
                                "StartDate")
                            .Select(c => new ConferenceJsonDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                EditionEventJsonDto = new EditionEventJsonDto
                                {
                                    Id = c.EditionEvent.Id,
                                    Uid = c.EditionEvent.Uid,
                                    Name = c.EditionEvent.Name
                                },
                                RoomJsonDto = new RoomJsonDto
                                {
                                    Id = c.Room.Id,
                                    Uid = c.Room.Uid,
                                    Name = c.Room.RoomNames.FirstOrDefault(n => !n.IsDeleted && n.LanguageId == languageId).Value
                                },
                                StartDate = c.StartDate,
                                EndDate =  c.EndDate,
                                Title = c.ConferenceTitles.FirstOrDefault(ct => !ct.IsDeleted && ct.LanguageId == languageId).Value,
                                Synopsis = c.ConferenceSynopses.FirstOrDefault(cs => !cs.IsDeleted && cs.LanguageId == languageId).Value,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(bool showAllEditions, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(showAllEditions, editionId);

            return await query.CountAsync();
        }

        /// <summary>Finds all for generate negotiations asynchronous.</summary>
        /// <param name="editionUid">The edition uid.</param>
        /// <returns></returns>
        public async Task<List<Conference>> FindAllForGenerateNegotiationsAsync(Guid editionUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionUid(false, editionUid)
                                .HasParticipants()
                                .Include(c => c.ConferenceParticipants.Select(cp => cp.AttendeeCollaborator.AttendeeOrganizationCollaborators));
                                //.IncludeFilter(c => c.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.AttendeeCollaborator.IsDeleted))
                                //.IncludeFilter(c => c.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.AttendeeCollaborator.IsDeleted).Select(cp => cp.AttendeeCollaborator))
                                //.IncludeFilter(c => c.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.AttendeeCollaborator.IsDeleted).Select(cp => cp.AttendeeCollaborator.AttendeeOrganizationCollaborators.Where(aoc => !aoc.IsDeleted)));

            return await query
                            .ToListAsync();
        }

        /// <summary>Finds all schedule dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public Task<List<ConferenceDto>> FindAllScheduleDtosAsync(int editionId, int? attendeeCollaboratorId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId)
                                .FindByDateRange(startDate, endDate)
                                .Select(c => new ConferenceDto
                                {
                                    Conference = c,
                                    EditionEvent = c.EditionEvent,
                                    RoomDto = new RoomDto
                                    {
                                        Room = c.Room,
                                        RoomNameDtos = c.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                        {
                                            RoomName = rn,
                                            LanguageDto = new LanguageDto
                                            {
                                                Id = rn.Language.Id,
                                                Uid = rn.Language.Uid,
                                                Code = rn.Language.Code
                                            }
                                        })
                                    },
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
                                    }),
                                    ConferenceSynopsisDtos = c.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
                                    {
                                        ConferenceSynopsis = cs,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = cs.Language.Id,
                                            Uid = cs.Language.Uid,
                                            Name = cs.Language.Name,
                                            Code = cs.Language.Code
                                        }
                                    }),
                                    IsParticipant = c.ConferenceParticipants.Any(cp => cp.AttendeeCollaboratorId == attendeeCollaboratorId && !cp.IsDeleted)
                                });

            return query
                        .ToListAsync();
        }

        #region Api

        /// <summary>Finds all public API paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="editionDates">The edition dates.</param>
        /// <param name="editionEventsUids">The edition events uids.</param>
        /// <param name="roomsUids">The rooms uids.</param>
        /// <param name="tracksUids">The tracks uids.</param>
        /// <param name="pillarsUids">The pillars uids.</param>
        /// <param name="presentationFormatsUids">The presentation formats uids.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<ConferenceDto>> FindAllPublicApiPaged(
            int editionId,
            string keywords,
            List<DateTimeOffset> editionDates,
            List<Guid> editionEventsUids,
            List<Guid> roomsUids,
            List<Guid> tracksUids,
            List<Guid> pillarsUids,
            List<Guid> presentationFormatsUids,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(false, editionId)
                                .FindByApiFilters(editionDates, editionEventsUids, roomsUids, tracksUids, pillarsUids, presentationFormatsUids);

            return await query
                            .Select(c => new ConferenceDto
                            {
                                Conference = c,
                                EditionEvent = c.EditionEvent,
                                RoomDto = new RoomDto
                                {
                                    Room = c.Room,
                                    RoomNameDtos = c.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                    {
                                        RoomName = rn,
                                        LanguageDto = new LanguageDto
                                        {
                                            Id = rn.Language.Id,
                                            Uid = rn.Language.Uid,
                                            Code = rn.Language.Code
                                        }
                                    })
                                },
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
                                }),
                                ConferenceSynopsisDtos = c.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
                                {
                                    ConferenceSynopsis = cs,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = cs.Language.Id,
                                        Uid = cs.Language.Uid,
                                        Name = cs.Language.Name,
                                        Code = cs.Language.Code
                                    }
                                }),
                                ConferenceTrackDtos = c.ConferenceTracks.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferenceTrackDto
                                {
                                    ConferenceTrack = cvt,
                                    Track = cvt.Track
                                }),
                                ConferencePillarDtos = c.ConferencePillars.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferencePillarDto
                                {
                                    ConferencePillar = cvt,
                                    Pillar = cvt.Pillar
                                }),
                                ConferencePresentationFormatDtos = c.ConferencePresentationFormats.Where(cht => !cht.IsDeleted).Select(cht => new ConferencePresentationFormatDto
                                {
                                    ConferencePresentationFormat = cht,
                                    PresentationFormat = cht.PresentationFormat
                                })
                            })
                            .OrderBy(c => c.Conference.StartDate)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds the API dto by uid asynchronous.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ConferenceDto> FindApiDtoByUidAsync(Guid conferenceUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(conferenceUid)
                                .FindByEditionId(false, editionId);

            return await query
                            .Select(c => new ConferenceDto
                            {
                                Conference = c,
                                EditionEvent = c.EditionEvent,
                                RoomDto = new RoomDto
                                {
                                    Room = c.Room,
                                    RoomNameDtos = c.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                    {
                                        RoomName = rn,
                                        LanguageDto = new LanguageDto
                                        {
                                            Id = rn.Language.Id,
                                            Uid = rn.Language.Uid,
                                            Code = rn.Language.Code
                                        }
                                    })
                                },
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
                                }),
                                ConferenceSynopsisDtos = c.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
                                {
                                    ConferenceSynopsis = cs,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = cs.Language.Id,
                                        Uid = cs.Language.Uid,
                                        Name = cs.Language.Name,
                                        Code = cs.Language.Code
                                    }
                                }),
                                ConferenceTrackDtos = c.ConferenceTracks.Where(cvt => !cvt.IsDeleted).Select(cvt => new ConferenceTrackDto
                                {
                                    ConferenceTrack = cvt,
                                    Track = cvt.Track
                                }),
                                ConferencePresentationFormatDtos = c.ConferencePresentationFormats.Where(cht => !cht.IsDeleted).Select(cht => new ConferencePresentationFormatDto
                                {
                                    ConferencePresentationFormat = cht,
                                    PresentationFormat = cht.PresentationFormat
                                }),
                                ConferenceParticipantDtos = c.ConferenceParticipants.Where(cp => !cp.IsDeleted).Select(cp => new ConferenceParticipantDto
                                {
                                    ConferenceParticipant = cp,
                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                    {
                                        AttendeeCollaborator = cp.AttendeeCollaborator,
                                        Collaborator = cp.AttendeeCollaborator.Collaborator,
                                        JobTitlesDtos = cp.AttendeeCollaborator.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                        MiniBiosDtos = cp.AttendeeCollaborator.Collaborator.MiniBios.Where(d => !d.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                        AttendeeOrganizationsDtos = cp.AttendeeCollaborator.AttendeeOrganizationCollaborators
                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                            .Select(aoc => new AttendeeOrganizationDto
                                            {
                                                AttendeeOrganization = aoc.AttendeeOrganization,
                                                Organization = aoc.AttendeeOrganization.Organization
                                            })
                                    },
                                    ConferenceParticipantRoleDto = new ConferenceParticipantRoleDto
                                    {
                                        ConferenceParticipantRole = cp.ConferenceParticipantRole,
                                        ConferenceParticipantRoleTitleDtos = cp.ConferenceParticipantRole.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                        {
                                            ConferenceParticipantRoleTitle = cprt,
                                            LanguageDto = new LanguageBaseDto
                                            {
                                                Id = cprt.Language.Id,
                                                Uid = cprt.Language.Uid,
                                                Name = cprt.Language.Name,
                                                Code = cprt.Language.Code
                                            }
                                        })
                                    }
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        #endregion
    }
}
