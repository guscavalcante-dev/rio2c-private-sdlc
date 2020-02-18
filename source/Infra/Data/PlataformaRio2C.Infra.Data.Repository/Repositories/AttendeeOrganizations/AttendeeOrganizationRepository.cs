// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-06-2020
// ***********************************************************************
// <copyright file="AttendeeOrganizationRepository.cs" company="Softo">
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
    #region Attendee Organization IQueryable Extensions

    /// <summary>
    /// AttendeeOrganizationIQueryableExtensions
    /// </summary>
    internal static class AttendeeeOrganizationIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByUid(this IQueryable<AttendeeOrganization> query, Guid attendeeOrganizationUid)
        {
            query = query.Where(ao => ao.Uid == attendeeOrganizationUid);

            return query;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByUids(this IQueryable<AttendeeOrganization> query, List<Guid> attendeeOrganizationsUids)
        {
            if (attendeeOrganizationsUids?.Any() == true)
            {
                query = query.Where(ao => attendeeOrganizationsUids.Contains(ao.Uid));
            }

            return query;
        }

        /// <summary>Finds the not by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindNotByUid(this IQueryable<AttendeeOrganization> query, Guid attendeeOrganizationUid)
        {
            query = query.Where(ao => ao.Uid != attendeeOrganizationUid);

            return query;
        }

        /// <summary>Finds the by organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByOrganizationUid(this IQueryable<AttendeeOrganization> query, Guid organizationUid)
        {
            query = query.Where(ao => ao.Organization.Uid == organizationUid);

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByKeywords(this IQueryable<AttendeeOrganization> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var predicate = PredicateBuilder.New<AttendeeOrganization>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        predicate = predicate.And(ao => ao.Organization.TradeName.Contains(keyword));
                    }
                }

                query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        /// <summary>Finds the by interest uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUids">The interest uids.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByInterestUids(this IQueryable<AttendeeOrganization> query, List<Guid> interestUids)
        {
            if (interestUids?.Any() == true)
            {
                query = query.Where(ao => ao.Organization.OrganizationInterests.Any(oi => !oi.IsDeleted
                                                                                          && !oi.Interest.IsDeleted
                                                                                          && interestUids.Contains(oi.Interest.Uid)));
            }
            else
            {
                query = query.Where(ao => 1 == 2);
            }

            return query;
        }

        /// <summary>Finds the by organization type uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByOrganizationTypeUid(this IQueryable<AttendeeOrganization> query, int editionId, Guid organizationTypeUid)
        {
            query = query.Where(ao => ao.EditionId == editionId
                                      && ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == organizationTypeUid
                                                                                  && !aot.IsDeleted
                                                                                  && !aot.OrganizationType.IsDeleted));

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByEditionId(this IQueryable<AttendeeOrganization> query, int editionId, bool showAllEditions)
        {
            query = query.Where(ao => (showAllEditions || ao.EditionId == editionId)
                                      && !ao.IsDeleted
                                      && !ao.Edition.IsDeleted
                                      && !ao.Organization.IsDeleted);
            return query;
        }

        /// <summary>Finds the by buyer project uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> FindByBuyerProjectUid(this IQueryable<AttendeeOrganization> query, Guid projectUid)
        {
            query = query.Where(ao => ao.ProjectBuyerEvaluations.Any(pbe => pbe.Project.Uid == projectUid
                                                                            && !ao.IsDeleted 
                                                                            && !pbe.IsDeleted
                                                                            && !pbe.Project.IsDeleted));

            return query;
        }

        //internal static IQueryable<AttendeeOrganization> FindByBuyerMatchingProjectUid(this IQueryable<AttendeeOrganization> query, Guid projectUid)
        //{
        //    query = query.Where(ao => ao.ProjectBuyerEvaluations.Any(pbe => pbe.Project.Uid == projectUid
        //                                                                    && !ao.IsDeleted
        //                                                                    && !pbe.IsDeleted
        //                                                                    && !pbe.Project.IsDeleted));

        //    return query;
        //}

        /// <summary>Determines whether [is onboarding finished].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> IsOnboardingFinished(this IQueryable<AttendeeOrganization> query)
        {
            query = query.Where(ao => ao.OnboardingFinishDate.HasValue);

            return query;
        }
        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<AttendeeOrganization> IsNotDeleted(this IQueryable<AttendeeOrganization> query)
        {
            query = query.Where(ao => !ao.IsDeleted);

            return query;
        }
    }

    #endregion

    #region AttendeeOrganizationDto IQueryable Extensions

    /// <summary>
    /// AttendeeOrganizationDtoIQueryableExtensions
    /// </summary>
    internal static class AttendeeOrganizationDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<AttendeeOrganizationDto>> ToListPagedAsync(this IQueryable<AttendeeOrganizationDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MatchAttendeeOrganizationDto IQueryable Extensions

    /// <summary>
    /// MatchAttendeeOrganizationDtoIQueryableExtensions
    /// </summary>
    internal static class MatchAttendeeOrganizationDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MatchAttendeeOrganizationDto>> ToListPagedAsync(this IQueryable<MatchAttendeeOrganizationDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>AttendeeOrganizationRepository</summary>
    public class AttendeeOrganizationRepository : Repository<PlataformaRio2CContext, AttendeeOrganization>, IAttendeeOrganizationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="AttendeeOrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public AttendeeOrganizationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<AttendeeOrganization> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all base dtos by edition uid asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganizationBaseDto>> FindAllBaseDtosByEditionUidAsync(int editionId, bool showAllEditions, Guid organizationTypeUid)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .FindByOrganizationTypeUid(editionId, organizationTypeUid);

            return await query
                            .Select(ao => new AttendeeOrganizationBaseDto
                            {
                                Id = ao.Id,
                                Uid = ao.Uid,
                                OrganizationBaseDto = new OrganizationBaseDto
                                {
                                    Id = ao.Organization.Id,
                                    Uid = ao.Organization.Uid,
                                    Name = ao.Organization.Name,
                                    HoldingBaseDto = ao.Organization.Holding == null ? null : new HoldingBaseDto
                                    {
                                        Id = ao.Organization.Holding.Id,
                                        Uid = ao.Organization.Holding.Uid,
                                        Name = ao.Organization.Holding.Name
                                    }
                                },
                                CreateDate = ao.CreateDate,
                                UpdateDate = ao.UpdateDate,
                            })
                            .OrderBy(ao => ao.OrganizationBaseDto.HoldingBaseDto.Name)
                            .ThenBy(ao => ao.OrganizationBaseDto.Name)
                            .ToListAsync();
        }

        /// <summary>Finds all by uids asynchronous.</summary>
        /// <param name="attendeeOrganizationsUids">The attendee organizations uids.</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganization>> FindAllByUidsAsync(List<Guid> attendeeOrganizationsUids)
        {
            var query = this.GetBaseQuery()
                                .FindByUids(attendeeOrganizationsUids);

            return await query.ToListAsync();
        }

        #region Site Project Widgets

        /// <summary>Finds all dto by buyer project uid.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<List<AttendeeOrganizationDto>> FindAllDtoByBuyerProjectUid(Guid projectUid)
        {
            var query = this.GetBaseQuery()
                                .FindByBuyerProjectUid(projectUid);

            return await query
                            .Select(ao => new AttendeeOrganizationDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization
                            })
                            .OrderBy(ao => ao.Organization.TradeName)
                            .ToListAsync();
        }

        /// <summary>Finds all dto by matching project buyer asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<MatchAttendeeOrganizationDto>> FindAllDtoByMatchingProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize)
        {
            var buyerOrganizationType = projectDto.ProjectType.OrganizationTypes.FirstOrDefault(ot => !ot.IsDeleted && !ot.IsSeller);
            var lookingForInterests = projectDto.ProjectInterestDtos?.Where(pi => pi.InterestGroup.Uid == InterestGroup.LookingFor.Uid)?.Select(pid => pid.Interest.Uid)?.ToList() ?? new List<Guid>();
            var projectStatusInterests = projectDto.ProjectInterestDtos?.Where(pi => pi.InterestGroup.Uid == InterestGroup.ProjectStatus.Uid)?.Select(pid => pid.Interest.Uid)?.ToList() ?? new List<Guid>();
            var platformsInterests = projectDto.ProjectInterestDtos?.Where(pi => pi.InterestGroup.Uid == InterestGroup.Platforms.Uid)?.Select(pid => pid.Interest.Uid)?.ToList() ?? new List<Guid>();
            var genreInterests = projectDto.ProjectInterestDtos?.Where(pi => pi.InterestGroup.Uid == InterestGroup.Genre.Uid)?.Select(pid => pid.Interest.Uid)?.ToList() ?? new List<Guid>();
            var matchInterests = lookingForInterests
                                 .Union(projectStatusInterests)
                                 .Union(platformsInterests)
                                 .Union(genreInterests)?
                                 .ToList();

            var query = this.GetBaseQuery()
                                .FindByOrganizationTypeUid(editionId, buyerOrganizationType?.Uid ?? Guid.Empty)
                                .FindNotByUid(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid)
                                .FindByKeywords(searchKeywords)
                                .FindByInterestUids(matchInterests)
                                .IsOnboardingFinished();

            return await query
                            .Select(ao => new MatchAttendeeOrganizationDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                InterestGroupsMatches = ao.Organization.OrganizationInterests
                                                                            .Where(oi => !oi.IsDeleted && !oi.Interest.IsDeleted && matchInterests.Contains(oi.Interest.Uid))
                                                                            .Select(oi => oi.Interest.InterestGroup)
                                                                            .Distinct()
                            })
                            .OrderByDescending(ao => ao.InterestGroupsMatches.Count())
                            .ThenBy(ao => ao.Organization.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds all dto by project buyer asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="projectDto">The project dto.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<AttendeeOrganizationDto>> FindAllDtoByProjectBuyerAsync(int editionId, ProjectDto projectDto, string searchKeywords, int page, int pageSize)
        {
            var buyerOrganizationType = projectDto.ProjectType?.OrganizationTypes?.FirstOrDefault(ot => !ot.IsDeleted && !ot.IsSeller);

            var query = this.GetBaseQuery()
                                .FindByOrganizationTypeUid(editionId, buyerOrganizationType?.Uid ?? Guid.Empty)
                                .FindNotByUid(projectDto.SellerAttendeeOrganizationDto.AttendeeOrganization.Uid)
                                .FindByKeywords(searchKeywords)
                                .IsOnboardingFinished();

            return await query
                            .Select(ao => new AttendeeOrganizationDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization
                            })
                            .OrderBy(ao => ao.Organization.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        #endregion

        #region Site Widgets

        /// <summary>Finds the site detailst dto by organization uid and by edition identifier asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttemdeeOrganizationSiteDetailsDto> FindSiteDetailstDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttemdeeOrganizationSiteDetailsDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                AttendeeOrganizationTypesDtos = ao.AttendeeOrganizationTypes
                                                                        .Where(aot => !aot.IsDeleted && !aot.OrganizationType.IsDeleted)
                                                                        .Select(aot => new AttendeeOrganizationTypeDto
                                                                        {
                                                                            AttendeeOrganizationType = aot,
                                                                            OrganizationType = aot.OrganizationType
                                                                        })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site main information widget dto by organization uid and by edition identifier asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttemdeeOrganizationSiteMainInformationWidgetDto> FindSiteMainInformationWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttemdeeOrganizationSiteMainInformationWidgetDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                DescriptionsDtos = ao.Organization.OrganizationDescriptions.Where(d => !d.IsDeleted).Select(d => new OrganizationDescriptionDto
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
                                Country = ao.Organization.Address.Country,
                                State = ao.Organization.Address.State
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site executive widget dto by organization uid and by edition identifier asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganizationSiteExecutiveWidgetDto> FindSiteExecutiveWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttendeeOrganizationSiteExecutiveWidgetDto
                            {
                                AttendeeOrganization = ao,
                                AttendeeCollaboratorsDtos = ao.AttendeeOrganizationCollaborators
                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeCollaborator.IsDeleted && !aoc.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                    .Select(aoc => new AttendeeCollaboratorDto
                                                                    {
                                                                        AttendeeCollaborator = aoc.AttendeeCollaborator,
                                                                        Collaborator = aoc.AttendeeCollaborator.Collaborator,
                                                                        JobTitlesDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Where(jb => !jb.IsDeleted).Select(jb => new CollaboratorJobTitleBaseDto
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
                                                                        })
                                                                    })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site address widget dto by organization uid asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganizationSiteAddressWidgetDto> FindSiteAddressWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttendeeOrganizationSiteAddressWidgetDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                Address = ao.Organization.Address,
                                Country = ao.Organization.Address.Country,
                                State = ao.Organization.Address.State,
                                City = ao.Organization.Address.City
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site a activity widget dto by organization uid asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganizationSiteActivityWidgetDto> FindSiteActivityWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttendeeOrganizationSiteActivityWidgetDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                OrganizationActivitiesDtos = ao.Organization.OrganizationActivities.Where(oa => !oa.IsDeleted).Select(oa => new OrganizationActivityDto
                                {
                                    OrganizationActivityId = oa.Id,
                                    OrganizationActivityUid = oa.Uid,
                                    OrganizationActivityAdditionalInfo = oa.AdditionalInfo,
                                    ActivityId = oa.Activity.Id,
                                    ActivityUid = oa.Activity.Uid,
                                    ActivityName = oa.Activity.Name,
                                    ActivityHasAdditionalInfo = oa.Activity.HasAdditionalInfo
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site target audience widget dto by organization uid asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganizationSiteTargetAudienceWidgetDto> FindSiteTargetAudienceWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(ao => new AttendeeOrganizationSiteTargetAudienceWidgetDto
                            {
                                AttendeeOrganization = ao,
                                Organization = ao.Organization,
                                OrganizationTargetAudiencesDtos = ao.Organization.OrganizationTargetAudiences.Where(ota => !ota.IsDeleted).Select(ota => new OrganizationTargetAudienceDto
                                {
                                    OrganizationTargetAudienceId = ota.Id,
                                    OrganizationTargetAudienceUid = ota.Uid,
                                    TargetAudienceId = ota.TargetAudience.Id,
                                    TargetAudienceUid = ota.TargetAudience.Uid,
                                    TargetAudienceName = ota.TargetAudience.Name
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site interest widget dto by organization uid asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AttendeeOrganizationSiteInterestWidgetDto> FindSiteInterestWidgetDtoByOrganizationUidAndByEditionIdAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByOrganizationUid(organizationUid)
                                .FindByEditionId(editionId, false);

            return await query
                                .Select(ao => new AttendeeOrganizationSiteInterestWidgetDto
                                {
                                    AttendeeOrganization = ao,
                                    Organization = ao.Organization,
                                    RestrictionSpecificDtos = ao.Organization.OrganizationRestrictionSpecifics.Where(rs => !rs.IsDeleted).Select(rs => new OrganizationRestrictionSpecificDto
                                    {
                                        Id = rs.Id,
                                        Uid = rs.Uid,
                                        Value = rs.Value,
                                        LanguageDto = new LanguageBaseDto
                                        {
                                            Id = rs.Language.Id,
                                            Uid = rs.Language.Uid,
                                            Name = rs.Language.Name,
                                            Code = rs.Language.Code
                                        }
                                    }),
                                    OrganizationInterestDtos = ao.Organization.OrganizationInterests.Where(oi => !oi.IsDeleted).Select(oi => new OrganizationInterestDto
                                    {
                                        OrganizationInterest = oi,
                                        Interest = oi.Interest,
                                        InterestGroup = oi.Interest.InterestGroup
                                    })
                                })
                                .FirstOrDefaultAsync();
        }

        #endregion
    }
}