// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-03-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 03/25/2025
// ***********************************************************************
// <copyright file="CreateConferenceParticipantCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Application.CQRS.Queries;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateConferenceParticipantCommandHandler</summary>
    public class CreateConferenceParticipantCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<CreateConferenceParticipant, AppValidationResult>
    {
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly IConferenceParticipantRoleRepository conferenceParticipantRoleRepo;
        private readonly INegotiationRepository negotiationRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateConferenceParticipantCommandHandler" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="conferenceParticipantRoleRepository">The conference participant role repository.</param>
        /// <param name="negotiationRepository">The negotiation repository.</param>
        public CreateConferenceParticipantCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            IConferenceParticipantRoleRepository conferenceParticipantRoleRepository,
            INegotiationRepository negotiationRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.conferenceParticipantRoleRepo = conferenceParticipantRoleRepository;
            this.negotiationRepo = negotiationRepository;
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

            #region Initial validations
            if (attendeeCollaborator.AvailabilityBeginDate != null && attendeeCollaborator.AvailabilityEndDate != null)
            {
                // Check if the conference period is WITHIN the speaker's availability
                if (!(attendeeCollaborator.AvailabilityBeginDate.Value <= conference.StartDate.Value.ToUniversalTime()
                      && attendeeCollaborator.AvailabilityEndDate.Value >= conference.EndDate.Value.ToUniversalTime()))
                {
                    this.ValidationResult.Add(new ValidationError(
                        string.Format(Messages.CollaboratorNotAvailableForConference,
                            attendeeCollaborator.Collaborator.GetStageNameOrBadgeOrFullName(),
                            attendeeCollaborator.AvailabilityBeginDate.Value.ToBrazilTimeZone().ToString("dd/MM/yyyy HH:mm"),
                            attendeeCollaborator.AvailabilityEndDate.Value.ToBrazilTimeZone().ToString("dd/MM/yyyy HH:mm")),
                        new[] { "ToastrError" }
                    ));
                }
            }


            if (conference.StartDate.HasValue && conference.EndDate.HasValue)
            {
                var scheduledNegotiationsAtThisTime = await this.negotiationRepo.FindAllScheduledNegotiationsDtosAsync(cmd.EditionId.Value, attendeeCollaborator.Id, conference.StartDate.Value, conference.EndDate.Value);
                if (scheduledNegotiationsAtThisTime.Count > 0)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(
                        Messages.HasBusinessRoundScheduled,
                        Labels.TheM,
                        Labels.Executive,
                        ($"{conference.StartDate.Value.ToBrazilTimeZone().ToShortDateString()} {conference.StartDate.Value.ToBrazilTimeZone().ToShortTimeString()} - {conference.EndDate.Value.ToBrazilTimeZone().ToShortTimeString()}")),
                            new string[] { "ToastrError" }));
                }
            }

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