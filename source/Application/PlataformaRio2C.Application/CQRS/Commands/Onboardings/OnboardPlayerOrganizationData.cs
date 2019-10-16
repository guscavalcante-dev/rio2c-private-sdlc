// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-14-2019
// ***********************************************************************
// <copyright file="OnboardPlayerOrganizationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardPlayerOrganizationData</summary>
    public class OnboardPlayerOrganizationData : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CountryUid { get; set; }

        public bool IsCompanyNumberRequired { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [RequiredIf("IsCompanyNumberRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [ValidCompanyNumber]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SocialMedia { get; set; }

        public AddressBaseCommand Address { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<OrganizationActivityBaseCommand> OrganizationActivities { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<Guid> TargetAudiencesUids { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public OrganizationType OrganizationType { get; private set; }
        //public List<Activity> Activities { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerOrganizationData"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public OnboardPlayerOrganizationData(
            OrganizationDto entity, 
            List<HoldingBaseDto> holdingBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            bool isDescriptionRequired, 
            bool isAddressRequired, 
            bool isImageRequired)
        {
            this.OrganizationUid = entity.Uid;
            this.CompanyName = entity?.CompanyName;
            this.TradeName = entity?.TradeName;
            this.CountryUid = entity?.AddressBaseDto?.CountryUid;
            this.IsCompanyNumberRequired = entity?.IsCompanyNumberRequired == true;
            this.Document = entity?.Document;
            this.Website = entity?.Website;
            this.SocialMedia = entity?.SocialMedia;
            this.TargetAudiencesUids = entity?.OrganizationTargetAudiencesDtos?.Select(otad => otad.TargetAudienceUid)?.ToList();
            this.UpdateAddress(entity, isAddressRequired);
            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateActivities(entity, activities);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos, targetAudiences);
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerOrganizationData"/> class.</summary>
        public OnboardPlayerOrganizationData()
        {
        }

        /// <summary>Updates the address.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        private void UpdateAddress(OrganizationDto entity, bool isAddressRequired)
        {
            this.Address = new AddressBaseCommand(entity?.AddressBaseDto, isAddressRequired);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        private void UpdateDescriptions(OrganizationDto entity, List<LanguageDto> languagesDtos, bool isDescriptionRequired)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new OrganizationDescriptionBaseCommand(description, isDescriptionRequired) : 
                                                            new OrganizationDescriptionBaseCommand(languageDto, isDescriptionRequired));
            }
        }

        /// <summary>Updates the activities.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        private void UpdateActivities(OrganizationDto entity, List<Activity> activities)
        {
            this.OrganizationActivities = new List<OrganizationActivityBaseCommand>();
            foreach (var activity in activities)
            {
                var organizationActivity = entity?.OrganizationActivitiesDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                this.OrganizationActivities.Add(organizationActivity != null ? new OrganizationActivityBaseCommand(organizationActivity) :
                                                                               new OrganizationActivityBaseCommand(activity));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(OrganizationDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.OrganizationImage, isImageRequired);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(
            List<HoldingBaseDto> holdingBaseDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<TargetAudience> targetAudiences)
        {
            this.HoldingBaseDtos = holdingBaseDtos;
            this.TargetAudiences = targetAudiences;
            this.CountriesBaseDtos = countriesBaseDtos?
                                            .OrderBy(c => c.Ordering)?
                                            .ThenBy(c => c.DisplayName)?
                                            .ToList();
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public void UpdatePreSendProperties(
            OrganizationType organizationType,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.OrganizationType = organizationType;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }
    }
}