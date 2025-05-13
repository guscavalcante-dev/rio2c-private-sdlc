// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateConferenceTracksAndPresentationFormatsCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateConferenceTracksAndPresentationFormatsCommandHandler</summary>
    public class UpdateConferenceTracksAndPresentationFormatsCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<UpdateConferenceTracksAndPresentationFormats, AppValidationResult>
    {
        private readonly ITrackRepository trackRepo;
        private readonly IPillarRepository pillarRepo;
        private readonly IPresentationFormatRepository presentationFormatRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracksAndPresentationFormatsCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="trackRepository">The track repository.</param>
        /// <param name="presentationFormatRepository">The presentation format repository.</param>
        public UpdateConferenceTracksAndPresentationFormatsCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            ITrackRepository trackRepository,
            IPillarRepository pillarRepository,
            IPresentationFormatRepository presentationFormatRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.trackRepo = trackRepository;
            this.pillarRepo = pillarRepository;
            this.presentationFormatRepo = presentationFormatRepository;
        }

        /// <summary>Handles the specified update conference tracks and presentation formats.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateConferenceTracksAndPresentationFormats cmd, CancellationToken cancellationToken)
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

            conference.UpdateTracksAndPresentationFormats(
                cmd.TrackUids?.Any() == true ? await this.trackRepo.FindAllByUidsAsync(cmd.TrackUids) : new List<Track>(),
                cmd.PillarUids?.Any() == true ? await this.pillarRepo.FindAllByUidsAsync(cmd.PillarUids) : new List<Pillar>(),
                cmd.PresentationFormatUids?.Any() == true ? await this.presentationFormatRepo.FindAllByUidsAsync(cmd.PresentationFormatUids) : new List<PresentationFormat>(),
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