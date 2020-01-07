// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="DeleteTrackCommandHandler.cs" company="Softo">
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
    /// <summary>DeleteTrackCommandHandler</summary>
    public class DeleteTrackCommandHandler : TrackBaseCommandHandler, IRequestHandler<DeleteTrack, AppValidationResult>
    {
        /// <summary>Initializes a new instance of the <see cref="DeleteTrackCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="trackRepository">The track repository.</param>
        public DeleteTrackCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ITrackRepository trackRepository)
            : base(eventBus, uow, trackRepository)
        {
        }

        /// <summary>Handles the specified delete track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(DeleteTrack cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var track = await this.GetTrackByUid(cmd.TrackUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            track.Delete(cmd.UserId);
            if (!track.IsValid())
            {
                this.AppValidationResult.Add(track.ValidationResult);
                return this.AppValidationResult;
            }

            this.TrackRepo.Update(track);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}