// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Repository
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-20-2020
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
        /// Finds the by email.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByEmail(this IQueryable<Collaborator> query, string email)
        {
            query = query.Where(c => c.PublicEmail == email);

            return query;
        }

        /// <summary>Finds the by collaborator type name and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<Collaborator> query, string[] collaboratorTypeNames, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            if (collaboratorTypeNames?.Any(ctn => !string.IsNullOrEmpty(ctn)) == true)
            {
                query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                           && !ac.IsDeleted
                                                                           && !ac.Edition.IsDeleted
                                                                           && (showAllParticipants
                                                                               || (showAllExecutives && ac.AttendeeOrganizationCollaborators
                                                                                                                .Any(aoc => !aoc.IsDeleted))
                                                                               || (!showAllExecutives && ac.AttendeeCollaboratorTypes
                                                                                                                .Any(act => !act.IsDeleted
                                                                                                                            && !act.CollaboratorType.IsDeleted
                                                                                                                            && collaboratorTypeNames.Contains(act.CollaboratorType.Name))))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by collaborator type name and by edition identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByCollaboratorTypeNameAndByEditionId(this IQueryable<Collaborator> query, string[] collaboratorTypeNames, bool showAllEditions, int? editionId)
        {
            //Preciso:
            //Trazer todos os usuários independente da role
            //Trazer todos os usuários com collaboratorTypeName != User
            //Porém um AdminFull não tem collaboratorType, logo se eu adicionar esta clausula where e definir que quero todos os AttendeeCollaboratorTypes != User, não trará os AdminFull!

            if (collaboratorTypeNames?.Any(ctn => !string.IsNullOrEmpty(ctn)) == true)
            {
                query = query.Where(c => (c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                                                               && !ac.IsDeleted
                                                                                                               && !ac.Edition.IsDeleted
                                                                                                               && ac.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted)
                                                                                                               || (ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                                                            && !act.CollaboratorType.IsDeleted
                                                                                                                                                            && collaboratorTypeNames.Contains(act.CollaboratorType.Name))))));
            }

            return query;
        }

        /// <summary>
        /// Finds the by role.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="rolesNames">The roles names.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByRole(this IQueryable<Collaborator> query, string[] rolesNames)
        {
            if (rolesNames?.Any(rn => !string.IsNullOrEmpty(rn)) == true)
            {
                query = query.Where(c => !c.IsDeleted
                                            && c.User.Roles.Any(r => rolesNames.Contains(r.Name)));
            }

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

        /// <summary>Finds the by organization type uid and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByOrganizationTypeUidAndByEditionId(this IQueryable<Collaborator> query, Guid organizationTypeUid, bool showAllEditions, bool showAllExecutives, bool showAllParticipants, int? editionId)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => (showAllEditions || ac.EditionId == editionId)
                                                                       && !ac.IsDeleted
                                                                       && !ac.Edition.IsDeleted
                                                                       && (showAllParticipants
                                                                           || ac.AttendeeOrganizationCollaborators
                                                                                   .Any(aoc => !aoc.IsDeleted
                                                                                               && !aoc.AttendeeOrganization.IsDeleted
                                                                                               && aoc.AttendeeOrganization.AttendeeOrganizationTypes
                                                                                                       .Any(aot => !aot.IsDeleted
                                                                                                                   && (showAllExecutives || aot.OrganizationType.Uid == organizationTypeUid))))));

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
                && collaboratorTypeNames?.Any(ctn => !string.IsNullOrEmpty(ctn)) == true)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                           && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
                                                                                                                      && collaboratorTypeNames.Contains(act.CollaboratorType.Name)
                                                                                                                      && act.IsApiDisplayEnabled
                                                                                                                      && act.ApiHighlightPosition.HasValue)));
            }

            return query;
        }

        /// <summary>Finds the by API highlights.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="highlights">The highlights.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> FindByApiHighlights(this IQueryable<Collaborator> query, string collaboratorTypeName, int? highlights)
        {
            if (highlights == 0 || highlights == 1)
            {
                query = query.Where(o => o.AttendeeCollaborators.Any(ac => !ac.IsDeleted
                                                                           && ac.AttendeeCollaboratorTypes.Any(act => !act.IsDeleted
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
        internal static IQueryable<Collaborator> FindByKeywords(this IQueryable<Collaborator> query, string keywords, int? editionId)
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
                                                                                                    && !ac.IsDeleted
                                                                                                    && !ac.Edition.IsDeleted
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                                        && !aoc.AttendeeOrganization.IsDeleted
                                                                                                                        && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                                        && aoc.AttendeeOrganization.Organization.Name.Contains(keyword))));
                        innerHoldingNameWhere = innerHoldingNameWhere
                                                        .And(c => c.AttendeeCollaborators.Any(ac => (!editionId.HasValue || ac.EditionId == editionId)
                                                                                                    && !ac.IsDeleted
                                                                                                    && !ac.Edition.IsDeleted
                                                                                                    && ac.AttendeeOrganizationCollaborators
                                                                                                        .Any(aoc => !aoc.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.Organization.IsDeleted
                                                                                                                    && !aoc.AttendeeOrganization.Organization.Holding.IsDeleted
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

        /// <summary>Determines whether [is API display enabled] [the specified edition identifier].</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> IsApiDisplayEnabled(this IQueryable<Collaborator> query, int editionId, string collaboratorTypeName)
        {
            query = query.Where(c => c.AttendeeCollaborators.Any(ac => ac.EditionId == editionId
                                                                       && ac.AttendeeCollaboratorTypes.Any(aot => !aot.IsDeleted
                                                                                                                  && aot.CollaboratorType.Name == collaboratorTypeName
                                                                                                                  && aot.IsApiDisplayEnabled)));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Collaborator> IsNotDeleted(this IQueryable<Collaborator> query)
        {
            query = query.Where(c => !c.IsDeleted);

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
        internal static async Task<IPagedList<CollaboratorBaseDto>> ToListPagedAsync(this IQueryable<CollaboratorBaseDto> query, int page, int pageSize)
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
        private IQueryable<Collaborator> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

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

        /// <summary>Finds the dto by uid and by edition identifier asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<CollaboratorDto> FindDtoByUidAndByEditionIdAsync(Guid collaboratorUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid);

            return await query
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
                                //HoldingBaseDto = new HoldingBaseDto
                                //{
                                //    Id = c.Holding.Id,
                                //    Uid = c.Holding.Uid,
                                //    Name = c.Holding.Name
                                //},
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
                                //Creator = h.Creator,
                                //HoldingBaseDto = new HoldingBaseDto
                                //{
                                //    Id = c.Holding.Id,
                                //    Uid = c.Holding.Uid,
                                //    Name = c.Holding.Name
                                //},
                                EditionAttendeeCollaboratorBaseDto = c.AttendeeCollaborators.Where(ac => !ac.IsDeleted && ac.EditionId == editionId).Select(ac => new AttendeeCollaboratorBaseDto
                                {
                                    Id = ac.Id,
                                    Uid = ac.Uid,
                                    WelcomeEmailSendDate = ac.WelcomeEmailSendDate,
                                    OnboardingStartDate = ac.OnboardingStartDate,
                                    OnboardingFinishDate = ac.OnboardingFinishDate,
                                    OnboardingUserDate = ac.OnboardingUserDate,
                                    OnboardingCollaboratorDate = ac.OnboardingCollaboratorDate,
                                    PlayerTermsAcceptanceDate = ac.PlayerTermsAcceptanceDate,
                                    ProducerTermsAcceptanceDate = ac.ProducerTermsAcceptanceDate
                                }).FirstOrDefault(),
                                UpdaterDto = new UserBaseDto
                                {
                                    Uid = c.Updater.Uid,
                                    Name = c.Updater.Name,
                                    Email = c.Updater.Email
                                },
                                JobTitlesDtos = c.JobTitles.Select(d => new CollaboratorJobTitleBaseDto
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
                                MiniBiosDtos = c.MiniBios.Select(d => new CollaboratorMiniBioBaseDto
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
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    }
                                                                                                }
                                                                                            }))
                            }).FirstOrDefaultAsync();
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
        /// <param name="showAllExecutives">if set to <c>true</c> [show all executives].</param>
        /// <param name="showAllParticipants">if set to <c>true</c> [show all participants].</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorBaseDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> collaboratorsUids,
            string[] collaboratorTypeNames,
            bool showAllEditions,
            bool showAllExecutives,
            bool showAllParticipants,
            bool? showHighlights,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, showAllExecutives, showAllParticipants, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);

            return await query
                            .DynamicOrder<Collaborator>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorBaseDto
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
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) :
                                                                                   null,
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId
                                                                                                           && !ac.IsDeleted),
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
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    }
                                                                                                }
                                                                                            })),

                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,

                                CollaboratorTypeName = c.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted)
                                                                                .AttendeeCollaboratorTypes.FirstOrDefault(act => !act.IsDeleted)
                                                                                .CollaboratorType.Name,

                                RoleName = c.User.Roles.FirstOrDefault().Name
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(string collaboratorTypeName, bool showAllEditions, int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, showAllEditions, false, false, editionId);

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
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Collaborator> FindByEmailAsync(string email)
        {
            var query = this.GetBaseQuery()
                               .FindByEmail(email);

            return await query.FirstOrDefaultAsync();
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
        /// Finds all amins by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <param name="collaboratorTypeNames">The collaborator type names.</param>
        /// <param name="rolesNames">The roles names.</param>
        /// <param name="showHighlights">The show highlights.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorBaseDto>> FindAllAminsByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            List<Guid> collaboratorsUids,
            string[] collaboratorTypeNames,
            string[] rolesNames,
            bool showAllEditions,
            bool? showHighlights,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords, editionId)
                                .FindByUids(collaboratorsUids)
                                .FindByRole(rolesNames)
                                .FindByCollaboratorTypeNameAndByEditionId(collaboratorTypeNames, showAllEditions, editionId)
                                .FindByHighlights(collaboratorTypeNames, showHighlights);

            return await query
                            .DynamicOrder<Collaborator>(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("FullName", "User.Name"),
                                    new Tuple<string, string>("Email", "User.Email"),
                                },
                                new List<string> { "User.Name", "User.Email", "CreateDate", "UpdateDate" },
                                "User.Name")
                            .Select(c => new CollaboratorBaseDto
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
                                                                                                                                                                           && collaboratorTypeNames.Contains(act.CollaboratorType.Name))) :
                                                                                   null,
                                IsInOtherEdition = editionId.HasValue && c.AttendeeCollaborators.Any(ac => ac.EditionId != editionId
                                                                                                           && !ac.IsDeleted),
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
                                                                                                    HoldingBaseDto = aoc.AttendeeOrganization.Organization.Holding == null ? null : new HoldingBaseDto
                                                                                                    {
                                                                                                        Name = aoc.AttendeeOrganization.Organization.Holding.Name
                                                                                                    }
                                                                                                }
                                                                                            })),

                                JobTitle = c.JobTitles.FirstOrDefault(jb => !jb.IsDeleted && jb.CollaboratorId == c.Id).Value,

                                CollaboratorTypeName = c.AttendeeCollaborators.FirstOrDefault(ac => !ac.IsDeleted)
                                                                                .AttendeeCollaboratorTypes.FirstOrDefault(act => !act.IsDeleted)
                                                                                .CollaboratorType.Name,

                                RoleName = c.User.Roles.FirstOrDefault().Name
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        #region Api

        /// <summary>Finds all public API paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="highlights">The highlights.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<CollaboratorApiListDto>> FindAllPublicApiPaged(int editionId, string keywords, int? highlights, string collaboratorTypeName, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, false, editionId)
                                .IsApiDisplayEnabled(editionId, collaboratorTypeName)
                                .FindByKeywords(keywords, editionId)
                                .FindByApiHighlights(collaboratorTypeName, highlights);

            return await query
                            .Select(c => new CollaboratorApiListDto
                            {
                                Uid = c.Uid,
                                BadgeName = c.Badge,
                                Name = c.FirstName + " " + c.LastNames,
                                ApiHighlightPosition = c.AttendeeCollaborators.Where(ac => !ac.IsDeleted && ac.EditionId == editionId).FirstOrDefault()
                                                        .AttendeeCollaboratorTypes.Where(act => !act.IsDeleted && act.CollaboratorType.Name == collaboratorTypeName).FirstOrDefault()
                                                        .ApiHighlightPosition,
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
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                            })
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.BadgeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds the public API dto asynchronous.</summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="collaboratorTypeName">Name of the collaborator type.</param>
        /// <returns></returns>
        public async Task<SpeakerCollaboratorDto> FindPublicApiDtoAsync(Guid collaboratorUid, int editionId, string collaboratorTypeName)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(collaboratorUid)
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, false, editionId)
                                .IsApiDisplayEnabled(editionId, collaboratorTypeName);

            return await query
                            .Select(c => new SpeakerCollaboratorDto
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
                                                                                        Conference = cp.Conference,
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
                                CreateDate = c.CreateDate,
                                UpdateDate = c.UpdateDate,
                            })
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.BadgeName)
                            .FirstOrDefaultAsync();
        }

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
            var query = this.GetBaseQuery()
                                .FindByCollaboratorTypeNameAndByEditionId(new string[] { collaboratorTypeName }, false, false, showAllParticipants, editionId)
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