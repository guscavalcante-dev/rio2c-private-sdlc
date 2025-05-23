﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-12-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorTicketCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateCollaboratorTicketCommandHandler</summary>
    public class DeleteCollaboratorTicketCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<DeleteCollaboratorTicket, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteCollaboratorTicketCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        public DeleteCollaboratorTicketCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
        }

        /// <summary>Handles the specified delete collaborator ticket.</summary>
        /// <param name="cmd">The command</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteCollaboratorTicket cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            if (cmd.Collaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.User, Labels.FoundM)));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            cmd.Collaborator.DeleteTicket(
                cmd.Edition,
                cmd.AttendeeSalesPlatformTicketType,
                cmd.CollaboratorType,
                cmd.Role,
                cmd.SalesPlatformAttendeeDto.AttendeeId,
                cmd.SalesPlatformAttendeeDto.SalesPlatformUpdateDate,
                cmd.SalesPlatformAttendeeDto.BarcodeUpdateDate,
                cmd.SalesPlatformAttendeeDto.TicketUpdateDate,
                1);
            if (!cmd.Collaborator.IsValid())
            {
                this.AppValidationResult.Add(cmd.Collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(cmd.Collaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = cmd.Collaborator;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}