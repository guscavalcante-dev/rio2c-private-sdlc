// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-16-2024
// ***********************************************************************
// <copyright file="OrganizationRepository.cs" company="Softo">
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
using System.Linq.Expressions;
using System.Threading.Tasks;
using LinqKit;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Organization IQueryable Extensions

    /// <summary>
    /// OrganizationIQueryableExtensions
    /// </summary>
    internal static class OrganizationIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByUid(this IQueryable<Organization> query, Guid organizationId)
        {
            query = query.Where(o => o.Uid == organizationId);

            return query;
        }

        /// <summary>Finds the name of the by company.</summary>
        /// <param name="query">The query.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByCompanyName(this IQueryable<Organization> query, string companyName)
        {
            if (!string.IsNullOrEmpty(companyName))
            {
                query = query.Where(o => o.CompanyName.Contains(companyName));
            }

            return query;
        }

        /// <summary>Finds the name of the by trade.</summary>
        /// <param name="query">The query.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByTradeName(this IQueryable<Organization> query, string tradeName)
        {
            if (!string.IsNullOrEmpty(tradeName))
            {
                query = query.Where(o => o.TradeName.Contains(tradeName));
            }

            return query;
        }

        /// <summary>Finds the by equal document.</summary>
        /// <param name="query">The query.</param>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByEqualDocument(this IQueryable<Organization> query, string document)
        {
            if (!string.IsNullOrEmpty(document))
            {
                query = query.Where(o => o.Document.Replace(".", "").Replace("-", "").Replace("/", "") == document.Replace(".", "").Replace("-", "").Replace("/", ""));
            }

            return query;
        }

        /// <summary>Finds the by organization type uid and by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByOrganizationTypeUidAndByEditionId(this IQueryable<Organization> query, Guid? organizationTypeUid, bool showAllEditions, bool showAllOrganizations, int? editionId, bool showDeleted = false)
        {
            if (showAllEditions && showAllOrganizations)
            {
                query = query.Where(o => (!o.IsDeleted || showDeleted));
            }
            else if (!showAllEditions && editionId.HasValue)
            {
                query = query.Where(o => o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                           && (!ao.IsDeleted || showDeleted)
                                                                           && (showAllOrganizations || ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                                               && (!aot.IsDeleted || showDeleted)))));
            }
            else
            {
                query = query.Where(o => o.AttendeeOrganizations.Any(ao => (!ao.IsDeleted || showDeleted)
                                                                           && (showAllOrganizations || ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                                               && (!aot.IsDeleted || showDeleted)))));
            }

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="isSimpleSearch">The is simple search.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByKeywords(this IQueryable<Organization> query, string keywords, bool? isSimpleSearch = false)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Organization>(false);
                var innerOrganizationTradeNameWhere = PredicateBuilder.New<Organization>(true);
                var innerOrganizationNameWhere = PredicateBuilder.New<Organization>(true);
                var innerOrganizationCompanyNameWhere = PredicateBuilder.New<Organization>(true);
                var innerHoldingNameWhere = PredicateBuilder.New<Organization>(true);
                var innerDocumentWhere = PredicateBuilder.New<Organization>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerOrganizationTradeNameWhere = innerOrganizationTradeNameWhere.And(o => o.TradeName.Contains(keyword));

                        if (isSimpleSearch == false)
                        {
                            innerOrganizationNameWhere = innerOrganizationNameWhere.And(o => o.Name.Contains(keyword));
                            innerOrganizationCompanyNameWhere = innerOrganizationCompanyNameWhere.And(o => o.CompanyName.Contains(keyword));
                            innerHoldingNameWhere = innerHoldingNameWhere.And(o => o.Holding.Name.Contains(keyword));
                            innerDocumentWhere = innerDocumentWhere.And(o => o.Document.Contains(keyword));
                        }

                    }
                }

                outerWhere = outerWhere.Or(innerOrganizationTradeNameWhere);

                if (isSimpleSearch == false)
                {
                    outerWhere = outerWhere.Or(innerOrganizationNameWhere);
                    outerWhere = outerWhere.Or(innerOrganizationCompanyNameWhere);
                    outerWhere = outerWhere.Or(innerHoldingNameWhere);
                    outerWhere = outerWhere.Or(innerDocumentWhere);
                }

                query = query.Where(outerWhere);
                //query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        /// <summary>
        /// Finds the by create or update date.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="modifiedAfterDate">The modified after date.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByCreateOrUpdateDate(this IQueryable<Organization> query, DateTime? modifiedAfterDate)
        {
            if (modifiedAfterDate.HasValue)
            {
                query = query.Where(o => o.CreateDate > modifiedAfterDate || o.UpdateDate > modifiedAfterDate);
            }

            return query;
        }

        /// <summary>Finds the by custom filter.</summary>
        /// <param name="query">The query.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByCustomFilter(this IQueryable<Organization> query, string customFilter, int editionId, Guid? organizationTypeUid)
        {
            if (!string.IsNullOrEmpty(customFilter))
            {
                if (customFilter == "HasProjectNegotiationScheduled")
                {
                    if (organizationTypeUid == OrganizationType.AudiovisualPlayer.Uid)
                    {
                        query = query.Where(o => o.AttendeeOrganizations
                                                        .Any(ao => ao.EditionId == editionId
                                                                   && !ao.IsDeleted
                                                                   && ao.ProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                                                            && !pbe.Project.IsDeleted
                                                                                                            && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid
                                                                                                            && pbe.Negotiations.Any(n => !n.IsDeleted))));
                    }
                    else if (organizationTypeUid == OrganizationType.Producer.Uid)
                    {
                        query = query.Where(o => o.AttendeeOrganizations
                                                        .Any(ao => ao.EditionId == editionId
                                                                   && !ao.IsDeleted
                                                                   && ao.SellProjects
                                                                               .Any(sp => !sp.IsDeleted
                                                                                          && sp.ProjectBuyerEvaluations
                                                                                              .Any(pbe => !pbe.IsDeleted
                                                                                                          && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid
                                                                                                          && pbe.Negotiations.Any(n => !n.IsDeleted)))));
                    }
                }
                else if (customFilter == "HasProjectNegotiationNotScheduled")
                {
                    if (organizationTypeUid == OrganizationType.AudiovisualPlayer.Uid)
                    {
                        query = query.Where(o => o.AttendeeOrganizations
                                                        .Any(ao => ao.EditionId == editionId
                                                                   && !ao.IsDeleted
                                                                   && ao.ProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                                                            && !pbe.Project.IsDeleted
                                                                                                            && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid
                                                                                                            && (!pbe.Negotiations.Any() || pbe.Negotiations.All(n => n.IsDeleted)))));
                    }
                    else if (organizationTypeUid == OrganizationType.Producer.Uid)
                    {
                        query = query.Where(o => o.AttendeeOrganizations
                                                        .Any(ao => ao.EditionId == editionId
                                                                   && !ao.IsDeleted
                                                                   && ao.SellProjects
                                                                               .Any(sp => !sp.IsDeleted
                                                                                          && sp.ProjectBuyerEvaluations
                                                                                              .Any(pbe => !pbe.IsDeleted
                                                                                                          && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid
                                                                                                          && (!pbe.Negotiations.Any() || pbe.Negotiations.All(n => n.IsDeleted))))));
                    }
                }
            }

            return query;
        }

        /// <summary>Finds the by filters uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="activitiesUids">The activities uids.</param>
        /// <param name="targetAudiencesUids">The target audiences uids.</param>
        /// <param name="interestsUids">The interests uids.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> FindByFiltersUids(this IQueryable<Organization> query, List<Guid> activitiesUids, List<Guid> targetAudiencesUids, List<Guid> interestsUids, bool showDeleted)
        {
            if (activitiesUids?.Any() == true || targetAudiencesUids?.Any() == true || interestsUids?.Any() == true)
            {
                var outerWhere = PredicateBuilder.New<Organization>(false);
                var innerActivitiesUidsWhere = PredicateBuilder.New<Organization>(true);
                var innerTargetAudiencesUidsWhere = PredicateBuilder.New<Organization>(true);
                var innerInterestsUidsWhere = PredicateBuilder.New<Organization>(true);

                if (activitiesUids?.Any() == true)
                {
                    innerActivitiesUidsWhere = innerActivitiesUidsWhere.Or(a => a.OrganizationActivities.Where(oa => (!oa.IsDeleted || showDeleted)).Any(oa => activitiesUids.Contains(oa.Activity.Uid)));
                }

                if (targetAudiencesUids?.Any() == true)
                {
                    innerTargetAudiencesUidsWhere = innerTargetAudiencesUidsWhere.Or(ta => ta.OrganizationTargetAudiences.Where(ota => (!ota.IsDeleted || showDeleted)).Any(oa => targetAudiencesUids.Contains(oa.TargetAudience.Uid)));
                }

                if (interestsUids?.Any() == true)
                {
                    innerInterestsUidsWhere = innerInterestsUidsWhere.Or(i => i.OrganizationInterests.Where(oi => (!oi.IsDeleted || showDeleted)).Any(oa => interestsUids.Contains(oa.Interest.Uid)));
                }

                outerWhere = outerWhere.And(innerActivitiesUidsWhere);
                outerWhere = outerWhere.And(innerTargetAudiencesUidsWhere);
                outerWhere = outerWhere.And(innerInterestsUidsWhere);
                query = query.Where(outerWhere);
                //query = query.AsExpandable().Where(predicate);
            }

            return query;
        }

        /// <summary>Determines whether [is API display enabled] [the specified edition identifier].</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> IsApiDisplayEnabled(this IQueryable<Organization> query, int editionId, Guid organizationTypeUid, bool showDeleted = false)
        {
            query = query.Where(o => o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                       && ao.AttendeeOrganizationTypes.Any(aot => (!aot.IsDeleted || showDeleted)
                                                                                                                  && aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                  && aot.IsApiDisplayEnabled)));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> IsNotDeleted(this IQueryable<Organization> query)
        {
            query = query.Where(o => !o.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by attendee organization collaborators.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorId">The user identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Organization> HasAttendeeOrganizationCollaborators(this IQueryable<Organization> query, int? collaboratorId)
        {
            if (collaboratorId.HasValue)
            {
                query = query.Where(o => o.AttendeeOrganizations.Any(ao => !ao.IsDeleted
                                                                           && !ao.Edition.IsDeleted
                                                                           && ao.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.CollaboratorId == collaboratorId)));
            }

            return query;
        }

    }

    #endregion

    #region OrganizationBaseDto IQueryable Extensions

    /// <summary>
    /// OrganizationBaseDtoIQueryableExtensions
    /// </summary>
    internal static class OrganizationBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<OrganizationBaseDto>> ToListPagedAsync(this IQueryable<OrganizationBaseDto> query, int page, int pageSize)
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

    #region OrganizationDto IQueryable Extensions

    /// <summary>
    /// OrganizationDtoIQueryableExtensions
    /// </summary>
    internal static class OrganizationDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<OrganizationDto>> ToListPagedAsync(this IQueryable<OrganizationDto> query, int page, int pageSize)
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

    #region OrganizationApiListDto IQueryable Extensions

    /// <summary>
    /// OrganizationApiListDtoIQueryableExtensions
    /// </summary>
    internal static class OrganizationApiListDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<OrganizationApiListDto>> ToListPagedAsync(this IQueryable<OrganizationApiListDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region AudiovisualPlayerOrganizationApiDto IQueryable Extensions

    internal static class AudiovisualPlayerOrganizationApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<AudiovisualPlayerOrganizationApiDto>> ToListPagedAsync(this IQueryable<AudiovisualPlayerOrganizationApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region MusicPlayerOrganizationApiDto IQueryable Extensions

    internal static class MusicPlayerOrganizationApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<MusicPlayerOrganizationApiDto>> ToListPagedAsync(this IQueryable<MusicPlayerOrganizationApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    #region InnovationPlayerOrganizationApiDto IQueryable Extensions

    internal static class InnovationPlayerOrganizationApiDtoIQueryableExtensions
    {
        /// <summary>
        /// Converts to listpagedasync.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<InnovationPlayerOrganizationApiDto>> ToListPagedAsync(this IQueryable<InnovationPlayerOrganizationApiDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }
    }

    #endregion

    /// <summary>OrganizationRepository</summary>
    public class OrganizationRepository : Repository<PlataformaRio2CContext, Organization>, IOrganizationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="OrganizationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public OrganizationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Organization> GetBaseQuery(bool @readonly = false, bool showDeleted = false)
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

        /// <summary>Finds the dto by uid asynchronous.</summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<OrganizationDto> FindDtoByUidAsync(Guid organizationUid, int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByUid(organizationUid);

            return await query
                            .Select(o => new OrganizationDto
                            {
                                Id = o.Id,
                                Uid = o.Uid,
                                Name = o.Name,
                                CompanyName = o.CompanyName,
                                TradeName = o.TradeName,
                                IsCompanyNumberRequired = o.Address != null && !o.Address.IsDeleted && o.Address.Country != null && o.Address.Country.IsCompanyNumberRequired,
                                Document = o.Document,
                                Website = o.Website,
                                Linkedin = o.Linkedin,
                                Twitter = o.Twitter,
                                Instagram = o.Instagram,
                                Youtube = o.Youtube,
                                PhoneNumber = o.PhoneNumber,
                                ImageUploadDate = o.ImageUploadDate,
                                CreateDate = o.CreateDate,
                                CreateUserId = o.CreateUserId,
                                UpdateDate = o.UpdateDate,
                                UpdateUserId = o.UpdateUserId,
                                //Creator = h.Creator,
                                IsVirtualMeeting = o.IsVirtualMeeting,
                                HoldingBaseDto = o.Holding == null ? null : new HoldingBaseDto
                                {
                                    Id = o.Holding.Id,
                                    Uid = o.Holding.Uid,
                                    Name = o.Holding.Name
                                },
                                UpdaterBaseDto = new UserBaseDto
                                {
                                    Id = o.Updater.Id,
                                    Uid = o.Updater.Uid,
                                    Name = o.Updater.Name,
                                    Email = o.Updater.Email
                                },
                                AddressBaseDto = o.Address.IsDeleted ? null : new AddressBaseDto
                                {
                                    Id = o.Address.Id,
                                    Uid = o.Address.Uid,
                                    CountryUid = o.Address.Country.Uid,
                                    StateUid = o.Address.State.Uid,
                                    CityUid = o.Address.City.Uid,
                                    Address1 = o.Address.Address1,
                                    AddressZipCode = o.Address.ZipCode,
                                },
                                OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                OrganizationRestrictionSpecificBaseDtos = o.OrganizationRestrictionSpecifics.Select(d => new OrganizationRestrictionSpecificDto
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
                                OrganizationActivitiesDtos = o.OrganizationActivities.Where(oa => !oa.IsDeleted).Select(oa => new OrganizationActivityDto
                                {
                                    OrganizationActivityId = oa.Id,
                                    OrganizationActivityUid = oa.Uid,
                                    OrganizationActivityAdditionalInfo = oa.AdditionalInfo,
                                    ActivityId = oa.Activity.Id,
                                    ActivityUid = oa.Activity.Uid,
                                    ActivityName = oa.Activity.Name,
                                    ActivityHasAdditionalInfo = oa.Activity.HasAdditionalInfo
                                }),
                                OrganizationTargetAudiencesDtos = o.OrganizationTargetAudiences.Where(ota => !ota.IsDeleted).Select(oa => new OrganizationTargetAudienceDto
                                {
                                    OrganizationTargetAudienceId = oa.Id,
                                    OrganizationTargetAudienceUid = oa.Uid,
                                    TargetAudienceId = oa.TargetAudience.Id,
                                    TargetAudienceUid = oa.TargetAudience.Uid,
                                    TargetAudienceName = oa.TargetAudience.Name
                                }),
                                OrganizationInterestDtos = o.OrganizationInterests.Where(ota => !ota.IsDeleted).Select(oi => new OrganizationInterestDto
                                {
                                    OrganizationInterest = oi,
                                    Interest = oi.Interest,
                                    InterestGroup = oi.Interest.InterestGroup
                                }),
                                AttendeeOrganizationTypesDtos = o.AttendeeOrganizations.Where(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                                    .SelectMany(ao => ao.AttendeeOrganizationTypes
                                                                                            .Where(aot => !aot.IsDeleted)
                                                                                            .Select(aot => new AttendeeOrganizationTypeDto
                                                                                            {
                                                                                                AttendeeOrganizationType = aot,
                                                                                                OrganizationType = aot.OrganizationType
                                                                                            }))
                            }).FirstOrDefaultAsync();
        }

        /// <summary>Finds all by data table.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationBaseDto>> FindAllByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            Guid organizationTypeUid,
            bool showAllEditions,
            bool showAllOrganizations,
            int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByOrganizationTypeUidAndByEditionId(organizationTypeUid, showAllEditions, showAllOrganizations, editionId);

            return await query
                            .DynamicOrder(
                                sortColumns,
                                new List<Tuple<string, string>>
                                {
                                    new Tuple<string, string>("HoldingBaseDto.Name", "Holding.Name")
                                },
                                new List<string> { "Name", "Holding.Name", "Document", "Website", "PhoneNumber", "CreateDate", "UpdateDate" },
                                "Name")
                            .Select(o => new OrganizationBaseDto
                            {
                                Id = o.Id,
                                Uid = o.Uid,
                                Name = o.Name,
                                HoldingBaseDto = o.Holding == null ? null : new HoldingBaseDto
                                {
                                    Id = o.Holding.Id,
                                    Uid = o.Holding.Uid,
                                    Name = o.Holding.Name
                                },
                                Document = o.Document,
                                Website = o.Website,
                                PhoneNumber = o.PhoneNumber,
                                ImageUploadDate = o.ImageUploadDate,
                                CreateDate = o.CreateDate,
                                UpdateDate = o.UpdateDate,
                                IsVirtualMeeting = o.IsVirtualMeeting,

                                IsInCurrentEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                                && !ao.Edition.IsDeleted
                                                                                                                && !ao.IsDeleted
                                                                                                                && ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                                                            && !aot.IsDeleted)),
                                IsInOtherEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId != editionId
                                                                                                            && !ao.IsDeleted),

                                IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                        && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                   && aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                                   && aot.IsApiDisplayEnabled)),
                            })
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Counts all by data table.</summary>
        /// <param name="organizationTypeId">The organization type identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(Guid organizationTypeId, bool showAllEditions, int? editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByOrganizationTypeUidAndByEditionId(organizationTypeId, showAllEditions, false, editionId);

            return await query.CountAsync();
        }

        /// <summary>
        /// Finds all dropdown API list dto paged.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationApiListDto>> FindAllDropdownApiListDtoPaged(
            int editionId,
            string keywords,
            string customFilter,
            Guid? organizationTypeUid,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByOrganizationTypeUidAndByEditionId(organizationTypeUid, false, organizationTypeUid.HasValue ? false : true, editionId)
                                .FindByKeywords(keywords, true)
                                .FindByCustomFilter(customFilter, editionId, organizationTypeUid);

            return await query
                            .Select(c => new OrganizationApiListDto
                            {
                                Uid = c.Uid,
                                Name = c.Name,
                                CompanyName = c.CompanyName,
                                TradeName = c.TradeName,
                                ImageUploadDate = c.ImageUploadDate,
                            })
                            .OrderBy(o => o.Name)
                            .ToListPagedAsync(page, pageSize);
        }

        #region Audiovisual Players

        /// <summary>
        /// Finds all audiovisual players by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="exportToExcel">if set to <c>true</c> [export to excel].</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationDto>> FindAllAudiovisualPlayersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllOrganizations,
            int? editionId,
            bool exportToExcel = false)
        {
            Guid playerOrganizationTypeUid = OrganizationType.AudiovisualPlayer.Uid;

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, showAllEditions, showAllOrganizations, editionId);

            IPagedList<OrganizationDto> organizationDtos;

            if (exportToExcel)
            {
                #region Report Select Query

                organizationDtos = await query
                                               .OrderBy(o => o.Name)
                                               .Select(o => new OrganizationDto
                                               {
                                                   Id = o.Id,
                                                   Uid = o.Uid,
                                                   Name = o.Name,
                                                   CompanyName = o.CompanyName,
                                                   TradeName = o.CompanyName,
                                                   Document = o.Document,
                                                   Website = o.Website,
                                                   Instagram = o.Instagram,
                                                   Youtube = o.Youtube,
                                                   Linkedin = o.Linkedin,
                                                   Twitter = o.Twitter,

                                                   OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                                   OrganizationInterestDtos = o.OrganizationInterests.Where(ota => !ota.IsDeleted).Select(oi => new OrganizationInterestDto
                                                   {
                                                       OrganizationInterest = oi,
                                                       Interest = oi.Interest,
                                                       InterestGroup = oi.Interest.InterestGroup
                                                   }),
                                                   OrganizationTargetAudiencesDtos = o.OrganizationTargetAudiences.Where(ota => !ota.IsDeleted).Select(oa => new OrganizationTargetAudienceDto
                                                   {
                                                       OrganizationTargetAudienceId = oa.Id,
                                                       OrganizationTargetAudienceUid = oa.Uid,
                                                       TargetAudienceId = oa.TargetAudience.Id,
                                                       TargetAudienceUid = oa.TargetAudience.Uid,
                                                       TargetAudienceName = oa.TargetAudience.Name
                                                   }),
                                                   ImageUploadDate = o.ImageUploadDate,
                                                   IsVirtualMeeting = o.IsVirtualMeeting,
                                                   IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                          && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                     && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                     && aot.IsApiDisplayEnabled)),
                                                   ReceivedProjectsCount = o.AttendeeOrganizations
                                                                                .Where(ao => ao.EditionId == editionId)
                                                                                .Sum(ao => ao.ProjectBuyerEvaluations.Count(pbe => !pbe.Project.IsDeleted &&
                                                                                                                                   pbe.Project.FinishDate != null &&
                                                                                                                                   !pbe.IsDeleted))
                                               })
                                               .ToListPagedAsync(page, pageSize);

                #endregion
            }
            else
            {
                #region DataTable Select Query

                organizationDtos = await query
                                                .DynamicOrder(
                                                    sortColumns,
                                                    new List<Tuple<string, string>>
                                                    {
                                                        new Tuple<string, string>("HoldingBaseDto.Name", "Holding.Name")
                                                    },
                                                    new List<string> { "Name", "Holding.Name", "Document", "Website", "PhoneNumber", "CreateDate", "UpdateDate" },
                                                    "Name")
                                                .Select(o => new OrganizationDto
                                                {
                                                    Id = o.Id,
                                                    Uid = o.Uid,
                                                    Name = o.Name,
                                                    HoldingBaseDto = o.Holding == null ? null : new HoldingBaseDto
                                                    {
                                                        Id = o.Holding.Id,
                                                        Uid = o.Holding.Uid,
                                                        Name = o.Holding.Name
                                                    },
                                                    Document = o.Document,
                                                    Website = o.Website,
                                                    PhoneNumber = o.PhoneNumber,
                                                    ImageUploadDate = o.ImageUploadDate,
                                                    CreateDate = o.CreateDate,
                                                    UpdateDate = o.UpdateDate,
                                                    IsVirtualMeeting = o.IsVirtualMeeting,

                                                    IsInCurrentEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                                                    && !ao.Edition.IsDeleted
                                                                                                                                    && !ao.IsDeleted
                                                                                                                                    && ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                                                && !aot.IsDeleted)),
                                                    IsInOtherEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId != editionId
                                                                                                                                && !ao.IsDeleted),

                                                    IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                            && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                       && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                       && aot.IsApiDisplayEnabled)),
                                                })
                                                .ToListPagedAsync(page, pageSize);

                #endregion
            }

            return organizationDtos;
        }

        /// <summary>
        /// Finds all audiovisual players public API paged.
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
        public async Task<IPagedList<AudiovisualPlayerOrganizationApiDto>> FindAllAudiovisualPlayersPublicApiPaged(
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
            Guid playerOrganizationTypeUid = OrganizationType.AudiovisualPlayer.Uid;

            var query = this.GetBaseQuery(showDeleted: showDeleted)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId, showDeleted)
                                .IsApiDisplayEnabled(editionId, playerOrganizationTypeUid, showDeleted)
                                .FindByFiltersUids(activitiesUids, targetAudiencesUids, interestsUids, showDeleted)
                                .FindByKeywords(keywords)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<AudiovisualPlayerOrganizationApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(o => new AudiovisualPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                            .ApiHighlightPosition,
                    OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                    OrganizationInterestDtos = o.OrganizationInterests
                                                    .Where(ota => (!ota.IsDeleted || showDeleted))
                                                    .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                    .ThenBy(oi => oi.Interest.DisplayOrder)
                                                    .Select(oi => new OrganizationInterestDto
                                                    {
                                                        OrganizationInterest = oi,
                                                        Interest = oi.Interest,
                                                        InterestGroup = oi.Interest.InterestGroup
                                                    }),
                    CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => (!aoc.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.Collaborator.IsDeleted || showDeleted))
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(o => new AudiovisualPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes.FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                        .ApiHighlightPosition,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the audiovisual player public API dto by uid.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<AudiovisualPlayerOrganizationApiDto> FindAudiovisualPlayerPublicApiDtoByUid(Guid organizationUid, int editionId)
        {
            Guid playerOrganizationTypeUid = OrganizationType.AudiovisualPlayer.Uid;

            var query = this.GetBaseQuery()
                                    .FindByUid(organizationUid)
                                    .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId)
                                    .IsApiDisplayEnabled(editionId, playerOrganizationTypeUid);

            return await query
                            .Select(o => new AudiovisualPlayerOrganizationApiDto
                            {
                                Uid = o.Uid,
                                Name = o.Name,
                                CompanyName = o.CompanyName,
                                TradeName = o.TradeName,
                                ImageUploadDate = o.ImageUploadDate,
                                OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                OrganizationInterestDtos = o.OrganizationInterests
                                                                .Where(ota => !ota.IsDeleted)
                                                                .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                                .ThenBy(oi => oi.Interest.DisplayOrder)
                                                                .Select(oi => new OrganizationInterestDto
                                                                {
                                                                    OrganizationInterest = oi,
                                                                    Interest = oi.Interest,
                                                                    InterestGroup = oi.Interest.InterestGroup
                                                                }),
                                CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeCollaborator.IsDeleted && !aoc.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                            }).FirstOrDefaultAsync();
        }

        #endregion

        #region Music Players

        /// <summary>
        /// Finds all music players by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="exportToExcel">if set to <c>true</c> [export to excel].</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationDto>> FindAllMusicPlayersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllOrganizations,
            int? editionId,
            bool exportToExcel = false)
        {
            Guid playerOrganizationTypeUid = OrganizationType.MusicPlayer.Uid;

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, showAllEditions, showAllOrganizations, editionId);

            IPagedList<OrganizationDto> organizationDtos;

            if (exportToExcel)
            {
                #region Report Select Query

                organizationDtos = await query
                                               .OrderBy(o => o.Name)
                                               .Select(o => new OrganizationDto
                                               {
                                                   Id = o.Id,
                                                   Uid = o.Uid,
                                                   Name = o.Name,
                                                   CompanyName = o.CompanyName,
                                                   TradeName = o.CompanyName,
                                                   Document = o.Document,
                                                   Website = o.Website,
                                                   Instagram = o.Instagram,
                                                   Youtube = o.Youtube,
                                                   Linkedin = o.Linkedin,
                                                   Twitter = o.Twitter,

                                                   OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                                   OrganizationInterestDtos = o.OrganizationInterests.Where(ota => !ota.IsDeleted).Select(oi => new OrganizationInterestDto
                                                   {
                                                       OrganizationInterest = oi,
                                                       Interest = oi.Interest,
                                                       InterestGroup = oi.Interest.InterestGroup
                                                   }),
                                                   OrganizationTargetAudiencesDtos = o.OrganizationTargetAudiences.Where(ota => !ota.IsDeleted).Select(oa => new OrganizationTargetAudienceDto
                                                   {
                                                       OrganizationTargetAudienceId = oa.Id,
                                                       OrganizationTargetAudienceUid = oa.Uid,
                                                       TargetAudienceId = oa.TargetAudience.Id,
                                                       TargetAudienceUid = oa.TargetAudience.Uid,
                                                       TargetAudienceName = oa.TargetAudience.Name
                                                   }),
                                                   ImageUploadDate = o.ImageUploadDate,
                                                   IsVirtualMeeting = o.IsVirtualMeeting,
                                                   IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                          && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                     && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                     && aot.IsApiDisplayEnabled)),
                                                   ReceivedProjectsCount = o.AttendeeOrganizations
                                                                                .Where(ao => ao.EditionId == editionId)
                                                                                .Sum(ao => ao.ProjectBuyerEvaluations.Count(pbe => !pbe.Project.IsDeleted &&
                                                                                                                                   pbe.Project.FinishDate != null &&
                                                                                                                                   !pbe.IsDeleted))
                                               })
                                               .ToListPagedAsync(page, pageSize);

                #endregion
            }
            else
            {
                #region DataTable Select Query

                organizationDtos = await query
                                                .DynamicOrder(
                                                    sortColumns,
                                                    new List<Tuple<string, string>>
                                                    {
                                                        new Tuple<string, string>("HoldingBaseDto.Name", "Holding.Name")
                                                    },
                                                    new List<string> { "Name", "Holding.Name", "Document", "Website", "PhoneNumber", "CreateDate", "UpdateDate" },
                                                    "Name")
                                                .Select(o => new OrganizationDto
                                                {
                                                    Id = o.Id,
                                                    Uid = o.Uid,
                                                    Name = o.Name,
                                                    HoldingBaseDto = o.Holding == null ? null : new HoldingBaseDto
                                                    {
                                                        Id = o.Holding.Id,
                                                        Uid = o.Holding.Uid,
                                                        Name = o.Holding.Name
                                                    },
                                                    Document = o.Document,
                                                    Website = o.Website,
                                                    PhoneNumber = o.PhoneNumber,
                                                    ImageUploadDate = o.ImageUploadDate,
                                                    CreateDate = o.CreateDate,
                                                    UpdateDate = o.UpdateDate,
                                                    IsVirtualMeeting = o.IsVirtualMeeting,

                                                    IsInCurrentEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                                                    && !ao.Edition.IsDeleted
                                                                                                                                    && !ao.IsDeleted
                                                                                                                                    && ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                                                && !aot.IsDeleted)),
                                                    IsInOtherEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId != editionId
                                                                                                                                && !ao.IsDeleted),

                                                    IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                            && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                       && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                       && aot.IsApiDisplayEnabled)),
                                                })
                                                .ToListPagedAsync(page, pageSize);

                #endregion
            }

            return organizationDtos;
        }

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
        public async Task<IPagedList<MusicPlayerOrganizationApiDto>> FindAllMusicPlayersPublicApiPaged(
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
            Guid playerOrganizationTypeUid = OrganizationType.MusicPlayer.Uid;

            var query = this.GetBaseQuery(showDeleted: showDeleted)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId, showDeleted)
                                //.IsApiDisplayEnabled(editionId, playerOrganizationTypeUid, showDeleted) //TODO: Enable this filter after implementing: Admin area > Player details view > API Configuration widget
                                .FindByFiltersUids(activitiesUids, targetAudiencesUids, interestsUids, showDeleted)
                                .FindByKeywords(keywords)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<MusicPlayerOrganizationApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(o => new MusicPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                            .ApiHighlightPosition,
                    OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                    OrganizationInterestDtos = o.OrganizationInterests
                                                    .Where(ota => (!ota.IsDeleted || showDeleted))
                                                    .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                    .ThenBy(oi => oi.Interest.DisplayOrder)
                                                    .Select(oi => new OrganizationInterestDto
                                                    {
                                                        OrganizationInterest = oi,
                                                        Interest = oi.Interest,
                                                        InterestGroup = oi.Interest.InterestGroup
                                                    }),
                    CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => (!aoc.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.Collaborator.IsDeleted || showDeleted))
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(o => new MusicPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes.FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                        .ApiHighlightPosition,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the music player public API dto by uid.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<MusicPlayerOrganizationApiDto> FindMusicPlayerPublicApiDtoByUid(Guid organizationUid, int editionId)
        {
            Guid playerOrganizationTypeUid = OrganizationType.MusicPlayer.Uid;

            var query = this.GetBaseQuery()
                                    .FindByUid(organizationUid)
                                    .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId);
                                    //.IsApiDisplayEnabled(editionId, playerOrganizationTypeUid); //TODO: Enable this filter after implementing: Admin area > Player details view > API Configuration widget

            return await query
                            .Select(o => new MusicPlayerOrganizationApiDto
                            {
                                Uid = o.Uid,
                                Name = o.Name,
                                CompanyName = o.CompanyName,
                                TradeName = o.TradeName,
                                ImageUploadDate = o.ImageUploadDate,
                                OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                OrganizationInterestDtos = o.OrganizationInterests
                                                                .Where(ota => !ota.IsDeleted)
                                                                .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                                .ThenBy(oi => oi.Interest.DisplayOrder)
                                                                .Select(oi => new OrganizationInterestDto
                                                                {
                                                                    OrganizationInterest = oi,
                                                                    Interest = oi.Interest,
                                                                    InterestGroup = oi.Interest.InterestGroup
                                                                }),
                                CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeCollaborator.IsDeleted && !aoc.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                            }).FirstOrDefaultAsync();
        }

        #endregion

        #region Innovation Players

        /// <summary>
        /// Finds all innovation players by data table.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <param name="showAllOrganizations">if set to <c>true</c> [show all organizations].</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="exportToExcel">if set to <c>true</c> [export to excel].</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationDto>> FindAllInnovationPlayersByDataTable(
            int page,
            int pageSize,
            string keywords,
            List<Tuple<string, string>> sortColumns,
            bool showAllEditions,
            bool showAllOrganizations,
            int? editionId,
            bool exportToExcel = false)
        {
            Guid playerOrganizationTypeUid = OrganizationType.StartupPlayer.Uid;

            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, showAllEditions, showAllOrganizations, editionId);

            IPagedList<OrganizationDto> organizationDtos;

            if (exportToExcel)
            {
                #region Report Select Query

                organizationDtos = await query
                                               .OrderBy(o => o.Name)
                                               .Select(o => new OrganizationDto
                                               {
                                                   Id = o.Id,
                                                   Uid = o.Uid,
                                                   Name = o.Name,
                                                   CompanyName = o.CompanyName,
                                                   TradeName = o.CompanyName,
                                                   Document = o.Document,
                                                   Website = o.Website,
                                                   Instagram = o.Instagram,
                                                   Youtube = o.Youtube,
                                                   Linkedin = o.Linkedin,
                                                   Twitter = o.Twitter,
                                                   OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                                   OrganizationInterestDtos = o.OrganizationInterests.Where(ota => !ota.IsDeleted).Select(oi => new OrganizationInterestDto
                                                   {
                                                       OrganizationInterest = oi,
                                                       Interest = oi.Interest,
                                                       InterestGroup = oi.Interest.InterestGroup
                                                   }),
                                                   OrganizationTargetAudiencesDtos = o.OrganizationTargetAudiences.Where(ota => !ota.IsDeleted).Select(oa => new OrganizationTargetAudienceDto
                                                   {
                                                       OrganizationTargetAudienceId = oa.Id,
                                                       OrganizationTargetAudienceUid = oa.Uid,
                                                       TargetAudienceId = oa.TargetAudience.Id,
                                                       TargetAudienceUid = oa.TargetAudience.Uid,
                                                       TargetAudienceName = oa.TargetAudience.Name
                                                   }),
                                                   ImageUploadDate = o.ImageUploadDate,
                                                   IsVirtualMeeting = o.IsVirtualMeeting,
                                                   IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                          && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                     && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                     && aot.IsApiDisplayEnabled)),
                                                   ReceivedProjectsCount = o.AttendeeOrganizations
                                                                                .Where(ao => ao.EditionId == editionId)
                                                                                .Sum(ao => ao.ProjectBuyerEvaluations.Count(pbe => !pbe.Project.IsDeleted &&
                                                                                                                                   pbe.Project.FinishDate != null &&
                                                                                                                                   !pbe.IsDeleted))
                                               })
                                               .ToListPagedAsync(page, pageSize);

                #endregion
            }
            else
            {
                #region DataTable Select Query

                organizationDtos = await query
                                                .DynamicOrder(
                                                    sortColumns,
                                                    new List<Tuple<string, string>>
                                                    {
                                                        new Tuple<string, string>("HoldingBaseDto.Name", "Holding.Name")
                                                    },
                                                    new List<string> { "Name", "Holding.Name", "Document", "Website", "PhoneNumber", "CreateDate", "UpdateDate" },
                                                    "Name")
                                                .Select(o => new OrganizationDto
                                                {
                                                    Id = o.Id,
                                                    Uid = o.Uid,
                                                    Name = o.Name,
                                                    HoldingBaseDto = o.Holding == null ? null : new HoldingBaseDto
                                                    {
                                                        Id = o.Holding.Id,
                                                        Uid = o.Holding.Uid,
                                                        Name = o.Holding.Name
                                                    },
                                                    Document = o.Document,
                                                    Website = o.Website,
                                                    PhoneNumber = o.PhoneNumber,
                                                    ImageUploadDate = o.ImageUploadDate,
                                                    CreateDate = o.CreateDate,
                                                    UpdateDate = o.UpdateDate,
                                                    IsVirtualMeeting = o.IsVirtualMeeting,

                                                    IsInCurrentEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                                                    && !ao.Edition.IsDeleted
                                                                                                                                    && !ao.IsDeleted
                                                                                                                                    && ao.AttendeeOrganizationTypes.Any(aot => aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                                                && !aot.IsDeleted)),
                                                    IsInOtherEdition = editionId.HasValue && o.AttendeeOrganizations.Any(ao => ao.EditionId != editionId
                                                                                                                                && !ao.IsDeleted),

                                                    IsApiDisplayEnabled = o.AttendeeOrganizations.Any(ao => ao.EditionId == editionId
                                                                                                            && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                                                       && aot.OrganizationType.Uid == playerOrganizationTypeUid
                                                                                                                                                       && aot.IsApiDisplayEnabled)),
                                                })
                                                .ToListPagedAsync(page, pageSize);

                #endregion
            }

            return organizationDtos;
        }

        /// <summary>
        /// Finds all innovation players public API paged.
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
        public async Task<IPagedList<InnovationPlayerOrganizationApiDto>> FindAllInnovationPlayersPublicApiPaged(
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
            Guid playerOrganizationTypeUid = OrganizationType.StartupPlayer.Uid;

            var query = this.GetBaseQuery(showDeleted: showDeleted)
                                .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId, showDeleted)
                                //.IsApiDisplayEnabled(editionId, playerOrganizationTypeUid, showDeleted) //TODO: Enable this filter after implementing: Admin area > Player details view > API Configuration widget
                                .FindByFiltersUids(activitiesUids, targetAudiencesUids, interestsUids, showDeleted)
                                .FindByKeywords(keywords)
                                .FindByCreateOrUpdateDate(modifiedAfterDate);

            IQueryable<InnovationPlayerOrganizationApiDto> filteredQuery;
            if (showDetails)
            {
                #region Detailed Query

                filteredQuery = query.Select(o => new InnovationPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes
                                                        .FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                            .ApiHighlightPosition,
                    OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                    OrganizationInterestDtos = o.OrganizationInterests
                                                    .Where(ota => (!ota.IsDeleted || showDeleted))
                                                    .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                    .ThenBy(oi => oi.Interest.DisplayOrder)
                                                    .Select(oi => new OrganizationInterestDto
                                                    {
                                                        OrganizationInterest = oi,
                                                        Interest = oi.Interest,
                                                        InterestGroup = oi.Interest.InterestGroup
                                                    }),
                    CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => (!aoc.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.IsDeleted || showDeleted) && (!aoc.AttendeeCollaborator.Collaborator.IsDeleted || showDeleted))
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                });

                #endregion
            }
            else
            {
                #region Simple Query

                filteredQuery = query.Select(o => new InnovationPlayerOrganizationApiDto
                {
                    Uid = o.Uid,
                    Name = o.Name,
                    CompanyName = o.CompanyName,
                    TradeName = o.TradeName,
                    ImageUploadDate = o.ImageUploadDate,
                    IsDeleted = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId).IsDeleted,
                    ApiHighlightPosition = o.AttendeeOrganizations
                                                .FirstOrDefault(ao => (!ao.IsDeleted || showDeleted) && ao.EditionId == editionId)
                                                    .AttendeeOrganizationTypes.FirstOrDefault(aot => (!aot.IsDeleted || showDeleted) && aot.OrganizationType.Uid == playerOrganizationTypeUid)
                                                        .ApiHighlightPosition,
                    CreateDate = o.CreateDate,
                    UpdateDate = o.UpdateDate
                });

                #endregion
            }

            return await filteredQuery
                            .OrderBy(o => o.ApiHighlightPosition ?? 99)
                            .ThenBy(o => o.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds the innovation player public API dto by uid.
        /// </summary>
        /// <param name="organizationUid">The organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<InnovationPlayerOrganizationApiDto> FindInnovationPlayerPublicApiDtoByUid(Guid organizationUid, int editionId)
        {
            Guid playerOrganizationTypeUid = OrganizationType.StartupPlayer.Uid;

            var query = this.GetBaseQuery()
                                    .FindByUid(organizationUid)
                                    .FindByOrganizationTypeUidAndByEditionId(playerOrganizationTypeUid, false, false, editionId);
                                    //.IsApiDisplayEnabled(editionId, playerOrganizationTypeUid); //TODO: Enable this filter after implementing: Admin area > Player details view > API Configuration widget

            return await query
                            .Select(o => new InnovationPlayerOrganizationApiDto
                            {
                                Uid = o.Uid,
                                Name = o.Name,
                                CompanyName = o.CompanyName,
                                TradeName = o.TradeName,
                                ImageUploadDate = o.ImageUploadDate,
                                OrganizationDescriptionBaseDtos = o.OrganizationDescriptions.Select(d => new OrganizationDescriptionDto
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
                                OrganizationInterestDtos = o.OrganizationInterests
                                                                .Where(ota => !ota.IsDeleted)
                                                                .OrderBy(oi => oi.Interest.InterestGroup.DisplayOrder)
                                                                .ThenBy(oi => oi.Interest.DisplayOrder)
                                                                .Select(oi => new OrganizationInterestDto
                                                                {
                                                                    OrganizationInterest = oi,
                                                                    Interest = oi.Interest,
                                                                    InterestGroup = oi.Interest.InterestGroup
                                                                }),
                                CollaboratorsDtos = o.AttendeeOrganizations
                                                            .Where(ao => !ao.IsDeleted && ao.EditionId == editionId)
                                                            .SelectMany(ao => ao.AttendeeOrganizationCollaborators
                                                                                    .Where(aoc => !aoc.IsDeleted && !aoc.AttendeeCollaborator.IsDeleted && !aoc.AttendeeCollaborator.Collaborator.IsDeleted)
                                                                                    .Select(aoc => new CollaboratorDto
                                                                                    {
                                                                                        Uid = aoc.AttendeeCollaborator.Collaborator.Uid,
                                                                                        FirstName = aoc.AttendeeCollaborator.Collaborator.FirstName,
                                                                                        LastNames = aoc.AttendeeCollaborator.Collaborator.LastNames,
                                                                                        Badge = aoc.AttendeeCollaborator.Collaborator.Badge,
                                                                                        ImageUploadDate = aoc.AttendeeCollaborator.Collaborator.ImageUploadDate,
                                                                                        JobTitleBaseDtos = aoc.AttendeeCollaborator.Collaborator.JobTitles.Select(jt => new CollaboratorJobTitleBaseDto
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
                                                                                        MiniBioBaseDtos = aoc.AttendeeCollaborator.Collaborator.MiniBios.Select(jt => new CollaboratorMiniBioBaseDto
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
                                                                                    }))
                            }).FirstOrDefaultAsync();
        }

        #endregion

        #region Api

        /// <summary>Finds all organizations API paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="companyName">Name of the company.</param>
        /// <param name="tradeName">Name of the trade.</param>
        /// <param name="document">The document.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<OrganizationApiListDto>> FindAllOrganizationsApiPaged(int? editionId, string companyName, string tradeName, string document, Guid organizationTypeUid, int? collaboratorId, bool showAllEditions, bool showAllOrganizations, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByOrganizationTypeUidAndByEditionId(organizationTypeUid, showAllEditions, showAllOrganizations, editionId)
                                .FindByCompanyName(companyName)
                                .FindByTradeName(tradeName)
                                .FindByEqualDocument(document);

            // If searching by document, must return the Organization data even if without past association between Collaborador and Organization.
            if (string.IsNullOrEmpty(document))
            {
                query = query.HasAttendeeOrganizationCollaborators(collaboratorId);
            }

            return await query
                            .Select(o => new OrganizationApiListDto
                            {
                                Uid = o.Uid,
                                Name = o.Name,
                                CompanyName = o.CompanyName,
                                TradeName = o.TradeName,
                                Document = o.Document,
                                ImageUploadDate = o.ImageUploadDate,
                                CreateDate = o.CreateDate,
                                UpdateDate = o.UpdateDate
                            })
                            .OrderBy(o => o.TradeName)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds all by hightlight position.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="organizationTypeUid">The organization type uid.</param>
        /// <param name="apiHighlightPosition">The API highlight position.</param>
        /// <param name="organizationUid">The organization uid.</param>
        /// <returns></returns>
        public async Task<List<Organization>> FindAllByHightlightPosition(int editionId, Guid organizationTypeUid, int apiHighlightPosition, Guid? organizationUid)
        {
            var query = this.GetBaseQuery()
                                .Where(o => o.Uid != organizationUid
                                            && o.AttendeeOrganizations.Any(ao => !ao.IsDeleted
                                                                                && ao.EditionId == editionId
                                                                                && ao.AttendeeOrganizationTypes.Any(aot => !aot.IsDeleted
                                                                                                                            && aot.OrganizationType.Uid == organizationTypeUid
                                                                                                                            && aot.ApiHighlightPosition == apiHighlightPosition)));

            return await query
                        .ToListAsync();
        }

        #endregion

        #region Old

        /// <summary>Método que traz todos os registros</summary>
        /// <param name="readonly"></param>
        /// <returns></returns>
        public override IQueryable<Organization> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                .IsNotDeleted();
            //.Include(i => i.Descriptions)
            //.Include(i => i.Descriptions.Select(t => t.Language));


            return @readonly
                ? consult.AsNoTracking()
                : consult;
        }

        /// <summary>Gets all asynchronous.</summary>
        /// <returns></returns>
        public async Task<List<Organization>> GetAllAsync()
        {
            var query = this.GetAll();

            return await query.ToListAsync();
        }

        public override IQueryable<Organization> GetAll(Expression<Func<Organization, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Organization Get(Expression<Func<Organization, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Organization Get(object id)
        {
            return this.dbSet
                            //.Include(i => i.Image)
                            .SingleOrDefault(x => x.Id == (int)id);
        }

        #endregion
    }
}