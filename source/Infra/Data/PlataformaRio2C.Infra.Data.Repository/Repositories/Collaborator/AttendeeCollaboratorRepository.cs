// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
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
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;

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

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByEditionId(this IQueryable<AttendeeCollaborator> query, int editionId, bool showAllEditions)
        {
            query = query.Where(ac => (showAllEditions || ac.EditionId == editionId)
                                      && !ac.IsDeleted
                                      && !ac.Edition.IsDeleted
                                      && !ac.Collaborator.IsDeleted);
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

        /// <summary>Finds the by collaborator uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByCollaboratorUid(this IQueryable<AttendeeCollaborator> query, Guid collaboratorUid)
        {
            query = query.Where(ac => ac.Collaborator.Uid == collaboratorUid);

            return query;
        }

        /// <summary>
        /// Finds the by organization uid.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationUids">The organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByOrganizationUids(this IQueryable<AttendeeCollaborator> query, List<Guid> organizationUids)
        {
            query = query.Where(ac => ac.AttendeeOrganizationCollaborators.Any(aoc => organizationUids.Contains(aoc.AttendeeOrganization.Organization.Uid)));

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

        /// <summary>Determines whether [is onboarding finished].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> IsOnboardingFinished(this IQueryable<AttendeeCollaborator> query)
        {
            query = query.Where(ac => ac.OnboardingFinishDate.HasValue);

            return query;
        }

        /// <summary>Determines whether [is contacts download excel].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> IsContactsDownloadExcel(this IQueryable<AttendeeCollaborator> query)
        {
            query = query.Where(ac => ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted // Industry
                                                                              && !act.CollaboratorType.IsDeleted
                                                                              && act.CollaboratorType.Name == Domain.Constants.CollaboratorType.Industry)
                                      || (ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted // Player without tickets
                                                                                 && !act.CollaboratorType.IsDeleted
                                                                                 && act.CollaboratorType.Name == Domain.Constants.CollaboratorType.ExecutiveAudiovisual)
                                          && !ac.AttendeeCollaboratorTickets.Any(act => !act.IsDeleted && !act.AttendeeSalesPlatformTicketType.IsDeleted)));

            return query;
        }

        /// <summary>Determines whether [is network participant].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> IsNetworkParticipant(this IQueryable<AttendeeCollaborator> query)
        {
            query = query.Where(ac => ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted // Industry
                                                                              && !act.CollaboratorType.IsDeleted
                                                                              && Domain.Constants.CollaboratorType.NetworksArray.Contains(act.CollaboratorType.Name)));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByKeywords(this IQueryable<AttendeeCollaborator> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<AttendeeCollaborator>(false);
                var innerBadgeWhere = PredicateBuilder.New<AttendeeCollaborator>(true);
                var innerUserNameWhere = PredicateBuilder.New<AttendeeCollaborator>(true);
                var innerJobTitleWhere = PredicateBuilder.New<AttendeeCollaborator>(true);
                var innerOrganizationNameWhere = PredicateBuilder.New<AttendeeCollaborator>(true);


                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerBadgeWhere = innerBadgeWhere.And(ac => ac.Collaborator.Badge.Contains(keyword));
                        innerUserNameWhere = innerUserNameWhere.And(ac => ac.Collaborator.User.Name.Contains(keyword));
                        innerJobTitleWhere = innerJobTitleWhere.And(ac => ac.Collaborator.JobTitles.Any(jb => !jb.IsDeleted
                                                                                                              && jb.Value.Contains(keyword)));
                        innerOrganizationNameWhere = innerOrganizationNameWhere.And(ac =>
                            ac.AttendeeOrganizationCollaborators.Any(aoc =>
                                    !aoc.IsDeleted
                                    && !aoc.AttendeeOrganization.IsDeleted
                                    && !aoc.AttendeeOrganization.Organization.IsDeleted
                                    && aoc.AttendeeOrganization.Organization.TradeName.Contains(keyword)));
                    }
                }

                outerWhere = outerWhere.Or(innerBadgeWhere);
                outerWhere = outerWhere.Or(innerUserNameWhere);
                outerWhere = outerWhere.Or(innerJobTitleWhere);
                outerWhere = outerWhere.Or(innerOrganizationNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by collaborator role uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorRoleUid">The collaborator role uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByCollaboratorRoleUid(this IQueryable<AttendeeCollaborator> query, Guid? collaboratorRoleUid)
        {
            if (collaboratorRoleUid.HasValue)
            {
                query = query.Where(ac => ac.Collaborator.Role.Uid == collaboratorRoleUid && !ac.Collaborator.Role.IsDeleted);
            }

            return query;
        }

        /// <summary>Finds the by collaborator industry uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorIndustryUid">The collaborator industry uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeCollaborator> FindByCollaboratorIndustryUid(this IQueryable<AttendeeCollaborator> query, Guid? collaboratorIndustryUid)
        {
            if (collaboratorIndustryUid.HasValue)
            {
                query = query.Where(ac => ac.Collaborator.Industry.Uid == collaboratorIndustryUid && !ac.Collaborator.Role.IsDeleted);
            }

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

    #region AttendeeCollaboratorNetworkDto IQueryable Extensions

    /// <summary>
    /// AttendeeCollaboratorNetworkDtoIQueryableExtensions
    /// </summary>
    internal static class AttendeeCollaboratorNetworkDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<AttendeeCollaboratorNetworkDto>> ToListPagedAsync(this IQueryable<AttendeeCollaboratorNetworkDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }

        internal static async Task<IPagedList<List>> ToListPagedAsync(this IQueryable<List> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
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

        /// <summary> Gets the base query.</summary>
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

        #region Widgets

        /// <summary>Finds the site detailst dto by collaborator uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorSiteDetailsDto> FindSiteDetailstDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorSiteDetailsDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                HasLogistic = ac.Logistics.Any(l => !l.IsDeleted)
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site main information widget dto by collaborator uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorSiteMainInformationWidgetDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                User = ac.Collaborator.User,
                                JobTitlesDtos = ac.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                MiniBioDtos = ac.Collaborator.MiniBios.Where(d => !d.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                EditionParticipationDtos = ac.Collaborator.EditionParticipantions.Where(d => !d.IsDeleted).Select(d => new CollaboratorEditionParticipationBaseDto()
                                {
                                    Id = d.Id,
                                    Uid = d.Uid,
                                    EditionId = d.EditionId,
                                    EditionUid = d.Edition.Uid,
                                    EditionName = d.Edition.Name
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site company widget dto by collaborator uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorSiteCompanyWidgetDto> FindSiteCompanyWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorSiteCompanyWidgetDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                AttendeeOrganizationsDtos = ac.AttendeeOrganizationCollaborators
                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                    .Select(aoc => new AttendeeOrganizationDto
                                                                    {
                                                                        AttendeeOrganization = aoc.AttendeeOrganization,
                                                                        Organization = aoc.AttendeeOrganization.Organization
                                                                    })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the onboarding information widget dto asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorOnboardingInfoWidgetDto> FindOnboardingInfoWidgetDtoAsync(Guid collaboratorUid, Guid collaboratorTypeUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorOnboardingInfoWidgetDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                User = ac.Collaborator.User,
                                AttendeeCollaboratorTypeDto = ac.AttendeeCollaboratorTypes
                                                                    .Where(act => !act.IsDeleted
                                                                                  && act.CollaboratorType.Uid == collaboratorTypeUid
                                                                                  && !act.CollaboratorType.IsDeleted)
                                                                    .Select(act => new AttendeeCollaboratorTypeDto
                                                                    {
                                                                        AttendeeCollaboratorType = act,
                                                                        CollaboratorType = act.CollaboratorType
                                                                    })
                                                                    .FirstOrDefault()
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the API configuration widget dto by collaborator uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorApiConfigurationWidgetDto> FindApiConfigurationWidgetDtoByCollaboratorUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorApiConfigurationWidgetDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                User = ac.Collaborator.User,
                                AttendeeCollaboratorTypeDtos = ac.AttendeeCollaboratorTypes.Where(act => !act.IsDeleted).Select(act => new AttendeeCollaboratorTypeDto
                                {
                                    AttendeeCollaboratorType = act,
                                    CollaboratorType = act.CollaboratorType
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the conference widget dto asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorDto> FindConferenceWidgetDtoAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                ConferenceDtos = ac.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted).Select(cp => cp.Conference).Distinct().Select(c => new ConferenceDto
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
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorDto> FindParticipantsWidgetDtoAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                ConferenceParticipantDtos = ac.ConferenceParticipants.Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted && !cp.ConferenceParticipantRole.IsDeleted).Select(cp => new ConferenceParticipantDto
                                {
                                    ConferenceParticipant = cp,
                                    ConferenceParticipantRoleDto = new ConferenceParticipantRoleDto
                                    {
                                        ConferenceParticipantRole = cp.ConferenceParticipantRole,
                                        ConferenceParticipantRoleTitleDtos = cp.ConferenceParticipantRole.ConferenceParticipantRoleTitles.Where(cprt => !cprt.IsDeleted).Select(cprt => new ConferenceParticipantRoleTitleDto
                                        {
                                            ConferenceParticipantRoleTitle = cprt,
                                            LanguageDto = new LanguageDto
                                            {
                                                Id = cprt.Language.Id,
                                                Uid = cprt.Language.Uid,
                                                Code = cprt.Language.Code
                                            }
                                        })
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

        /// <summary>Finds the logistic information widget dto asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeCollaboratorDto> FindLogisticInfoWidgetDtoAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorUid(collaboratorUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                LogisticDto = ac.Logistics.Where(l => !l.IsDeleted).Select(l => new LogisticDto
                                {
                                    Logistic = l,
                                    LogisticAirfareDtos = l.LogisticAirfares.Where(la => !la.IsDeleted).Select(la => new LogisticAirfareDto
                                    {
                                        LogisticAirfare = la
                                    }),
                                    LogisticAccommodationDtos = l.LogisticAccommodations.Where(la => !la.IsDeleted).Select(la => new LogisticAccommodationDto
                                    {
                                        LogisticAccommodation = la,
                                        PlaceDto = new PlaceDto
                                        {
                                            Place = la.AttendeePlace.Place,
                                            AddressDto = la.AttendeePlace.Place.Address == null || la.AttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                            {
                                                Address = la.AttendeePlace.Place.Address,
                                                City = la.AttendeePlace.Place.Address.City,
                                                State = la.AttendeePlace.Place.Address.State,
                                                Country = la.AttendeePlace.Place.Address.Country
                                            }
                                        }
                                    }),
                                    LogisticTransferDtos = l.LogisticTransfers.Where(lt => !lt.IsDeleted).Select(lt => new LogisticTransferDto
                                    {
                                        LogisticTransfer = lt,
                                        FromPlaceDto = new PlaceDto
                                        {
                                            Place = lt.FromAttendeePlace.Place,
                                            AddressDto = lt.FromAttendeePlace.Place.Address == null || lt.FromAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                            {
                                                Address = lt.FromAttendeePlace.Place.Address,
                                                City = lt.FromAttendeePlace.Place.Address.City,
                                                State = lt.FromAttendeePlace.Place.Address.State,
                                                Country = lt.FromAttendeePlace.Place.Address.Country
                                            }
                                        },
                                        ToPlaceDto = new PlaceDto
                                        {
                                            Place = lt.ToAttendeePlace.Place,
                                            AddressDto = lt.ToAttendeePlace.Place.Address == null || lt.ToAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                            {
                                                Address = lt.ToAttendeePlace.Place.Address,
                                                City = lt.ToAttendeePlace.Place.Address.City,
                                                State = lt.ToAttendeePlace.Place.Address.State,
                                                Country = lt.ToAttendeePlace.Place.Address.Country
                                            }
                                        }
                                    })
                                }).FirstOrDefault()
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the player executives logistics information widget dto asynchronous.
        /// </summary>
        /// <param name="organizationUids">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCollaboratorDto>> FindPlayerExecutivesLogisticsDtosAsync(List<Guid> organizationUids, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUids(organizationUids)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ac => new AttendeeCollaboratorDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                AttendeeOrganizationsDtos = ac.AttendeeOrganizationCollaborators
                                                                .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                .Select(aoc => new AttendeeOrganizationDto
                                                                {
                                                                    AttendeeOrganization = aoc.AttendeeOrganization,
                                                                    Organization = aoc.AttendeeOrganization.Organization
                                                                })
                                                                .ToList(),
                                LogisticDto = ac.Logistics
                                                        .Where(l => !l.IsDeleted)
                                                        .Select(l => new LogisticDto
                                                        {
                                                            Logistic = l,
                                                            LogisticAirfareDtos = l.LogisticAirfares.Where(la => !la.IsDeleted).OrderBy(la => la.ArrivalDate).Select(la => new LogisticAirfareDto
                                                            {
                                                                LogisticAirfare = la
                                                            }),
                                                            LogisticAccommodationDtos = l.LogisticAccommodations.Where(la => !la.IsDeleted).Select(la => new LogisticAccommodationDto
                                                            {
                                                                LogisticAccommodation = la,
                                                                PlaceDto = new PlaceDto
                                                                {
                                                                    Place = la.AttendeePlace.Place,
                                                                    AddressDto = la.AttendeePlace.Place.Address == null || la.AttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                                                    {
                                                                        Address = la.AttendeePlace.Place.Address,
                                                                        City = la.AttendeePlace.Place.Address.City,
                                                                        State = la.AttendeePlace.Place.Address.State,
                                                                        Country = la.AttendeePlace.Place.Address.Country
                                                                    }
                                                                }
                                                            }),
                                                            LogisticTransferDtos = l.LogisticTransfers.Where(lt => !lt.IsDeleted).Select(lt => new LogisticTransferDto
                                                            {
                                                                LogisticTransfer = lt,
                                                                FromPlaceDto = new PlaceDto
                                                                {
                                                                    Place = lt.FromAttendeePlace.Place,
                                                                    AddressDto = lt.FromAttendeePlace.Place.Address == null || lt.FromAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                                                    {
                                                                        Address = lt.FromAttendeePlace.Place.Address,
                                                                        City = lt.FromAttendeePlace.Place.Address.City,
                                                                        State = lt.FromAttendeePlace.Place.Address.State,
                                                                        Country = lt.FromAttendeePlace.Place.Address.Country
                                                                    }
                                                                },
                                                                ToPlaceDto = new PlaceDto
                                                                {
                                                                    Place = lt.ToAttendeePlace.Place,
                                                                    AddressDto = lt.ToAttendeePlace.Place.Address == null || lt.ToAttendeePlace.Place.Address.IsDeleted ? null : new AddressDto
                                                                    {
                                                                        Address = lt.ToAttendeePlace.Place.Address,
                                                                        City = lt.ToAttendeePlace.Place.Address.City,
                                                                        State = lt.ToAttendeePlace.Place.Address.State,
                                                                        Country = lt.ToAttendeePlace.Place.Address.Country
                                                                    }
                                                                }
                                                            })
                                                        })
                                                        .FirstOrDefault(),
                                ConferenceDtos = ac.ConferenceParticipants
                                                        .Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted)
                                                        .Select(cp => cp.Conference)
                                                        .OrderBy(c => c.StartDate)
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
                                                        .ToList(),
                                BuyerNegotiationDtos = ac.AttendeeOrganizationCollaborators
                                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                            .SelectMany(aoc => aoc.AttendeeOrganization.ProjectBuyerEvaluations.Where(pbe => !pbe.IsDeleted && !pbe.Project.IsDeleted && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid)
                                                                .SelectMany(pbe => pbe.Negotiations.Where(n => !n.IsDeleted)
                                                                    .Select(n => new NegotiationDto
                                                                {
                                                                    Negotiation = n,
                                                                    ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto
                                                                    {
                                                                        ProjectBuyerEvaluation = n.ProjectBuyerEvaluation,
                                                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                                                        {
                                                                            AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                                                            Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization
                                                                        },
                                                                        ProjectDto = new ProjectDto
                                                                        {
                                                                            Project = n.ProjectBuyerEvaluation.Project,
                                                                            SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                                                            {
                                                                                AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                                                                Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization
                                                                            },
                                                                            ProjectTitleDtos = n.ProjectBuyerEvaluation.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                                                            {
                                                                                ProjectTitle = t,
                                                                                Language = t.Language
                                                                            }),
                                                                            ProjectLogLineDtos = n.ProjectBuyerEvaluation.Project.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                                                            {
                                                                                ProjectLogLine = ll,
                                                                                Language = ll.Language
                                                                            })
                                                                        }
                                                                    },
                                                                    RoomDto = new RoomDto
                                                                    {
                                                                        Room = n.Room,
                                                                        RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                                                        {
                                                                            RoomName = rn,
                                                                            LanguageDto = new LanguageDto
                                                                            {
                                                                                Id = rn.Language.Id,
                                                                                Uid = rn.Language.Uid,
                                                                                Code = rn.Language.Code
                                                                            }
                                                                        })
                                                                    }
                                                                })))
                                                                    .OrderBy(ndto => ndto.Negotiation.StartDate),
                                SellerNegotiationDtos = ac.AttendeeOrganizationCollaborators
                                                                .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted&& !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                .SelectMany(aoc => aoc.AttendeeOrganization.SellProjects
                                                                    .SelectMany(sp => sp.ProjectBuyerEvaluations.Where(pbe => !pbe.IsDeleted && !pbe.Project.IsDeleted && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid)
                                                                        .SelectMany(pbe => pbe.Negotiations.Where(n => !n.IsDeleted)
                                                                            .Select(n => new NegotiationDto
                                                                            {
                                                                                Negotiation = n,
                                                                                ProjectBuyerEvaluationDto = new ProjectBuyerEvaluationDto
                                                                                {
                                                                                    ProjectBuyerEvaluation = n.ProjectBuyerEvaluation,
                                                                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                                                                    {
                                                                                        AttendeeOrganization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization,
                                                                                        Organization = n.ProjectBuyerEvaluation.BuyerAttendeeOrganization.Organization
                                                                                    },
                                                                                    ProjectDto = new ProjectDto
                                                                                    {
                                                                                        Project = n.ProjectBuyerEvaluation.Project,
                                                                                        SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                                                                        {
                                                                                            AttendeeOrganization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization,
                                                                                            Organization = n.ProjectBuyerEvaluation.Project.SellerAttendeeOrganization.Organization
                                                                                        },
                                                                                        ProjectTitleDtos = n.ProjectBuyerEvaluation.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                                                                        {
                                                                                            ProjectTitle = t,
                                                                                            Language = t.Language
                                                                                        }),
                                                                                        ProjectLogLineDtos = n.ProjectBuyerEvaluation.Project.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                                                                        {
                                                                                            ProjectLogLine = ll,
                                                                                            Language = ll.Language
                                                                                        })
                                                                                    }
                                                                                },
                                                                                RoomDto = new RoomDto
                                                                                {
                                                                                    Room = n.Room,
                                                                                    RoomNameDtos = n.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
                                                                                    {
                                                                                        RoomName = rn,
                                                                                        LanguageDto = new LanguageDto
                                                                                        {
                                                                                            Id = rn.Language.Id,
                                                                                            Uid = rn.Language.Uid,
                                                                                            Code = rn.Language.Code
                                                                                        }
                                                                                    })
                                                                                }
                                                                            }))))
                                                                            .OrderBy(ndto => ndto.Negotiation.StartDate)
                            })
                            .ToListAsync();
        }

        #endregion

        #region Networks

        /// <summary>Finds all excel network dto by edition identifier asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCollaboratorNetworkDto>> FindAllExcelNetworkDtoByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByEditionId(editionId, false)
                                .IsOnboardingFinished()
                                .IsContactsDownloadExcel();

            return await query
                            .Select(ac => new AttendeeCollaboratorNetworkDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                JobTitlesDtos = ac.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationsDtos = ac.AttendeeOrganizationCollaborators
                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                    .Select(aoc => new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = aoc.AttendeeOrganization,
                                        Organization = aoc.AttendeeOrganization.Organization
                                    })
                            })
                            .ToListAsync();
        }

        /// <summary>Finds all network dto by edition identifier paged asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="collaboratorRoleUid">The collaborator role uid.</param>
        /// <param name="collaboratorIndustryUid">The collaborator industry uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeCollaboratorNetworkDto>> FindAllNetworkDtoByEditionIdPagedAsync(
            int editionId,
            string keywords,
            Guid? collaboratorRoleUid,
            Guid? collaboratorIndustryUid,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery(true)
                                .FindByEditionId(editionId, false)
                                .IsOnboardingFinished()
                                .IsNetworkParticipant()
                                .FindByKeywords(keywords)
                                .FindByCollaboratorRoleUid(collaboratorRoleUid)
                                .FindByCollaboratorIndustryUid(collaboratorIndustryUid);

            return await query
                            .Select(ac => new AttendeeCollaboratorNetworkDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                User = ac.Collaborator.User,
                                JobTitlesDtos = ac.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationsDtos = ac.AttendeeOrganizationCollaborators
                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                    .Select(aoc => new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = aoc.AttendeeOrganization,
                                        Organization = aoc.AttendeeOrganization.Organization
                                    })
                            })
                            .OrderBy(acnd => acnd.Collaborator.FirstName)
                            .ThenBy(acnd => acnd.Collaborator.LastNames)
                            .ToListPagedAsync(page, pageSize);
        }


        #endregion

        #region Api

        /// <summary>Finds all API configuration widget dto by highlight.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        public async Task<List<AttendeeCollaboratorApiConfigurationWidgetDto>> FindAllApiConfigurationWidgetDtoByHighlight(int editionId, string collaboratorTypeName)
        {
            var query = this.GetBaseQuery()
                                .Where(ac => !ac.IsDeleted
                                             && ac.EditionId == editionId
                                             && ac.AttendeeCollaboratorTypes.Any(aot => !aot.IsDeleted
                                                                                     && aot.CollaboratorType.Name == collaboratorTypeName
                                                                                     && aot.ApiHighlightPosition.HasValue));

            return await query
                            .Select(ac => new AttendeeCollaboratorApiConfigurationWidgetDto
                            {
                                AttendeeCollaborator = ac,
                                Collaborator = ac.Collaborator,
                                User = ac.Collaborator.User,
                                AttendeeCollaboratorTypeDtos = ac.AttendeeCollaboratorTypes.Where(act => !act.IsDeleted).Select(act => new AttendeeCollaboratorTypeDto
                                {
                                    AttendeeCollaboratorType = act,
                                    CollaboratorType = act.CollaboratorType
                                })
                            })
                            .ToListAsync();
        }

        /// <summary>Finds all dropdown API list dto paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<List>> FindAllDropdownApiListDtoPaged(int editionId, string keywords, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, false)
                                .FindByKeywords(keywords);

            return await query
                            .Select(c => new List()
                            {
                                Uid = c.Uid,
                                CollaboratorUid = c.Collaborator.Uid,
                                Name = c.Collaborator.FirstName + " " + c.Collaborator.LastNames,
                                BadgeName = c.Collaborator.Badge,
                                ImageUploadDate = c.Collaborator.ImageUploadDate,
                                JobTitlesDtos = c.Collaborator.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                OrganizationsDtos = c.Collaborator.AttendeeCollaborators
                                    .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                    .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                        .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                        .Select(aoc => new OrganizationApiListDto
                                        {
                                            Uid = aoc.AttendeeOrganization.Organization.Uid,
                                            CompanyName = aoc.AttendeeOrganization.Organization.CompanyName,
                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                            ImageUploadDate = aoc.AttendeeOrganization.Organization.ImageUploadDate

                                        })),
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate
                            })
                            .OrderBy(o => o.Name)
                            .ToListPagedAsync(page, pageSize);
        }

        #endregion
    }
}