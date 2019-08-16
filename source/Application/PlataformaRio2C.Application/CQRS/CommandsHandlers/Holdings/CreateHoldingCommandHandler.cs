// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-15-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="CreateHoldingCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateHoldingCommandHandler</summary>
    public class CreateHoldingCommandHandler : BaseCommandHandler, IRequestHandler<CreateHolding, AppValidationResult>
    {
        private AppValidationResult appValidationResult = new AppValidationResult();
        private readonly IHoldingRepository holdingRepo;
        private readonly ILanguageRepository languageRepo;

        public CreateHoldingCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHoldingRepository holdingRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow)
        {
            this.holdingRepo = holdingRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified create holding.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateHolding cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var holdingUid = Guid.NewGuid();

            #region Other entities validations

            var existHoldingByName = this.holdingRepo.Get(e => e.Name == cmd.Name);
            if (existHoldingByName != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format("Já existe um holding com o nome '{0}'.", cmd.Name), new string[] { "Name" })); //TODO: use resources
            }

            if (!this.ValidationResult.IsValid)
            {
                this.appValidationResult.Add(this.ValidationResult);
                return this.appValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();

            var holding = new Holding(
                holdingUid,
                cmd.Name,
                cmd.UserId,
                cmd.CropperImage?.ImageFile != null,
                cmd.Descriptions?.Where(d => !string.IsNullOrEmpty(d.Value))?.Select(d => new HoldingDescription(
                    d.Value,
                    languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language,
                    cmd.UserId))?.ToList());

            if (!holding.IsValid())
            {
                this.appValidationResult.Add(holding.ValidationResult);
                return this.appValidationResult;
            }

            this.holdingRepo.Create(holding);
            this.Uow.SaveChanges();
            this.appValidationResult.Data = holding;

            if (holding.IsImageUploaded)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    holding.Uid,
                    cmd.CropperImage.ImageFile,
                    cmd.CropperImage.DataX,
                    cmd.CropperImage.DataY,
                    cmd.CropperImage.DataWidth,
                    cmd.CropperImage.DataHeight,
                    FileRepositoryPathType.HoldingImage);
            }

            return this.appValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}