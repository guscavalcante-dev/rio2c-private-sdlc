﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-21-2021
// ***********************************************************************
// <copyright file="DeleteProjectBuyerEvaluationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>DeleteProjectBuyerEvaluationCommandHandler</summary>
    public class DeleteProjectBuyerEvaluationCommandHandler : BaseProjectCommandHandler, IRequestHandler<DeleteProjectBuyerEvaluation, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteProjectBuyerEvaluationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        /// <param name="projectRepository">The project repository.</param>
        public DeleteProjectBuyerEvaluationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeOrganizationRepository attendeeOrganizationRepository,
            IProjectRepository projectRepository)
            : base(eventBus, uow, attendeeOrganizationRepository, projectRepository)
        {
        }

        /// <summary>Handles the specified delete project buyer evaluation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteProjectBuyerEvaluation cmd, CancellationToken cancellationToken)
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

            project.DeleteProjectBuyerEvaluation(
                cmd.AttendeeOrganizationUid.HasValue ? await this.AttendeeOrganizationRepo.GetAsync(ao => ao.Uid == cmd.AttendeeOrganizationUid) : null,
                cmd.UserId,
                cmd.IsAdmin);
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