// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="OnboardPlayerOrganizationDataCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>OnboardPlayerOrganizationDataCommandHandler</summary>
    public class OnboardPlayerOrganizationDataCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<OnboardPlayerOrganizationData, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly ICountryRepository countryRepo;

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerOrganizationDataCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public OnboardPlayerOrganizationDataCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            ILanguageRepository languageRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.languageRepo = languageRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified onboard player organization data.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardPlayerOrganizationData cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organization = await this.GetOrganizationByUid(cmd.OrganizationUid ?? Guid.Empty);

            #region Initial validations

            if (!this.ValidationResult.IsValid)
            {
                this.AppValidationResult.Add(this.ValidationResult);
                return this.AppValidationResult;
            }

            #endregion

            // Before update values
            var beforeImageUploadDate = organization.ImageUploadDate;

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var activities = await this.activityRepo.FindAllAsync();

            organization.OnboardPlayerData(
                await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty),
                cmd.CompanyName,
                cmd.TradeName,
                cmd.Document,
                cmd.Website,
                cmd.SocialMedia,
                await this.countryRepo.GetAsync(cmd.CountryUid ?? Guid.Empty),
                cmd.Address?.StateUid,
                cmd.Address?.StateName,
                cmd.Address?.CityUid,
                cmd.Address?.CityName,
                cmd.Address?.Address1,
                cmd.Address?.AddressZipCode,
                true, //TODO: get AddressIsManual from form
                cmd.CropperImage?.ImageFile != null,
                cmd.CropperImage?.IsImageDeleted == true,
                cmd.Descriptions?.Select(d => new OrganizationDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.OrganizationActivities?.Where(oa => oa.IsChecked)?.Select(oa => new OrganizationActivity(activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid), oa.AdditionalInfo, cmd.UserId))?.ToList(),
                cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
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