// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-10-2019
// ***********************************************************************
// <copyright file="RefuseProjectEvaluationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RefuseProjectEvaluationCommandHandler</summary>
    public class RefuseProjectEvaluationCommandHandler : BaseProjectCommandHandler, IRequestHandler<RefuseProjectEvaluation, AppValidationResult>
    {
        private readonly IProjectEvaluationStatusRepository projectEvaluationStatusRepo;
        private readonly IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepo;

        /// <summary>Initializes a new instance of the <see cref="RefuseProjectEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        /// <param name="projectEvaluationRefuseReasonRepository">The project evaluation refuse reason repository.</param>
        public RefuseProjectEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository,
            IProjectEvaluationRefuseReasonRepository projectEvaluationRefuseReasonRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
            this.projectEvaluationRefuseReasonRepo = projectEvaluationRefuseReasonRepository;
        }

        /// <summary>Handles the specified refuse project evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(RefuseProjectEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetProjectByUid(cmd.ProjectUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var projectBuyerEvaluation = project.RefuseProjectBuyerEvaluation(
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

            this.ProjectRepo.Update(project);
            this.Uow.SaveChanges();
            //this.AppValidationResult.Data = project;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}