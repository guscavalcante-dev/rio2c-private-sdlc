// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreateConferenceCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateConferenceCommandHandler</summary>
    public class CreateConferenceCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<CreateConference, AppValidationResult>
    {
        private readonly IEditionEventRepository editionEventRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IRoomRepository roomRepo;
        private readonly ITrackRepository trackRepo;
        private readonly IPillarRepository pillarRepo;
        private readonly IPresentationFormatRepository presentationFormatRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="editionEventRepository">The edition event repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="roomRepository">The room repository.</param>
        /// <param name="trackRepository">The track repository.</param>
        /// <param name="presentationFormatRepository">The presentation format repository.</param>
        public CreateConferenceCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IEditionEventRepository editionEventRepository,
            ILanguageRepository languageRepository,
            IRoomRepository roomRepository,
            ITrackRepository trackRepository,
            IPillarRepository pillarRepo,
        IPresentationFormatRepository presentationFormatRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.editionEventRepo = editionEventRepository;
            this.languageRepo = languageRepository;
            this.roomRepo = roomRepository;
            this.trackRepo = trackRepository;
            this.pillarRepo = pillarRepo;
            this.presentationFormatRepo = presentationFormatRepository;
        }

        /// <summary>Handles the specified create conference.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateConference cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var conferenceUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var conference = new Conference(
                conferenceUid,
                await this.editionEventRepo.GetAsync(cmd.EditionEventUid ?? Guid.Empty),
                cmd.Date.Value,
                cmd.StartTime,
                cmd.EndTime,
                cmd.RoomUid.HasValue ? await this.roomRepo.FindByUidAsync(cmd.RoomUid.Value) : null,
                cmd.Titles?.Select(d => new ConferenceTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Synopsis?.Select(d => new ConferenceSynopsis(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.TrackUids?.Any() == true ? await this.trackRepo.FindAllByUidsAsync(cmd.TrackUids) : new List<Track>(),
                cmd.PillarUids?.Any() == true ? await this.pillarRepo.FindAllByUidsAsync(cmd.PillarUids) : new List<Pillar>(),
                cmd.PresentationFormatUids?.Any() == true ? await this.presentationFormatRepo.FindAllByUidsAsync(cmd.PresentationFormatUids) : new List<PresentationFormat>(),
                cmd.UserId);
            if (!conference.IsValid())
            {
                this.AppValidationResult.Add(conference.ValidationResult);
                return this.AppValidationResult;
            }

            this.ConferenceRepo.Create(conference);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = conference;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}