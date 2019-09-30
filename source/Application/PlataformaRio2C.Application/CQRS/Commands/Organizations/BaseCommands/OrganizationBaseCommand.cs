// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="OrganizationBaseCommand.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OrganizationBaseCommand</summary>
    public class OrganizationBaseCommand : BaseCommand
    {
        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? HoldingUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(81, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CountryUid { get; set; }

        public bool IsCompanyNumberRequired { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [ValidCompanyNumber]
        public string Document { get; set; }

        [Display(Name = "Website", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "SocialMedia", ResourceType = typeof(Labels))]
        [StringLength(256, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string SocialMedia { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        public AddressBaseCommand Address { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public List<OrganizationRestrictionSpecificsBaseCommand> RestrictionSpecifics { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<OrganizationActivityBaseCommand> OrganizationActivities { get; set; }

        public List<Guid> TargetAudiencesUids { get; set; }
        public List<Guid> InterestsUids { get; set; }

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public OrganizationType OrganizationType { get; private set; }
        public List<Activity> Activities { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<IGrouping<InterestGroup, Interest>> GroupedInterests { get; private set; }
        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseCommand"/> class.</summary>
        public OrganizationBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public void UpdateBaseProperties(
            OrganizationDto entity, 
            List<HoldingBaseDto> holdingBaseDtos, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            bool isDescriptionRequired, 
            bool isAddressRequired, 
            bool isRestrictionSpecificRequired, 
            bool isImageRequired)
        {
            this.HoldingUid = entity?.HoldingBaseDto?.Uid;
            this.Name = entity?.Name;
            this.CompanyName = entity?.CompanyName;
            this.TradeName = entity?.TradeName;
            this.CountryUid = entity?.AddressBaseDto?.CountryUid;
            this.IsCompanyNumberRequired = entity?.IsCompanyNumberRequired == true;
            this.Document = entity?.Document;
            this.Website = entity?.Website;
            this.SocialMedia = entity?.SocialMedia;
            this.PhoneNumber = entity?.PhoneNumber;
            this.UpdateAddress(entity, isAddressRequired);
            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateRestrictionSpecifics(entity, languagesDtos, isRestrictionSpecificRequired);
            this.UpdateActivities(entity, activities);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos, activities, targetAudiences, groupedInterests);
            this.TargetAudiencesUids = entity?.OrganizationTargetAudiencesDtos?.Select(otad => otad.TargetAudienceUid)?.ToList();
            this.InterestsUids = entity?.OrganizationInterestsDtos?.Select(oid => oid.InterestUid)?.ToList();
            this.IsApiDisplayEnabled = entity?.IsApiDisplayEnabled ?? false;
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

        /// <summary>Updates the restriction specifics.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        private void UpdateRestrictionSpecifics(OrganizationDto entity, List<LanguageDto> languagesDtos, bool isRestrictionSpecificRequired)
        {
            this.RestrictionSpecifics = new List<OrganizationRestrictionSpecificsBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var restrictionSpecific = entity?.RestrictionSpecificsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.RestrictionSpecifics.Add(restrictionSpecific != null ? new OrganizationRestrictionSpecificsBaseCommand(restrictionSpecific, isRestrictionSpecificRequired) :
                                                                            new OrganizationRestrictionSpecificsBaseCommand(languageDto, isRestrictionSpecificRequired));
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
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        public void UpdateDropdownProperties(
            List<HoldingBaseDto> holdingBaseDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests)
        {
            this.HoldingBaseDtos = holdingBaseDtos;
            this.Activities = activities;
            this.TargetAudiences = targetAudiences;
            this.GroupedInterests = groupedInterests;
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