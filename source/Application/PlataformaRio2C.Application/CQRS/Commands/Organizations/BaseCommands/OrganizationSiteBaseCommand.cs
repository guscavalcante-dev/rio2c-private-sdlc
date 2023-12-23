// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-29-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-23-2023
// ***********************************************************************
// <copyright file="OrganizationSiteBaseCommand.cs" company="Softo">
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
    /// <summary>OrganizationSiteBaseCommand</summary>
    public class OrganizationSiteBaseCommand : BaseCommand
    {
        public Guid? OrganizationUid { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

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
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Website { get; set; }

        [Display(Name = "LinkedIn")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Linkedin { get; set; }

        [Display(Name = "Twitter")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Twitter { get; set; }

        [Display(Name = "Instagram")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Instagram { get; set; }

        [Display(Name = "YouTube")]
        [StringLength(300, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Youtube { get; set; }

        public bool IsVirtualMeetingRequired { get; set; }

        [Display(Name = "MeetingType", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf(nameof(IsVirtualMeetingRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsVirtualMeeting { get; set; }

        public AddressBaseCommand Address { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<OrganizationActivityBaseCommand> OrganizationActivities { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<OrganizationTargetAudienceBaseCommand> OrganizationTargetAudiences { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }

        public UserBaseDto UpdaterBaseDto { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OrganizationSiteBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public OrganizationSiteBaseCommand(
            OrganizationDto entity, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            bool isDescriptionRequired, 
            bool isAddressRequired, 
            bool isImageRequired,
            bool isVirtualMeetingRequired = true)
        {
            this.OrganizationUid = entity?.Uid;
            this.CompanyName = entity?.CompanyName;
            this.TradeName = entity?.TradeName;
            this.CountryUid = entity?.AddressBaseDto?.CountryUid;
            this.IsCompanyNumberRequired = entity?.IsCompanyNumberRequired == true;
            this.Document = entity?.Document;
            this.Website = entity?.Website;
            this.Linkedin = entity?.Linkedin;
            this.Twitter = entity?.Twitter;
            this.Instagram = entity?.Instagram;
            this.Youtube = entity?.Youtube;
            this.UpdateAddress(entity, isAddressRequired);
            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateActivities(entity, activities);
            this.UpdateTargetAudiences(entity, targetAudiences);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateDropdownProperties(countriesBaseDtos, targetAudiences);
            this.UpdaterBaseDto = entity?.UpdaterBaseDto;
            this.UpdateDate = entity?.UpdateDate;
            this.IsVirtualMeetingRequired = isVirtualMeetingRequired;
            this.IsVirtualMeeting = entity?.IsVirtualMeeting;
        }

        /// <summary>Initializes a new instance of the <see cref="OrganizationSiteBaseCommand"/> class.</summary>
        public OrganizationSiteBaseCommand()
        {
        }

        /// <summary>Updates the address.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        protected void UpdateAddress(OrganizationDto entity, bool isAddressRequired)
        {
            this.Address = new AddressBaseCommand(entity?.AddressBaseDto, isAddressRequired);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        protected void UpdateDescriptions(OrganizationDto entity, List<LanguageDto> languagesDtos, bool isDescriptionRequired)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.OrganizationDescriptionBaseDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new OrganizationDescriptionBaseCommand(description, isDescriptionRequired) : 
                                                            new OrganizationDescriptionBaseCommand(languageDto, isDescriptionRequired));
            }
        }

        /// <summary>Updates the activities.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="activities">The activities.</param>
        protected void UpdateActivities(OrganizationDto entity, List<Activity> activities)
        {
            this.OrganizationActivities = new List<OrganizationActivityBaseCommand>();
            foreach (var activity in activities)
            {
                var organizationActivity = entity?.OrganizationActivitiesDtos?.FirstOrDefault(oad => oad.ActivityUid == activity.Uid);
                this.OrganizationActivities.Add(organizationActivity != null ? new OrganizationActivityBaseCommand(organizationActivity) :
                                                                               new OrganizationActivityBaseCommand(activity));
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        protected void UpdateTargetAudiences(OrganizationDto entity, List<TargetAudience> targetAudiences)
        {
            this.OrganizationTargetAudiences = new List<OrganizationTargetAudienceBaseCommand>();
            foreach (var targetAudience in targetAudiences)
            {
                var organizationTargetAudience = entity?.OrganizationTargetAudiencesDtos?.FirstOrDefault(oad => oad.TargetAudienceUid == targetAudience.Uid);
                this.OrganizationTargetAudiences.Add(organizationTargetAudience != null ? new OrganizationTargetAudienceBaseCommand(organizationTargetAudience) :
                                                                                          new OrganizationTargetAudienceBaseCommand(targetAudience));
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        protected void UpdateCropperImage(OrganizationDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.OrganizationImage, isImageRequired);
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateDropdownProperties(
            List<CountryBaseDto> countriesBaseDtos,
            List<TargetAudience> targetAudiences)
        {
            this.TargetAudiences = targetAudiences;
            this.CountriesBaseDtos = countriesBaseDtos?
                                            .OrderBy(c => c.Ordering)?
                                            .ThenBy(c => c.DisplayName)?
                                            .ToList();
        }
    }
}