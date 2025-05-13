// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 13-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 13-04-2024
// ***********************************************************************
// <copyright file="CreateAvailabilityCommandHandler.cs" company="Softo">
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateAvailabilityCommandHandler</summary>
    public class CreateAvailabilityCommandHandler : BaseCommandHandler, IRequestHandler<CreateAvailability, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAvailabilityCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public CreateAvailabilityCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(eventBus, uow)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>
        /// Handles the specified create logistic.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateAvailability cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => ac.Uid == cmd.AttendeeCollaboratorUid && !ac.IsDeleted);
            if (attendeeCollaborator?.AvailabilityBeginDate.HasValue == true || attendeeCollaborator?.AvailabilityEndDate.HasValue == true)
            {
                this.ValidationResult.Add(new ValidationError(Messages.ThereIsAlreadyAvailabilityRegisteredForThisParticipant));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult.Errors?.FirstOrDefault().Message, "AttendeeCollaboratorUid");
                return this.AppValidationResult;
            }

            attendeeCollaborator.UpdateAvailability(cmd.AvailabilityBeginDate, cmd.AvailabilityEndDate, cmd.UserId);

            if (!attendeeCollaborator.IsValid())
            {
                this.AppValidationResult.Add(attendeeCollaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.attendeeCollaboratorRepo.Update(attendeeCollaborator);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = attendeeCollaborator;

            return this.AppValidationResult;
        }
    }
}