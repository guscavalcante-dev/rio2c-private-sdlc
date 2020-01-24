// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-24-2020
// ***********************************************************************
// <copyright file="ProjectRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;
using X.PagedList;
using LinqKit;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project IQueryable Extensions

    /// <summary>
    /// ProjectIQueryableExtensions
    /// </summary>
    internal static class ProjectIQueryableExtensions
    {
        /// <summary>Finds the by uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByUid(this IQueryable<Project> query, Guid projectUid)
        {
            query = query.Where(p => p.Uid == projectUid);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByEditionId(this IQueryable<Project> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(p => (showAllEditions || p.SellerAttendeeOrganization.EditionId == editionId));

            return query;
        }

        /// <summary>Finds the by seller attendee organization uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerAttendeeOrganizationUid">The seller attendee organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindBySellerAttendeeOrganizationUid(this IQueryable<Project> query, Guid sellerAttendeeOrganizationUid)
        {
            query = query.Where(p => p.SellerAttendeeOrganization.Uid == sellerAttendeeOrganizationUid);

            return query;
        }

        /// <summary>Finds the by seller attendee organizations uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sellerAttendeeOrganizationsUids">The seller attendee organizations uids.</param>
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindBySellerAttendeeOrganizationsUids(this IQueryable<Project> query, List<Guid> sellerAttendeeOrganizationsUids, bool showAll)
        {
            if (sellerAttendeeOrganizationsUids?.Any() == true)
            {
                query = query.Where(p => sellerAttendeeOrganizationsUids.Contains(p.SellerAttendeeOrganization.Uid));
            }
            else if (!showAll)
            {
                query = query.Where(p => 1 == 2);
            }

            return query;
        }

        /// <summary>Finds the by buyer attendee collabrator uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerAttendeeCollaboratorUid">The buyer attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByBuyerAttendeeCollabratorUid(this IQueryable<Project> query, Guid buyerAttendeeCollaboratorUid)
        {
            query = query.Where(p => p.ProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                          && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                                          && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Uid == buyerAttendeeCollaboratorUid
                                                                                                                                                        && !aoc.IsDeleted)));

            return query;
        }

        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByKeywords(this IQueryable<Project> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Project>(false);
                var innerProjectTitleNameWhere = PredicateBuilder.New<Project>(true);
                var innerProjectSummaryNameWhere = PredicateBuilder.New<Project>(true);
                var innerProjectInterestNameWhere = PredicateBuilder.New<Project>(true);
                var innerSellerAttendeeOrganizationNameWhere = PredicateBuilder.New<Project>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerProjectTitleNameWhere = innerProjectTitleNameWhere.Or(p => p.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                        innerProjectSummaryNameWhere = innerProjectSummaryNameWhere.Or(p => p.ProjectSummaries.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                        innerProjectInterestNameWhere = innerProjectInterestNameWhere.Or(p => p.ProjectInterests.Any(pi => !pi.IsDeleted && pi.Interest.Name.Contains(keyword)));
                        innerSellerAttendeeOrganizationNameWhere = innerSellerAttendeeOrganizationNameWhere.Or(sao => sao.SellerAttendeeOrganization.Organization.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerProjectTitleNameWhere);
                outerWhere = outerWhere.Or(innerProjectSummaryNameWhere);
                outerWhere = outerWhere.Or(innerProjectInterestNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeOrganizationNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }


        /// <summary>Finds the by keywords.</summary>
        /// <param name="query">The query.</param>
        /// <param name="keywords">The keywords.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindPitchingByKeywords(this IQueryable<Project> query, string keywords)
        {
            if (!string.IsNullOrEmpty(keywords))
            {
                var outerWhere = PredicateBuilder.New<Project>(false);
                var innerProjectTitleNameWhere = PredicateBuilder.New<Project>(true);
                var innerSellerAttendeeOrganizationNameWhere = PredicateBuilder.New<Project>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerProjectTitleNameWhere = innerProjectTitleNameWhere.Or(p => p.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                        innerSellerAttendeeOrganizationNameWhere = innerSellerAttendeeOrganizationNameWhere.Or(sao => sao.SellerAttendeeOrganization.Organization.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerProjectTitleNameWhere);
                //outerWhere = outerWhere.Or(innerSellerAttendeeOrganizationNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by interest uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByInterestUid(this IQueryable<Project> query, Guid? interestUid)
        {
            if (interestUid != null)
            {
                query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
                                                                      && !pi.Interest.IsDeleted
                                                                      && pi.Interest.Uid == interestUid));
            }

            return query;
        }


        /// <summary>Finds by interest uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUids">The interest uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByInterestUids(this IQueryable<Project> query, List<Guid> interestUids)
        {
            if (interestUids?.Any() == true)
            {
                query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
                                                                      && !pi.Interest.IsDeleted
                                                                      && interestUids.Contains(pi.Interest.Uid)));
            }

            return query;
        }


        /// <summary>Finds by target audience uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByTargetAudienceUid(this IQueryable<Project> query, Guid? targetAudienceUid)
        {
            if (targetAudienceUid != null)
            {
                query = query.Where(p => p.ProjectTargetAudiences.Any(pi => !pi.IsDeleted
                                                                      && !pi.TargetAudience.IsDeleted
                                                                      && pi.TargetAudience.Uid == targetAudienceUid));
            }

            return query;
        }


        /// <summary>Finds by target audience uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByTargetAudienceUids(this IQueryable<Project> query, List<Guid> targetAudienceUids)
        {
            if (targetAudienceUids?.Any() == true)
            {
                query = query.Where(p => p.ProjectTargetAudiences.Any(pi => !pi.IsDeleted
                                                                      && !pi.TargetAudience.IsDeleted
                                                                      && targetAudienceUids.Contains(pi.TargetAudience.Uid)));
            }

            return query;
        }

        /// <summary>Finds by the start and end date</summary>
        /// <param name="query"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByDate(this IQueryable<Project> query, DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null)
            {
                query = query.Where(p => p.CreateDate >= startDate || p.FinishDate >= startDate);
            }
            if (endDate != null)
            {
                var maxEndDate = new DateTime(endDate.Value.Year, endDate.Value.Month, endDate.Value.Day, 23, 59, 59);
                query = query.Where(p => p.CreateDate <= maxEndDate || p.FinishDate <= maxEndDate);
            }
            return query;
        }

        /// <summary>Finds the by project evaluation status.</summary>
        /// <param name="query">The query.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByProjectEvaluationStatus(this IQueryable<Project> query, Guid? evaluationStatusUid, Guid attendeeCollaboratorUid)
        {
            if (evaluationStatusUid != null)
            {
                query = query.Where(p => p.ProjectBuyerEvaluations.Any(pbe => !pbe.IsDeleted
                                                                              && !pbe.ProjectEvaluationStatus.IsDeleted
                                                                              && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted
                                                                                                                                                            && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid)
                                                                              && pbe.ProjectEvaluationStatus.Uid == evaluationStatusUid));
            }

            return query;
        }

        /// <summary>Determines whether this instance is finished.</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Project> IsFinished(this IQueryable<Project> query)
        {
            query = query.Where(p => p.FinishDate.HasValue);

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Project> IsNotDeleted(this IQueryable<Project> query)
        {
            query = query.Where(p => !p.IsDeleted);

            return query;
        }

        /// <summary>Determines whether is pitching</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Project> IsPitching(this IQueryable<Project> query, bool isPitching = true)
        {
            if (isPitching)
            {
                query = query.Where(p => p.IsPitching);
            }

            return query;
        }
    }

    #endregion

    #region ProjectDto IQueryable Extensions

    /// <summary>
    /// ProjectDtoIQueryableExtensions
    /// </summary>
    internal static class ProjectDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ProjectDto>> ToListPagedAsync(this IQueryable<ProjectDto> query, int page, int pageSize)
        {
            // Page the list
            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }

        /// <summary>Finds the by uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="collaboratorsUids">The collaborators uids.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByUids(this IQueryable<Project> query, List<Guid> selectedProjectsUids)
        {
            if (selectedProjectsUids?.Any() == true)
            {
                query = query.Where(c => selectedProjectsUids.Contains(c.Uid));
            }

            return query;
        }
    }

    #endregion

    #region ProjectBaseDto IQueryable Extensions
    internal static class ProjectBaseDtoIQueryableExtensions
    {
        /// <summary>
        /// To the list paged.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal static async Task<IPagedList<ProjectBaseDto>> ToListPagedAsync(this IQueryable<ProjectBaseDto> query, int page, int pageSize)
        {
            // Page the list
            page++;

            var pagedList = await query.ToPagedListAsync(page, pageSize);
            if (pagedList.PageNumber != 1 && pagedList.PageCount > 0 && page > pagedList.PageCount)
                pagedList = await query.ToPagedListAsync(pagedList.PageCount, pageSize);

            return pagedList;
        }

    }
    #endregion 

    /// <summary>ProjectRepository</summary>
    public class ProjectRepository : Repository<Context.PlataformaRio2CContext, Project>, IProjectRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<Project> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all dtos to sell asynchronous.</summary>
        /// <param name="attendeeOrganizationUid">The attendee organization uid.</param>
        /// <param name="showAll">if set to <c>true</c> [show all].</param>
        /// <returns></returns>
        public async Task<List<ProjectDto>> FindAllDtosToSellAsync(Guid attendeeOrganizationUid, bool showAll)
        {
            var query = this.GetBaseQuery()
                                .FindBySellerAttendeeOrganizationUid(attendeeOrganizationUid)
                                .Select(p => new ProjectDto
                                {
                                    Project = p,
                                    ProjectType = p.ProjectType,
                                    SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = p.SellerAttendeeOrganization,
                                        Organization = p.SellerAttendeeOrganization.Organization,
                                        Edition = p.SellerAttendeeOrganization.Edition
                                    },
                                    ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                    {
                                        ProjectTitle = t,
                                        Language = t.Language
                                    }),
                                    ProjectLogLineDtos = p.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                    {
                                        ProjectLogLine = ll,
                                        Language = ll.Language
                                    }),
                                    ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                    {
                                        ProjectInterest = i,
                                        Interest = i.Interest,
                                        InterestGroup = i.Interest.InterestGroup
                                    }),
                                    ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                    {
                                        ProjectBuyerEvaluation = be,
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = be.BuyerAttendeeOrganization,
                                            Organization = be.BuyerAttendeeOrganization.Organization,
                                            Edition = be.BuyerAttendeeOrganization.Edition
                                        },
                                        ProjectEvaluationStatus = be.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = be.ProjectEvaluationRefuseReason
                                    })
                                });

            return await query
                            .OrderBy(pd => pd.Project.CreateDate)
                            .ToListAsync();
        }

        /// <summary>Finds all dtos to evaluate asynchronous.</summary>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? interestUid, Guid? evaluationStatusUid, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByBuyerAttendeeCollabratorUid(attendeeCollaboratorUid)
                                .IsFinished()
                                .FindByKeywords(searchKeywords)
                                .FindByInterestUid(interestUid)
                                .FindByProjectEvaluationStatus(evaluationStatusUid, attendeeCollaboratorUid)
                                .Select(p => new ProjectDto
                                {
                                    Project = p,
                                    ProjectType = p.ProjectType,
                                    SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = p.SellerAttendeeOrganization,
                                        Organization = p.SellerAttendeeOrganization.Organization,
                                        Edition = p.SellerAttendeeOrganization.Edition
                                    },
                                    ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                    {
                                        ProjectTitle = t,
                                        Language = t.Language
                                    }),
                                    ProjectLogLineDtos = p.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                    {
                                        ProjectLogLine = ll,
                                        Language = ll.Language
                                    }),
                                    ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                    {
                                        ProjectInterest = i,
                                        Interest = i.Interest,
                                        InterestGroup = i.Interest.InterestGroup
                                    }),
                                    ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations
                                                                    .Where(pbe => !pbe.IsDeleted
                                                                                  && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                                                      .Any(aoc => !aoc.IsDeleted
                                                                                                                                  && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid))
                                    .Select(be => new ProjectBuyerEvaluationDto
                                    {
                                        ProjectBuyerEvaluation = be,
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = be.BuyerAttendeeOrganization,
                                            Organization = be.BuyerAttendeeOrganization.Organization,
                                            Edition = be.BuyerAttendeeOrganization.Edition
                                        },
                                        ProjectEvaluationStatus = be.ProjectEvaluationStatus,
                                        ProjectEvaluationRefuseReason = be.ProjectEvaluationRefuseReason
                                    })
                                });

            return await query
                            .OrderBy(pd => pd.Project.CreateDate)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds all pitching base dtos by filters and by page asynchronous.</summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectBaseDto>> FindAllPitchingBaseDtosByFiltersAndByPageAsync(
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns,
            string keywords,
            Guid? interestUid,
            string languageCode,
            int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsFinished()
                                .IsPitching()
                                .FindByKeywords(keywords)
                                .FindByInterestUid(interestUid)
                                .DynamicOrder<Project>(
                                    sortColumns,
                                    null,
                                    new List<string> { "CreateDate", "FinishDate" },
                                    "FinishDate")
                                .Select(p => new ProjectBaseDto
                                {
                                    Id = p.Id,
                                    Uid = p.Uid,
                                    ProjectName = p.ProjectTitles.Where(t => t.Language.Code == languageCode).Select(t => t.Value).FirstOrDefault(),
                                    ProducerName = p.SellerAttendeeOrganization.Organization.Name,
                                    ProducerImageUploadDate = p.SellerAttendeeOrganization.Organization.ImageUploadDate,
                                    ProducerUid = p.SellerAttendeeOrganization.Organization.Uid,
                                    ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted && i.Interest.InterestGroup.Uid == InterestGroup.Genre.Uid).Select(i => new ProjectInterestDto
                                    {
                                        Interest = i.Interest,
                                        InterestGroup = i.Interest.InterestGroup
                                    }),
                                    CreateDate = p.CreateDate,
                                    FinishDate = p.FinishDate
                                });

            return await query
                           .ToListPagedAsync(page, pageSize);
        }

        /// <summary>Finds all pitching dtos by filters asynchronous.</summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="projectUids">The project uids.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<ProjectDto>> FindAllPitchingDtosByFiltersAsync(
            string keywords,
            Guid? interestUid,
            List<Guid> projectUids,
            string languageCode,
            int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsFinished()
                                .IsPitching()
                                .FindByKeywords(keywords)
                                .FindByInterestUid(interestUid)
                                .FindByUids(projectUids)
                                .Select(p => new ProjectDto
                                {
                                    Project = p,
                                    SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = p.SellerAttendeeOrganization,
                                        Organization = p.SellerAttendeeOrganization.Organization,
                                        Edition = p.SellerAttendeeOrganization.Edition,
                                    },
                                    ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                    {
                                        ProjectTitle = t,
                                        Language = t.Language
                                    }),
                                    ProjectSummaryDtos = p.ProjectSummaries.Where(s => !s.IsDeleted).Select(s => new ProjectSummaryDto
                                    {
                                        ProjectSummary = s,
                                        Language = s.Language
                                    }),
                                    ProjectLogLineDtos = p.ProjectLogLines.Where(l => !l.IsDeleted).Select(l => new ProjectLogLineDto
                                    {
                                        ProjectLogLine = l,
                                        Language = l.Language
                                    }),
                                    ProjectProductionPlanDtos = p.ProjectProductionPlans.Where(pp => !pp.IsDeleted).Select(pp => new ProjectProductionPlanDto
                                    {
                                        ProjectProductionPlan = pp,
                                        Language = pp.Language
                                    }),
                                    ProjectAdditionalInformationDtos = p.ProjectAdditionalInformations.Where(a => !a.IsDeleted).Select(a => new ProjectAdditionalInformationDto
                                    {
                                        ProjectAdditionalInformation = a,
                                        Language = a.Language
                                    }),
                                    ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                    {
                                        ProjectInterest = i,
                                        Interest = i.Interest,
                                        InterestGroup = i.Interest.InterestGroup
                                    })
                                });

            return await query
                            .ToListAsync();
        }

        #region Site Widgets

        /// <summary>Finds the site details dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                }),
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site main information widget dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteMainInformationWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                }),
                                ProjectLogLineDtos = p.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                {
                                    ProjectLogLine = ll,
                                    Language = ll.Language
                                }),
                                ProjectSummaryDtos = p.ProjectSummaries.Where(s => !s.IsDeleted).Select(s => new ProjectSummaryDto
                                {
                                    ProjectSummary = s,
                                    Language = s.Language
                                }),
                                ProjectProductionPlanDtos = p.ProjectProductionPlans.Where(pp => !pp.IsDeleted).Select(pp => new ProjectProductionPlanDto
                                {
                                    ProjectProductionPlan = pp,
                                    Language = pp.Language
                                }),
                                ProjectAdditionalInformationDtos = p.ProjectAdditionalInformations.Where(aa => !aa.IsDeleted).Select(aa => new ProjectAdditionalInformationDto
                                {
                                    ProjectAdditionalInformation = aa,
                                    Language = aa.Language
                                }),
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site interest dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteInterestWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                {
                                    ProjectInterest = i,
                                    Interest = i.Interest,
                                    InterestGroup = i.Interest.InterestGroup
                                }),
                                ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                {
                                    TargetAudience = ta.TargetAudience
                                }),
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site links widget dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteLinksWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectImageLinkDtos = p.ProjectImageLinks.Where(il => !il.IsDeleted).Select(il => new ProjectImageLinkDto
                                {
                                    ProjectImageLink = il
                                }),
                                ProjectTeaserLinkDtos = p.ProjectTeaserLinks.Where(tl => !tl.IsDeleted).Select(tl => new ProjectTeaserLinkDto
                                {
                                    ProjectTeaserLink = tl
                                }),
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site buyer company dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                 .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus,
                                    ProjectEvaluationRefuseReason = be.ProjectEvaluationRefuseReason
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site duplicate dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteDuplicateDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                }),
                                ProjectLogLineDtos = p.ProjectLogLines.Where(ll => !ll.IsDeleted).Select(ll => new ProjectLogLineDto
                                {
                                    ProjectLogLine = ll,
                                    Language = ll.Language
                                }),
                                ProjectSummaryDtos = p.ProjectSummaries.Where(s => !s.IsDeleted).Select(s => new ProjectSummaryDto
                                {
                                    ProjectSummary = s,
                                    Language = s.Language
                                }),
                                ProjectProductionPlanDtos = p.ProjectProductionPlans.Where(pp => !pp.IsDeleted).Select(pp => new ProjectProductionPlanDto
                                {
                                    ProjectProductionPlan = pp,
                                    Language = pp.Language
                                }),
                                ProjectAdditionalInformationDtos = p.ProjectAdditionalInformations.Where(aa => !aa.IsDeleted).Select(aa => new ProjectAdditionalInformationDto
                                {
                                    ProjectAdditionalInformation = aa,
                                    Language = aa.Language
                                }),
                                ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                {
                                    ProjectInterest = i,
                                    Interest = i.Interest,
                                    InterestGroup = i.Interest.InterestGroup
                                }),
                                ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                {
                                    TargetAudience = ta.TargetAudience
                                }),
                                ProjectImageLinkDtos = p.ProjectImageLinks.Where(il => !il.IsDeleted).Select(il => new ProjectImageLinkDto
                                {
                                    ProjectImageLink = il
                                }),
                                ProjectTeaserLinkDtos = p.ProjectTeaserLinks.Where(tl => !tl.IsDeleted).Select(tl => new ProjectTeaserLinkDto
                                {
                                    ProjectTeaserLink = tl
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>Finds the site buyer evaluation widget dto by project uid asynchronous.</summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteBuyerEvaluationWidgetDtoByProjectUidAsync(Guid projectUid, Guid attendeeCollaboratorUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization,
                                    Edition = p.SellerAttendeeOrganization.Edition
                                },
                                ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations
                                                                    .Where(pbe => !pbe.IsDeleted
                                                                                  && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                          .Any(aoc => !aoc.IsDeleted
                                                                                                      && aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid))
                                .Select(be => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = be,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = be.BuyerAttendeeOrganization,
                                        Organization = be.BuyerAttendeeOrganization.Organization,
                                        Edition = be.BuyerAttendeeOrganization.Edition
                                    },
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus,
                                    ProjectEvaluationRefuseReason = be.ProjectEvaluationRefuseReason
                                })
                            })
                            .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Counts all projects for pitching by edition or in all editions.
        /// </summary>
        /// <param name="editionId"></param>
        /// <param name="showAllEditions"></param>
        /// <returns></returns>
        public async Task<int> CountAllByDataTable(
            int editionId,
            bool showAllEditions = false)

        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .IsFinished()
                                .IsPitching();

            return await query.CountAsync();
        }

        #endregion

        #region Audivisual Projects Report

        /// <summary>Finds the audiovisual subscribed project list</summary>
        /// <param name="keywords"></param>
        /// <param name="interestUid"></param>
        /// <param name="editionId"></param>
        /// <param name="isPitching"></param>
        /// <param name="targetAudienceUid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="showAllEditions"></param>
        /// <returns></returns>
        public IEnumerable<AudiovisualProjectSubscriptionDto> FindAudiovisualSubscribedProjectsDtosByFilter(string keywords, List<Guid> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByInterestUids(interestUids)
                                .FindByEditionId(editionId, showAllEditions)
                                .IsFinished()
                                .IsPitching(isPitching)
                                .FindByTargetAudienceUids(targetAudienceUids)
                                .FindByDate(startDate, endDate)
                                .Select(p => new AudiovisualProjectSubscriptionDto()
                                {
                                    Project = p,
                                    SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = p.SellerAttendeeOrganization,
                                        Organization = p.SellerAttendeeOrganization.Organization,
                                        Edition = p.SellerAttendeeOrganization.Edition,
                                    },
                                    ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                    {
                                        ProjectTitle = t,
                                        Language = t.Language
                                    }),
                                    ProjectSummaryDtos = p.ProjectSummaries.Where(s => !s.IsDeleted).Select(s => new ProjectSummaryDto
                                    {
                                        ProjectSummary = s,
                                        Language = s.Language
                                    }),
                                    ProjectLogLineDtos = p.ProjectLogLines.Where(l => !l.IsDeleted).Select(l => new ProjectLogLineDto
                                    {
                                        ProjectLogLine = l,
                                        Language = l.Language
                                    }),
                                    ProjectProductionPlanDtos = p.ProjectProductionPlans.Where(pp => !pp.IsDeleted).Select(pp => new ProjectProductionPlanDto
                                    {
                                        ProjectProductionPlan = pp,
                                        Language = pp.Language
                                    }),
                                    ProjectAdditionalInformationDtos = p.ProjectAdditionalInformations.Where(a => !a.IsDeleted).Select(a => new ProjectAdditionalInformationDto
                                    {
                                        ProjectAdditionalInformation = a,
                                        Language = a.Language
                                    }),
                                    ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                    {
                                        ProjectInterest = i,
                                        Interest = i.Interest,
                                        InterestGroup = i.Interest.InterestGroup
                                    }),
                                    ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(be => !be.IsDeleted).Select(be => new ProjectBuyerEvaluationDto
                                    {
                                        ProjectBuyerEvaluation = be,
                                        BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = be.BuyerAttendeeOrganization,
                                            Organization = be.BuyerAttendeeOrganization.Organization,
                                            Edition = be.BuyerAttendeeOrganization.Edition
                                        },
                                        ProjectEvaluationStatus = be.ProjectEvaluationStatus
                                    }),
                                    ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                    {
                                        TargetAudience = ta.TargetAudience
                                    })
                                });
            return query;
        }

        /// <summary>Finds the audiovisual subscribed project list by page</summary>
        /// <param name="keywords"></param>
        /// <param name="interestUids"></param>
        /// <param name="editionId"></param>
        /// <param name="isPitching"></param>
        /// <param name="targetAudienceUids"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="showAllEditions"></param>
        /// <returns></returns>
        public async Task<IPagedList<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAndByPageAsync(
            string keywords,
            List<Guid> interestUids,
            int editionId, 
            bool isPitching, 
            List<Guid> targetAudienceUids, 
            DateTime? startDate, 
            DateTime? endDate, 
            int page = 1, 
            int pageSize = 10, 
            bool showAllEditions = false)
        {
            var query = this.FindAudiovisualSubscribedProjectsDtosByFilter(keywords, interestUids, editionId, isPitching, targetAudienceUids, startDate, endDate, showAllEditions);

            return await query
                            .OrderBy(p => p.SellerAttendeeOrganizationDto.Organization.TradeName)
                            .ThenBy(p => p.Project.CreateDate)
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>Finds the audiovisual subscribed project list</summary>
        /// <param name="keywords"></param>
        /// <param name="interestUids"></param>
        /// <param name="editionId"></param>
        /// <param name="isPitching"></param>
        /// <param name="targetAudienceUids"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="showAllEditions"></param>
        /// <returns></returns>
        public async Task<List<AudiovisualProjectSubscriptionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAsync(
            string keywords,
            List<Guid> interestUids, 
            int editionId, 
            bool isPitching, 
            List<Guid> targetAudienceUids,
            DateTime? startDate, 
            DateTime? endDate, 
            bool showAllEditions = false)
        {
            var query = this.FindAudiovisualSubscribedProjectsDtosByFilter(keywords, interestUids, editionId, isPitching, targetAudienceUids, startDate, endDate, showAllEditions);

            return await query
                            .OrderBy(p => p.SellerAttendeeOrganizationDto.Organization.TradeName)
                            .ThenBy(p => p.Project.CreateDate)
                            .ToListAsync();
        }
        #endregion

        #region Old methods

        public IEnumerable<Project> GetAllByAdmin()
        {
            return this.dbSet
                                //.Include(i => i.Titles)
                                //.Include(i => i.Titles.Select(t => t.Language))
                                //.Include(i => i.PlayersRelated)
                                //.Include(i => i.PlayersRelated.Select(t => t.Player))
                                //.Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                //.Include(i => i.PlayersRelated.Select(t => t.Evaluation.Status))
                                //.Include(i => i.Producer)
                                .AsNoTracking();
        }



        public IEnumerable<Project> GetAllExcel()
        {
            return this.dbSet
                                //.Include(i => i.Titles)
                                //.Include(i => i.Titles.Select(t => t.Language))
                                //.Include(i => i.LogLines)
                                //.Include(i => i.LogLines.Select(t => t.Language))
                                //.Include(i => i.Summaries)
                                //.Include(i => i.Summaries.Select(t => t.Language))
                                //.Include(i => i.ProductionPlans)
                                //.Include(i => i.ProductionPlans.Select(t => t.Language))
                                //.Include(i => i.Interests)
                                //.Include(i => i.Interests.Select(e => e.Interest))
                                //.Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                                //.Include(i => i.LinksImage)
                                //.Include(i => i.LinksTeaser)
                                //.Include(i => i.AdditionalInformations)
                                //.Include(i => i.AdditionalInformations.Select(t => t.Language))
                                //.Include(i => i.PlayersRelated)
                                //.Include(i => i.PlayersRelated.Select(t => t.Player))
                                //.Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                //.Include(i => i.PlayersRelated.Select(t => t.Evaluation).Select(t => t.Status))
                                //.Include(i => i.Producer)
                                //.Include(i => i.Producer.EventsCollaborators)
                                //.Include(i => i.Producer.EventsCollaborators.Select(t => t.Collaborator))
                                //.Include(i => i.Producer.EventsCollaborators.Select(t => t.Collaborator).Select(t => t.User))
                                .AsNoTracking();
        }

        public IEnumerable<Project> GetDataExcel()
        {
            return this.dbSet
                                //.Include(i => i.PlayersRelated)
                                //.Include(i => i.PlayersRelated.Select(t => t.Player))
                                //.Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                //.Include(i => i.Titles)
                                //.Include(i => i.Titles.Select(t => t.Language))
                                //.Include(i => i.Producer)
                                .AsNoTracking();
        }

        public override IQueryable<Project> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet;
            //.Include(i => i.Titles)
            //.Include(i => i.Titles.Select(t => t.Language))
            //.Include(i => i.LogLines)
            //.Include(i => i.LogLines.Select(t => t.Language))
            //.Include(i => i.Summaries)
            //.Include(i => i.Summaries.Select(t => t.Language))
            //.Include(i => i.ProductionPlans)
            //.Include(i => i.ProductionPlans.Select(t => t.Language))
            //.Include(i => i.Interests)
            //.Include(i => i.Interests.Select(e => e.Interest))
            //.Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
            //.Include(i => i.LinksImage)
            //.Include(i => i.LinksTeaser)
            //.Include(i => i.AdditionalInformations)
            //.Include(i => i.AdditionalInformations.Select(t => t.Language))
            //.Include(i => i.PlayersRelated)
            //.Include(i => i.PlayersRelated.Select(t => t.Player))
            //.Include(i => i.PlayersRelated.Select(t => t.Evaluation))
            //.Include(i => i.Producer);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Project> GetAll(Expression<Func<Project, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Project Get(Expression<Func<Project, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Project Get(Guid uid)
        {
            return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        }

        public Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                        //.Include(i => i.Producer)
                        //.Include(i => i.Producer.Projects)
                        //.Include(i => i.PlayersRelated)
                        .FirstOrDefault(filter);
        }

        public Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                        //.Include(i => i.PlayersRelated)
                        //.Include(i => i.PlayersRelated.Select(e => e.Player))
                        .FirstOrDefault(filter);
        }


        public override void Delete(Project entity)
        {
            //if (entity.AdditionalInformations != null && entity.AdditionalInformations.Any())
            //{
            //    var items = entity.AdditionalInformations.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.AdditionalInformations.Clear();

            //if (entity.Interests != null && entity.Interests.Any())
            //{
            //    var items = entity.Interests.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.Interests.Clear();

            //if (entity.LinksImage != null && entity.LinksImage.Any())
            //{
            //    var items = entity.LinksImage.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.LinksImage.Clear();

            //if (entity.LinksTeaser != null && entity.LinksTeaser.Any())
            //{
            //    var items = entity.LinksTeaser.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.LinksTeaser.Clear();

            //if (entity.LogLines != null && entity.Summaries.Any())
            //{
            //    var items = entity.LogLines.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.LogLines.Clear();


            //if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
            //{
            //    var items = entity.PlayersRelated.ToList();
            //    foreach (var item in items)
            //    {
            //        if (item.Evaluation != null)
            //        {
            //            _context.Entry(item.Evaluation).State = EntityState.Deleted;
            //        }

            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.PlayersRelated.Clear();

            //if (entity.ProductionPlans != null && entity.ProductionPlans.Any())
            //{
            //    var items = entity.ProductionPlans.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.ProductionPlans.Clear();

            //if (entity.Summaries != null && entity.Summaries.Any())
            //{
            //    var items = entity.Summaries.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.Summaries.Clear();

            //if (entity.Titles != null && entity.Titles.Any())
            //{
            //    var items = entity.Titles.ToList();
            //    foreach (var item in items)
            //    {
            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}
            //entity.Titles.Clear();

            base.Delete(entity);
        }

        public Project GetWithPlayerSelection(Guid uid)
        {
            return this.dbSet
                                //.Include(i => i.Titles)
                                //.Include(i => i.Titles.Select(t => t.Language))
                                //.Include(i => i.PlayersRelated)
                                //.Include(i => i.PlayersRelated.Select(e => e.Player))
                                //.Include(i => i.PlayersRelated.Select(e => e.SavedUser))
                                //.Include(i => i.PlayersRelated.Select(e => e.SendingUser))
                                //.Include(i => i.PlayersRelated.Select(e => e.Evaluation))
                                //.Include(i => i.PlayersRelated.Select(e => e.Evaluation.Status))
                                .FirstOrDefault(e => e.Uid == uid);
        }

        public IQueryable<Project> GetAllOption(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                                //.Include(i => i.Titles)
                                //.Include(i => i.Titles.Select(e => e.Language))
                                //.Include(i => i.Producer)
                                .Where(filter);

        }

        public int CountUnsent()
        {
            return this.dbSet.Count();
            //.Include(i => i.PlayersRelated)
            //.Count(e => !e.PlayersRelated.Any() || (e.PlayersRelated.Count(p => !p.Sent) == e.PlayersRelated.Count()));

        }

        public int CountSent()
        {
            return this.dbSet.Count();
            //.Include(i => i.PlayersRelated)
            //.Count(e => e.PlayersRelated.Any(p => p.Sent));

        }

        #endregion
    }
}