// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-17-2020
// ***********************************************************************
// <copyright file="UpdateLogisticSponsorMainInformationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateLogisticSponsorMainInformationCommandHandler</summary>
    public class UpdateLogisticSponsorMainInformationCommandHandler : LogisticSponsorBaseCommandHandler, IRequestHandler<UpdateLogisticSponsorMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateLogisticSponsorMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="logisticSponsorRepository">The logistic sponsor repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateLogisticSponsorMainInformationCommandHandler(
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

        /// <summary>Handles the specified update logistic sponsor main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateLogisticSponsorMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var logisticSponsor = await this.GetLogisticSponsorByUid(cmd.LogisticSponsorUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            logisticSponsor.UpdateMainInformation(
                cmd.Names?.Select(d => new TranslatedName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language))?.ToList(),
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                cmd.IsAirfareTicketRequired ?? false,
                cmd.IsOtherRequired ?? false,
                cmd.IsOther ?? false,
                cmd.IsLogisticListDisplayed ?? false,
                cmd.UserId);

            if (!logisticSponsor.IsValid())
            {
                this.AppValidationResult.Add(logisticSponsor.ValidationResult);
                return this.AppValidationResult;
            }

            // Unchecking IsOtherRequired for old logistic sponsor (can only have one)
            if (cmd.IsOtherRequired == true)
            {
                var oldOtherRequiredLogisticSponsor = await this.logisticSponsorRepo.FindByOtherRequiredAsync(cmd.EditionId ?? 0);

                if (oldOtherRequiredLogisticSponsor != null && logisticSponsor.Uid != oldOtherRequiredLogisticSponsor.Uid)
                {
                    oldOtherRequiredLogisticSponsor.DisableOtherRequired(cmd.UserId);
                    this.logisticSponsorRepo.Update(oldOtherRequiredLogisticSponsor);
                }
            }

            this.logisticSponsorRepo.Update(logisticSponsor);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = logisticSponsor;
            
            return this.AppValidationResult;
        }
    }
}