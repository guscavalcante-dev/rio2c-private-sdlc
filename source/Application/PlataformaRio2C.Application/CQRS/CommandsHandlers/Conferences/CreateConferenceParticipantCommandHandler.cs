// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-03-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="CreateConferenceParticipantCommandHandler.cs" company="Softo">
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
    /// <summary>CreateConferenceParticipantCommandHandler</summary>
    public class CreateConferenceParticipantCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<CreateConferenceParticipant, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IConferenceParticipantRoleRepository conferenceParticipantRoleRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipantCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="conferenceParticipantRoleRepository">The conference participant role repository.</param>
        public CreateConferenceParticipantCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IConferenceParticipantRoleRepository conferenceParticipantRoleRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.conferenceParticipantRoleRepo = conferenceParticipantRoleRepository;
        }

        /// <summary>Handles the specified create conference participant.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateConferenceParticipant cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conference = await this.GetConferenceByUid(cmd.ConferenceUid ?? Guid.Empty);
            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac =>
                                        !ac.IsDeleted &&
                                        ac.Collaborator.Uid == cmd.CollaboratorUid &&
                                        ac.EditionId == cmd.EditionId);

            //TODO: Pegar todas as palestras desse attendeeCollaborator!
            ASDFASDFASDFA

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            conference.CreateConferenceParticipant(
                attendeeCollaborator,
                await this.conferenceParticipantRoleRepo.GetAsync(cpr => !cpr.IsDeleted && cpr.Uid == cmd.ConferenceParticipanteRoleUid),
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