// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-27-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateCollaboratorCommandHandler</summary>
    public class UpdateCollaboratorCommandHandler : BaseCollaboratorCommandHandler, IRequestHandler<UpdateCollaborator, AppValidationResult>
    {
        private readonly IHoldingRepository holdingRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICountryRepository countryRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="collaboratorRepository">The collaborator repository.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public UpdateCollaboratorCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            ICollaboratorRepository collaboratorRepository,
            IHoldingRepository holdingRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            ILanguageRepository languageRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, collaboratorRepository)
        {
            this.holdingRepo = holdingRepository;
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.languageRepo = languageRepository;
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified update collaborator.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateCollaborator cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var collaborator = await this.GetCollaboratorByUid(cmd.CollaboratorUid);

            //#region Initial validations

            //var existingOrganizationByName = this.OrganizationRepo.Get(o => o.Name == cmd.Name 
            //                                                                && o.Holding.Uid == cmd.HoldingUid 
            //                                                                && o.Uid != cmd.OrganizationUid
            //                                                                && !o.IsDeleted);
            //if (existingOrganizationByName != null)
            //{
            //    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.Name), new string[] { "Name" }));
            //}

            //if (!this.ValidationResult.IsValid)
            //{
            //    this.AppValidationResult.Add(this.ValidationResult);
            //    return this.AppValidationResult;
            //}

            //#endregion

            // Before update values
            var beforeImageUploadDate = collaborator.ImageUploadDate;

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var holding = await this.holdingRepo.GetAsync(cmd.HoldingUid ?? Guid.Empty);
            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var organizationType = await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty);

            collaborator.Update(
                //await this.holdingRepo.GetAsync(cmd.HoldingUid ?? Guid.Empty),
                null,
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty),
                cmd.FirstName,
                cmd.LastNames,
                cmd.Badge,
                cmd.Email,
                cmd.PhoneNumber,
                cmd.CellPhone,
                await this.countryRepo.GetAsync(cmd.Address?.CountryUid ?? Guid.Empty),
                cmd.Address?.StateUid,
                cmd.Address?.StateName,
                cmd.Address?.CityUid,
                cmd.Address?.CityName,
                cmd.Address?.Address1,
                cmd.Address?.Address2,
                cmd.Address?.AddressZipCode,
                true, //TODO: get AddressIsManual from form
                cmd.CropperImage?.ImageFile != null,
                cmd.JobTitles?.Select(d => new CollaboratorJobTitle(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.MiniBios?.Select(d => new CollaboratorMiniBio(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);
            if (!collaborator.IsValid())
            {
                this.AppValidationResult.Add(collaborator.ValidationResult);
                return this.AppValidationResult;
            }

            this.CollaboratorRepo.Update(collaborator);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = collaborator;

            // Update images
            if (cmd.CropperImage?.ImageFile != null)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    collaborator.Uid,
                    cmd.CropperImage.ImageFile,
                    cmd.CropperImage.DataX,
                    cmd.CropperImage.DataY,
                    cmd.CropperImage.DataWidth,
                    cmd.CropperImage.DataHeight,
                    FileRepositoryPathType.UserImage);
            }
            // Delete images
            else if (cmd.CropperImage?.IsImageDeleted == true && beforeImageUploadDate.HasValue)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(collaborator.Uid, FileRepositoryPathType.UserImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}