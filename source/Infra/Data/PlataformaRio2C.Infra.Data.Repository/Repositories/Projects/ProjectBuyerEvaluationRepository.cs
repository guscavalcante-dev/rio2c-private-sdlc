// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="ProjectBuyerEvaluationRepository.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    #region Project Buyer Evaluation IQueryable Extensions

    /// <summary>
    /// ProjectBuyerEvaluationIQueryableExtensions
    /// </summary>
    internal static class ProjectBuyerEvaluationIQueryableExtensions
    {
        /// <summary>Finds the by project edition identifier.</summary>
        /// <param name="query">The query.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> FindByProjectEditionId(this IQueryable<ProjectBuyerEvaluation> query, int editionId)
        {
            query = query.Where(pbe => !pbe.Project.SellerAttendeeOrganization.IsDeleted
                                       && pbe.Project.SellerAttendeeOrganization.EditionId == editionId);

            return query;
        }

        /// <summary>Determines whether [is project not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsProjectNotDeleted(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.Project.IsDeleted);

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

        /// <summary>Determines whether [is not deleted].</summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        internal static IQueryable<ProjectBuyerEvaluation> IsNotDeleted(this IQueryable<ProjectBuyerEvaluation> query)
        {
            query = query.Where(pbe => !pbe.IsDeleted);

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
        /// <returns></returns>
        public async Task<List<ProjectBuyerEvaluationEmailDto>> FindAllBuyerEmailDtosAsync(int editionId)
        {
            var query = this.GetBaseQuery()
                                .IsBuyerEvaluationEmailPending()
                                .FindByProjectEditionId(editionId)
                                .IsProjectFinished()
                                .IsProjectNotDeleted();

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
    }
}