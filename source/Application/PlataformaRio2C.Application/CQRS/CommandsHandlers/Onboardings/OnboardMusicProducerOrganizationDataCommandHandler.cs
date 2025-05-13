// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-29-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="OnboardProducerOrganizationDataCommandHandler.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2c.Infra.Data.FileRepository.Helpers;
using PlataformaRio2C.Application.CQRS.Commands;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.CQRS.CommandsHandlers
{
    /// <summary>OnboardProducerOrganizationDataCommandHandler</summary>
    public class OnboardMusicProducerOrganizationDataCommandHandler : BaseOrganizationCommandHandler, IRequestHandler<OnboardMusicProducerOrganizationData, AppValidationResult>
    {
        private readonly IEditionRepository editionRepo;
        private readonly IAttendeeCollaboratorRepository attendeeCollaboratorRepo;
        private readonly ILanguageRepository languageRepo;
        private readonly IActivityRepository activityRepo;
        private readonly ITargetAudienceRepository targetAudienceRepo;
        private readonly ICountryRepository countryRepo;

        /// <summary>Initializes a new instance of the <see cref="OnboardProducerOrganizationDataCommandHandler"/> class.</summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="uow">The uow.</param>
        /// <param name="organizationRepository">The organization repository.</param>
        /// <param name="editionRepository">The edition repository.</param>
        /// <param name="attendeeCollaboratorRepository">The attendee collaborator repository.</param>
        /// <param name="languageRepository">The language repository.</param>
        /// <param name="activityRepository">The activity repository.</param>
        /// <param name="targetAudienceRepository">The target audience repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        public OnboardMusicProducerOrganizationDataCommandHandler(
            IMediator eventBus,
            IUnitOfWork uow,
            IOrganizationRepository organizationRepository,
            IEditionRepository editionRepository,
            IAttendeeCollaboratorRepository attendeeCollaboratorRepository,
            ILanguageRepository languageRepository,
            IActivityRepository activityRepository,
            ITargetAudienceRepository targetAudienceRepository,
            ICountryRepository countryRepository)
            : base(eventBus, uow, organizationRepository)
        {
            this.editionRepo = editionRepository;
            this.attendeeCollaboratorRepo = attendeeCollaboratorRepository;
            this.languageRepo = languageRepository;
            this.activityRepo = activityRepository;
            this.targetAudienceRepo = targetAudienceRepository;
            this.countryRepo = countryRepository;
        }

        /// <summary>Handles the specified onboard producer organization data.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<AppValidationResult> Handle(OnboardMusicProducerOrganizationData cmd, CancellationToken cancellationToken)
        {
            this.Uow.BeginTransaction();

            Organization organization = null;

            var languageDtos = await this.languageRepo.FindAllDtosAsync();
            var activities = await this.activityRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);
            var targetAudiences = await this.targetAudienceRepo.FindAllByProjectTypeIdAsync(ProjectType.Music.Id);

            var attendeeCollaborator = await this.attendeeCollaboratorRepo.GetAsync(ac => ac.Collaborator.User.Uid == cmd.UserUid && ac.EditionId == cmd.EditionId);
            if (attendeeCollaborator == null)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityNotAction, Labels.Executive, Labels.FoundM), new string[] { "CompanyName" }));
            }

            if (cmd.OrganizationUid.HasValue)
            {
                organization = await this.GetOrganizationByUid(cmd.OrganizationUid.Value);

                #region Initial validations

                if (!this.ValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                #endregion

                var x = cmd.OrganizationActivities?.Where(oa => oa.IsChecked)?.Select(oa => new OrganizationActivity(activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid), oa.AdditionalInfo, cmd.UserId))?.ToList();

                organization.OnboardProducerData(
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    attendeeCollaborator,
                    cmd.CompanyName,
                    cmd.TradeName,
                    cmd.Document,
                    cmd.Website,
                    cmd.Linkedin,
                    cmd.Twitter,
                    cmd.Instagram,
                    cmd.Youtube,
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
                    cmd.IsVirtualMeeting,
                    cmd.Descriptions?.Select(d => new OrganizationDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                    cmd.OrganizationActivities?.Where(oa => oa.IsChecked)?.Select(oa => new OrganizationActivity(activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid), oa.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.OrganizationTargetAudiences?.Where(ota => ota.IsChecked)?.Select(ota => new OrganizationTargetAudience(targetAudiences?.FirstOrDefault(a => a.Uid == ota.TargetAudienceUid), ota.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.UserId,
                    ProjectType.Music.Id);
                if (!organization.IsValid())
                {
                    this.AppValidationResult.Add(organization.ValidationResult);
                    return this.AppValidationResult;
                }

                this.OrganizationRepo.Update(organization);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = organization;
            }
            else
            {
                #region Initial validations

                var existingOrganizationByName = this.OrganizationRepo.Get(o => o.TradeName == cmd.TradeName && !o.IsDeleted);
                if (existingOrganizationByName != null)
                {
                    this.ValidationResult.Add(new ValidationError(string.Format(Messages.EntityExistsWithSameProperty, Labels.APlayer, Labels.TheName, cmd.CompanyName), new string[] { "CompanyName" }));
                }

                if (!this.ValidationResult.IsValid)
                {
                    this.AppValidationResult.Add(this.ValidationResult);
                    return this.AppValidationResult;
                }

                #endregion

                organization = new Organization(
                    await this.editionRepo.GetAsync(cmd.EditionUid ?? Guid.Empty),
                    attendeeCollaborator,
                    cmd.CompanyName,
                    cmd.TradeName,
                    cmd.Document,
                    cmd.Website,
                    cmd.Linkedin,
                    cmd.Twitter,
                    cmd.Instagram,
                    cmd.Youtube,
                    await this.countryRepo.GetAsync(cmd.CountryUid ?? Guid.Empty),
                    cmd.Address?.StateUid,
                    cmd.Address?.StateName,
                    cmd.Address?.CityUid,
                    cmd.Address?.CityName,
                    cmd.Address?.Address1,
                    cmd.Address?.AddressZipCode,
                    true, //TODO: get AddressIsManual from form
                    cmd.CropperImage?.ImageFile != null,
                    cmd.IsVirtualMeeting,
                    cmd.Descriptions?.Select(d => new OrganizationDescription(d.Value, languageDtos?.FirstOrDefault(l => l.Code == d.LanguageCode)?.Language, cmd.UserId))?.ToList(),
                    cmd.OrganizationActivities?.Where(oa => oa.IsChecked)?.Select(oa => new OrganizationActivity(activities?.FirstOrDefault(a => a.Uid == oa.ActivityUid), oa.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.OrganizationTargetAudiences?.Where(ota => ota.IsChecked)?.Select(ota => new OrganizationTargetAudience(targetAudiences?.FirstOrDefault(a => a.Uid == ota.TargetAudienceUid), ota.AdditionalInfo, cmd.UserId))?.ToList(),
                    cmd.UserId,
                    ProjectType.Music.Id);
                if (!organization.IsValid())
                {
                    this.AppValidationResult.Add(organization.ValidationResult);
                    return this.AppValidationResult;
                }

                this.OrganizationRepo.Create(organization);
                this.Uow.SaveChanges();
                this.AppValidationResult.Data = organization;
            }

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