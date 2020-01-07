// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="TrackBaseCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>TrackBaseCommandHandler</summary>
    public class TrackBaseCommandHandler : BaseCommandHandler
    {
        protected readonly ITrackRepository TrackRepo;

        /// <summary>Initializes a new instance of the <see cref="TrackBaseCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="trackRepository">The track repository.</param>
        public TrackBaseCommandHandler(IMediator eventBus, IUnitOfWork uow, ITrackRepository trackRepository)
            : base(eventBus, uow)
        {
            this.TrackRepo = trackRepository;
        }

        /// <summary>Gets the track by uid.</summary>
        /// <param name="trackUid">The track uid.</param>
        /// <returns></returns>
        public async Task<Track> GetTrackByUid(Guid trackUid)
        {
            var track = await this.TrackRepo.GetAsync(trackUid);
            if (track == null || track.IsDeleted)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Track, Labels.FoundF), new string[] { "Track" }));
            }

            return track;
        }
    }
}