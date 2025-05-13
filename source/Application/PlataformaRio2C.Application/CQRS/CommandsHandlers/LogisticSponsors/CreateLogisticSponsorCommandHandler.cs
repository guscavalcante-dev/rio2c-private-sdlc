// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="CreateLogisticSponsorCommandHandler.cs" company="Softo">
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
    /// <summary>CreateLogisticSponsorCommandHandler</summary>
    public class CreateLogisticSponsorCommandHandler : LogisticSponsorBaseCommandHandler, IRequestHandler<CreateLogisticSponsor, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateLogisticSponsorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public CreateLogisticSponsorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ILogisticSponsorRepository logisticSponsorRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, logisticSponsorRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create logistic sponsor.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateLogisticSponsor cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            LogisticSponsor logisticSponsor = null;

            if (!cmd.LogisticSponsorUid.HasValue)
            {
                var logisticSponsorUid = Guid.NewGuid();

                logisticSponsor = new LogisticSponsor(
                    logisticSponsorUid,
                    await this.editionRepo.GetAsync(cmd.EditionId ?? 0),
                    cmd.Names?.Select(d => new TranslatedName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language))?.ToList(),
                    cmd.IsOther ?? false,
                    cmd.UserId);

                if (!logisticSponsor.IsValid())
                {
                    this.AppValidationResult.Add(logisticSponsor.ValidationResult);
                    return this.AppValidationResult;
                }

                this.logisticSponsorRepo.Create(logisticSponsor);
            }
            else
            {
                logisticSponsor = await this.GetLogisticSponsorByUid(cmd.LogisticSponsorUid.Value);

                #region Initial validations

                if (!this.ValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                #endregion

                logisticSponsor.Update(
                    cmd.Names?.Select(d => new TranslatedName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language))?.ToList(),
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    cmd.IsOther ?? false,
                    cmd.UserId);

                if (!logisticSponsor.IsValid())
                {
                    this.AppValidationResult.Add(logisticSponsor.ValidationResult);
                    return this.AppValidationResult;
                }

                this.logisticSponsorRepo.Update(logisticSponsor);
            }

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticSponsor;

            return this.AppValidationResult;
        }
    }
}