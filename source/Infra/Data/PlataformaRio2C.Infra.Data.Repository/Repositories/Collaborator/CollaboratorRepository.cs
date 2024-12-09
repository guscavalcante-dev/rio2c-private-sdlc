// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-21-2024
// ***********************************************************************
// <copyright file="CollaboratorRepository.cs" company="Softo">
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
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Collaborator IQueryable Extensions

    /// <summary>
    /// CollaboratorIQueryableExtensions
    /// </summary>
    internal static class CollaboratorIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByUid(this IQueryable<Collaborator> query, Guid collaboratorUid)
        {
            query = query.Where(c => c.Uid == collaboratorUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByUids(this IQueryable<Collaborator> query, List<Guid> collaboratorsUids)
        {
            if (collaboratorsUids?.Any() == true)
            {
                query = query.Where(c => collaboratorsUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>Finds the by sales platform attendee identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindBySalesPlatformAttendeeId(this IQueryable<Collaborator> query, string salesPlatformAttendeeId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.AttendeeCollaboratorTickets.Any(act => !act.IsDeleted
                                                                                                                 && act.SalesPlatformAttendeeId == salesPlatformAttendeeId)));

            return query;
        }

        /// <summary>
        /// Finds by the user email.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByUserEmail(this IQueryable<Collaborator> query, string email)
        {
            query = query.Where(c => c.User.Email == email);

            return query;
        }

        /// <summary>
        /// Finds the by admin role name and admin collaborator type name and by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByAdminRoleNameAndAdminCollaboratorTypeNameAndByEditionId(
            this IQueryable<Collaborator> query,
            string roleName,
            string collaboratorTypeName,
            bool showAllEditions,
            bool showAllParticipants,
            int? editionId)
        {
            query = query.Where(c => c.User.Roles.Any(r => r.Name == Constants.Role.Admin)
                                     || ((showAllParticipants || c.User.Roles.Any(r => r.Name == Constants.Role.AdminPartial))
                                         && c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                              && !ac.IsDeleted
                                                                              && !ac.Edition.IsDeleted
                                                                              && (showAllParticipants
                                                                                  || ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                             && !act.CollaboratorType.IsDeleted
                                                                                                                             && Constants.CollaboratorType.Admins.Contains(act.CollaboratorType.Name))))));

            if (!string.IsNullOrEmpty(roleName))
            {
                query = query.Where(c => c.User.Roles.Any(r => r.Name == roleName));
            }

            if (!string.IsNullOrEmpty(collaboratorTypeName))
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                           && !ac.IsDeleted
                                                                           && !ac.Edition.IsDeleted
                                                                           && (ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                       && !act.CollaboratorType.IsDeleted
                                                                                                                       && act.CollaboratorType.Name == collaboratorTypeName))));
            }

            return query;
        }

        /// <summary>Finds the by collaborator type name and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<Collaborator> query, string[] collaboratorTypeNames, bool showAllEditions, bool showAllParticipants, int? editionId, bool showDeleted = false)
        {
            if (collaboratorTypeNames == null)
            {
                collaboratorTypeNames = new string[] { };
            }

            query = query.Where(c => showAllParticipants || c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                                               && (!ac.IsDeleted || showDeleted)
                                                                                               && (!ac.Edition.IsDeleted || showDeleted)
                                                                                               && (showAllParticipants
                                                                                                   || ac.AttendeeCollaboratorTypes
                                                                                                           .Any(act => (!act.IsDeleted || showDeleted)
                                                                                                                        && (!act.CollaboratorType.IsDeleted || showDeleted)
                                                                                                                        && collaboratorTypeNames.Contains(act.CollaboratorType.Name)))));

            return query;
        }

        /// <summary>Finds the by collaborator type name and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">Name of the collaborator type.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<Collaborator> query, string[] collaboratorTypeNames, int? editionId, bool showDeleted = false)
        {
            if (collaboratorTypeNames == null)
            {
                collaboratorTypeNames = new string[] { };
            }

            query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                        && (!ac.IsDeleted || showDeleted)
                                                                        && (!ac.Edition.IsDeleted || showDeleted)
                                                                        && (ac.AttendeeCollaboratorTypes
                                                                                .Any(act => (!act.IsDeleted || showDeleted)
                                                                                            && (!act.CollaboratorType.IsDeleted || showDeleted)
                                                                                            && collaboratorTypeNames.Contains(act.CollaboratorType.Name)))));
            return query;
        }

        /// <summary>Finds the logistics by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsored">if set to <c>true</c> [show all sponsored].</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindLogisticsByEditionId(this IQueryable<Collaborator> query, int editionId, bool showAllParticipants, bool showAllSponsored)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                       && !ac.IsDeleted
                                                                       && !ac.Edition.IsDeleted
                                                                       && (showAllParticipants
                                                                           || (showAllSponsored && ac.Logistics.Any(l => !l.IsDeleted))
                                                                               || (!showAllSponsored && ac.Logistics.Any(l => !l.IsDeleted
                                                                                                                              && (l.AirfareAttendeeLogisticSponsor.IsLogisticListDisplayed
                                                                                                                                  || l.AccommodationAttendeeLogisticSponsor.IsLogisticListDisplayed
                                                                                                                                  || l.AirportTransferAttendeeLogisticSponsor.IsLogisticListDisplayed
                                                                                                                                  || l.IsCityTransferRequired
                                                                                                                                  || l.IsVehicleDisposalRequired))))));

            return query;
        }

        /// <summary>Finds the by highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">Name of the collaborator type.</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByHighlights(this IQueryable<Collaborator> query, string[] collaboratorTypeNames, bool? showHighlights)
        {
            if (showHighlights.HasValue && showHighlights.Value
                && collaboratorTypeNames.HasValue())
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                           && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                      && collaboratorTypeNames.Contains(act.CollaboratorType.Name)
                                                                                                                      && act.IsApiDisplayEnabled
                                                                                                                      && act.ApiHighlightPosition.HasValue)));
            }

            return query;
        }

        /// <summary>Finds the by not publishable.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showNotPublishableToApi">The show not publishable.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindNotPublishableToApi(this IQueryable<Collaborator> query, int? editionId, bool? showNotPublishableToApi)
        {
            if (showNotPublishableToApi == true)
            {
                query = query
                    .Where(o =>
                        o.ImageUploadDate == null
                        || o.FirstName == null
                        || o.LastNames == null
                        || o.Badge == null
                        || o.AttendeeCollaborators.Any(ac => !ac.IsDeleted && ac.EditionId == editionId && ac.SpeakerTermsAcceptanceDate == null)
                        || o.JobTitles.Any(jt => !jt.IsDeleted && jt.Value == null)
                        || o.MiniBios.Any(mb => !mb.IsDeleted && mb.Value == null)
                    );
            }
            return query;
        }

        /// <summary>Finds the by API highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="highlights">The highlights.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByApiHighlights(this IQueryable<Collaborator> query, string collaboratorTypeName, int? highlights, bool showDeleted = false)
        {
            if (highlights == 0 || highlights == 1)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                           && ac.AttendeeCollaboratorTypes.Any(act => (!act.IsDeleted || showDeleted)
                                                                                                                      && act.CollaboratorType.Name == collaboratorTypeName
                                                                                                                      && act.IsApiDisplayEnabled
                                                                                                                      && (highlights.Value == 1 ? act.ApiHighlightPosition.HasValue : !act.ApiHighlightPosition.HasValue))));
            }

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByEditionId(this IQueryable<Collaborator> query, bool showAllEditions, int? editionId)
        {
            if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                           && !ac.IsDeleted
                                                                           && !ac.Edition.IsDeleted));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByKeywords(this IQueryable<Collaborator> query, string keywords, int? editionId, bool showDeleted = false)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Collaborator>(false);
                var innerExecutiveBadgeNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerExecutiveNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerExecutiveEmailWhere = PredicateBuilder.New<Collaborator>(true);
                var innerOrganizationNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerHoldingNameWhere = PredicateBuilder.New<Collaborator>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerExecutiveBadgeNameWhere = innerExecutiveBadgeNameWhere.And(c => c.Badge.Contains(keyword));
                        innerExecutiveNameWhere = innerExecutiveNameWhere.And(c => c.User.Name.Contains(keyword));
                        innerExecutiveEmailWhere = innerExecutiveEmailWhere.And(c => c.User.Email.Contains(keyword));
                        innerOrganizationNameWhere = innerOrganizationNameWhere
                                                        .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
                                                                                                    && (!ac.IsDeleted || showDeleted)
                                                                                                    && (!ac.Edition.IsDeleted || showDeleted)
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                            .Any(aoc => (!aoc.IsDeleted || showDeleted)
                                                                                                                        && (!aoc.AttendeeOrganization.IsDeleted || showDeleted)
                                                                                                                        && (!aoc.AttendeeOrganization.Organization.IsDeleted || showDeleted)
                                                                                                                        && aoc.AttendeeOrganization.Organization.Name.Contains(keyword))));
                        innerHoldingNameWhere = innerHoldingNameWhere
                                                        .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
                                                                                                    && (!ac.IsDeleted || showDeleted)
                                                                                                    && (!ac.Edition.IsDeleted || showDeleted)
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                        .Any(aoc => (!aoc.IsDeleted || showDeleted)
                                                                                                                    && (!aoc.AttendeeOrganization.IsDeleted || showDeleted)
                                                                                                                    && (!aoc.AttendeeOrganization.Organization.IsDeleted || showDeleted)
                                                                                                                    && (!aoc.AttendeeOrganization.Organization.Holding.IsDeleted || showDeleted)
                                                                                                                    && aoc.AttendeeOrganization.Organization.Holding.Name.Contains(keyword))));

                    }
                }

                outerWhere = outerWhere.Or(innerExecutiveBadgeNameWhere);
                outerWhere = outerWhere.Or(innerExecutiveNameWhere);
                outerWhere = outerWhere.Or(innerExecutiveEmailWhere);
                outerWhere = outerWhere.Or(innerOrganizationNameWhere);
                outerWhere = outerWhere.Or(innerHoldingNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by names.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByNames(this IQueryable<Collaborator> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Collaborator>(false);
                var innerExecutiveBadgeNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerExecutiveNameWhere = PredicateBuilder.New<Collaborator>(true);
                var innerExecutiveEmailWhere = PredicateBuilder.New<Collaborator>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerExecutiveBadgeNameWhere = innerExecutiveBadgeNameWhere.And(c => c.Badge.Contains(keyword));
                        innerExecutiveNameWhere = innerExecutiveNameWhere.And(c => c.User.Name.Contains(keyword));
                        innerExecutiveEmailWhere = innerExecutiveEmailWhere.And(c => c.User.Email.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerExecutiveBadgeNameWhere);
                outerWhere = outerWhere.Or(innerExecutiveNameWhere);
                outerWhere = outerWhere.Or(innerExecutiveEmailWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Determines whether [has project in negotiation] [the specified edition identifier].</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="hasProjectNegotiation">if set to <c>true</c> [has project negotiation].</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> HasProjectInNegotiation(this IQueryable<Collaborator> query, int editionId, bool hasProjectNegotiation)
        {
            if (hasProjectNegotiation)
            {
                query = query.Where(c => c.AttendeeCollaborators
                                                .Any(ac => ac.EditionId == editionId
                                                           && ac.AttendeeOrganizationCollaborators
                                                                    .Any(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted
                                                                                && aoc.AttendeeOrganization.ProjectBuyerEvaluations
                                                                                        .Any(pbe => !pbe.IsDeleted && !pbe.Project.IsDeleted
                                                                                                    && pbe.Negotiations.Any(n => !n.IsDeleted)))));
            }

            return query;
        }

        /// <summary>
        /// Determines whether [is API display enabled] [the specified edition identifier].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showDeleted">if set to <c>true</c> [show deleted].</param>
        /// <param name="skipIsApiDisplayEnabledVerification">if set to <c>true</c> [skip is API display enabled verification].</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> IsApiDisplayEnabled(this IQueryable<Collaborator> query, int editionId, string collaboratorTypeName, bool showDeleted = false, bool skipIsApiDisplayEnabledVerification = false)
        {
            if (skipIsApiDisplayEnabledVerification)
            {
                return query;
            }
            else
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                           && ac.AttendeeCollaboratorTypes.Any(aot => (!aot.IsDeleted || showDeleted)
                                                                                                                      && aot.CollaboratorType.Name == collaboratorTypeName
                                                                                                                      && aot.IsApiDisplayEnabled)));

                return query;
            }
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> IsNotDeleted(this IQueryable<Collaborator> query)
        {
            query = query.Where(c => !c.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by innovation organization track options uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackUids">The attendee innovation organization track uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByInnovationOrganizationTrackOptionsUids(this IQueryable<Collaborator> query, int editionId, List<Guid?> innovationOrganizationTrackUids)
        {
            if (innovationOrganizationTrackUids?.Any(aiotUid => aiotUid != null) == true)
            {
                query = query.Where(c => innovationOrganizationTrackUids.Any(iotUid =>
                                            c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId &&
                                                ac.AttendeeCollaboratorInnovationOrganizationTracks.Any(aciot => aciot.InnovationOrganizationTrackOption.Uid == iotUid && !aciot.IsDeleted))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by innovation organization track options groups uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="innovationOrganizationTrackOptionGroupUids">The innovation organization track option group uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByInnovationOrganizationTrackOptionsGroupsUids(this IQueryable<Collaborator> query, int? editionId, List<Guid?> innovationOrganizationTrackOptionGroupUids)
        {
            if (innovationOrganizationTrackOptionGroupUids?.Any(aiotgUid => aiotgUid != null) == true)
            {
                query = query.Where(c => innovationOrganizationTrackOptionGroupUids.Any(iotgUid =>
                                            c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId &&
                                                ac.AttendeeCollaboratorInnovationOrganizationTracks.Any(aciot => aciot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.Uid == iotgUid && !aciot.IsDeleted))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by interests uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByInterestsUids(this IQueryable<Collaborator> query, List<Guid?> interestsUids)
        {
            if (interestsUids?.Any(iUid => iUid.HasValue) == true)
            {
                query = query.Where(c => interestsUids.Any(iUid =>
                    c.AttendeeCollaborators.Any(ac =>
                        ac.AttendeeCollaboratorInterests.Any(caci => caci.Interest.Uid == iUid && !caci.IsDeleted))));
            }

            return query;
        }

        /// <summary>
        /// Finds the name of the by organization type.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeNames">The organization type names.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByOrganizationTypeNames(this IQueryable<Collaborator> query, string[] organizationTypeNames, bool showAllEditions, bool showAllParticipants, int? editionId)
        {
            if (organizationTypeNames?.Any(name => !string.IsNullOrEmpty(name)) == true)
            {
                query = query.Where(c => showAllParticipants || c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                                                  && !ac.IsDeleted
                                                                                                  && !ac.Edition.IsDeleted
                                                                                                  && (showAllParticipants
                                                                                                      || ac.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted
                                                                                                                                                         && aoc.AttendeeOrganization.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                                                                                           && organizationTypeNames.Contains(aot.OrganizationType.Name))))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by conferences dates.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="conferencesDates">The conferences dates.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByConferencesDates(this IQueryable<Collaborator> query, List<DateTimeOffset?> conferencesDates, bool showDeleted = false)
        {
            if (conferencesDates?.Any(d => d.HasValue) == true)
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => (!ac.IsDeleted || showDeleted) &&
                                                                            ac.ConferenceParticipants.Any(cp => (!cp.IsDeleted || showDeleted) &&
                                                                                                                conferencesDates.Contains(DbFunctions.TruncateTime(cp.Conference.StartDate)))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by conferences uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="conferencesUids">The conferences uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByConferencesUids(this IQueryable<Collaborator> query, List<Guid?> conferencesUids, bool showDeleted = false)
        {
            if (conferencesUids?.Any(d => d.HasValue) == true)
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => (!ac.IsDeleted || showDeleted) &&
                                                                            ac.ConferenceParticipants.Any(cp => (!cp.IsDeleted || showDeleted) &&
                                                                                                                conferencesUids.Contains(cp.Conference.Uid))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by conferences rooms uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="roomsUids">The rooms uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByConferencesRoomsUids(this IQueryable<Collaborator> query, List<Guid?> roomsUids, bool showDeleted = false)
        {
            if (roomsUids?.Any(d => d.HasValue) == true)
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => (!ac.IsDeleted || showDeleted) &&
                                                                            ac.ConferenceParticipants.Any(cp => (!cp.IsDeleted || showDeleted) &&
                                                                                                                roomsUids.Contains(cp.Conference.Room.Uid))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by create or update date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="modifiedAfterDate">The modified after date.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCreateOrUpdateDate(this IQueryable<Collaborator> query, DateTime? modifiedAfterDate)
        {
            if (modifiedAfterDate.HasValue)
            {
                query = query.Where(o => o.CreateDate >= modifiedAfterDate || o.UpdateDate >= modifiedAfterDate);
            }

            return query;
        }

        /// <summary>Finds the by filters uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="activitiesUids">The activities uids.</param>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByFiltersUids(this IQueryable<Collaborator> query, List<Guid> activitiesUids, List<Guid> targetAudiencesUids, List<Guid> interestsUids, bool showDeleted)
        {
            if (activitiesUids?.Any() == true || targetAudiencesUids?.Any() == true || interestsUids?.Any() == true)
            {
                var outerWhere = PredicateBuilder.New<Collaborator>(false);
                var innerActivitiesUidsWhere = PredicateBuilder.New<Collaborator>(true);
                var innerTargetAudiencesUidsWhere = PredicateBuilder.New<Collaborator>(true);
                var innerInterestsUidsWhere = PredicateBuilder.New<Collaborator>(true);

                if (activitiesUids?.Any() == true)
                {
                    innerActivitiesUidsWhere = innerActivitiesUidsWhere.Or(c => c.AttendeeCollaborators
                                                                                    .Where(ac => !ac.IsDeleted || showDeleted)
                                                                                    .Any(ac => ac.AttendeeCollaboratorActivities
                                                                                                    .Where(aca => !ac.IsDeleted || showDeleted)
                                                                                                    .Any(aca => activitiesUids.Contains(aca.Activity.Uid))));
                }

                if (targetAudiencesUids?.Any() == true)
                {
                    innerTargetAudiencesUidsWhere = innerTargetAudiencesUidsWhere.Or(c => c.AttendeeCollaborators
                                                                                    .Where(ac => !ac.IsDeleted || showDeleted)
                                                                                    .Any(ac => ac.AttendeeCollaboratorTargetAudiences
                                                                                                    .Where(acta => !acta.IsDeleted || showDeleted)
                                                                                                    .Any(acta => targetAudiencesUids.Contains(acta.TargetAudience.Uid))));
                }

                if (interestsUids?.Any() == true)
                {
                    innerInterestsUidsWhere = innerInterestsUidsWhere.Or(c => c.AttendeeCollaborators
                                                                                    .Where(ac => !ac.IsDeleted || showDeleted)
                                                                                    .Any(ac => ac.AttendeeCollaboratorInterests
                                                                                                    .Where(aci => !aci.IsDeleted || showDeleted)
                                                                                                    .Any(aci => interestsUids.Contains(aci.Interest.Uid))));
                }

                outerWhere = outerWhere.And(innerActivitiesUidsWhere);
                outerWhere = outerWhere.And(innerTargetAudiencesUidsWhere);
                outerWhere = outerWhere.And(innerInterestsUidsWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>
        /// Finds the by conferences uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> HasConferencesOrNegotiations(this IQueryable<Collaborator> query, bool showAllEditions, int? editionId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => !ac.IsDeleted &&
                                                                       !ac.Edition.IsDeleted &&
                                                                       (showAllEditions || ac.EditionId == editionId) &&
                                                                       (
                                                                            // Has Conferences
                                                                            ac.ConferenceParticipants.Any(cp => !cp.IsDeleted) ||

                                                                            // Has Negotiations
                                                                            ac.AttendeeOrganizationCollaborators.Any(aoc =>
                                                                            !aoc.IsDeleted &&
                                                                            !aoc.AttendeeOrganization.IsDeleted &&
                                                                            aoc.AttendeeOrganization.ProjectBuyerEvaluations.Any(pbe =>
                                                                                !pbe.IsDeleted &&
                                                                                pbe.Negotiations.Any(n => !n.IsDeleted)))
                                                                       )));

            return query;
        }
    }

    #endregion

    #region CollaboratorBaseDto IQueryable Extensions

    /// <summary>
    /// CollaboratorBaseDtoIQueryableExtensions
    /// </summary>
    internal static class CollaboratorBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<CollaboratorDto>> ToListPagedAsync(this IQueryable<CollaboratorDto> query, int page, int pageSize)
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

    #region CollaboratorApiListDto IQueryable Extensions

    /// <summary>
    /// CollaboratorApiListDtoIQueryableExtensions
    /// </summary>
    internal static class CollaboratorApiListDtoIQueryableExtensions
    {
        /// <summary>Converts to listpagedasync.</summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<CollaboratorApiListDto>> ToListPagedAsync(this IQueryable<CollaboratorApiListDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region SpeakerCollaboratorApiDto IQueryable Extensions

    /// <summary>
    /// SpeakerCollaboratorApiDtoIQueryableExtensions
    /// </summary>
    internal static class SpeakerCollaboratorApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<SpeakerCollaboratorApiDto>> ToListPagedAsync(this IQueryable<SpeakerCollaboratorApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MusicPlayerCollaboratorApiDto IQueryable Extensions

    /// <summary>
    /// MusicPlayerCollaboratorApiDtoIQueryableExtensions
    /// </summary>
    internal static class MusicPlayerCollaboratorApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicPlayerCollaboratorApiDto>> ToListPagedAsync(this IQueryable<MusicPlayerCollaboratorApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region InnovationPlayerCollaboratorApiDto IQueryable Extensions

    /// <summary>
    /// InnovationPlayerCollaboratorApiDtoIQueryableExtensions
    /// </summary>
    internal static class InnovationPlayerCollaboratorApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<InnovationPlayerCollaboratorApiDto>> ToListPagedAsync(this IQueryable<InnovationPlayerCollaboratorApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>CollaboratorRepository</summary>
    public class CollaboratorRepository : Repository<PlataformaRio2CContext, Collaborator>, ICollaboratorRepository
    {
        /// <summary>Initializes a new instance of the <see cref="CollaboratorRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public CollaboratorRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Collaborator> GetBaseQuery(bool @readonly = false, bool showDeleted = false)
        {
            var consult = this.dbSet.AsQueryable();

            if (!showDeleted)
            {
                consult = consult.IsNotDeleted();
            }

            return @readonly
                    ? consult.AsNoTracking()
                    : consult;
        }

        /// <summary>Finds all collaborators by collaborators uids.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        public async Task<List<AdminAccessControlDto>> FindAllCollaboratorsByCollaboratorsUids(int editionId, List<Guid> collaboratorsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(collaboratorsUids);

            return await query
                            .Select(c => new AdminAccessControlDto
                            {
                                User = c.User,
                                Roles = c.User.Roles,
                                Language = c.User.UserInterfaceLanguage,
                                Collaborator = c,
                                EditionCollaboratorTypes = c.AttendeeCollaborators
                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                .SelectMany(ac => ac.AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                        .Select(act => act.CollaboratorType)),
                            })
                            .ToListAsync();
        }

        /// <summary>
        /// Finds the dto by uid and by edition identifier asynchronous.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId, string userInterfaceLanguage)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid);

            var collaboratorDto = await query
                                    .Select(c => new CollaboratorDto
                                    {
                                        Id = c.Id,
                                        Uid = c.Uid,
                                        FirstName = c.FirstName,
                                        LastNames = c.LastNames,
                                        Badge = c.Badge,
                                        Email = c.User.Email,
                                        PhoneNumber = c.PhoneNumber,
                                        CellPhone = c.CellPhone,
                                        PublicEmail = c.PublicEmail,
                                        BirthDate = c.BirthDate,
                                        Gender = c.Gender,
                                        Industry = c.Industry,
                                        CollaboratorRole = c.Role,
                                        CollaboratorGenderAdditionalInfo = c.CollaboratorGenderAdditionalInfo,
                                        CollaboratorIndustryAdditionalInfo = c.CollaboratorIndustryAdditionalInfo,
                                        CollaboratorRoleAdditionalInfo = c.CollaboratorRoleAdditionalInfo,
                                        HasAnySpecialNeeds = c.HasAnySpecialNeeds,
                                        SpecialNeedsDescription = c.SpecialNeedsDescription,
                                        EditionsUids = c.EditionParticipantions.Where(p => !p.IsDeleted).Select(p => p.Edition.Uid).ToList(),
                                        ImageUploadDate = c.ImageUploadDate,
                                        Website = c.Website,
                                        Linkedin = c.Linkedin,
                                        Twitter = c.Twitter,
                                        Instagram = c.Instagram,
                                        Youtube = c.Youtube,
                                        CreateDate = c.CreateDate,
                                        CreateUserId = c.CreateUserId,
                                        UpdateDate = c.UpdateDate,
                                        UpdateUserId = c.UpdateUserId,
                                        UserInterfaceLanguage = userInterfaceLanguage,
                                        Roles = c.User.Roles,
                                        AttendeeCollaboratorTypeDtos = c.AttendeeCollaborators
                                                                            .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .AttendeeCollaboratorTypes
                                                                                    .Where(act => !act.IsDeleted
                                                                                                    && !act.CollaboratorType.IsDeleted)
                                                                                    .Select(act => new AttendeeCollaboratorTypeDto()
                                                                                    {
                                                                                        AttendeeCollaboratorType = act,
                                                                                        CollaboratorType = act.CollaboratorType
                                                                                    })
                                                                                .ToList(),
                                        EditionAttendeeCollaborator = c.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                                        EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Uid = ac.Uid,
                                                                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                    OnboardingStartDate = ac.OnboardingStartDate,
                                                                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                    OnboardingUserDate = ac.OnboardingUserDate,
                                                                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                    AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                    InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                    MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                    AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                    AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,                                                                                    
                                                                                    SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                }).FirstOrDefault(),
                                        UpdaterBaseDto = new UserBaseDto
                                        {
                                            Id = c.Updater.Id,
                                            Uid = c.Updater.Uid,
                                            Name = c.Updater.Name,
                                            Email = c.Updater.Email
                                        },
                                        JobTitleBaseDtos = c.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
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
                                        MiniBioBaseDtos = c.MiniBios.Select(d => new CollaboratorMiniBioBaseDto
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
                                        AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                            .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                            .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                    .Where(aoc => !aoc.IsDeleted)
                                                                                                    .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                    {
                                                                                                        Uid = aoc.AttendeeOrganization.Uid,
                                                                                                        OrganizationBaseDto = new OrganizationBaseDto
                                                                                                        {
                                                                                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                            HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                            }
                                                                                                        }
                                                                                                    }))
                                    })
                                    .FirstOrDefaultAsync();

            return collaboratorDto;
        }

        /// <summary>
        /// Finds all by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> collaboratorsUids,
            string[] collaboratorTypeNames,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);

            var collaborators = await query
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,

                                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                                && !ac.IsDeleted
                                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) : null,

                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Uid = ac.Uid,
                                                                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                    OnboardingStartDate = ac.OnboardingStartDate,
                                                                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                    OnboardingUserDate = ac.OnboardingUserDate,
                                                                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                    AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                    InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                    MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                    AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                    AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                                    SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                }).FirstOrDefault(),

                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    },
                                                                                                    IsVirtualMeeting = aoc.AttendeeOrganization.Organization.IsVirtualMeeting
                                                                                                }
                                                                                            })),

                                IsApiDisplayEnabled = c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                                        && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                    && collaboratorTypeNames.Contains(act.CollaboratorType.Name)
                                                                                                                                    && act.IsApiDisplayEnabled)),

                                IsInOtherEdition = c.User.Roles.Any(r => r.Name == Constants.Role.Admin)
                                                   || (editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted)),

                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,
                                Active = c.User.Active,
                                UserBaseDto = new UserBaseDto
                                {
                                    Id = c.User.Id,
                                    Uid = c.User.Uid
                                }
                            })
                            .ToListPagedAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return collaborators;
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(string collaboratorTypeName, string organizationTypeName, bool showAllEditions, int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, showAllEditions, false, editionId)
                                .FindByOrganizationTypeNames(new string[] { organizationTypeName }, showAllEditions, false, editionId);

            return await query.CountAsync();
        }

        /// <summary>Finds the by sales platform attendee identifier asynchronous.</summary>
        /// <param name="salesPlatformAttendeeId">The sales platform attendee identifier.</param>
        /// <returns></returns>
        public async Task<Collaborator> FindBySalesPlatformAttendeeIdAsync(string salesPlatformAttendeeId)
        {
            var query = this.GetBaseQuery()
                                .FindBySalesPlatformAttendeeId(salesPlatformAttendeeId);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindByEmailAsync(string email, int? editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByUserEmail(email);

            return await query.Select(c => new CollaboratorDto
            {
                Id = c.Id,
                Uid = c.Uid,
                FirstName = c.FirstName,
                LastNames = c.LastNames,
                Badge = c.Badge,
                Email = c.User.Email,
                PhoneNumber = c.PhoneNumber,
                CellPhone = c.CellPhone,
                PublicEmail = c.PublicEmail,
                BirthDate = c.BirthDate,
                Gender = c.Gender,
                Industry = c.Industry,
                CollaboratorRole = c.Role,
                CollaboratorGenderAdditionalInfo = c.CollaboratorGenderAdditionalInfo,
                CollaboratorIndustryAdditionalInfo = c.CollaboratorIndustryAdditionalInfo,
                CollaboratorRoleAdditionalInfo = c.CollaboratorRoleAdditionalInfo,
                HasAnySpecialNeeds = c.HasAnySpecialNeeds,
                SpecialNeedsDescription = c.SpecialNeedsDescription,
                EditionsUids = c.EditionParticipantions.Where(p => !p.IsDeleted).Select(p => p.Edition.Uid).ToList(),
                ImageUploadDate = c.ImageUploadDate,
                Website = c.Website,
                Linkedin = c.Linkedin,
                Twitter = c.Twitter,
                Instagram = c.Instagram,
                Youtube = c.Youtube,
                CreateDate = c.CreateDate,
                CreateUserId = c.CreateUserId,
                UpdateDate = c.UpdateDate,
                UpdateUserId = c.UpdateUserId,
                Roles = c.User.Roles,
                UpdaterBaseDto = new UserBaseDto
                {
                    Id = c.Updater.Id,
                    Uid = c.Updater.Uid,
                    Name = c.Updater.Name,
                    Email = c.Updater.Email
                },
                JobTitleBaseDtos = c.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
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
                MiniBioBaseDtos = c.MiniBios.Select(d => new CollaboratorMiniBioBaseDto
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
                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                && !ac.IsDeleted
                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted)) : null,

                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                            .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                            .Select(ac => new AttendeeCollaboratorBaseDto
                                                            {
                                                                Id = ac.Id,
                                                                Uid = ac.Uid,
                                                                WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                OnboardingStartDate = ac.OnboardingStartDate,
                                                                OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                OnboardingUserDate = ac.OnboardingUserDate,
                                                                OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                            }).FirstOrDefault(),
            }).FirstOrDefaultAsync();
        }

        /// <summary>Finds all logistics by datatable.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchValue">The search value.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showAllSponsors">if set to <c>true</c> [show all sponsors].</param>
        /// <returns></returns>
        public async Task<IPagedList<LogisticJsonDto>> FindAllLogisticsByDatatable(
            int editionId,
            int page,
            int pageSize,
            string searchValue,
            List<Tuple<string, string>> sortColumns,
            bool showAllParticipants,
            bool showAllSponsors)
        {
            var query = this.GetBaseQuery()
                                    .FindLogisticsByEditionId(editionId, showAllParticipants, showAllSponsors)
                                    .FindByKeywords(searchValue, editionId);

            return await query
                            .DynamicOrder<Collaborator>(
                                sortColumns,
                                new List<Tuple<string, string>>(),
                                new List<string> { "FirstName", "CreateDate", "UpdateDate" }, "FirstName")
                            .Select(c => new LogisticJsonDto
                            {
                                CollaboratorUid = c.Uid,
                                AttendeeCollaboratorUid = c.AttendeeCollaborators.Where(ac => ac.EditionId == editionId && !ac.IsDeleted).Select(ac => ac.Uid).FirstOrDefault(),
                                Name = c.FirstName + " " + c.LastNames,
                                CollaboratorImageUploadDate = c.ImageUploadDate,
                                HasRequest = c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId && !ac.IsDeleted && ac.Logistics.Any(l => !l.IsDeleted)),
                                HasLogistics = c.AttendeeCollaborators.Any(ac => ac.Logistics.Any(l => !l.IsDeleted &&
                                                                                                       (l.LogisticAirfares.Any(a => !a.IsDeleted) ||
                                                                                                        l.LogisticAccommodations.Any(a => !a.IsDeleted) ||
                                                                                                        l.LogisticTransfers.Any(a => !a.IsDeleted)))),
                                Id = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.Id)
                                    .FirstOrDefault(),
                                Uid = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.Uid)
                                    .FirstOrDefault(),
                                AccommodationSponsor = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted && l.AccommodationAttendeeLogisticSponsor != null))
                                    .Select(e => e.AccommodationAttendeeLogisticSponsor.LogisticSponsor.Name)
                                    .FirstOrDefault(),
                                AirfareSponsor = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted && l.AirfareAttendeeLogisticSponsor != null))
                                    .Select(e => e.AirfareAttendeeLogisticSponsor.LogisticSponsor.Name)
                                    .FirstOrDefault(),
                                AirportTransferSponsor = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted && l.AirportTransferAttendeeLogisticSponsor != null))
                                    .Select(e => e.AirportTransferAttendeeLogisticSponsor.LogisticSponsor.Name)
                                    .FirstOrDefault(),
                                CreateDate = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.CreateDate)
                                    .FirstOrDefault(),
                                UpdateDate = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.UpdateDate)
                                    .FirstOrDefault(),
                                TransferCity = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.IsCityTransferRequired)
                                    .FirstOrDefault(),
                                IsVehicleDisposalRequired = c.AttendeeCollaborators
                                    .Where(ac => ac.EditionId == editionId && !ac.IsDeleted)
                                    .Select(ac => ac.Logistics.FirstOrDefault(l => !l.IsDeleted))
                                    .Select(e => e.IsVehicleDisposalRequired)
                                    .FirstOrDefault(),
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all admins by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllAdminsByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            string collaboratorTypeName,
            string roleName,
            bool showAllEditions,
            bool showAllParticipants,
            string userInterfaceLanguage,
            int? editionId)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                                .FindByKeywords(keywords, editionId)
                                .FindByAdminRoleNameAndAdminCollaboratorTypeNameAndByEditionId(roleName, collaboratorTypeName, showAllEditions, showAllParticipants, editionId);

            var collaborators = await query
                                        .DynamicOrder(
                                            sortColumns,
                                            new List<Tuple<string, string>>
                                            {
                                                new Tuple<string, string>("FullName", "User.Name"),
                                                new Tuple<string, string>("Email", "User.Email"),
                                            },
                                            new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                            "User.Name")
                                        .Select(c => new CollaboratorDto
                                        {
                                            Id = c.Id,
                                            Uid = c.Uid,
                                            Active = c.User.Active,
                                            UserBaseDto = new UserBaseDto
                                            {
                                                Id = c.User.Id,
                                                Uid = c.User.Uid
                                            },
                                            FirstName = c.FirstName,
                                            LastNames = c.LastNames,
                                            Badge = c.Badge,
                                            Email = c.User.Email,
                                            PhoneNumber = c.PhoneNumber,
                                            CellPhone = c.CellPhone,
                                            PublicEmail = c.PublicEmail,
                                            ImageUploadDate = c.ImageUploadDate,
                                            CreateDate = c.CreateDate,
                                            UpdateDate = c.UpdateDate,
                                            UserInterfaceLanguage = userInterfaceLanguage,
                                            Roles = c.User.Roles,
                                            IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted),
                                            EditionAttendeeCollaborator = c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                        && !ac.Edition.IsDeleted
                                                                                                                        && !ac.IsDeleted
                                                                                                                        && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                   && (c.User.Roles.Any(r => r.Name == Constants.Role.Admin)
                                                                                                                                                                       || Constants.CollaboratorType.Admins.Contains(act.CollaboratorType.Name)))),

                                            EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                        .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                        {
                                                                                            Id = ac.Id,
                                                                                            Uid = ac.Uid,
                                                                                            WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                            OnboardingStartDate = ac.OnboardingStartDate,
                                                                                            OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                            OnboardingUserDate = ac.OnboardingUserDate,
                                                                                            OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                            AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                            InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                            MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                            AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                            AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                                            SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                        }).FirstOrDefault(),

                                            AttendeeCollaboratorTypeDtos = c.AttendeeCollaborators
                                                                                .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                    .AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                            .Select(act => new AttendeeCollaboratorTypeDto()
                                                                                            {
                                                                                                AttendeeCollaboratorType = act,
                                                                                                CollaboratorType = act.CollaboratorType
                                                                                            })
                                        })
                                        .ToListPagedAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return collaborators;
        }

        #region Audiovisual Commissions

        /// <summary>
        /// Gets the audiovisual commissions base query.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <returns></returns>
        private IQueryable<Collaborator> GetAudiovisualCommissionsBaseQuery(
            int? editionId,
            string keywords,
            string[] collaboratorTypeNames,
            List<Guid?> interestsUids,
            bool showAllEditions,
            bool showAllParticipants)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByInterestsUids(interestsUids);

            return query;
        }

        /// <summary>
        /// Finds all audiovisual commission members by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllAudiovisualCommissionMembersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            string[] collaboratorTypeNames,
            bool showAllEditions,
            bool showAllParticipants,
            int? editionId,
            List<Guid?> interestsUids)
        {
            var baseQuery = this.GetAudiovisualCommissionsBaseQuery(
                editionId,
                keywords,
                collaboratorTypeNames,
                interestsUids,
                showAllEditions,
                showAllParticipants);

            return await baseQuery
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted),
                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,

                                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                                && !ac.IsDeleted
                                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) : null,

                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Uid = ac.Uid,
                                                                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                    OnboardingStartDate = ac.OnboardingStartDate,
                                                                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                    OnboardingUserDate = ac.OnboardingUserDate,
                                                                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                    AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                    InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                    MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                    AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                    AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                                    SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                }).FirstOrDefault(),

                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    },
                                                                                                    IsVirtualMeeting = aoc.AttendeeOrganization.Organization.IsVirtualMeeting
                                                                                                }
                                                                                            }))
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all audiovisual commission members API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllAudiovisualCommissionMembersApiPaged(
            int? editionId,
            string keywords,
            int page,
            int pageSize)
        {
            var baseQuery = this.GetAudiovisualCommissionsBaseQuery(
                editionId,
                keywords,
                new string[] { Constants.CollaboratorType.CommissionAudiovisual },
                null,
                false,
                false);

            return await baseQuery
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                            .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                            .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                    .Where(aoc => !aoc.IsDeleted)
                                                                                                    .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                    {
                                                                                                        Uid = aoc.AttendeeOrganization.Uid,
                                                                                                        OrganizationBaseDto = new OrganizationBaseDto
                                                                                                        {
                                                                                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                            HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                            }
                                                                                                        }
                                                                                                    }))
                            })
                            .OrderBy(c => c.FirstName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the audiovisual commission member API.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindAudiovisualCommissionMemberApi(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { Constants.CollaboratorType.CommissionAudiovisual }, false, false, editionId);

            return await query
                            .Select(c => new CollaboratorDto
                            {
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                            .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                            .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                    .Where(aoc => !aoc.IsDeleted)
                                                                                                    .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                    {
                                                                                                        Uid = aoc.AttendeeOrganization.Uid,
                                                                                                        OrganizationBaseDto = new OrganizationBaseDto
                                                                                                        {
                                                                                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                            HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                            }
                                                                                                        }
                                                                                                    })),
                                MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                })
                            })
                            .OrderBy(o => o.FirstName)
                            .FirstOrDefaultAsync();
        }

        #endregion

        #region Music Commissions

        /// <summary>
        /// Gets the music commissions base query.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        private IQueryable<Collaborator> GetMusicCommissionsBaseQuery(
            int? editionId,
            string keywords)
        {
            var collaboratorTypeNames = new string[] { Constants.CollaboratorType.CommissionMusic };

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, editionId);

            return query;
        }

        /// <summary>
        /// Finds all music commission members API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllMusicCommissionMembersApiPaged(
            int? editionId,
            string keywords,
            int page,
            int pageSize)
        {
            var baseQuery = this.GetMusicCommissionsBaseQuery(
                editionId,
                keywords);

            return await baseQuery
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                            .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                            .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                    .Where(aoc => !aoc.IsDeleted)
                                                                                                    .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                    {
                                                                                                        Uid = aoc.AttendeeOrganization.Uid,
                                                                                                        OrganizationBaseDto = new OrganizationBaseDto
                                                                                                        {
                                                                                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                            HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                            }
                                                                                                        }
                                                                                                    }))
                            })
                            .OrderBy(c => c.FirstName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the music commission member API.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindMusicCommissionMemberApi(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { Constants.CollaboratorType.CommissionMusic }, false, false, editionId);

            return await query
                            .Select(c => new CollaboratorDto
                            {
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                            .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                            .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                    .Where(aoc => !aoc.IsDeleted)
                                                                                                    .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                    {
                                                                                                        Uid = aoc.AttendeeOrganization.Uid,
                                                                                                        OrganizationBaseDto = new OrganizationBaseDto
                                                                                                        {
                                                                                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                            HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                            }
                                                                                                        }
                                                                                                    })),
                                MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                })
                            })
                            .OrderBy(o => o.FirstName)
                            .FirstOrDefaultAsync();
        }

        #endregion

        #region Innovation Commissions

        /// <summary>
        /// Finds all innovation commissions by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="innovationOrganizationTrackOptionGroupsUids">The innovation organization track option groups uids.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllInnovationCommissionsByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> collaboratorsUids,
            string[] collaboratorTypeNames,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId,
            List<Guid?> innovationOrganizationTrackOptionGroupsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByInnovationOrganizationTrackOptionsGroupsUids(editionId, innovationOrganizationTrackOptionGroupsUids)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);

            return await query
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted),
                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,

                                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                                && !ac.IsDeleted
                                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) : null,

                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Uid = ac.Uid,
                                                                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                    OnboardingStartDate = ac.OnboardingStartDate,
                                                                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                    OnboardingUserDate = ac.OnboardingUserDate,
                                                                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                    AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                    InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                    MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                    AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                    AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                                    SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                }).FirstOrDefault(),

                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    },
                                                                                                    IsVirtualMeeting = aoc.AttendeeOrganization.Organization.IsVirtualMeeting
                                                                                                }
                                                                                            })),

                                InnovationOrganizationTrackOptionGroupDtos = c.AttendeeCollaborators
                                                                                .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                                .SelectMany(ac => ac.AttendeeCollaboratorInnovationOrganizationTracks
                                                                                                        .Where(aciot => !aciot.IsDeleted && aciot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroupId != null)
                                                                                                        .GroupBy(aciot => aciot.InnovationOrganizationTrackOption.InnovationOrganizationTrackOptionGroup.Name)
                                                                                                        .Select(aciot => new InnovationOrganizationTrackOptionGroupDto
                                                                                                        {
                                                                                                            GroupName = aciot.Key
                                                                                                        }))
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        #endregion

        #region Creator Commissions

        public async Task<IPagedList<CollaboratorDto>> FindAllCreatorCommissionsByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> collaboratorsUids,
            string[] collaboratorTypeNames,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);

            return await query
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted),
                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,

                                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                                && !ac.IsDeleted
                                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) : null,

                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                {
                                                                                    Id = ac.Id,
                                                                                    Uid = ac.Uid,
                                                                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                    OnboardingStartDate = ac.OnboardingStartDate,
                                                                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                    OnboardingUserDate = ac.OnboardingUserDate,
                                                                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                                                                    AudiovisualPlayerTermsAcceptanceDate = ac.AudiovisualPlayerTermsAcceptanceDate,
                                                                                    InnovationPlayerTermsAcceptanceDate = ac.InnovationPlayerTermsAcceptanceDate,
                                                                                    MusicPlayerTermsAcceptanceDate = ac.MusicPlayerTermsAcceptanceDate,
                                                                                    AudiovisualProducerBusinessRoundTermsAcceptanceDate = ac.AudiovisualProducerBusinessRoundTermsAcceptanceDate,
                                                                                    AudiovisualProducerPitchingTermsAcceptanceDate = ac.AudiovisualProducerPitchingTermsAcceptanceDate,
                                                                                    SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate
                                                                                }).FirstOrDefault(),

                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted)
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    },
                                                                                                    IsVirtualMeeting = aoc.AttendeeOrganization.Organization.IsVirtualMeeting
                                                                                                }
                                                                                            }))
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        #endregion

        #region Audiovisual Players Executives

        /// <summary>
        /// Finds all players executives report by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<PlayerExecutiveReportDto>> FindAllPlayersExecutivesReportByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId,
            string collaboratorTypeName = null
        )
        {
            this.SetProxyEnabled(false);

            string[] collaboratorTypeNames = string.IsNullOrEmpty(collaboratorTypeName)
                ? new string[] { CollaboratorType.PlayerExecutiveAudiovisual.Name }
                : new string[] { collaboratorTypeName };
            string[] organizationTypeNames = new string[] { OrganizationType.AudiovisualPlayer.Name };

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);
                                //.FindByOrganizationTypeNames(organizationTypeNames, showAllEditions, showAllParticipants, editionId);

            var collaborators = await query
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new PlayerExecutiveReportDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                Email = c.User.Email,
                                PhoneNumber = c.PhoneNumber,
                                CellPhone = c.CellPhone,
                                PublicEmail = c.PublicEmail,
                                Website = c.Website,
                                Linkedin = c.Linkedin,
                                Twitter = c.Twitter,
                                Instagram = c.Instagram,
                                Youtube = c.Youtube,
                                BirthDate = c.BirthDate,
                                ImageUploadDate = c.ImageUploadDate,
                                Industry = c.Industry,
                                Role = c.Role,
                                Gender = c.Gender,
                                HasAnySpecialNeeds = c.HasAnySpecialNeeds,
                                SpecialNeedsDescription = c.SpecialNeedsDescription,
                                EditionAttendeeCollaborator = c.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId),
                                JobTitleBaseDtos = c.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
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
                                MiniBioBaseDtos = c.MiniBios.Select(d => new CollaboratorMiniBioBaseDto
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
                                EditionParticipationBaseDtos = c.EditionParticipantions.Where(d => !d.IsDeleted).Select(d => new CollaboratorEditionParticipationBaseDto
                                {
                                    EditionUrlCode = d.Edition.UrlCode
                                }),
                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                            .Where(aoc => !aoc.IsDeleted
                                                                                                            && (organizationTypeNames.Any(ctn => !string.IsNullOrEmpty(ctn)) == true ?
                                                                                                                    //Search by OrganizationType
                                                                                                                    aoc.AttendeeOrganization.AttendeeOrganizationTypes
                                                                                                                        .Where(aot => !aot.IsDeleted)
                                                                                                                        .Any(aot => organizationTypeNames.Contains(aot.OrganizationType.Name))
                                                                                                                    :
                                                                                                                    //Return true because isn't searching by OrganizationType
                                                                                                                    true))
                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                            {
                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                {
                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName
                                                                                                }
                                                                                            }))
                            })
                            .ToPagedListAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return collaborators;
        }

        #endregion

        #region Music Players Executives

        /// <summary>
        /// Finds all music players public API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="activitiesUids">The activities uids.</param>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <param name="modifiedAfterDate">The modified after date.</param>
        /// <param name="showDetails">if set to <c>true</c> [show details].</param>
        /// <param name="showDeleted">if set to <c>true</c> [show deleted].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MusicPlayerCollaboratorApiDto>> FindAllMusicPlayersExecutivesPublicApiPaged(
            int editionId,
            string keywords,
            List<Guid> activitiesUids,
            List<Guid> targetAudiencesUids,
            List<Guid> interestsUids,
            DateTime? modifiedAfterDate,
            bool showDetails,
            bool showDeleted,
            int page,
            int pageSize)
        {
            var collaboratorTypeName = CollaboratorType.PlayerExecutiveMusic.Name;

            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId, showDeleted)
                                //.IsApiDisplayEnabled(editionId, collaboratorTypeName, showDeleted) //TODO: Enable this filter after implementing: Admin area > Player Executive details view > API Configuration widget
                                .FindByFiltersUids(activitiesUids, targetAudiencesUids, interestsUids, showDeleted)
                                .FindByKeywords(keywords, editionId)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<MusicPlayerCollaboratorApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(c => new MusicPlayerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    FirstName = c.FirstName,
                    LastNames = c.LastNames,
                    Badge = c.Badge,
                    ImageUploadDate = c.ImageUploadDate,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                    JobTitleBaseDtos = c.JobTitles.Where(jt => !jt.IsDeleted).Select(jt => new CollaboratorJobTitleBaseDto
                    {
                        Id = jt.Id,
                        Uid = jt.Uid,
                        Value = jt.Value,
                        LanguageDto = new LanguageBaseDto
                        {
                            Id = jt.Language.Id,
                            Uid = jt.Language.Uid,
                            Name = jt.Language.Name,
                            Code = jt.Language.Code
                        }
                    }),
                    MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(jt => new CollaboratorMiniBioBaseDto
                    {
                        Id = jt.Id,
                        Uid = jt.Uid,
                        Value = jt.Value,
                        LanguageDto = new LanguageBaseDto
                        {
                            Id = jt.Language.Id,
                            Uid = jt.Language.Uid,
                            Name = jt.Language.Name,
                            Code = jt.Language.Code
                        }
                    })
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(c => new MusicPlayerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    FirstName = c.FirstName,
                    LastNames = c.LastNames,
                    Badge = c.Badge,
                    ImageUploadDate = c.ImageUploadDate,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(c => c.FirstName)
                            //.OrderBy(o => o.ApiHighlightPosition ?? 99)  //TODO: Enable this after implementing: Admin area > Player Executive details view > API Configuration widget
                            //.ThenBy(o => o.FirstName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the music player public API dto by uid.
        /// </summary>
        /// <param name="collaboratorUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<MusicPlayerCollaboratorApiDto> FindMusicPlayerExecutivePublicApiDtoByUid(Guid collaboratorUid, int editionId)
        {
            var collaboratorTypeName = CollaboratorType.PlayerExecutiveMusic.Name;

            var query = this.GetBaseQuery()
                                    .FindByUid(collaboratorUid)
                                    .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId);
            //.IsApiDisplayEnabled(editionId, collaboratorTypeName); //TODO: Enable this filter after implementing: Admin area > Player Executive details view > API Configuration widget

            return await query
                            .Select(c => new MusicPlayerCollaboratorApiDto
                            {
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => !aot.IsDeleted && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                                JobTitleBaseDtos = c.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
                                {
                                    Id = jt.Id,
                                    Uid = jt.Uid,
                                    Value = jt.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = jt.Language.Id,
                                        Uid = jt.Language.Uid,
                                        Name = jt.Language.Name,
                                        Code = jt.Language.Code
                                    }
                                }),
                                MiniBioBaseDtos = c.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
                                {
                                    Id = jt.Id,
                                    Uid = jt.Uid,
                                    Value = jt.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = jt.Language.Id,
                                        Uid = jt.Language.Uid,
                                        Name = jt.Language.Name,
                                        Code = jt.Language.Code
                                    }
                                })
                            }).FirstOrDefaultAsync();
        }

        #endregion

        #region Innovation Players Executives

        /// <summary>
        /// Finds all innovation players executives public API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="activitiesUids">The activities uids.</param>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <param name="modifiedAfterDate">The modified after date.</param>
        /// <param name="showDetails">if set to <c>true</c> [show details].</param>
        /// <param name="showDeleted">if set to <c>true</c> [show deleted].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<InnovationPlayerCollaboratorApiDto>> FindAllInnovationPlayersExecutivesPublicApiPaged(
            int editionId,
            string keywords,
            List<Guid> activitiesUids,
            List<Guid> targetAudiencesUids,
            List<Guid> interestsUids,
            DateTime? modifiedAfterDate,
            bool showDetails,
            bool showDeleted,
            int page,
            int pageSize)
        {
            var collaboratorTypeName = CollaboratorType.PlayerExecutiveInnovation.Name;

            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId, showDeleted)
                                //.IsApiDisplayEnabled(editionId, collaboratorTypeName, showDeleted) //TODO: Enable this filter after implementing: Admin area > Player Executive details view > API Configuration widget
                                .FindByFiltersUids(activitiesUids, targetAudiencesUids, interestsUids, showDeleted)
                                .FindByKeywords(keywords, editionId)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<InnovationPlayerCollaboratorApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(c => new InnovationPlayerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    FirstName = c.FirstName,
                    LastNames = c.LastNames,
                    Badge = c.Badge,
                    ImageUploadDate = c.ImageUploadDate,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                    JobTitleBaseDtos = c.JobTitles.Where(jt => !jt.IsDeleted).Select(jt => new CollaboratorJobTitleBaseDto
                    {
                        Id = jt.Id,
                        Uid = jt.Uid,
                        Value = jt.Value,
                        LanguageDto = new LanguageBaseDto
                        {
                            Id = jt.Language.Id,
                            Uid = jt.Language.Uid,
                            Name = jt.Language.Name,
                            Code = jt.Language.Code
                        }
                    }),
                    MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(jt => new CollaboratorMiniBioBaseDto
                    {
                        Id = jt.Id,
                        Uid = jt.Uid,
                        Value = jt.Value,
                        LanguageDto = new LanguageBaseDto
                        {
                            Id = jt.Language.Id,
                            Uid = jt.Language.Uid,
                            Name = jt.Language.Name,
                            Code = jt.Language.Code
                        }
                    })
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(c => new InnovationPlayerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    FirstName = c.FirstName,
                    LastNames = c.LastNames,
                    Badge = c.Badge,
                    ImageUploadDate = c.ImageUploadDate,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(c => c.FirstName)
                            //.OrderBy(o => o.ApiHighlightPosition ?? 99)  //TODO: Enable this after implementing: Admin area > Player Executive details view > API Configuration widget
                            //.ThenBy(o => o.FirstName)
                            .ToListPagedAsync(page, pageSize);
        }


        /// <summary>
        /// Finds the innovation player executive public API dto by uid.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<InnovationPlayerCollaboratorApiDto> FindInnovationPlayerExecutivePublicApiDtoByUid(Guid collaboratorUid, int editionId)
        {
            var collaboratorTypeName = CollaboratorType.PlayerExecutiveInnovation.Name;

            var query = this.GetBaseQuery()
                                    .FindByUid(collaboratorUid)
                                    .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId);
            //.IsApiDisplayEnabled(editionId, collaboratorTypeName); //TODO: Enable this filter after implementing: Admin area > Player Executive details view > API Configuration widget

            return await query
                            .Select(c => new InnovationPlayerCollaboratorApiDto
                            {
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                Badge = c.Badge,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                ApiHighlightPosition = c.AttendeeCollaborators
                                                .FirstOrDefault(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                    .AttendeeCollaboratorTypes
                                                        .FirstOrDefault(aot => !aot.IsDeleted && aot.CollaboratorType.Name == collaboratorTypeName)
                                                            .ApiHighlightPosition,
                                JobTitleBaseDtos = c.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
                                {
                                    Id = jt.Id,
                                    Uid = jt.Uid,
                                    Value = jt.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = jt.Language.Id,
                                        Uid = jt.Language.Uid,
                                        Name = jt.Language.Name,
                                        Code = jt.Language.Code
                                    }
                                }),
                                MiniBioBaseDtos = c.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
                                {
                                    Id = jt.Id,
                                    Uid = jt.Uid,
                                    Value = jt.Value,
                                    LanguageDto = new LanguageBaseDto
                                    {
                                        Id = jt.Language.Id,
                                        Uid = jt.Language.Uid,
                                        Name = jt.Language.Name,
                                        Code = jt.Language.Code
                                    }
                                })
                            }).FirstOrDefaultAsync();
        }

        #endregion

        #region Speakers

        /// <summary>
        /// Finds all speakers API list dto paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="filterByProjectsInNegotiation">if set to <c>true</c> [filter by projects in negotiation].</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorApiListDto>> FindAllSpeakersApiListDtoPaged(
            int editionId,
            string keywords,
            bool filterByProjectsInNegotiation,
            string collaboratorTypeName,
            bool showAllParticipants,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, showAllParticipants, editionId)
                                .FindByNames(keywords);

            return await query
                            .Select(c => new CollaboratorApiListDto
                            {
                                Uid = c.Uid,
                                BadgeName = c.Badge,
                                Name = c.FirstName + " " + c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate
                            })
                            .OrderBy(o => o.BadgeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all speakers by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="exportToExcel">if set to <c>true</c> [export to excel].</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllSpeakersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId,
            bool? showNotPublishableToApi,
            List<Guid?> roomsUids,
            bool exportToExcel = false)
        {
            string[] collaboratorTypeNames = new string[] { CollaboratorType.Speaker.Name };

            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllParticipants, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights)
                                .FindNotPublishableToApi(editionId, showNotPublishableToApi)
                                .FindByConferencesRoomsUids(roomsUids, false);

            IPagedList<CollaboratorDto> collaboratorDtos;

            if (exportToExcel)
            {
                #region Report Select Query

                collaboratorDtos = await query
                                            .Select(c => new CollaboratorDto
                                            {
                                                Id = c.Id,
                                                Uid = c.Uid,
                                                FirstName = c.FirstName,
                                                LastNames = c.LastNames,
                                                Badge = c.Badge,
                                                Email = c.PublicEmail,
                                                CellPhone = c.CellPhone,
                                                PhoneNumber = c.PhoneNumber,
                                                Website = c.Website,
                                                Linkedin = c.Linkedin,
                                                Instagram = c.Instagram,
                                                Youtube = c.Youtube,
                                                ImageUploadDate = c.ImageUploadDate,
                                                CreatorBaseDto = new UserBaseDto
                                                {
                                                    Email = c.Creator.Email
                                                },
                                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                                MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId && ac.AttendeeCollaboratorTypes.Any(act => act.CollaboratorType.Uid == CollaboratorType.Speaker.Uid))
                                                                                        .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                        {
                                                                                            OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                            SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate,
                                                                                            AttendeeCollaboratorTypeDto = ac.AttendeeCollaboratorTypes
                                                                                                                                .Where(act => !act.IsDeleted && act.CollaboratorType.Uid == CollaboratorType.Speaker.Uid)
                                                                                                                                .Select(act => new AttendeeCollaboratorTypeDto
                                                                                                                                {
                                                                                                                                    IsApiDisplayEnabled = act.IsApiDisplayEnabled,
                                                                                                                                    ApiHighlightPosition = act.ApiHighlightPosition
                                                                                                                                }).FirstOrDefault()
                                                                                        }).FirstOrDefault(),
                                                AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                                    .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                                    .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                            .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                                            .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                            {
                                                                                                                Uid = aoc.AttendeeOrganization.Uid,
                                                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                                                {
                                                                                                                    Uid = aoc.AttendeeOrganization.Organization.Uid,
                                                                                                                    Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                                    TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                                                    Website = aoc.AttendeeOrganization.Organization.Website,
                                                                                                                    Linkedin = aoc.AttendeeOrganization.Organization.Linkedin,
                                                                                                                    Instagram = aoc.AttendeeOrganization.Organization.Instagram,
                                                                                                                    Twitter = aoc.AttendeeOrganization.Organization.Twitter,
                                                                                                                    Youtube = aoc.AttendeeOrganization.Organization.Youtube,
                                                                                                                    ImageUploadDate = aoc.AttendeeOrganization.Organization.ImageUploadDate,
                                                                                                                    OrganizationDescriptionBaseDtos = aoc.AttendeeOrganization.Organization.OrganizationDescriptions.Select(d => new OrganizationDescriptionBaseDto
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
                                                                                                                }
                                                                                                            })),
                                                ConferencesDtos = c.AttendeeCollaborators
                                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                        .SelectMany(ac => ac.ConferenceParticipants
                                                                                                .Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted)
                                                                                                .Select(cp => new ConferenceDto
                                                                                                {
                                                                                                    StartDate = cp.Conference.StartDate,
                                                                                                    EndDate = cp.Conference.EndDate,
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
                                                                                                    }),
                                                                                                    RoomDto = new RoomDto
                                                                                                    {
                                                                                                        RoomNameDtos = cp.Conference.Room.RoomNames.Select(rn => new RoomNameDto
                                                                                                        {
                                                                                                            RoomName = rn,
                                                                                                            LanguageDto = new LanguageBaseDto
                                                                                                            {
                                                                                                                Id = rn.Language.Id,
                                                                                                                Uid = rn.Language.Uid,
                                                                                                                Name = rn.Language.Name,
                                                                                                                Code = rn.Language.Code
                                                                                                            }
                                                                                                        })
                                                                                                    }
                                                                                                })),
                                            })
                                            .OrderBy(o => o.EditionAttendeeCollaboratorBaseDto.AttendeeCollaboratorTypeDto.ApiHighlightPosition ?? 99)
                                            .ThenBy(o => o.FirstName)
                                            .ToListPagedAsync(page, pageSize);
                #endregion
            }
            else
            {
                #region DataTable Select Query

                collaboratorDtos = await query
                                            .DynamicOrder(sortColumns,
                                                            new List<Tuple<string, string>>
                                                            {
                                                                new Tuple<string, string>("FullName", "User.Name"),
                                                                new Tuple<string, string>("Email", "User.Email")
                                                            },
                                                            new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                                            "User.Name")
                                            .Select(c => new CollaboratorDto
                                            {
                                                Id = c.Id,
                                                Uid = c.Uid,
                                                FirstName = c.FirstName,
                                                LastNames = c.LastNames,
                                                Badge = c.Badge,
                                                Email = c.User.Email,
                                                PublicEmail = c.PublicEmail,
                                                ImageUploadDate = c.ImageUploadDate,
                                                CreateDate = c.CreateDate,
                                                UpdateDate = c.UpdateDate,
                                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,
                                                Active = c.User.Active,
                                                UserBaseDto = new UserBaseDto
                                                {
                                                    Id = c.User.Id,
                                                    Uid = c.User.Uid
                                                },
                                                JobTitleBaseDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                                MiniBioBaseDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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

                                                IsApiDisplayEnabled = c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                                                        && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                    && collaboratorTypeNames.Contains(act.CollaboratorType.Name)
                                                                                                                                                    && act.IsApiDisplayEnabled)),

                                                IsInOtherEdition = c.User.Roles.Any(r => r.Name == Constants.Role.Admin)
                                                                    || (editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId && !ac.IsDeleted)),

                                                EditionAttendeeCollaborator = editionId.HasValue ? c.AttendeeCollaborators.FirstOrDefault(ac => ac.EditionId == editionId
                                                                                                                                                && !ac.Edition.IsDeleted
                                                                                                                                                && !ac.IsDeleted
                                                                                                                                                && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                                                            && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) : null,

                                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators
                                                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId && ac.AttendeeCollaboratorTypes.Any(act => act.CollaboratorType.Uid == CollaboratorType.Speaker.Uid))
                                                                                        .Select(ac => new AttendeeCollaboratorBaseDto
                                                                                        {
                                                                                            SpeakerTermsAcceptanceDate = ac.SpeakerTermsAcceptanceDate,
                                                                                            WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                                                                            OnboardingFinishDate = ac.OnboardingFinishDate,
                                                                                            AttendeeCollaboratorTypeDto = ac.AttendeeCollaboratorTypes
                                                                                                                                .Where(act => !act.IsDeleted && act.CollaboratorType.Uid == CollaboratorType.Speaker.Uid)
                                                                                                                                .Select(act => new AttendeeCollaboratorTypeDto
                                                                                                                                {
                                                                                                                                    IsApiDisplayEnabled = act.IsApiDisplayEnabled,
                                                                                                                                    ApiHighlightPosition = act.ApiHighlightPosition
                                                                                                                                }).FirstOrDefault()
                                                                                        }).FirstOrDefault()
                                            })
                                            .ToListPagedAsync(page, pageSize);

                #endregion
            }

            this.SetProxyEnabled(true);

            return collaboratorDtos;
        }

        /// <summary>
        /// Finds all speakers public API paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="highlights">The highlights.</param>
        /// <param name="conferencesUids">The conferences uids.</param>
        /// <param name="conferencesDates">The conferences dates.</param>
        /// <param name="roomsUids">The rooms uids.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="modifiedAfterDate">The modified after date.</param>
        /// <param name="showDetails">if set to <c>true</c> [show details].</param>
        /// <param name="showDeleted">if set to <c>true</c> [show deleted].</param>
        /// <param name="skipIsApiDisplayEnabledVerification">if set to <c>true</c> [skip is API display enabled verification].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<SpeakerCollaboratorApiDto>> FindAllSpeakersPublicApiPaged(
            int editionId,
            string keywords,
            int? highlights,
            List<Guid?> conferencesUids,
            List<DateTimeOffset?> conferencesDates,
            List<Guid?> roomsUids,
            string collaboratorTypeName,
            DateTime? modifiedAfterDate,
            bool showDetails,
            bool showDeleted,
            bool skipIsApiDisplayEnabledVerification,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery(showDeleted: showDeleted)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId, showDeleted)
                                .IsApiDisplayEnabled(editionId, collaboratorTypeName, showDeleted, skipIsApiDisplayEnabledVerification)
                                .FindByKeywords(keywords, editionId, showDeleted)
                                .FindByApiHighlights(collaboratorTypeName, highlights, showDeleted)
                                .FindByConferencesDates(conferencesDates, showDeleted)
                                .FindByConferencesUids(conferencesUids, showDeleted)
                                .FindByConferencesRoomsUids(roomsUids, showDeleted)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<SpeakerCollaboratorApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(c => new SpeakerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    BadgeName = c.Badge,
                    Name = c.FirstName + " " + c.LastNames,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                            .FirstOrDefault(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                                                .AttendeeCollaboratorTypes
                                                                    .FirstOrDefault(act => (!act.IsDeleted || showDeleted) && act.CollaboratorType.Name == collaboratorTypeName)
                                                                        .ApiHighlightPosition,
                    ImageUploadDate = c.ImageUploadDate,
                    Website = c.Website,
                    Linkedin = c.Linkedin,
                    Twitter = c.Twitter,
                    Instagram = c.Instagram,
                    Youtube = c.Youtube,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId).IsDeleted,
                    MiniBiosDtos = c.MiniBios
                                        .Where(mb => (!mb.IsDeleted || showDeleted))
                                        .Select(d => new CollaboratorMiniBioBaseDto
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
                    JobTitlesDtos = c.JobTitles
                                        .Where(jb => (!jb.IsDeleted || showDeleted))
                                        .Select(d => new CollaboratorJobTitleBaseDto
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
                    OrganizationsDtos = c.AttendeeCollaborators
                                            .Where(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                            .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                                    .Where(aoc => (!aoc.IsDeleted || showDeleted) && (!aoc.AttendeeOrganization.IsDeleted || showDeleted) && (!aoc.AttendeeOrganization.Organization.IsDeleted || showDeleted))
                                                                    .Select(aoc => new OrganizationApiListDto
                                                                    {
                                                                        Uid = aoc.AttendeeOrganization.Organization.Uid,
                                                                        Name = aoc.AttendeeOrganization.Organization.Name,
                                                                        CompanyName = aoc.AttendeeOrganization.Organization.CompanyName,
                                                                        TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                        ImageUploadDate = aoc.AttendeeOrganization.Organization.ImageUploadDate
                                                                    })),
                    TracksDtos = c.AttendeeCollaborators
                                            .Where(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                            .SelectMany(ac => ac.ConferenceParticipants
                                                                    .Where(cp => (!cp.IsDeleted || showDeleted) && (!cp.Conference.IsDeleted || showDeleted))
                                                                    .SelectMany(cp => cp.Conference.ConferenceTracks
                                                                                            .Where(ct => (!ct.IsDeleted || showDeleted) && (!ct.Track.IsDeleted || showDeleted))
                                                                                            .Select(ct => new TrackDto
                                                                                            {
                                                                                                Track = ct.Track
                                                                                            }))).Distinct(),
                    ConferencesDtos = c.AttendeeCollaborators
                                                        .Where(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                                        .SelectMany(ac => ac.ConferenceParticipants
                                                                                .Where(cp => (!cp.IsDeleted || showDeleted) && (!cp.Conference.IsDeleted || showDeleted))
                                                                                .Select(cp => new ConferenceDto
                                                                                {
                                                                                    Uid = cp.Conference.Uid,
                                                                                    StartDate = cp.Conference.StartDate,
                                                                                    EndDate = cp.Conference.EndDate,
                                                                                    EditionEvent = cp.Conference.EditionEvent,
                                                                                    ConferenceTitleDtos = cp.Conference.ConferenceTitles.Where(ct => (!ct.IsDeleted || showDeleted)).Select(ct => new ConferenceTitleDto
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
                                                                                    RoomDto = new RoomDto
                                                                                    {
                                                                                        Uid = cp.Conference.Room.Uid,
                                                                                        RoomNameDtos = cp.Conference.Room.RoomNames.Where(rn => (!rn.IsDeleted || showDeleted)).Select(rn => new RoomNameDto
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
                                                                                    ConferenceSynopsisDtos = cp.Conference.ConferenceSynopses.Where(cs => (!cs.IsDeleted || showDeleted)).Select(cs => new ConferenceSynopsisDto
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
                                                                                })),
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(c => new SpeakerCollaboratorApiDto
                {
                    Uid = c.Uid,
                    BadgeName = c.Badge,
                    Name = c.FirstName + " " + c.LastNames,
                    ApiHighlightPosition = c.AttendeeCollaborators
                                                            .FirstOrDefault(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                                                .AttendeeCollaboratorTypes
                                                                    .FirstOrDefault(act => (!act.IsDeleted || showDeleted) && act.CollaboratorType.Name == collaboratorTypeName)
                                                                        .ApiHighlightPosition,
                    IsApiDisplayEnabled = c.AttendeeCollaborators
                                                            .FirstOrDefault(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId)
                                                                .AttendeeCollaboratorTypes
                                                                    .FirstOrDefault(act => (!act.IsDeleted || showDeleted) && act.CollaboratorType.Name == collaboratorTypeName)
                                                                        .IsApiDisplayEnabled,
                    CreateDate = c.CreateDate,
                    UpdateDate = c.UpdateDate,
                    IsDeleted = c.AttendeeCollaborators.FirstOrDefault(ac => (!ac.IsDeleted || showDeleted) && ac.EditionId == editionId).IsDeleted,
                    ImageUploadDate = c.ImageUploadDate,
                    Website = c.Website,
                    MiniBiosDtos = c.MiniBios
                                        .Where(mb => (!mb.IsDeleted || showDeleted))
                                        .Select(d => new CollaboratorMiniBioBaseDto
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
                    JobTitlesDtos = c.JobTitles
                                        .Where(jb => (!jb.IsDeleted || showDeleted))
                                        .Select(d => new CollaboratorJobTitleBaseDto
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
                                        })
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.BadgeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the speaker public API dto by uid.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        public async Task<SpeakerCollaboratorApiDto> FindSpeakerPublicApiDtoByUid(Guid collaboratorUid, int editionId, string collaboratorTypeName)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, editionId)
                                .IsApiDisplayEnabled(editionId, collaboratorTypeName);

            return await query
                            .Select(c => new SpeakerCollaboratorApiDto
                            {
                                Uid = c.Uid,
                                BadgeName = c.Badge,
                                Name = c.FirstName + " " + c.LastNames,
                                ApiHighlightPosition = c.AttendeeCollaborators.Where(ac => !ac.IsDeleted && ac.EditionId == editionId).FirstOrDefault()
                                                        .AttendeeCollaboratorTypes.Where(act => !act.IsDeleted && act.CollaboratorType.Name == collaboratorTypeName).FirstOrDefault()
                                                        .ApiHighlightPosition,
                                ImageUploadDate = c.ImageUploadDate,
                                Website = c.Website,
                                Linkedin = c.Linkedin,
                                Twitter = c.Twitter,
                                Instagram = c.Instagram,
                                Youtube = c.Youtube,
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                                MiniBiosDtos = c.MiniBios.Where(mb => !mb.IsDeleted).Select(d => new CollaboratorMiniBioBaseDto
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
                                JobTitlesDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                OrganizationsDtos = c.AttendeeCollaborators
                                                            .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                            .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                    .Select(aoc => new OrganizationApiListDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeOrganization.Organization.Uid,
                                                                                        Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                        CompanyName = aoc.AttendeeOrganization.Organization.CompanyName,
                                                                                        TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                                                                        ImageUploadDate = aoc.AttendeeOrganization.Organization.ImageUploadDate
                                                                                    })),
                                TracksDtos = c.AttendeeCollaborators
                                                            .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                            .SelectMany(ac => ac.ConferenceParticipants
                                                                                    .Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted)
                                                                                    .SelectMany(cp => cp.Conference.ConferenceTracks
                                                                                                            .Where(ct => !ct.IsDeleted && !ct.Track.IsDeleted)
                                                                                                            .Select(ct => new TrackDto
                                                                                                            {
                                                                                                                Track = ct.Track
                                                                                                            }))).Distinct(),
                                ConferencesDtos = c.AttendeeCollaborators
                                                            .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                            .SelectMany(ac => ac.ConferenceParticipants
                                                                                    .Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted)
                                                                                    .Select(cp => new ConferenceDto
                                                                                    {
                                                                                        Uid = cp.Conference.Uid,
                                                                                        StartDate = cp.Conference.StartDate,
                                                                                        EndDate = cp.Conference.EndDate,
                                                                                        EditionEvent = cp.Conference.EditionEvent,
                                                                                        RoomDto = new RoomDto
                                                                                        {
                                                                                            Room = cp.Conference.Room,
                                                                                            RoomNameDtos = cp.Conference.Room.RoomNames.Where(rn => !rn.IsDeleted).Select(rn => new RoomNameDto
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
                                                                                        }),
                                                                                        ConferenceSynopsisDtos = cp.Conference.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
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
                                                                                    })),
                            })
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.BadgeName)
                            .FirstOrDefaultAsync();
        }

        #endregion

        #region Agenda

        /// <summary>
        /// Finds all with agenda by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorDto>> FindAllWithAgendaByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            string[] collaboratorTypeNames,
            string userInterfaceLanguage,
            int? editionId)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                                .FindByKeywords(keywords, editionId)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, false, false, editionId);

            var collaboratorDtos = await query
                                        .DynamicOrder(sortColumns,
                                                        new List<Tuple<string, string>>
                                                        {
                                                                new Tuple<string, string>("FullName", "User.Name"),
                                                                new Tuple<string, string>("Email", "User.Email")
                                                        },
                                                        new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                                        "User.Name")
                                        .Select(c => new CollaboratorDto
                                        {
                                            Id = c.Id,
                                            Uid = c.Uid,
                                            FirstName = c.FirstName,
                                            LastNames = c.LastNames,
                                            Email = c.User.Email,
                                            ImageUploadDate = c.ImageUploadDate,
                                            CreateDate = c.CreateDate,
                                            UpdateDate = c.UpdateDate,
                                            UserInterfaceLanguage = userInterfaceLanguage,
                                            AttendeeOrganizationBasesDtos = c.AttendeeCollaborators
                                                                                .Where(at => !at.IsDeleted && at.EditionId == editionId)
                                                                                .SelectMany(at => at.AttendeeOrganizationCollaborators
                                                                                                        .Where(aoc => !aoc.IsDeleted)
                                                                                                        .Select(aoc => new AttendeeOrganizationBaseDto
                                                                                                        {
                                                                                                            Uid = aoc.AttendeeOrganization.Uid,
                                                                                                            OrganizationBaseDto = new OrganizationBaseDto
                                                                                                            {
                                                                                                                Name = aoc.AttendeeOrganization.Organization.Name,
                                                                                                                TradeName = aoc.AttendeeOrganization.Organization.TradeName
                                                                                                            }
                                                                                                        })),
                                            AttendeeCollaboratorTypeDtos = c.AttendeeCollaborators
                                                                                .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                    .AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                        .Select(act => new AttendeeCollaboratorTypeDto()
                                                                                        {
                                                                                            CollaboratorTypeDescription = act.CollaboratorType.Description
                                                                                        })
                                        })
                                        .ToListPagedAsync(page, pageSize);

            this.SetProxyEnabled(true);

            return collaboratorDtos;

        }

        /// <summary>
        /// Counts all with agenda by data table.
        /// </summary>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllWithAgendaByDataTable(bool showAllEditions, int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(Constants.CollaboratorType.ReceivesAgendaEmail, showAllEditions, false, editionId);

            return await query.CountAsync();
        }

        /// <summary>Finds all collaborators by collaborators uids.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        public async Task<List<CollaboratorDto>> FindAllCollaboratorDtosWithAgendaByUids(int editionId, List<Guid> collaboratorsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(Constants.CollaboratorType.ReceivesAgendaEmail, false, false, editionId)
                                .FindByUids(collaboratorsUids);

            return await query
                            .Select(c => new CollaboratorDto
                            {
                                Id = c.Id,
                                Uid = c.Uid,
                                FirstName = c.FirstName,
                                LastNames = c.LastNames,
                                UserBaseDto = new UserBaseDto
                                {
                                    Id = c.User.Id,
                                    Uid = c.User.Uid,
                                    Email = c.User.Email,
                                    SecurityStamp = c.User.SecurityStamp,
                                    Name = c.User.Name,
                                },
                                UserInterfaceLanguage = c.User.UserInterfaceLanguage.Code,

                                // Collaborator Types
                                AttendeeCollaboratorTypeDtos = c.AttendeeCollaborators
                                                                                .FirstOrDefault(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                                                    .AttendeeCollaboratorTypes
                                                                                        .Where(act => !act.IsDeleted && !act.CollaboratorType.IsDeleted)
                                                                                        .Select(act => new AttendeeCollaboratorTypeDto()
                                                                                        {
                                                                                            CollaboratorTypeName = act.CollaboratorType.Name,
                                                                                            CollaboratorTypeDescription = act.CollaboratorType.Description
                                                                                        }),

                                // Conferecences
                                ConferencesDtos = c.AttendeeCollaborators
                                                    .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                    .SelectMany(ac => ac.ConferenceParticipants
                                                                            .Where(cp => !cp.IsDeleted && !cp.Conference.IsDeleted)
                                                                            .Select(cp => new ConferenceDto
                                                                            {
                                                                                StartDate = cp.Conference.StartDate,
                                                                                EndDate = cp.Conference.EndDate,
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
                                                                                }),
                                                                                ConferenceSynopsisDtos = cp.Conference.ConferenceSynopses.Where(cs => !cs.IsDeleted).Select(cs => new ConferenceSynopsisDto
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
                                                                                ConferenceParticipantDtos = cp.Conference.ConferenceParticipants.Where(cp1 => !cp1.IsDeleted).Select(cp1 => new ConferenceParticipantDto
                                                                                {
                                                                                    ConferenceParticipant = cp1,
                                                                                    AttendeeCollaboratorDto = new AttendeeCollaboratorDto
                                                                                    {
                                                                                        Collaborator = cp1.AttendeeCollaborator.Collaborator,
                                                                                        JobTitlesDtos = cp1.AttendeeCollaborator.Collaborator.JobTitles.Where(d => !d.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                                                                        })
                                                                                    },
                                                                                }),
                                                                                RoomDto = new RoomDto
                                                                                {
                                                                                    RoomNameDtos = cp.Conference.Room.RoomNames.Select(rn => new RoomNameDto
                                                                                    {
                                                                                        RoomName = rn,
                                                                                        LanguageDto = new LanguageBaseDto
                                                                                        {
                                                                                            Id = rn.Language.Id,
                                                                                            Uid = rn.Language.Uid,
                                                                                            Name = rn.Language.Name,
                                                                                            Code = rn.Language.Code
                                                                                        }
                                                                                    })
                                                                                }
                                                                            })),
                                // Player Negotiations
                                NegotiationBaseDtos = c.AttendeeCollaborators
                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                        .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                            .SelectMany(aoc => aoc.AttendeeOrganization.ProjectBuyerEvaluations
                                                                .SelectMany(pbe => pbe.Negotiations.Where(n => !n.IsDeleted && !n.ProjectBuyerEvaluation.IsDeleted && !n.ProjectBuyerEvaluation.Project.IsDeleted)
                                                                    .Select(n => new NegotiationBaseDto
                                                                    {
                                                                        Id = n.Id,
                                                                        Uid = n.Uid,
                                                                        StartDate = n.StartDate,
                                                                        EndDate = n.EndDate,
                                                                        TableNumber = n.TableNumber,
                                                                        RoundNumber = n.RoundNumber,
                                                                        IsAutomatic = n.IsAutomatic,
                                                                        ProjectBuyerEvaluationBaseDto = new ProjectBuyerEvaluationBaseDto
                                                                        {
                                                                            Id = n.ProjectBuyerEvaluation.Id,
                                                                            Uid = n.ProjectBuyerEvaluation.Uid,
                                                                            EvaluationDate = n.ProjectBuyerEvaluation.EvaluationDate,
                                                                            Reason = n.ProjectBuyerEvaluation.Reason,
                                                                            ProjectBaseDto = new ProjectBaseDto
                                                                            {
                                                                                Id = pbe.Id,
                                                                                Uid = pbe.Uid,
                                                                                // Get the project title according to the User's language
                                                                                ProjectName = pbe.Project.ProjectTitles.FirstOrDefault(t => t.LanguageId == ac.Collaborator.User.UserInterfaceLanguageId).Value,
                                                                                CreateDate = pbe.Project.CreateDate,
                                                                                FinishDate = pbe.Project.FinishDate
                                                                            },
                                                                            SellerAttendeeOrganizationBaseDto = new AttendeeOrganizationBaseDto
                                                                            {
                                                                                Id = pbe.Project.SellerAttendeeOrganization.Id,
                                                                                Uid = pbe.Project.SellerAttendeeOrganization.Uid,
                                                                                OrganizationBaseDto = new OrganizationBaseDto
                                                                                {
                                                                                    Id = pbe.Project.SellerAttendeeOrganization.Organization.Id,
                                                                                    Uid = pbe.Project.SellerAttendeeOrganization.Organization.Uid,
                                                                                    Name = pbe.Project.SellerAttendeeOrganization.Organization.Name,
                                                                                    TradeName = pbe.Project.SellerAttendeeOrganization.Organization.TradeName,
                                                                                    ImageUploadDate = pbe.Project.SellerAttendeeOrganization.Organization.ImageUploadDate
                                                                                },
                                                                                CreateDate = pbe.Project.SellerAttendeeOrganization.CreateDate,
                                                                                UpdateDate = pbe.Project.SellerAttendeeOrganization.UpdateDate,
                                                                            }
                                                                        },
                                                                        RoomJsonDto = new RoomJsonDto
                                                                        {
                                                                            Id = n.Room.Id,
                                                                            Uid = n.Room.Uid,
                                                                            // Get the room name according to the User's language
                                                                            Name = n.Room.RoomNames.FirstOrDefault(rn => !rn.IsDeleted && rn.LanguageId == ac.Collaborator.User.UserInterfaceLanguageId).Value,
                                                                            IsVirtualMeeting = n.Room.IsVirtualMeeting,
                                                                            CreateDate = n.Room.CreateDate,
                                                                            UpdateDate = n.Room.UpdateDate
                                                                        }
                                                                    })))),

                                // Producer Negotiations
                                ProducerNegotiationBaseDtos = c.AttendeeCollaborators
                                                        .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                                        .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                                            .SelectMany(aoc => aoc.AttendeeOrganization.SellProjects
                                                                .SelectMany(p => p.ProjectBuyerEvaluations
                                                                    .SelectMany(pbe => pbe.Negotiations.Where(n => !n.IsDeleted && !n.ProjectBuyerEvaluation.IsDeleted && !n.ProjectBuyerEvaluation.Project.IsDeleted)
                                                                        .Select(n => new NegotiationBaseDto
                                                                        {
                                                                            Id = n.Id,
                                                                            Uid = n.Uid,
                                                                            StartDate = n.StartDate,
                                                                            EndDate = n.EndDate,
                                                                            TableNumber = n.TableNumber,
                                                                            RoundNumber = n.RoundNumber,
                                                                            IsAutomatic = n.IsAutomatic,
                                                                            ProjectBuyerEvaluationBaseDto = new ProjectBuyerEvaluationBaseDto
                                                                            {
                                                                                Id = n.ProjectBuyerEvaluation.Id,
                                                                                Uid = n.ProjectBuyerEvaluation.Uid,
                                                                                EvaluationDate = n.ProjectBuyerEvaluation.EvaluationDate,
                                                                                Reason = n.ProjectBuyerEvaluation.Reason,
                                                                                ProjectBaseDto = new ProjectBaseDto
                                                                                {
                                                                                    Id = pbe.Id,
                                                                                    Uid = pbe.Uid,
                                                                                    // Get the project title according to the User's language
                                                                                    ProjectName = pbe.Project.ProjectTitles.FirstOrDefault(t => t.LanguageId == ac.Collaborator.User.UserInterfaceLanguageId).Value,
                                                                                    CreateDate = pbe.Project.CreateDate,
                                                                                    FinishDate = pbe.Project.FinishDate
                                                                                },
                                                                                SellerAttendeeOrganizationBaseDto = new AttendeeOrganizationBaseDto
                                                                                {
                                                                                    Id = pbe.BuyerAttendeeOrganization.Id,
                                                                                    Uid = pbe.BuyerAttendeeOrganization.Uid,
                                                                                    OrganizationBaseDto = new OrganizationBaseDto
                                                                                    {
                                                                                        Id = pbe.BuyerAttendeeOrganization.Organization.Id,
                                                                                        Uid = pbe.BuyerAttendeeOrganization.Organization.Uid,
                                                                                        Name = pbe.BuyerAttendeeOrganization.Organization.Name,
                                                                                        TradeName = pbe.BuyerAttendeeOrganization.Organization.TradeName,
                                                                                        ImageUploadDate = pbe.BuyerAttendeeOrganization.Organization.ImageUploadDate
                                                                                    },
                                                                                    CreateDate = pbe.BuyerAttendeeOrganization.CreateDate,
                                                                                    UpdateDate = pbe.BuyerAttendeeOrganization.UpdateDate,
                                                                                }
                                                                            },
                                                                            RoomJsonDto = new RoomJsonDto
                                                                            {
                                                                                Id = n.Room.Id,
                                                                                Uid = n.Room.Uid,
                                                                                // Get the room name according to the User's language
                                                                                Name = n.Room.RoomNames.FirstOrDefault(rn => !rn.IsDeleted && rn.LanguageId == ac.Collaborator.User.UserInterfaceLanguageId).Value,
                                                                                IsVirtualMeeting = n.Room.IsVirtualMeeting,
                                                                                CreateDate = n.Room.CreateDate,
                                                                                UpdateDate = n.Room.UpdateDate
                                                                            }
                                                                        })))
                                                                )),
                            })
                            .ToListAsync();
        }

        #endregion

        #region Api

        /// <summary>Finds all dropdown API list dto paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="filterByProjectsInNegotiation">if set to <c>true</c> [filter by projects in negotiation].</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllParticipants"></param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorApiListDto>> FindAllDropdownApiListDtoPaged(
            int editionId,
            string keywords,
            bool filterByProjectsInNegotiation,
            string collaboratorTypeName,
            bool showAllParticipants,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery(true)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, editionId)
                                .FindByKeywords(keywords, editionId)
                                .HasProjectInNegotiation(editionId, filterByProjectsInNegotiation);

            return await query
                            .Select(c => new CollaboratorApiListDto
                            {
                                Uid = c.Uid,
                                BadgeName = c.Badge,
                                Name = c.FirstName + " " + c.LastNames,
                                ImageUploadDate = c.ImageUploadDate,
                                JobTitlesDtos = c.JobTitles.Where(jb => !jb.IsDeleted).Select(d => new CollaboratorJobTitleBaseDto
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
                                OrganizationsDtos = c.AttendeeCollaborators
                                    .Where(ac => !ac.IsDeleted && ac.EditionId == editionId)
                                    .SelectMany(ac => ac.AttendeeOrganizationCollaborators
                                        .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                        .Select(aoc => new OrganizationApiListDto
                                        {
                                            Uid = aoc.AttendeeOrganization.Organization.Uid,
                                            Name = aoc.AttendeeOrganization.Organization.Name,
                                            CompanyName = aoc.AttendeeOrganization.Organization.CompanyName,
                                            TradeName = aoc.AttendeeOrganization.Organization.TradeName,
                                            ImageUploadDate = aoc.AttendeeOrganization.Organization.ImageUploadDate

                                        })),
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate
                            })
                            .OrderBy(o => o.BadgeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds all by hightlight position.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeUid">The collaborator type uid.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        public async Task<List<Collaborator>> FindAllByHightlightPosition(int editionId, Guid collaboratorTypeUid, int apiHighlightPosition, Guid? organizationUid)
        {
            var query = this.GetBaseQuery()
                                .Where(o => o.Uid != organizationUid
                                            && o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                                 && ac.EditionId == editionId
                                                                                 && ac.AttendeeCollaboratorTypes.Any(aot => !aot.IsDeleted
                                                                                                                            && aot.CollaboratorType.Uid == collaboratorTypeUid
                                                                                                                            && aot.ApiHighlightPosition == apiHighlightPosition)));

            return await query
                            .ToListAsync();
        }

        #endregion
    }
}