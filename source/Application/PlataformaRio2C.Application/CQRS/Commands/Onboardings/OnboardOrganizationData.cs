// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="OnboardOrganizationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardOrganizationData</summary>
    public class OnboardOrganizationData : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SocialMedia { get; set; }

        public AddressBaseCommand Address { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public List<Guid> ActivitiesUids { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public List<Guid> TargetAudiencesUids { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public OrganizationType OrganizationType { get; private set; }
        public List<Activity> Activities { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }

        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTime UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardOrganizationData"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        public OnboardOrganizationData(
            OrganizationDto entity, 
            List<HoldingBaseDto> holdingBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            bool isDescriptionRequired, 
            bool isAddressRequired)
        {
            this.OrganizationUid = entity.Uid;
            this.UpdaterBaseDto = entity.UpdaterDto;
            this.UpdateDate = entity.UpdateDate;
            this.Document = entity?.Document;
            this.CompanyName = entity?.CompanyName;
            this.TradeName = entity?.TradeName;
            this.Website = entity?.Website;
            this.SocialMedia = entity?.SocialMedia;
            this.ActivitiesUids = entity?.OrganizationActivitiesDtos?.Select(oad => oad.ActivityUid)?.ToList();
            this.TargetAudiencesUids = entity?.OrganizationTargetAudiencesDtos?.Select(otad => otad.TargetAudienceUid)?.ToList();
            this.UpdateAddress(entity, countriesBaseDtos, isAddressRequired);
            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateCropperImage(entity);
            this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos, activities, targetAudiences);
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardOrganizationData"/> class.</summary>
        public OnboardOrganizationData()
        {
        }

        /// <summary>Updates the address.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        private void UpdateAddress(OrganizationDto entity, List<CountryBaseDto> countriesBaseDtos, bool isAddressRequired)
        {
            this.Address = new AddressBaseCommand(entity?.AddressBaseDto, countriesBaseDtos, isAddressRequired);
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

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        private void UpdateCropperImage(OrganizationDto entity)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.OrganizationImage);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(
            List<HoldingBaseDto> holdingBaseDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences)
        {
            this.HoldingBaseDtos = holdingBaseDtos;
            this.Address?.UpdateDropdownProperties(countriesBaseDtos);
            this.Activities = activities;
            this.TargetAudiences = targetAudiences;
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