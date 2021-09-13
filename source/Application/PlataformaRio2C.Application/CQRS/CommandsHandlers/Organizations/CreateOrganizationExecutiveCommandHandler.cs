// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 09-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-13-2021
// ***********************************************************************
// <copyright file="CreateOrganizationExecutiveCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateOrganizationExecutiveCommandHandler</summary>
    public class CreateOrganizationExecutiveCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<CreateOrganizationExecutive, AppValidationResult>
    {
        private readonly IAttendeeOrganizationRepository attendeeOrganizationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrganizationExecutiveCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="attendeeOrganizationRepository">The attendee organization repository.</param>
        public CreateOrganizationExecutiveCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IAttendeeOrganizationRepository attendeeOrganizationRepository
            )
            : base(eventBus, uow, collaboratorRepository)
        {
            this.attendeeOrganizationRepo = attendeeOrganizationRepository;
        }

        /// <summary>Handles the specified create tiny collaborator.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateOrganizationExecutive cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Initial validations

            var attendeeOrganization = await this.attendeeOrganizationRepo.FindByUidAsync(cmd.AttendeeOrganizationUid ?? Guid.Empty);
            if (attendeeOrganization == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Company, Labels.FoundF)));
            }

            var collaboratorDto = await this.CollaboratorRepo.FindDtoByUidAndByEditionIdAsync(cmd.CollaboratorUid ?? Guid.Empty, cmd.EditionId ?? 0, cmd.UserInterfaceLanguage);
            if (collaboratorDto == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundF)));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            attendeeOrganization.SynchronizeAttendeeOrganizationCollaborators(new List<AttendeeOrganization> { attendeeOrganization }, collaboratorDto.EditionAttendeeCollaborator, false, cmd.UserId);

            if (!attendeeOrganization.IsValid())
            {
                this.AppValidationResult.Add(attendeeOrganization.ValidationResult);
                return this.AppValidationResult;
            }

            this.attendeeOrganizationRepo.Update(attendeeOrganization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = attendeeOrganization;

            return this.AppValidationResult;
        }
    }
}