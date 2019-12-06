// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : William Sergio Almado Junior
// Created          : 12-4-2019
//
// Last Modified By : William Sergio Almado Junior
// Last Modified On : 12-4-2019
// ***********************************************************************
// <copyright file="RejectProjectBuyerEvaluationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>RejectProjectBuyerEvaluationCommandHandler</summary>
    public class RejectProjectBuyerEvaluationCommandHandler : BaseProjectCommandHandler, IRequestHandler<RejectProjectBuyerEvaluation, AppValidationResult>
    {
        private IProjectBuyerEvaluationRepository ProjectBuyerEvaluationRepository;

        /// <summary>Initializes a new instance of the <see cref="RejectProjectBuyerEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="projectBuyerEvaluationRepository">The project buyer evaluation repository.</param>
        public RejectProjectBuyerEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IProjectBuyerEvaluationRepository projectBuyerEvaluationRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.ProjectBuyerEvaluationRepository = projectBuyerEvaluationRepository;
        }

        /// <summary>Handles the specified create project buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(RejectProjectBuyerEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var projectBuyer = await this.ProjectBuyerEvaluationRepository.GetAsync(cmd.ProjectBuyerEvaluationData.Uid);

            #region Initial validations

            if (projectBuyer == null)
            {
                this.ValidationResult.Add(new ValidationError(Messages.ProjectNotFound));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            projectBuyer.Reject(cmd.ProjectBuyerEvaluationData);
            if (!projectBuyer.IsValid())
            {
                this.AppValidationResult.Add(projectBuyer.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectBuyerEvaluationRepository.Update(projectBuyer);
            this.Uow.SaveChanges();
            
            this.AppValidationResult.Data = projectBuyer;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}