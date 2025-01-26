// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-26-2025
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-26-2025
// ***********************************************************************
// <copyright file="RefuseMusicBusinessRoundProjectEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Interfaces.Repositories.Music.Projects;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RefuseMusicBusinessRoundProjectEvaluationCommandHandler</summary>
    public class RefuseMusicBusinessRoundProjectEvaluationCommandHandler : BaseMusicBusinessRoundProjectCommandHandler, IRequestHandler<RefuseMusicBusinessRoundProjectEvaluation, AppValidationResult>
    {
        private readonly IProjectEvaluationStatusRepository projectEvaluationStatusRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="RefuseMusicBusinessRoundProjectEvaluationCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="musicBusinessRoundProjectRepository">The project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="projectEvaluationRefuseReasonRepository">The project evaluation refuse reason repository.</param>
        public RefuseMusicBusinessRoundProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IMusicBusinessRoundProjectRepository musicBusinessRoundProjectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, musicBusinessRoundProjectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepository;
        }

        /// <summary>Handles the specified refuse project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(RefuseMusicBusinessRoundProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetProjectByUid(cmd.MusicBusinessRoundProjectUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var projectBuyerEvaluation = project.RefuseMusicBusinessRoundProjectBuyerEvaluation(
                cmd.AttendeeOrganizationUid.Value,
                await this.projectEvaluationRefuseReasonRepo.FindByUidAsync(cmd.ProjectEvaluationRefuseReasonUid ?? Guid.Empty),
                cmd.Reason,
                await this.projectEvaluationStatusRepo.FindAllAsync(),
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