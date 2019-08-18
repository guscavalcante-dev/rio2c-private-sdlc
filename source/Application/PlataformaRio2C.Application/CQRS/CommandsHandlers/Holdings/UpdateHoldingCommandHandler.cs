// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-17-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-18-2019
// ***********************************************************************
// <copyright file="UpdateHoldingCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateHoldingCommandHandler</summary>
    public class UpdateHoldingCommandHandler : BaseCommandHandler, IRequestHandler<UpdateHolding, AppValidationResult>
    {
        private AppValidationResult appValidationResult = new AppValidationResult();
        private readonly IHoldingRepository holdingRepo;
        private readonly IHoldingDescriptionRepository holdingDescriptionRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateHoldingCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        /// <param name="holdingDescriptionRepository">The holding description repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateHoldingCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IHoldingRepository holdingRepository,
            IHoldingDescriptionRepository holdingDescriptionRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow)
        {
            this.holdingRepo = holdingRepository;
            this.holdingDescriptionRepo = holdingDescriptionRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update holding.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateHolding cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            #region Other entities validations

            var existHoldingByName = this.holdingRepo.Get(e => e.Name == cmd.Name && e.Uid != cmd.HoldingUid);
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
            var holding = await this.holdingRepo.GetAsync(cmd.HoldingUid);
            var beforeIsImageUploaded = holding.IsImageUploaded;

            var newDescriptions = cmd.Descriptions?.Where(d => !string.IsNullOrEmpty(d.Value))?.Select(d => new HoldingDescription(
                d.Value,
                languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language,
                cmd.UserId))?.ToList();

            // Delete holding descriptions
            var deletedDescriptions = holding.DeleteDescriptions(newDescriptions);
            if (deletedDescriptions?.Any() == true)
            {
                this.holdingDescriptionRepo.DeleteAll(deletedDescriptions);
            }

            // Update holding
            holding.Update(
                cmd.Name,
                cmd.CropperImage?.IsImageUploaded == true,
                newDescriptions,
                cmd.UserId);

            if (!holding.IsValid())
            {
                this.appValidationResult.Add(holding.ValidationResult);
                return this.appValidationResult;
            }

            this.holdingRepo.Update(holding);
            this.Uow.SaveChanges();
            this.appValidationResult.Data = holding;

            // Update images
            if (cmd.CropperImage?.ImageFile != null)
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
            // Delete images
            else if (beforeIsImageUploaded && !holding.IsImageUploaded && cmd.CropperImage?.ImageFile == null)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(
                    holding.Uid,
                    FileRepositoryPathType.HoldingImage);
            }

            return this.appValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}