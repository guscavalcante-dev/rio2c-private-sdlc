// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 15-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 15-04-2024
// ***********************************************************************
// <copyright file="UpdateAvailabilityCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateAvailabilityCommandHandler</summary>
    public class UpdateAvailabilityCommandHandler : BaseCommandHandler, IRequestHandler<UpdateAvailability, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAvailabilityCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public UpdateAvailabilityCommandHandler(
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
        public async Task<AppValidationResult> Handle(UpdateAvailability cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();
            
            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => ac.Uid == cmd.AttendeeCollaboratorUid && !ac.IsDeleted);

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
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