// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-30-2025
// ***********************************************************************
// <copyright file="UpdateConferenceMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateConferenceMainInformationCommandHandler</summary>
    public class UpdateConferenceMainInformationCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<UpdateConferenceMainInformation, AppValidationResult>
    {
        private readonly IEditionEventRepository editionEventRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IRoomRepository roomRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        public UpdateConferenceMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IEditionEventRepository editionEventRepository,
            ILanguageRepository languageRepository,
            IRoomRepository roomRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.editionEventRepo = editionEventRepository;
            this.languageRepo = languageRepository;
            this.roomRepo = roomRepository;
        }

        /// <summary>Handles the specified update conference main information.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateConferenceMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conference = await this.GetConferenceByUid(cmd.ConferenceUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            //Availability Check
            var startDate = cmd.Date?.Add(TimeSpan.Parse(cmd.StartTime ?? "00:00"));
            var endDate = cmd.Date?.Add(TimeSpan.Parse(cmd.EndTime ?? "00:00"));

            if (!await ValidateParticipantsAvailabilityAsync(cmd.ConferenceUid, cmd.EditionId, startDate, endDate))
            {
                this.AppValidationResult.Add(Messages.ParticipantsUnavailables, "Date");
                return this.AppValidationResult;
            }

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            conference.UpdateMainInformation(
                await this.editionEventRepo.GetAsync(cmd.EditionEventUid ?? Guid.Empty),
                cmd.Date,
                cmd.StartTime,
                cmd.EndTime,
                cmd.RoomUid.HasValue ? await this.roomRepo.FindByUidAsync(cmd.RoomUid.Value) : null,
                cmd.Titles?.Select(d => new ConferenceTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Synopsis?.Select(d => new ConferenceSynopsis(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Dynamics?.Select(d => new ConferenceDynamic(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);

            if (!conference.IsValid())
            {
                this.AppValidationResult.Add(conference.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceRepo.Update(conference);
            this.Uow.SaveChanges();

            return this.AppValidationResult;
        }

        private async Task<bool> ValidateParticipantsAvailabilityAsync(Guid conferenceUid, int? editionId, DateTime? newStart, DateTime? newEnd)
        {
            if (!newStart.HasValue || !newEnd.HasValue)
                return true;

            var conferenceDto = await this.ConferenceRepo.FindParticipantsWidgetDtoAsync(conferenceUid, editionId.Value);
            var participants = conferenceDto?.ConferenceParticipantDtos;

            if (participants == null || !participants.Any())
                return true;

            foreach (var participant in participants)
            {
                var availabilityStart = participant.ConferenceParticipant?.AttendeeCollaborator.AvailabilityBeginDate;
                var availabilityEnd = participant.ConferenceParticipant?.AttendeeCollaborator.AvailabilityEndDate;

                // ignores speakers with no availability registered.
                if (!availabilityStart.HasValue || !availabilityEnd.HasValue)
                    continue;

                // availability  date check
                bool isAvailable = availabilityStart.Value <= newStart && availabilityEnd.Value >= newEnd;

                if (!isAvailable)
                    return false;
            }

            return true;
        }



    }
}