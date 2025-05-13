// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-03-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="DeleteConferenceParticipantCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteConferenceParticipantCommandHandler</summary>
    public class DeleteConferenceParticipantCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<DeleteConferenceParticipant, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;

        /// <summary>Initializes a new instance of the <see cref="DeleteConferenceParticipantCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        public DeleteConferenceParticipantCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
        }

        /// <summary>Handles the specified delete conference participant.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteConferenceParticipant cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conference = await this.GetConferenceByUid(cmd.ConferenceUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            conference.DeleteConferenceParticipant(
                await this.attendeeCollaboratorRepo.GetAsync(ac => !ac.IsDeleted && ac.Collaborator.Uid == cmd.CollaboratorUid && ac.EditionId == cmd.EditionId),
                cmd.UserId);
            if (!conference.IsValid())
            {
                this.AppValidationResult.Add(conference.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceRepo.Update(conference);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}