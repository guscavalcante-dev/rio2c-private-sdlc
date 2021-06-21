// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-19-2021
// ***********************************************************************
// <copyright file="UpdateOrganizationSiteMainInformationCommandHandler.cs" company="Softo">
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
    /// <summary>UpdateOrganizationSiteMainInformationCommandHandler</summary>
    public class UpdateOrganizationSiteMainInformationCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<UpdateOrganizationSiteMainInformation, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationSiteMainInformationCommandHandler"/> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        public UpdateOrganizationSiteMainInformationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            ILanguageRepository languageRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            IHoldingRepository holdingRepository
            )
            : base(eventBus, uow, organizationRepository)
        {
            this.editionRepo = editionRepository;
            this.languageRepo = languageRepository;
            this.organizationTypeRepo = organizationTypeRepository;
        }

        /// <summary>
        /// Handles the specified update organization site main information.
        /// </summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(UpdateOrganizationSiteMainInformation cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organization = await this.GetOrganizationByUid(cmd.OrganizationUid);

            #region Initial validations

            // Check if the trade name already exists
            var existingOrganizationByName = this.OrganizationRepo.Get(o => o.TradeName == cmd.TradeName 
                                                                            && o.HoldingId == organization.HoldingId 
                                                                            && o.Uid != cmd.OrganizationUid
                                                                            && !o.IsDeleted);
            if (existingOrganizationByName != null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.TradeName), new string[] { "TradeName" }));
            }

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Before update values
            var beforeImageUploadDate = organization.ImageUploadDate;
            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);

            organization.UpdateSiteMainInformation(
                edition,
                cmd.CompanyName,
                cmd.TradeName,
                cmd.Document,
                cmd.CropperImage?.ImageFile != null,
                cmd.CropperImage?.IsImageDeleted == true,
                cmd.Descriptions?.Select(d => new OrganizationDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.UserId);

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
                ImageHelper.DeleteOriginalAndCroppedImages(organization.Uid, FileRepositoryPathType.OrganizationImage);
            }

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}