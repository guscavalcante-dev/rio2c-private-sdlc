// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="CreateHorizontalTrackCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateHorizontalTrackCommandHandler</summary>
    public class CreateHorizontalTrackCommandHandler : HorizontalTrackBaseCommandHandler, IRequestHandler<CreateHorizontalTrack, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateHorizontalTrackCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="horizontalTrackRepository">The horizontal track repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateHorizontalTrackCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHorizontalTrackRepository horizontalTrackRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, horizontalTrackRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create horizontal track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateHorizontalTrack cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var horizontalTrackUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var horizontalTrack = new HorizontalTrack(
                horizontalTrackUid,
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Names?.Select(d => new HorizontalTrackName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!horizontalTrack.IsValid())
            {
                this.AppValidationResult.Add(horizontalTrack.ValidationResult);
                return this.AppValidationResult;
            }

            this.HorizontalTrackRepo.Create(horizontalTrack);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = horizontalTrack;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}