// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-09-2019
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
                                                                          && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => aoc.AttendeeCollaborator.Collaborator.Uid == buyerAttendeeCollaboratorUid
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
                var innerProjectInterestNameWhere = PredicateBuilder.New<Project>(true);
                var innerSellerAttendeeOrganizationNameWhere = PredicateBuilder.New<Project>(true);

                foreach (var keyword in keywords.Split(' '))
                {
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        innerProjectTitleNameWhere = innerProjectTitleNameWhere.Or(p => p.ProjectTitles.Any(pt => !pt.IsDeleted && pt.Value.Contains(keyword)));
                        innerProjectInterestNameWhere = innerProjectInterestNameWhere.Or(p => p.ProjectInterests.Any(pi => !pi.IsDeleted && pi.Interest.Name.Contains(keyword)));
                        innerSellerAttendeeOrganizationNameWhere = innerSellerAttendeeOrganizationNameWhere.Or(sao => sao.SellerAttendeeOrganization.Organization.Name.Contains(keyword));
                    }
                }

                outerWhere = outerWhere.Or(innerProjectTitleNameWhere);
                outerWhere = outerWhere.Or(innerProjectInterestNameWhere);
                outerWhere = outerWhere.Or(innerSellerAttendeeOrganizationNameWhere);
                query = query.Where(outerWhere);
            }

            return query;
        }

        /// <summary>Finds the by interest.</summary>
        /// <param name="query">The query.</param>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        internal static IQueryable<Project> FindByInterest(this IQueryable<Project> query, Guid? interestUid)
        {
            if (interestUid != null)
            {
                query = query.Where(p => p.ProjectInterests.Any(pi => !pi.IsDeleted
                                                                      && !pi.Interest.IsDeleted
                                                                      && pi.Interest.Uid == interestUid));
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
                                        ProjectEvaluationStatus = be.ProjectEvaluationStatus
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
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async Task<IPagedList<ProjectDto>> FindAllDtosToEvaluateAsync(Guid attendeeCollaboratorUid, string searchKeywords, Guid? interestUid, int page, int pageSize)
        {
            var query = this.GetBaseQuery()
                                .FindByBuyerAttendeeCollabratorUid(attendeeCollaboratorUid)
                                .IsFinished()
                                .FindByKeywords(searchKeywords)
                                .FindByInterest(interestUid)
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
                                    ProjectBuyerEvaluationDtos = p.ProjectBuyerEvaluations.Where(pbe => !pbe.IsDeleted
                                                                                                        && !pbe.BuyerAttendeeOrganization.IsDeleted
                                                                                                        && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                                                                                            .Any(aoc => !aoc.IsDeleted
                                                                                                                                                        && aoc.AttendeeCollaborator.Collaborator.Uid == attendeeCollaboratorUid))
                                    .Select(be => new ProjectBuyerEvaluationDto
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
                                });

            return await query
                            .OrderBy(pd => pd.Project.CreateDate)
                            .ToListPagedAsync(page, pageSize);
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
                                    ProjectEvaluationStatus = be.ProjectEvaluationStatus
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