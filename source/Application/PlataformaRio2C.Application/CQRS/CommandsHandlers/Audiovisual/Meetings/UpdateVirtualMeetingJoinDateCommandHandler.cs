// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 04-26-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 04-26-2023
// ***********************************************************************
// <copyright file="UpdateVirtualMeetingJoinDateCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateVirtualMeetingJoinDateCommandHandler</summary>
    public class UpdateVirtualMeetingJoinDateCommandHandler : BaseCommandHandler, IRequestHandler<UpdateVirtualMeetingJoinDate, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        public UpdateVirtualMeetingJoinDateCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(eventBus, uow)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>Handles the specified create negotiation.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateVirtualMeetingJoinDate cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => !ac.IsDeleted && ac.Collaborator.Id == cmd.UserId && ac.EditionId == cmd.EditionId);

            attendeeCollaborator?.JoinAudiovisualVirtualMeeting(cmd.UserId);

            if (!attendeeCollaborator.IsValid())
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }
           
            this.attendeeCollaboratorRepo.Update(attendeeCollaborator);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }
    }
}