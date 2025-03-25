// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-04-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="AcceptProjectEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>AcceptProjectEvaluationCommandHandler</summary>
    public class AcceptProjectEvaluationCommandHandler : BaseProjectCommandHandler, IRequestHandler<AcceptProjectEvaluation, AppValidationResult>
    {
        private IProjectEvaluationStatusRepository projectEvaluationStatusRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptProjectEvaluationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        public AcceptProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
        }

        /// <summary>Handles the specified accept project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(AcceptProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetProjectByUid(cmd.ProjectUid ?? Guid.Empty);

            #region Initial validations

            var maximumAvailableSlotsByEditionIdResponseDto = await CommandBus.Send(new GetAudiovisualNegotiationAvailableSlotsCountByEditionId(cmd.EditionId ?? 0));
            var playerAcceptedProjectsCount = await CommandBus.Send(new CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUid(cmd.AttendeeOrganizationUid ?? Guid.Empty));
            var projectsApprovalLimitExceeded = playerAcceptedProjectsCount >= maximumAvailableSlotsByEditionIdResponseDto.MaximumSlotsByPlayer;
            if (projectsApprovalLimitExceeded)
            {
                cmd.PlayerAcceptedProjectsCount = playerAcceptedProjectsCount;
                cmd.AvailableSlotsByPlayer = maximumAvailableSlotsByEditionIdResponseDto.MaximumSlotsByPlayer;

                // This line is responsible to block the project acceptance when Player exceeds the maximum of projects approved by player
                // If you need, just uncomment this and project acceptance using the "ProjectBuyerEvaluation.IsVirtualMeeting" parameter will be blocked
                // this.ValidationResult.Add(new ValidationError(string.Format(Messages.YouReachedProjectsApprovalLimit, maximumAvailableSlotsByEditionIdResponseDto.MaximumSlotsByPlayer), new string[] { "ToastrError" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var projectBuyerEvaluation = project.AcceptProjectBuyerEvaluation(
                cmd.AttendeeOrganizationUid.Value, 
                await this.projectEvaluationStatusRepo.FindAllAsync(),
                projectsApprovalLimitExceeded,
                cmd.UserId);
            if (!projectBuyerEvaluation.IsEvaluationValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectRepo.Update(project);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}