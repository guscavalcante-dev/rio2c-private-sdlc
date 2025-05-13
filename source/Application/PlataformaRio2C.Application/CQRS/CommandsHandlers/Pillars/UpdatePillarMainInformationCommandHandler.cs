// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="UpdatePillarMainInformationCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdatePillarMainInformationCommandHandler</summary>
    public class UpdatePillarMainInformationCommandHandler : PillarBaseCommandHandler, IRequestHandler<UpdatePillarMainInformation, AppValidationResult>
    {
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdatePillarMainInformationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="pillarRepository">The pillar repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdatePillarMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IPillarRepository pillarRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, pillarRepository)
        {
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update pillar main information.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdatePillarMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var pillar = await this.GetPillarByUid(cmd.PillarUid);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            pillar.UpdateMainInformation(
                cmd.Names?.Select(d => new PillarName(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.Color,
                cmd.UserId);
            if (!pillar.IsValid())
            {
                this.AppValidationResult.Add(pillar.ValidationResult);
                return this.AppValidationResult;
            }

            this.PillarRepo.Update(pillar);
            this.Uow.SaveChanges();

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}