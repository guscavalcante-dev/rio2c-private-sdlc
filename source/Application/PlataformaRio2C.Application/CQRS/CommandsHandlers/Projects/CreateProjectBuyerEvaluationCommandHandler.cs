// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-14-2019
// ***********************************************************************
// <copyright file="CreateProjectBuyerEvaluationCommandHandler.cs" company="Softo">
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
    /// <summary>CreateProjectBuyerEvaluationCommandHandler</summary>
    public class CreateProjectBuyerEvaluationCommandHandler : BaseProjectCommandHandler, IRequestHandler<CreateProjectBuyerEvaluation, AppValidationResult>
    {
        private readonly IProjectEvaluationStatusRepository projectEvaluationStatusRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateProjectBuyerEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="projectRepository">The project repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectEvaluationStatusRepository">The project evaluation status repository.</param>
        public CreateProjectBuyerEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository,
            IProjectEvaluationStatusRepository projectEvaluationStatusRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
            this.projectEvaluationStatusRepo = projectEvaluationStatusRepository;
        }

        /// <summary>Handles the specified create project buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateProjectBuyerEvaluation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var project = await this.GetProjectByUid(cmd.ProjectUid ?? Guid.Empty);

            #region Initial validations

            //// Check if the trade name already exists
            //var existingOrganizationByName = this.OrganizationRepo.Get(o => o.TradeName == cmd.TradeName
            //                                                                && o.HoldingId == organization.HoldingId
            //                                                                && o.Uid != cmd.OrganizationUid
            //                                                                && !o.IsDeleted);
            //if (existingOrganizationByName != null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.TradeName), new string[] { "TradeName" }));
            //}

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            project.CreateBuyerEvaluation(
                cmd.AttendeeOrganizationUid.HasValue ? await this.AttendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                await this.projectEvaluationStatusRepo.GetAsync(pes => !pes.IsDeleted && !pes.IsEvaluated),
                cmd.UserId);
            if (!project.IsValid())
            {
                this.AppValidationResult.Add(project.ValidationResult);
                return this.AppValidationResult;
            }

            this.ProjectRepo.Update(project);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = project;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}