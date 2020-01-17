// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="CreateOrganizationCommandHandler.cs" company="Softo">
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
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>CreateOrganizationCommandHandler</summary>
    public class CreateOrganizationCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<CreateOrganization, AppValidationResult>
    {
        private readonly IHoldingRepository holdingRepo;
        private readonly IEditionRepository editionRepo;
        private readonly IOrganizationTypeRepository organizationTypeRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly ICountryRepository countryRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly IInterestRepository interestRepo;

        /// <summary>Initializes a new instance of the <see cref="CreateOrganizationCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="holdingRepository">The holding repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="organizationTypeRepository">The organization type repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="interestRepository">The interest repository.</param>
        public CreateOrganizationCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IHoldingRepository holdingRepository,
            IEditionRepository editionRepository,
            IOrganizationTypeRepository organizationTypeRepository,
            ILanguageRepository languageRepository,
            ICountryRepository countryRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            IInterestRepository interestRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.holdingRepo = holdingRepository;
            this.editionRepo = editionRepository;
            this.organizationTypeRepo = organizationTypeRepository;
            this.languageRepo = languageRepository;
            this.countryRepo = countryRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.interestRepo = interestRepository;
        }

        /// <summary>Handles the specified create organization.</summary>
        /// <param name="cmd"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(CreateOrganization cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            var organizationUid = Guid.NewGuid();

            #region Initial validations

            var existingOrganizationByName = this.OrganizationRepo.Get(o => o.Name == cmd.Name
                                                                            && o.Holding.Uid == cmd.HoldingUid
                                                                            && !o.IsDeleted);
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

            var edition = await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty);
            var organizationType = await this.organizationTypeRepo.GetAsync(cmd.OrganizationType?.Uid ?? Guid.Empty);
            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var activities = await this.activityRepo.FindAllAsync();
            var interestsDtos = await this.interestRepo.FindAllDtosAsync();

            // Interests
            var organizationInterests = new List<OrganizationInterest>();
            if (cmd.Interests?.Any() == true)
            {
                foreach (var interestBaseCommands in cmd.Interests)
                {
                    foreach (var interestBaseCommand in interestBaseCommands?.Where(ibc => ibc.IsChecked)?.ToList())
                    {
                        organizationInterests.Add(new OrganizationInterest(interestsDtos?.FirstOrDefault(id => id.Interest.Uid == interestBaseCommand.InterestUid)?.Interest, interestBaseCommand.AdditionalInfo, cmd.UserId));
                    }
                }
            }

            var organization = new Organization(
                organizationUid,
                await this.holdingRepo.GetAsync(cmd.HoldingUid ?? Guid.Empty),
                edition,
                organizationType,
                cmd.IsApiDisplayEnabled,
                cmd.ApiHighlightPosition,
                cmd.Name,
                cmd.CompanyName,
                cmd.TradeName,
                cmd.Document,
                cmd.Website,
                cmd.Linkedin,
                cmd.Twitter,
                cmd.Instagram,
                cmd.Youtube,
                cmd.PhoneNumber,
                await this.countryRepo.GetAsync(cmd.CountryUid ?? Guid.Empty),
                cmd.Address?.StateUid,
                cmd.Address?.StateName,
                cmd.Address?.CityUid,
                cmd.Address?.CityName,
                cmd.Address?.Address1,
                cmd.Address?.AddressZipCode,
                true, //TODO: get AddressIsManual from form
                cmd.CropperImage?.ImageFile != null,
                cmd.Descriptions?.Select(d => new OrganizationDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.RestrictionSpecifics?.Select(d => new OrganizationRestrictionSpecific(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                cmd.OrganizationActivities?.Where(oa => oa.IsChecked)?.Select(oa => new OrganizationActivity(activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid), oa.AdditionalInfo, cmd.UserId))?.ToList(),
                cmd.TargetAudiencesUids?.Any() == true ? await this.targetAudienceRepo.FindAllByUidsAsync(cmd.TargetAudiencesUids) : new List<TargetAudience>(),
                organizationInterests,
                cmd.UserId);
            if (!organization.IsValid())
            {
                this.AppValidationResult.Add(organization.ValidationResult);
                return this.AppValidationResult;
            }

            this.OrganizationRepo.Create(organization);

            #region Disable same highlight position of other organizations

            if (cmd.IsApiDisplayEnabled && cmd.ApiHighlightPosition.HasValue)
            {
                var sameHighlightPositionOrganizations = await this.OrganizationRepo.FindAllByHightlightPosition(
                    cmd.EditionId ?? 0, 
                    cmd.OrganizationType?.Uid ?? Guid.Empty, 
                    cmd.ApiHighlightPosition.Value, 
                    null);
                if (sameHighlightPositionOrganizations?.Any() == true)
                {
                    foreach (var sameHighlightPositionOrganization in sameHighlightPositionOrganizations)
                    {
                        sameHighlightPositionOrganization.DeleteApiHighlightPosition(edition, organizationType, cmd.UserId);
                        this.OrganizationRepo.Update(sameHighlightPositionOrganization);
                    }
                }
            }

            #endregion

            this.Uow.SaveChanges();
            this.AppValidationResult.Data = organization;

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

            return this.AppValidationResult;

            //this.eventBus.Publish(new PropertyCreated(propertyId), cancellationToken);

            //return Task.FromResult(propertyId); // use it when the methed is not async
        }
    }
}