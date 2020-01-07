// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateTrackMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>UpdateTrackMainInformationCommandHandler</summary>
    public class UpdateTrackMainInformationCommandHandler : TrackBaseCommandHandler, IRequestHandler<UpdateTrackMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateTrackMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="trackRepository">The track repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateTrackMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ITrackRepository trackRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, trackRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update track main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateTrackMainInformation cmd, CancellationToken cancellationToken)
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

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            track.UpdateMainInformation(
                cmd.Names?.Select(d => new TrackName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
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