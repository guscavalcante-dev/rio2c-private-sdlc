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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>UpdateHoldingCommandHandler</summary>
    public class UpdateHoldingCommandHandler : BaseHoldingCommandHandler, IRequestHandler<UpdateHolding, AppValidationResult>
    {
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
            : base(eventBus, uow, holdingRepository)
        {
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

            var holding = await this.GetHoldingByUid(cmd.HoldingUid);

            #region Initial Validations

            var existHoldingByName = this.HoldingRepo.Get(e => e.Name == cmd.Name && e.Uid != cmd.HoldingUid);
            if (existHoldingByName != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.AHolding, Labels.TheName, cmd.Name), new string[] { "Name" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var beforeImageUploadDate = holding.ImageUploadDate;

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
                cmd.CropperImage?.ImageFile != null,
                cmd.CropperImage?.IsImageDeleted == true,
                newDescriptions,
                cmd.UserId);

            if (!holding.IsValid())
            {
                this.AppValidationResult.Add(holding.ValidationResult);
                return this.AppValidationResult;
            }

            this.HoldingRepo.Update(holding);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = holding;

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
            else if (cmd.CropperImage?.IsImageDeleted == true && beforeImageUploadDate.HasValue)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(
                    holding.Uid,
                    FileRepositoryPathType.HoldingImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}