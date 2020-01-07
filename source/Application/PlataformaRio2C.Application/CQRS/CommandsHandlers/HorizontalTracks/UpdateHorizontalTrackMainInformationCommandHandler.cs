// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateHorizontalTrackMainInformationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateHorizontalTrackMainInformationCommandHandler</summary>
    public class UpdateHorizontalTrackMainInformationCommandHandler : HorizontalTrackBaseCommandHandler, IRequestHandler<UpdateHorizontalTrackMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateHorizontalTrackMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="horizontalTrackRepository">The horizontal track repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateHorizontalTrackMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHorizontalTrackRepository horizontalTrackRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, horizontalTrackRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update horizontal track main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateHorizontalTrackMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var horizontalTrack = await this.GetHorizontalTrackByUid(cmd.HorizontalTrackUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            horizontalTrack.UpdateMainInformation(
                cmd.Names?.Select(d => new HorizontalTrackName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!horizontalTrack.IsValid())
            {
                this.AppValidationResult.Add(horizontalTrack.ValidationResult);
                return this.AppValidationResult;
            }

            this.HorizontalTrackRepo.Update(horizontalTrack);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}