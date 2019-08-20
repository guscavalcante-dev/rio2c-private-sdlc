// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-20-2019
// ***********************************************************************
// <copyright file="UpdateOrganizationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateOrganizationCommandHandler</summary>
    public class UpdateOrganizationCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganization, AppValidationResult>
    {
        private readonly IHoldingRepository holdingRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly ILanguageRepository languageRepo;

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        public UpdateOrganizationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IHoldingRepository holdingRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            ILanguageRepository languageRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.holdingRepo = holdingRepository;
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.languageRepo = languageRepository;
        }

        /// <summary>Handles the specified update organization.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganization cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organization = await this.GetOrganizationByUid(cmd.OrganizationUid);

            #region Initial validations

            var existingOrganizationByName = this.OrganizationRepo.Get(e => e.Name == cmd.Name 
                                                                            && e.Holding.Uid == cmd.HoldingUid 
                                                                            && e.Uid != cmd.OrganizationUid);
            if (existingOrganizationByName != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.Name), new string[] { "Name" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Before update values
            var beforeImageUploadDate = organization.ImageUploadDate;

            //var languageDtos = await this.languageRepo.FindAllDtosAsync();
            //var holding = await this.holdingRepo.GetAsync(cmd.HoldingUid ?? Guid.Empty);
            //var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            //var organizationType = await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty);

            //var newDescriptions = cmd.Descriptions?.Where(d => !string.IsNullOrEmpty(d.Value))?.Select(d => new OrganizationDescription(
            //    d.Value,
            //    languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language,
            //    cmd.UserId))?.ToList();

            //// Delete holding descriptions
            //var deletedDescriptions = organization.DeleteDescriptions(newDescriptions);
            //if (deletedDescriptions?.Any() == true)
            //{
            //    this.organizationDescriptionRepo.DeleteAll(deletedDescriptions);
            //}

            //// Update holding
            //organization.Update(
            //    holding,
            //    cmd.Name,
            //    cmd.CompanyName,
            //    cmd.TradeName,
            //    cmd.Document,
            //    cmd.Website,
            //    cmd.SocialMedia,
            //    cmd.PhoneNumber,
            //    null,
            //    cmd.CropperImage?.ImageFile != null,
            //    cmd.CropperImage?.IsImageDeleted == true,
            //    newDescriptions,
            //    cmd.UserId);

            if (!organization.IsValid())
            {
                this.AppValidationResult.Add(organization.ValidationResult);
                return this.AppValidationResult;
            }

            this.OrganizationRepo.Update(organization);
            this.Uow.SaveChanges();
            this.AppValidationResult.Data = organization;

            // Update images
            if (cmd.CropperImage?.ImageFile != null)
            {
                ImageHelper.UploadOriginalAndCroppedImages(
                    organization.Uid,
                    cmd.CropperImage.ImageFile,
                    cmd.CropperImage.DataX,
                    cmd.CropperImage.DataY,
                    cmd.CropperImage.DataWidth,
                    cmd.CropperImage.DataHeight,
                    FileRepositoryPathType.OrganizationImage);
            }
            // Delete images
            else if (cmd.CropperImage?.IsImageDeleted == true && beforeImageUploadDate.HasValue)
            {
                ImageHelper.DeleteOriginalAndCroppedImages(
                    organization.Uid,
                    FileRepositoryPathType.OrganizationImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}