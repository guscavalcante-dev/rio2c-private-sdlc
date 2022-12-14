// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-14-2022
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

        /// <summary>
        /// Finds the by id.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindById(this IQueryable<Project> query, int projectId)
        {
            query = query.Where(p => p.Id == projectId);

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
                var innerSellerAttendeeOrganizationNameWhere = PredicateBuilder.New<Project>(true);

                if (!string.IsNullOrEmpty(keywords))
                {
                    innerProjectTitleNameWhere = innerProjectTitleNameWhere.Or(p => p.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keywords)));
                    innerSellerAttendeeOrganizationNameWhere = innerSellerAttendeeOrganizationNameWhere.Or(sao => sao.SellerAttendeeOrganization.Organization.Name.Contains(keywords));
                }

                outerWhere = outerWhere.Or(innerProjectTitleNameWhere);
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
        internal static IQueryable<Project> FindByInterestUids(this IQueryable<Project> query, List<Guid?> interestUids)
        {
            if (interestUids?.Any(i => i.HasValue) == true)
            {
                query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
                                                                      && !pi.Interest.IsDeleted
                                                                      && interestUids.Contains(pi.Interest.Uid)));
            }

            return query;
        }

        /// <summary>Finds the by target audience uids.</summary>
        /// <param name="query">The query.</param>
        /// <param name="targetAudienceUids">The target audience uids.</param>
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

        /// <summary>Finds the by custom filer.</summary>
        /// <param name="query">The query.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByCustomFiler(this IQueryable<Project> query, string customFilter, Guid? buyerOrganizationUid)
        {
            if (!string.IsNullOrEmpty(customFilter))
            {
                if (customFilter == "HasNegotiationNotScheduled")
                {
                    query = query.Where(p => p.ProjectBuyerEvaluations
                                                    .Any(pbe => !pbe.IsDeleted
                                                                && pbe.ProjectEvaluationStatus.Uid == ProjectEvaluationStatus.Accepted.Uid
                                                                && (!buyerOrganizationUid.HasValue || pbe.BuyerAttendeeOrganization.Organization.Uid == buyerOrganizationUid)
                                                                && (!pbe.Negotiations.Any() || pbe.Negotiations.All(n => n.IsDeleted))));
                }
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

        /// <summary>
        /// Determines whether the specified show pitchings is pitching.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <returns></returns>
        internal static IQueryable<Project> IsPitching(this IQueryable<Project> query, bool showPitchings = false)
        {
            if (showPitchings)
            {
                query = query.Where(p => p.IsPitching);
            }

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

        /// <summary>
        /// Finds the by is evaluated.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByIsEvaluated(this IQueryable<Project> query)
        {
            query = query.Where(p => p.CommissionGrade != null);

            return query;
        }

        /// <summary>
        /// Finds the by uids.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="selectedProjectsUids">The selected projects uids.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByUids(this IQueryable<Project> query, List<Guid> selectedProjectsUids)
        {
            if (selectedProjectsUids?.Any() == true)
            {
                query = query.Where(c => selectedProjectsUids.Contains(c.Uid));
            }

            return query;
        }

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<Project> Order(this IQueryable<Project> query)
        {
            query = query.OrderBy(p => p.FinishDate);

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

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectDto> Order(this IQueryable<ProjectDto> query)
        {
            query = query.OrderBy(dto => dto.Project.FinishDate);

            return query;
        }
    }

    #endregion

    #region ProjectBaseDto IQueryable Extensions

    /// <summary>ProjectBaseDtoIQueryableExtensions</summary>
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

        /// <summary>
        /// Orders the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBaseDto> Order(this IQueryable<ProjectBaseDto> query)
        {
            query = query.OrderBy(dto => dto.FinishDate);

            return query;
        }
    }

    #endregion 

    /// <summary>ProjectRepository</summary>
    public class ProjectRepository : Repository<Context.PlataformaRio2CContext, Project>, IProjectRepository
    {
        private readonly IEditionRepository editioRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="editionRepository">The edition repository.</param>
        public ProjectRepository(
            Context.PlataformaRio2CContext context,
            IEditionRepository editionRepository
            )
            : base(context)
        {
            this.editioRepo = editionRepository;
        }

        #region Private Methods

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

        /// <summary>
        /// Finds all project dtos asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        private async Task<List<ProjectDto>> FindAllProjectDtosAsync(
            int editionId,
            string searchKeywords,
            bool showPitchings,
            List<Guid?> interestUids)
        {
            var query = this.GetBaseQuery()
                               .IsFinished()
                               .FindByEditionId(editionId)
                               .FindByKeywords(searchKeywords)
                               .FindByInterestUids(interestUids)
                               .IsPitching(showPitchings)
                               .Select(p => new ProjectDto
                               {
                                   Project = p,
                                   SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                   {
                                       AttendeeOrganization = p.SellerAttendeeOrganization,
                                       Organization = p.SellerAttendeeOrganization.Organization,
                                       Edition = p.SellerAttendeeOrganization.Edition
                                   },
                                   ProjectCommissionEvaluationDtos = p.CommissionEvaluations.Where(ce => !ce.IsDeleted).Select(ce => new ProjectCommissionEvaluationDto
                                   {
                                       CommissionEvaluation = ce,
                                       EvaluatorUser = ce.EvaluatorUser
                                   }),
                                   ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted && i.Interest.InterestGroup.Uid == InterestGroup.Genre.Uid).Select(i => new ProjectInterestDto
                                   {
                                       Interest = i.Interest,
                                       InterestGroup = i.Interest.InterestGroup
                                   }),
                                   ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                   {
                                       TargetAudience = ta.TargetAudience
                                   }),
                               });

            return await query
                            .Order()
                            .ToListAsync();
        }

        #endregion

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        public async Task<Project> FindByIdAsync(int projectId)
        {
            var query = this.GetBaseQuery()
                            .FindById(projectId);

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the by uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<Project> FindByUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery()
                            .FindByUid(projectUid);

            return await query.FirstOrDefaultAsync();
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
        public async Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(
            Guid attendeeCollaboratorUid,
            string searchKeywords,
            Guid? interestUid,
            Guid? evaluationStatusUid,
            int page,
            int pageSize)
        {
            var matchInterestsGroups = new List<Guid>
            {
                InterestGroup.LookingFor.Uid,
                InterestGroup.ProjectStatus.Uid,
                InterestGroup.Platforms.Uid,
                InterestGroup.Genre.Uid
            };

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
                                    InterestGroupsMatches = p.ProjectInterests
                                                                .Where(pi => !pi.IsDeleted && !pi.Interest.IsDeleted
                                                                             && p.ProjectBuyerEvaluations
                                                                                    .Any(pbe => !pbe.IsDeleted
                                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                                            .Where(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid
                                                                                                                          && !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                                            .FirstOrDefault()
                                                                                                            .AttendeeOrganization.Organization.OrganizationInterests
                                                                                                                    .Any(oi => !oi.IsDeleted && !oi.Interest.IsDeleted
                                                                                                                               && matchInterestsGroups.Contains(oi.Interest.InterestGroup.Uid)
                                                                                                                               && oi.Interest.Id == pi.Interest.Id)))
                                                                .Select(pi => pi.Interest.InterestGroup)
                                                                .Distinct(),
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
                            .OrderByDescending(ao => ao.InterestGroupsMatches.Count())
                            .ThenBy(pd => pd.Project.CreateDate)
                            .ToListPagedAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all base dtos by filters and by page asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectBaseDto>> FindAllBaseDtosPagedAsync(
            int page,
            int pageSize,
            List<Tuple<string, string>> sortColumns,
            string keywords,
            bool showPitchings,
            Guid? interestUid,
            Guid? evaluationStatusUid,
            string languageCode,
            int editionId)
        {
            var projectBaseDtos = await this.FindAllProjectBaseDtosByFilters(sortColumns, keywords, showPitchings, interestUid, languageCode, editionId);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<ProjectBaseDto> projectBaseDtosResult = projectBaseDtos;
            if (editionDto.IsAudiovisualCommissionProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectBaseDtosResult = new List<ProjectBaseDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectBaseDtosResult = new List<ProjectBaseDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedProjectsIds = await this.FindAllApprovedCommissionProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectBaseDtosResult = projectBaseDtos.Where(dto => approvedProjectsIds.Contains(dto.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectBaseDtosResult = projectBaseDtos.Where(dto => !approvedProjectsIds.Contains(dto.Id) && dto.IsPitching == true);
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    projectBaseDtosResult = new List<ProjectBaseDto>();
                }

                #endregion
            }

            return await projectBaseDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all dtos paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectDto>> FindAllDtosPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> interestUids,
            Guid? evaluationStatusUid,
            bool showPitchings,
            int page,
            int pageSize)
        {
            var projectsDtos = await this.FindAllProjectDtosAsync(editionId, searchKeywords, showPitchings, interestUids);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<ProjectDto> projectDtosResult = projectsDtos;
            if (editionDto.IsAudiovisualCommissionProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectDtosResult = new List<ProjectDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectDtosResult = new List<ProjectDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedProjectsIds = await this.FindAllApprovedCommissionProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectDtosResult = projectsDtos.Where(dto => approvedProjectsIds.Contains(dto.Project.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectDtosResult = projectsDtos.Where(dto => !approvedProjectsIds.Contains(dto.Project.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    projectDtosResult = new List<ProjectDto>();
                }

                #endregion
            }

            return await projectDtosResult
                            .ToPagedListAsync(page, pageSize);
        }

        /// <summary>
        /// Finds all dtos by filters asynchronous.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="interestUids">The interest uid.</param>
        /// <param name="projectUids">The project uids.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<ProjectDto>> FindAllDtosByFiltersAsync(
            string keywords,
            bool showPitchings,
            List<Guid?> interestUids,
            List<Guid> projectUids,
            string languageCode,
            int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsFinished()
                                .IsPitching(showPitchings)
                                .FindByKeywords(keywords)
                                .FindByInterestUids(interestUids)
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
                                    }),
                                    ProjectTeaserLinkDtos = p.ProjectTeaserLinks.Where(tl => !tl.IsDeleted).Select(tl => new ProjectTeaserLinkDto
                                    {
                                        ProjectTeaserLink = tl
                                    }),
                                    ProjectImageLinkDtos = p.ProjectImageLinks.Where(il => !il.IsDeleted).Select(il => new ProjectImageLinkDto
                                    {
                                        ProjectImageLink = il
                                    }),
                                });

            return await query
                            .Order()
                            .ToListAsync();
        }

        /// <summary>
        /// Finds all approved projects ids asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllApprovedCommissionProjectsIdsAsync(int editionId)
        {
            var edition = await this.editioRepo.FindByIdAsync(editionId);

            var query = this.GetBaseQuery()
                                .IsPitching(true)
                                .FindByEditionId(editionId)
                                .FindByIsEvaluated();

            return await query
                            .OrderByDescending(p => p.CommissionGrade)
                            .Take(edition.AudiovisualCommissionMaximumApprovedProjectsCount)
                            .Select(p => p.Id)
                            .ToArrayAsync();
        }

        /// <summary>
        /// Finds all projects ids paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int[]> FindAllProjectsIdsPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> interestUids,
            Guid? evaluationStatusUid,
            bool showPitchings,
            int page,
            int pageSize)
        {
            var projectsIds = await this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsFinished()
                                .IsPitching(showPitchings)
                                .FindByKeywords(searchKeywords)
                                .FindByInterestUids(interestUids)
                                .Order()
                                .Select(p => p.Id)
                                .ToListAsync();

            var editionDto = await this.editioRepo.FindDtoAsync(editionId);

            IEnumerable<int> projectsIdsResult = projectsIds;
            if (editionDto.IsAudiovisualCommissionProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectsIdsResult = new List<int>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectsIdsResult = new List<int>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                var approvedProjectsIds = await this.FindAllApprovedCommissionProjectsIdsAsync(editionId);

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectsIdsResult = projectsIds.Where(id => approvedProjectsIds.Contains(id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectsIdsResult = projectsIds.Where(id => !approvedProjectsIds.Contains(id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    projectsIdsResult = new List<int>();
                }

                #endregion
            }

            var projectsPagedList = projectsIdsResult.ToPagedList(page, pageSize);

            return projectsPagedList.ToArray();
        }

        /// <summary>
        /// Counts the paged asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="searchKeywords">The search keywords.</param>
        /// <param name="interestUids">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<int> CountPagedAsync(
            int editionId,
            string searchKeywords,
            List<Guid?> interestUids,
            Guid? evaluationStatusUid,
            bool showPitchings,
            int page,
            int pageSize)
        {
            var projectsDtos = await this.FindAllDtosByFiltersAsync(searchKeywords, showPitchings, interestUids, null, null, editionId);
            var editionDto = await this.editioRepo.FindDtoAsync(editionId);
            var approvedProjectsIds = await this.FindAllApprovedCommissionProjectsIdsAsync(editionId);

            IEnumerable<ProjectDto> projectsResult = projectsDtos;
            if (editionDto.IsAudiovisualCommissionProjectEvaluationOpen())
            {
                #region Evaluation is Open

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectsResult = new List<ProjectDto>(); //Returns a empty list
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectsResult = new List<ProjectDto>(); //Returns a empty list
                }

                #endregion
            }
            else
            {
                #region Evaluation is Closed

                if (evaluationStatusUid == ProjectEvaluationStatus.Accepted.Uid)
                {
                    projectsResult = projectsDtos.Where(dto => approvedProjectsIds.Contains(dto.Project.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.Refused.Uid)
                {
                    projectsResult = projectsDtos.Where(dto => !approvedProjectsIds.Contains(dto.Project.Id));
                }
                else if (evaluationStatusUid == ProjectEvaluationStatus.UnderEvaluation.Uid)
                {
                    projectsResult = new List<ProjectDto>();
                }

                #endregion
            }

            var projectsPagedList = await projectsResult
                                                 .ToPagedListAsync(page, pageSize);

            return projectsPagedList.Count;
        }

        /// <summary>
        /// Finds all base dtos by filters.
        /// </summary>
        /// <param name="sortColumns">The sort columns.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="showPitchings">if set to <c>true</c> [show pitchings].</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <param name="evaluationStatusUid">The evaluation status uid.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        private async Task<List<ProjectBaseDto>> FindAllProjectBaseDtosByFilters(
            List<Tuple<string, string>> sortColumns,
            string keywords,
            bool showPitchings,
            Guid? interestUid,
            string languageCode,
            int editionId)
        {
            this.SetProxyEnabled(false);

            var query = this.GetBaseQuery(true)
                            .FindByEditionId(editionId)
                            .IsFinished()
                            .IsPitching(showPitchings)
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
                                ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                {
                                    TargetAudience = ta.TargetAudience
                                }),
                                IsPitching = p.IsPitching,
                                CreateDate = p.CreateDate,
                                FinishDate = p.FinishDate,
                                CommissionGrade = p.CommissionGrade,
                                CommissionEvaluationsCount = p.CommissionEvaluationsCount
                            });

            var result = await query
                            //.Order()
                            .ToListAsync();

            this.SetProxyEnabled(true);

            return result;
        }

        /// <summary>
        /// Finds all by edition identifier asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<Project>> FindAllByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                               .FindByEditionId(editionId);

            return await query
                            .ToListAsync();
        }

        #region Admin Widgets

        /// <summary>
        /// Finds the admin details dto by project uid and by edition identifier asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminDetailsDtoByProjectUidAndByEditionIdAsync(Guid projectUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid)
                                .FindByEditionId(editionId, false);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the admin details dto by project identifier and by edition identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminDetailsDtoByProjectIdAndByEditionIdAsync(int projectId, int editionId)
        {
            var query = this.GetBaseQuery(true)
                               .FindById(projectId)
                               .FindByEditionId(editionId, false);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectTitleDtos = p.ProjectTitles
                                .Where(t => !t.IsDeleted)
                                .Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                }),
                                ProjectCommissionEvaluationDtos = p.CommissionEvaluations
                                .Where(ce => !ce.IsDeleted)
                                .Select(ce => new ProjectCommissionEvaluationDto
                                {
                                    CommissionEvaluation = ce,
                                    EvaluatorUser = ce.EvaluatorUser
                                }).ToList()
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the admin main information widget dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminMainInformationWidgetDtoByProjectUidAsync(Guid projectUid)
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
                                ProjectCommissionEvaluationDtos = p.CommissionEvaluations
                                .Where(ce => !ce.IsDeleted)
                                .Select(ce => new ProjectCommissionEvaluationDto
                                {
                                    CommissionEvaluation = ce,
                                    EvaluatorUser = ce.EvaluatorUser
                                }).ToList()
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the admin interest widget dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminInterestWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
                                ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                {
                                    ProjectInterest = i,
                                    Interest = i.Interest,
                                    InterestGroup = i.Interest.InterestGroup
                                }),
                                ProjectTargetAudienceDtos = p.ProjectTargetAudiences.Where(ta => !ta.IsDeleted).Select(ta => new ProjectTargetAudienceDto
                                {
                                    TargetAudience = ta.TargetAudience
                                })
                            })
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the admin links widget dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminLinksWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
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

        /// <summary>
        /// Finds the admin buyer company widget dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAdminBuyerCompanyWidgetDtoByProjectUidAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery(true)
                                 .FindByUid(projectUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectType = p.ProjectType,
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

        /// <summary>
        /// Finds the commission evaluation widget dto asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAudiovisualCommissionEvaluationWidgetDtoAsync(Guid projectUid, int userId)
        {
            var query = this.GetBaseQuery()
                              .FindByUid(projectUid)
                              .Select(p => new ProjectDto
                              {
                                  Project = p,
                                  ProjectCommissionEvaluationDtos = p.CommissionEvaluations
                                                                                      .Where(ce => !ce.IsDeleted)
                                                                                      .Select(ce => new ProjectCommissionEvaluationDto
                                                                                      {
                                                                                          CommissionEvaluation = ce,
                                                                                          EvaluatorUser = ce.EvaluatorUser
                                                                                      }).ToList(),
                                  //Current ProjectCommissionEvaluation by user Id
                                  ProjectCommissionEvaluationDto = p.CommissionEvaluations
                                                                                   .Where(ce => !ce.IsDeleted && ce.EvaluatorUserId == userId)
                                                                                   .Select(ce => new ProjectCommissionEvaluationDto
                                                                                   {
                                                                                       CommissionEvaluation = ce,
                                                                                       EvaluatorUser = ce.EvaluatorUser
                                                                                   }).FirstOrDefault()
                              });

            return await query
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the evaluators widget dto asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindAudiovisualCommissionEvaluatorsWidgetDtoAsync(Guid projectUid)
        {
            var query = this.GetBaseQuery()
                              .FindByUid(projectUid)
                              .Select(p => new ProjectDto
                              {
                                  Project = p,
                                  ProjectCommissionEvaluationDtos = p.CommissionEvaluations
                                                                        .Where(ce => !ce.IsDeleted)
                                                                        .Select(ce =>
                                                                        new ProjectCommissionEvaluationDto
                                                                        {
                                                                            CommissionEvaluation = ce,
                                                                            EvaluatorUser = ce.EvaluatorUser
                                                                        })
                              });

            return await query
                            .FirstOrDefaultAsync();
        }

        #endregion

        #region Site Widgets

        /// <summary>
        /// Finds the site details dto by project uid asynchronous.
        /// </summary>
        /// <param name="projectUid">The project uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindSiteDetailsDtoByProjectUidAsync(Guid projectUid, int editionId)
        {
            var query = this.GetBaseQuery(true)
                                .FindByUid(projectUid)
                                .FindByEditionId(editionId, false);

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
                                }),
                                ProjectInterestDtos = p.ProjectInterests.Where(i => !i.IsDeleted).Select(i => new ProjectInterestDto
                                {
                                    ProjectInterest = i,
                                    Interest = i.Interest,
                                    InterestGroup = i.Interest.InterestGroup
                                }),
                                ProjectCommissionEvaluationDtos = p.CommissionEvaluations
                                .Where(ce => !ce.IsDeleted)
                                .Select(ce => new ProjectCommissionEvaluationDto
                                {
                                    CommissionEvaluation = ce,
                                    EvaluatorUser = ce.EvaluatorUser
                                }).ToList()
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
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="attendeeCollaboratorUid">The attendee collaborator uid.</param>
        /// <param name="projectUid">The project uid.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindDtoToEvaluateAsync(Guid attendeeCollaboratorUid, Guid projectUid)
        {
            var matchInterestsGroups = new List<Guid>
            {
                InterestGroup.LookingFor.Uid,
                InterestGroup.ProjectStatus.Uid,
                InterestGroup.Platforms.Uid,
                InterestGroup.Genre.Uid
            };

            var query = this.GetBaseQuery()
                                .FindByUid(projectUid)
                                .IsFinished()
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
                                    InterestGroupsMatches = p.ProjectInterests
                                                                .Where(pi => !pi.IsDeleted && !pi.Interest.IsDeleted
                                                                             && p.ProjectBuyerEvaluations
                                                                                    .Any(pbe => !pbe.IsDeleted
                                                                                                  && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                                            .Where(aoc => aoc.AttendeeCollaborator.Uid == attendeeCollaboratorUid
                                                                                                                          && !aoc.IsDeleted && !aoc.AttendeeOrganization.IsDeleted && !aoc.AttendeeOrganization.Organization.IsDeleted)
                                                                                                            .FirstOrDefault()
                                                                                                            .AttendeeOrganization.Organization.OrganizationInterests
                                                                                                                    .Any(oi => !oi.IsDeleted && !oi.Interest.IsDeleted
                                                                                                                               && matchInterestsGroups.Contains(oi.Interest.InterestGroup.Uid)
                                                                                                                               && oi.Interest.Id == pi.Interest.Id)))
                                                                .Select(pi => pi.Interest.InterestGroup)
                                                                .Distinct(),
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
                            .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the dto to evaluate asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        public async Task<ProjectDto> FindDtoToEvaluateAsync(int projectId)
        {
            var query = this.GetBaseQuery()
                                .FindById(projectId)
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
                                    })
                                });

            return await query
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

        #region Audivisual Projects Submissions Report

        /// <summary>
        /// Finds the audiovisual project submission dtos by filter.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="interestUids">The interest uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="targetAudienceUids">The target audience uids.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public IEnumerable<AudiovisualProjectSubmissionDto> FindAudiovisualProjectSubmissionDtosByFilter(string keywords, List<Guid?> interestUids, int editionId, bool isPitching, List<Guid> targetAudienceUids, DateTime? startDate, DateTime? endDate, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByKeywords(keywords)
                                .FindByInterestUids(interestUids)
                                .FindByEditionId(editionId, showAllEditions)
                                .IsFinished()
                                .IsPitching(isPitching)
                                .FindByTargetAudienceUids(targetAudienceUids)
                                .FindByDate(startDate, endDate)
                                .Select(p => new AudiovisualProjectSubmissionDto()
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

        /// <summary>
        /// Finds the audiovisual project submission dtos by filter and by page asynchronous.
        /// </summary>
        /// <param name="keywords">The keywords.</param>
        /// <param name="interestUids">The interest uids.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="isPitching">if set to <c>true</c> [is pitching].</param>
        /// <param name="targetAudienceUids">The target audience uids.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<IPagedList<AudiovisualProjectSubmissionDto>> FindAudiovisualProjectSubmissionDtosByFilterAndByPageAsync(
            string keywords,
            List<Guid?> interestUids,
            int editionId,
            bool isPitching,
            List<Guid> targetAudienceUids,
            DateTime? startDate,
            DateTime? endDate,
            int page = 1,
            int pageSize = 10,
            bool showAllEditions = false)
        {
            var query = this.FindAudiovisualProjectSubmissionDtosByFilter(keywords, interestUids, editionId, isPitching, targetAudienceUids, startDate, endDate, showAllEditions);

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
        public async Task<List<AudiovisualProjectSubmissionDto>> FindAudiovisualSubscribedProjectsDtosByFilterAsync(
            string keywords,
            List<Guid?> interestUids,
            int editionId,
            bool isPitching,
            List<Guid> targetAudienceUids,
            DateTime? startDate,
            DateTime? endDate,
            bool showAllEditions = false)
        {
            var query = this.FindAudiovisualProjectSubmissionDtosByFilter(keywords, interestUids, editionId, isPitching, targetAudienceUids, startDate, endDate, showAllEditions);

            return await query
                            .OrderBy(p => p.SellerAttendeeOrganizationDto.Organization.TradeName)
                            .ThenBy(p => p.Project.CreateDate)
                            .ToListAsync();
        }
        #endregion

        #region Dropdown

        /// <summary>Finds all dropdown dto paged.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="keywords">The keywords.</param>
        /// <param name="customFilter">The custom filter.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectDto>> FindAllDropdownDtoPaged(
            int editionId,
            string keywords,
            string customFilter,
            Guid? buyerOrganizationUid,
            int page,
            int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByKeywords(keywords)
                                .FindByCustomFiler(customFilter, buyerOrganizationUid);

            return await query
                            .Select(p => new ProjectDto
                            {
                                Project = p,
                                ProjectTitleDtos = p.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                {
                                    ProjectTitle = t,
                                    Language = t.Language
                                }),
                                SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                {
                                    AttendeeOrganization = p.SellerAttendeeOrganization,
                                    Organization = p.SellerAttendeeOrganization.Organization
                                }
                            })
                            .OrderBy(pd => pd.SellerAttendeeOrganizationDto.Organization.TradeName)
                            .ToListPagedAsync(page, pageSize);
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