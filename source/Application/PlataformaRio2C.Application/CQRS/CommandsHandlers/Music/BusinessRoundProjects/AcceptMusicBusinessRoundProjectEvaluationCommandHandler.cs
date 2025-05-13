// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-26-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2025
// ***********************************************************************
// <copyright file="AcceptMusicBusinessRoundProjectEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>AcceptMusicBusinessRoundProjectEvaluationCommandHandler</summary>
    public class AcceptMusicBusinessRoundProjectEvaluationCommandHandler : BaseMusicBusinessRoundProjectCommandHandler, IRequestHandler<AcceptMusicBusinessRoundProjectEvaluation, AppValidationResult>
    {
        private IProjectEvaluationStatusRepository projectEvaluationStatusRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceptMusicBusinessRoundProjectEvaluationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="musicBusinessRoundProjectRepository">The project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        public AcceptMusicBusinessRoundProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, musicBusinessRoundProjectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
        }

        /// <summary>Handles the specified accept project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(AcceptMusicBusinessRoundProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetMusicBusinessRoundProjectByUid(cmd.MusicBusinessRoundProjectUid ?? Guid.Empty);

            //TODO: Refactor this! Create "GetMusicMaximumAvailableSlotsByEditionId"
            //var audiovisualMaximumAvailableSlotsByEditionIdResponseDto = await CommandBus.Send(new GetAudiovisualNegotiationAvailableSlotsCountByEditionId(cmd.EditionId ?? 0));
            //var playerAcceptedProjectsCount = await CommandBus.Send(new CountAcceptedProjectBuyerEvaluationsByBuyerAttendeeOrganizationUid(cmd.AttendeeOrganizationUid ?? Guid.Empty));
            //var projectsApprovalLimitExceeded = playerAcceptedProjectsCount >= audiovisualMaximumAvailableSlotsByEditionIdResponseDto.MaximumAvailableSlotsByPlayer;
            //if (projectsApprovalLimitExceeded)
            //{
            //    cmd.PlayerAcceptedProjectsCount = playerAcceptedProjectsCount;
            //    cmd.MaximumAvailableSlotsByPlayer = audiovisualMaximumAvailableSlotsByEditionIdResponseDto.MaximumAvailableSlotsByPlayer;
            //}

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var projectBuyerEvaluation = project.AcceptMusicBusinessRoundProjectBuyerEvaluation(
                cmd.AttendeeOrganizationUid.Value,
                await this.projectEvaluationStatusRepo.FindAllAsync(),
                false,//projectsApprovalLimitExceeded,
                cmd.UserId);
            if (!projectBuyerEvaluation.IsEvaluationValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.MusicBusinessRoundProjectRepo.Update(project);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}