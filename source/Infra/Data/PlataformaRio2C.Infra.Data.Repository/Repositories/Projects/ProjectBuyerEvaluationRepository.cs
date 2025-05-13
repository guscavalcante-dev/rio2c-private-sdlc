// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-11-2025
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project Buyer Evaluation IQueryable Extensions

    /// <summary>
    /// ProjectBuyerEvaluationIQueryableExtensions
    /// </summary>
    internal static class ProjectBuyerEvaluationIQueryableExtensions
    {
        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindById(this IQueryable<ProjectBuyerEvaluation> query, Guid projectBuyerEvaluationId)
        {
            query = query.Where(pbe => pbe.Uid == projectBuyerEvaluationId);

            return query;
        }

        /// <summary>Finds the by edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByEditionId(this IQueryable<ProjectBuyerEvaluation> query, int editionId, bool showAllEditions = false)
        {
            query = query.Where(pbe => (showAllEditions || (!pbe.Project.SellerAttendeeOrganization.IsDeleted
                                                            && pbe.Project.SellerAttendeeOrganization.EditionId == editionId)));

            return query;
        }

        /// <summary>Finds the by project evaluation status uid.</summary>
        /// <param name="query">The query.</param>
        /// <param name="projectEvaluationStatusUid">The project evaluation status uid.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByProjectEvaluationStatusUid(this IQueryable<ProjectBuyerEvaluation> query, Guid projectEvaluationStatusUid)
        {
            query = query.Where(pbe => pbe.ProjectEvaluationStatus.Uid == projectEvaluationStatusUid);

            return query;
        }

        /// <summary>Determines whether [is project finished].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsProjectFinished(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => pbe.Project.FinishDate.HasValue);

            return query;
        }

        /// <summary>
        /// Determines whether [is not virtual meeting].
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNotVirtualMeeting(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.IsVirtualMeeting);

            return query;
        }

        /// <summary>Determines whether [is negotiation unscheduled].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNegotiationUnscheduled(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.Negotiations.Any()
                                       || pbe.Negotiations.All(n => n.IsDeleted));

            return query;
        }

        /// <summary>Determines whether [is project finish date between evaluation period] [the specified edition project evaluation start date].</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionProjectEvaluationStartDate">The edition project evaluation start date.</param>
        /// <param name="editionProjectEvaluationEndDate">The edition project evaluation end date.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsProjectFinishDateBetweenEvaluationPeriod(this IQueryable<ProjectBuyerEvaluation> query, DateTimeOffset editionProjectEvaluationStartDate, DateTimeOffset editionProjectEvaluationEndDate)
        {
            query = query.Where(pbe => pbe.Project.FinishDate >= editionProjectEvaluationStartDate && pbe.Project.FinishDate <= editionProjectEvaluationEndDate);

            return query;
        }

        /// <summary>Determines whether [is buyer evaluation email pending].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsBuyerEvaluationEmailPending(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.BuyerEmailSendDate.HasValue
                                       && !pbe.BuyerAttendeeOrganization.IsDeleted
                                       && pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators.Any(aoc => !aoc.IsDeleted
                                                                                                                     && !aoc.AttendeeCollaborator.IsDeleted
                                                                                                                     && !aoc.AttendeeCollaborator.Collaborator.IsDeleted));

            return query;
        }

        /// <summary>Determines whether [is negotiation scheduled].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNegotiationScheduled(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => pbe.Negotiations.Any(n => !n.IsDeleted));

            return query;
        }

        /// <summary>Determines whether [is negotiation not scheduled].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNegotiationNotScheduled(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.Negotiations.Any() || pbe.Negotiations.All(n => n.IsDeleted));

            return query;
        }

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNotDeleted(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.IsDeleted && !pbe.Project.IsDeleted && !pbe.Project.SellerAttendeeOrganization.IsDeleted);

            return query;
        }

        /// <summary>
        /// Finds the by buyer attendee organization identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerAttendeeOrganizationUid">The buyer attendee organization identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByBuyerAttendeeOrganizationUid(this IQueryable<ProjectBuyerEvaluation> query, Guid buyerAttendeeOrganizationUid)
        {
            query = query.Where(pbe => pbe.BuyerAttendeeOrganization.Uid == buyerAttendeeOrganizationUid);

            return query;
        }

        /// <summary>
        /// Finds the by buyer attendee organization identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="buyerOrganizationUid">The buyer attendee organization identifier.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByEditionAndBuyerOrganizationUid(this IQueryable<ProjectBuyerEvaluation> query, Guid buyerOrganizationUid, int editionId)
        {
            query = query.Where(pbe => pbe.BuyerAttendeeOrganization.EditionId == editionId
                                        && pbe.BuyerAttendeeOrganization.Organization.Uid == buyerOrganizationUid);

            return query;
        }

        /// <summary>
        /// Finds the by project identifier.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="projectId">The project identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByProjectId(this IQueryable<ProjectBuyerEvaluation> query, int projectId)
        {
            query = query.Where(pbe => pbe.ProjectId == projectId);

            return query;
        }
    }

    #endregion

    /// <summary>ProjectBuyerEvaluationRepository</summary>
    public class ProjectBuyerEvaluationRepository : Repository<Context.PlataformaRio2CContext, ProjectBuyerEvaluation>, IProjectBuyerEvaluationRepository
    {
        /// <summary>Initializes a new instance of the <see cref="ProjectBuyerEvaluationRepository"/> class.</summary>
        /// <param name="context">The context.</param>
        public ProjectBuyerEvaluationRepository(Context.PlataformaRio2CContext context)
            : base(context)
        {
        }

        /// <summary>Gets the base query.</summary>
        /// <param name="readonly">if set to <c>true</c> [readonly].</param>
        /// <returns></returns>
        private IQueryable<ProjectBuyerEvaluation> GetBaseQuery(bool @readonly = false)
        {
            var consult = this.dbSet
                                .IsNotDeleted();

            return @readonly
                        ? consult.AsNoTracking()
                        : consult;
        }

        /// <summary>Finds all buyer email dtos asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionProjectEvaluationStartDate">The edition project evaluation start date.</param>
        /// <param name="editionProjectEvaluationEndDate">The edition project evaluation end date.</param>
        /// <returns></returns>
        public async Task<List<ProjectBuyerEvaluationEmailDto>> FindAllBuyerEmailDtosAsync(
            int editionId,
            DateTimeOffset editionProjectEvaluationStartDate,
            DateTimeOffset editionProjectEvaluationEndDate)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .IsBuyerEvaluationEmailPending()
                                .IsProjectFinished()
                                .IsProjectFinishDateBetweenEvaluationPeriod(editionProjectEvaluationStartDate, editionProjectEvaluationEndDate);

            return await query
                                .Select(pbe => new ProjectBuyerEvaluationEmailDto
                                {
                                    ProjectBuyerEvaluation = pbe,
                                    Project = pbe.Project,
                                    SellerAttendeeOrganization = pbe.Project.SellerAttendeeOrganization,
                                    SellerOrganization = pbe.Project.SellerAttendeeOrganization.Organization,
                                    EmailRecipientsDtos = pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators
                                                                .Where(aoc => !aoc.IsDeleted
                                                                                && !aoc.AttendeeOrganization.IsDeleted
                                                                                && !aoc.AttendeeCollaborator.IsDeleted
                                                                                && !aoc.AttendeeCollaborator.Collaborator.IsDeleted
                                                                                && aoc.AttendeeCollaborator.AttendeeCollaboratorTypes.Any(act => act.CollaboratorType.Uid == CollaboratorType.PlayerExecutiveAudiovisual.Uid)
                                                                                && !aoc.AttendeeCollaborator.Collaborator.User.UserUnsubscribedLists.Any(uul => !uul.IsDeleted
                                                                                                                                                                && uul.SubscribeList.Code == SubscribeList.ProjectBuyerEvaluationEmail.Code))
                                                                .Select(aoc => new EmailRecipientDto
                                                                {
                                                                    RecipientUser = aoc.AttendeeCollaborator.Collaborator.User,
                                                                    RecipientCollaborator = aoc.AttendeeCollaborator.Collaborator,
                                                                    RecipientLanguage = aoc.AttendeeCollaborator.Collaborator.User.UserInterfaceLanguage
                                                                })
                                })
                                .ToListAsync();
        }

        /// <summary>Finds all for generate negotiations asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<ProjectBuyerEvaluation>> FindAllForGenerateNegotiationsAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished()
                                .Include(pbe => pbe.Project)
                                .Include(pbe => pbe.Project.SellerAttendeeOrganization)
                                .Include(pbe => pbe.Project.SellerAttendeeOrganization.AttendeeOrganizationCollaborators)
                                .Include(pbe => pbe.BuyerAttendeeOrganization)
                                .Include(pbe => pbe.BuyerAttendeeOrganization.AttendeeOrganizationCollaborators);

            return await query
                            .ToListAsync();
        }

        /// <summary>Finds the unscheduled widget dto asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<List<ProjectBuyerEvaluationDto>> FindUnscheduledWidgetDtoAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished()
                                .IsNegotiationUnscheduled()
                                .Select(pbe => new ProjectBuyerEvaluationDto
                                {
                                    ProjectBuyerEvaluation = pbe,
                                    BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                    {
                                        AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                        Organization = pbe.BuyerAttendeeOrganization.Organization
                                    },
                                    ProjectDto = new ProjectDto
                                    {
                                        Project = pbe.Project,
                                        SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                        {
                                            AttendeeOrganization = pbe.Project.SellerAttendeeOrganization,
                                            Organization = pbe.Project.SellerAttendeeOrganization.Organization
                                        },
                                        ProjectTitleDtos = pbe.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                        {
                                            ProjectTitle = t,
                                            Language = t.Language
                                        })
                                    }
                                });

            return await query
                            .OrderBy(ped => ped.BuyerAttendeeOrganizationDto.Organization.TradeName)
                            .ToListAsync();
        }

        /// <summary>Counts the negotiation scheduled asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountNegotiationScheduledAsync(int editionId, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished()
                                .IsNegotiationScheduled();

            return await query.CountAsync();
        }

        /// <summary>Counts the negotiation not scheduled asynchronous.</summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="showAllEditions">if set to <c>true</c> [show all editions].</param>
        /// <returns></returns>
        public async Task<int> CountNegotiationNotScheduledAsync(int editionId, bool showAllEditions = false)
        {
            var query = this.GetBaseQuery()
                                .FindByEditionId(editionId, showAllEditions)
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .IsProjectFinished()
                                .IsNegotiationNotScheduled();

            return await query.CountAsync();
        }

        /// <summary>
        /// Finds the dto asynchronous.
        /// </summary>
        /// <param name="projectBuyerEvaluationUid">The project buyer evaluation uid.</param>
        /// <returns></returns>
        public async Task<ProjectBuyerEvaluationDto> FindDtoAsync(Guid projectBuyerEvaluationUid)
        {
            var query = this.GetBaseQuery()
                               .FindById(projectBuyerEvaluationUid)
                               .Select(pbe => new ProjectBuyerEvaluationDto
                               {
                                   ProjectBuyerEvaluation = pbe,
                                   BuyerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                   {
                                       AttendeeOrganization = pbe.BuyerAttendeeOrganization,
                                       Organization = pbe.BuyerAttendeeOrganization.Organization
                                   },
                                   ProjectDto = new ProjectDto
                                   {
                                       Project = pbe.Project,
                                       SellerAttendeeOrganizationDto = new AttendeeOrganizationDto
                                       {
                                           AttendeeOrganization = pbe.Project.SellerAttendeeOrganization,
                                           Organization = pbe.Project.SellerAttendeeOrganization.Organization
                                       },
                                       ProjectTitleDtos = pbe.Project.ProjectTitles.Where(t => !t.IsDeleted).Select(t => new ProjectTitleDto
                                       {
                                           ProjectTitle = t,
                                           Language = t.Language
                                       })
                                   }
                               });

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Counts the project buyer evaluations accepted by buyer attendee organization uid asynchronous.
        /// </summary>
        /// <param name="buyerAttendeeOrganizationUid">The buyer attendee organization uid.</param>
        /// <returns></returns>
        public async Task<int> CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUidAsync(Guid buyerAttendeeOrganizationUid)
        {
            var query = this.GetBaseQuery()
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .FindByBuyerAttendeeOrganizationUid(buyerAttendeeOrganizationUid)
                                .IsProjectFinished()
                                .IsNotVirtualMeeting(); // Consider only presential negotiations in count

            return await query.CountAsync();
        }

        /// <summary>
        /// Counts the negotiation accepted by buyer attendee organization uid asynchronous.
        /// </summary>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<int> CountAcceptedProjectBuyerEvaluationsByEditionIdAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .FindByProjectEvaluationStatusUid(ProjectEvaluationStatus.Accepted.Uid)
                                .FindByEditionId(editionId)
                                .IsProjectFinished()
                                .IsNotVirtualMeeting(); // Consider only presential negotiations in count

            return await query.CountAsync();
        }

        /// <summary>
        /// Finds the by project identifier and buyer attendee organization identifier asynchronous.
        /// </summary>
        /// <param name="projectId">The project identifier.</param>
        /// <param name="buyerOrganizationUid">The buyer organization uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        public async Task<ProjectBuyerEvaluation> FindByProjectIdAndBuyerOrganizationUidAsync(int projectId, Guid buyerOrganizationUid, int editionId)
        {
            var query = this.GetBaseQuery()
                            .FindByProjectId(projectId)
                            .FindByEditionAndBuyerOrganizationUid(buyerOrganizationUid, editionId);

            return await query.FirstOrDefaultAsync();
        }
    }
}