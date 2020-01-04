// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-04-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-04-2020
// ***********************************************************************
// <copyright file="UpdateConferenceTracksCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>UpdateConferenceTracksCommandHandler</summary>
    public class UpdateConferenceTracksCommandHandler : ConferenceBaseCommandHandler, IRequestHandler<UpdateConferenceTracks, AppValidationResult>
    {
        private readonly IVerticalTrackRepository verticalTrackRepo;
        private readonly IHorizontalTrackRepository horizontalTrackRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceTracksCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="conferenceRepository">The conference repository.</param>
        /// <param name="verticalTrackRepository">The vertical track repository.</param>
        /// <param name="horizontalTrackRepository">The horizontal track repository.</param>
        public UpdateConferenceTracksCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IConferenceRepository conferenceRepository,
            IVerticalTrackRepository verticalTrackRepository,
            IHorizontalTrackRepository horizontalTrackRepository)
            : base(eventBus, uow, conferenceRepository)
        {
            this.verticalTrackRepo = verticalTrackRepository;
            this.horizontalTrackRepo = horizontalTrackRepository;
        }

        /// <summary>Handles the specified update conference tracks.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateConferenceTracks cmd, CancellationToken cancellationToken)
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

            conference.UpdateTracks(
                cmd.VerticalTrackUids?.Any() == true ? await this.verticalTrackRepo.FindAllByUidsAsync(cmd.VerticalTrackUids) : new List<VerticalTrack>(),
                cmd.HorizontalTrackUids?.Any() == true ? await this.horizontalTrackRepo.FindAllByUidsAsync(cmd.HorizontalTrackUids) : new List<HorizontalTrack>(),
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