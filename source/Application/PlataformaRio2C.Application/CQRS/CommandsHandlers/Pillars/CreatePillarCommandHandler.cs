// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreatePillarCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreatePillarCommandHandler</summary>
    public class CreatePillarCommandHandler : PillarBaseCommandHandler, IRequestHandler<CreatePillar, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreatePillarCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="pillarRepository">The pillar repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreatePillarCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPillarRepository pillarRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, pillarRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create pillar.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreatePillar cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var pillarUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var pillar = new Pillar(
                pillarUid,
                await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                cmd.Names?.Select(d => new PillarName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Color,
                cmd.UserId);
            if (!pillar.IsValid())
            {
                this.AppValidationResult.Add(pillar.ValidationResult);
                return this.AppValidationResult;
            }

            this.PillarRepo.Create(pillar);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = pillar;

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}