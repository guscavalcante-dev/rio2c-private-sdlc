// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="CreateVerticalTrackCommandHandler.cs" company="Softo">
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
    /// <summary>CreateVerticalTrackCommandHandler</summary>
    public class CreateVerticalTrackCommandHandler : VerticalTrackBaseCommandHandler, IRequestHandler<CreateVerticalTrack, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateVerticalTrackCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="verticakTrackRepository">The verticak track repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateVerticalTrackCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IVerticalTrackRepository verticakTrackRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, verticakTrackRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create vertical track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateVerticalTrack cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var verticalTrackUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var verticalTrack = new VerticalTrack(
                verticalTrackUid,
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Names?.Select(d => new VerticalTrackName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!verticalTrack.IsValid())
            {
                this.AppValidationResult.Add(verticalTrack.ValidationResult);
                return this.AppValidationResult;
            }

            this.VerticalTrackRepo.Create(verticalTrack);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = verticalTrack;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}