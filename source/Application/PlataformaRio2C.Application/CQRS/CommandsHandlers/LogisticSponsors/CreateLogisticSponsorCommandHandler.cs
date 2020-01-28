// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="CreateTrackCommandHandler.cs" company="Softo">
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
    /// <summary>CreateTrackCommandHandler</summary>
    public class CreateLogisticSponsorCommandHandler : LogisticSponsorBaseCommandHandler, IRequestHandler<CreateLogisticSponsors, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ILogisticSponsorRepository logisticSponsorRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateTrackCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="trackRepository">The track repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateLogisticSponsorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticSponsorRepository logisticSponsorRepo,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, logisticSponsorRepo)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.logisticSponsorRepo = logisticSponsorRepo;
        }

        /// <summary>Handles the specified create track.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticSponsors cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var trackUid = Guid.NewGuid();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var logisticSponsor = new LogisticSponsor(
                cmd.Names?.Select(d => new TranslatedName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language))?.ToList(),
                cmd.UserId);

            if (!logisticSponsor.IsValid())
            {
                this.AppValidationResult.Add(logisticSponsor.ValidationResult);
                return this.AppValidationResult;
            }

            this.repository.Create(logisticSponsor);

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticSponsor;

            return this.AppValidationResult;
        }
    }
}