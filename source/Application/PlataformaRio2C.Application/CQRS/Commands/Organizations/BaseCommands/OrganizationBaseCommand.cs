﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2024
// ***********************************************************************
// <copyright file="OrganizationBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Foolproof;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Statics;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OrganizationBaseCommand</summary>
    public class OrganizationBaseCommand : BaseCommand
    {
        public bool IsHoldingRequired { get; set; }
        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsHoldingRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? HoldingUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(81, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Labels))]
        public Guid? CountryUid { get; set; }

        public bool IsCompanyNumberRequired { get; set; }

        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        [ValidCompanyNumber]
        public string Document { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Labels))]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string PhoneNumber { get; set; }

        public bool IsVirtualMeetingRequired { get; set; }

        [Display(Name = "MeetingType", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf(nameof(IsVirtualMeetingRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsVirtualMeeting { get; set; }

        #region Socials

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

        #endregion

        public AddressBaseCommand Address { get; set; }

        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public List<OrganizationRestrictionSpecificsBaseCommand> RestrictionSpecifics { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        [Display(Name = "Activities", ResourceType = typeof(Labels))]
        public List<OrganizationActivityBaseCommand> OrganizationActivities { get; set; }

        [Display(Name = "TargetAudiences", ResourceType = typeof(Labels))]
        public List<OrganizationTargetAudienceBaseCommand> OrganizationTargetAudiences { get; set; }

        public bool IsVerticalRequired { get; set; }

        [Display(Name = "Verticals", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsVerticalRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SelectAtLeastOneOption")]
        public List<InnovationOrganizationTrackOptionBaseCommand> InnovationOrganizationTrackGroups { get; set; }

        public InterestBaseCommand[][] Interests { get; set; }

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public int? ApiHighlightPosition { get; set; }

        public OrganizationType OrganizationType { get; private set; }
        public ProjectType ProjectType { get; private set; }

        #region Dropdowns Properties

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public List<Activity> Activities { get; private set; }
        public List<TargetAudience> TargetAudiences { get; private set; }
        public List<CountryBaseDto> CountriesBaseDtos { get; private set; }
        public List<InnovationOrganizationTrackOptionDto> InnovationOrganizationTrackOptionDtos { get; private set; }
        public int[] ApiHighlightPositions = new[] { 1, 2, 3, 4, 5 };

        #endregion

        /// <summary>Initializes a new instance of the <see cref="OrganizationBaseCommand"/> class.</summary>
        public OrganizationBaseCommand()
        {
        }

        /// <summary>
        /// Updates the base properties.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="isVirtualMeetingRequired">if set to <c>true</c> [is virtual meeting required].</param>
        /// <param name="isHoldingRequired">if set to <c>true</c> [is holding required].</param>
        /// <param name="isVericalRequired">if set to <c>true</c> [is verical required].</param>
        public void UpdateBaseProperties(
            OrganizationDto entity,
            OrganizationType organizationType,
            List<HoldingBaseDto> holdingBaseDtos,
            List<LanguageDto> languagesDtos,
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos,
            bool isDescriptionRequired,
            bool isAddressRequired,
            bool isRestrictionSpecificRequired,
            bool isImageRequired,
            bool isVirtualMeetingRequired,
            bool isHoldingRequired,
            bool isVericalRequired)
        {
            this.HoldingUid = entity?.HoldingBaseDto?.Uid;
            this.IsHoldingRequired = isHoldingRequired;

            this.Name = entity?.Name;
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
            this.PhoneNumber = entity?.PhoneNumber;

            this.OrganizationType = organizationType;
            this.ProjectType = this.GetProjectTypeByOrganizationType(organizationType);

            this.IsVirtualMeeting = entity?.IsVirtualMeeting;
            this.IsVirtualMeetingRequired = isVirtualMeetingRequired;

            this.IsVerticalRequired = isVericalRequired;

            this.UpdateDropdownProperties(holdingBaseDtos, countriesBaseDtos, activities, targetAudiences, innovationOrganizationTrackOptionDtos);

            this.UpdateAddress(entity, isAddressRequired);
            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateRestrictionSpecifics(entity, languagesDtos, isRestrictionSpecificRequired);
            this.UpdateActivities(entity, activities);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateTargetAudiences(entity, targetAudiences);
            this.UpdateInterests(entity, interestsDtos);
            this.UpdateInnovationOrganizationTrackOptionGroups(entity, innovationOrganizationTrackOptionDtos);
            this.UpdateApiConfigurations(entity, organizationType);
        }

        /// <summary>
        /// Updates the dropdown properties.
        /// </summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        public void UpdateDropdownProperties(
            List<HoldingBaseDto> holdingBaseDtos,
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.HoldingBaseDtos = holdingBaseDtos;
            this.Activities = activities;
            this.TargetAudiences = targetAudiences;
            this.CountriesBaseDtos = countriesBaseDtos?
                                        .OrderBy(c => c.Ordering)?
                                        .ThenBy(c => c.DisplayName)?
                                        .ToList();
            this.InnovationOrganizationTrackOptionDtos = innovationOrganizationTrackOptionDtos;
        }

        /// <summary>Updates the pre send properties.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>d
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
            this.ProjectType = this.GetProjectTypeByOrganizationType(organizationType);
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }

        #region Private methods

        /// <summary>Updates the API configurations.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationType">Type of the organization.</param>
        private void UpdateApiConfigurations(OrganizationDto entity, OrganizationType organizationType)
        {
            var attendeeOrganizationTypeDto = entity?.AttendeeOrganizationTypesDtos?.FirstOrDefault(aotd => aotd.OrganizationType.Uid == organizationType.Uid);
            if (attendeeOrganizationTypeDto == null)
            {
                return;
            }

            this.IsApiDisplayEnabled = attendeeOrganizationTypeDto?.AttendeeOrganizationType?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = this.IsApiDisplayEnabled ? attendeeOrganizationTypeDto?.AttendeeOrganizationType?.ApiHighlightPosition : null;
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
                var description = entity?.OrganizationDescriptionBaseDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
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
                var restrictionSpecific = entity?.OrganizationRestrictionSpecificBaseDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
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

        /// <summary>Updates the interests.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        private void UpdateInterests(OrganizationDto entity, List<InterestDto> interestsDtos)
        {
            var interestsBaseCommands = new List<InterestBaseCommand>();
            foreach (var interestDto in interestsDtos)
            {
                var organizationInterest = entity?.OrganizationInterestDtos?.FirstOrDefault(oad => oad.Interest.Uid == interestDto.Interest.Uid);
                interestsBaseCommands.Add(organizationInterest != null ? new InterestBaseCommand(organizationInterest) :
                                                                         new InterestBaseCommand(interestDto));
            }

            var groupedInterestsDtos = interestsBaseCommands?
                                            .GroupBy(i => new { i.InterestGroupUid, i.InterestGroupName, i.InterestGroupDisplayOrder })?
                                            .OrderBy(g => g.Key.InterestGroupDisplayOrder)?
                                            .ToList();

            if (groupedInterestsDtos?.Any() == true)
            {
                this.Interests = new InterestBaseCommand[groupedInterestsDtos.Count][];
                for (int i = 0; i < groupedInterestsDtos.Count; i++)
                {
                    this.Interests[i] = groupedInterestsDtos[i].ToArray();
                }
            }
        }

        /// <summary>
        /// Updates the target audiences.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        public void UpdateTargetAudiences(OrganizationDto entity, List<TargetAudience> targetAudiences)
        {
            this.OrganizationTargetAudiences = new List<OrganizationTargetAudienceBaseCommand>();
            foreach (var targetAudience in targetAudiences)
            {
                var organizationTargetAudience = entity?.OrganizationTargetAudiencesDtos?.FirstOrDefault(ota => ota.TargetAudienceUid == targetAudience.Uid);
                this.OrganizationTargetAudiences.Add(organizationTargetAudience != null ? new OrganizationTargetAudienceBaseCommand(organizationTargetAudience) :
                                                                                          new OrganizationTargetAudienceBaseCommand(targetAudience));
            }
        }

        /// <summary>
        /// Updates the innovation organization track option groups.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="innovationOrganizationTrackOptionDtos">The innovation organization track option dtos.</param>
        private void UpdateInnovationOrganizationTrackOptionGroups(OrganizationDto entity, List<InnovationOrganizationTrackOptionDto> innovationOrganizationTrackOptionDtos)
        {
            this.InnovationOrganizationTrackGroups = new List<InnovationOrganizationTrackOptionBaseCommand>();

            var selectedInnovationOrganizationTrackOptionGroupsUids = entity?.InnovationOrganizationTrackOptionGroupDtos
                                                                                ?.GroupBy(aciotDto => aciotDto.InnovationOrganizationTrackOptionGroup?.Uid)
                                                                                ?.Select(group => group.Key);
            if (innovationOrganizationTrackOptionDtos?.Any() == true)
            {
                foreach (var innovationOrganizationTrackOptionGroup in innovationOrganizationTrackOptionDtos.GroupBy(dto => dto.InnovationOrganizationTrackOptionGroup))
                {
                    this.InnovationOrganizationTrackGroups.Add(
                        new InnovationOrganizationTrackOptionBaseCommand(
                            innovationOrganizationTrackOptionGroup.Key,
                            selectedInnovationOrganizationTrackOptionGroupsUids?.Contains(innovationOrganizationTrackOptionGroup.Key?.Uid) == true));
                }
            }
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(OrganizationDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity?.ImageUploadDate, entity?.Uid, FileRepositoryPathType.OrganizationImage, isImageRequired);
        }

        /// <summary>
        /// Gets the type of the project type by organization.
        /// </summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        private ProjectType GetProjectTypeByOrganizationType(OrganizationType organizationType)
        {
            if (organizationType.Name == OrganizationType.AudiovisualPlayer.Name)
            {
                return ProjectType.AudiovisualBusinessRound;
            }
            else if (organizationType.Name == OrganizationType.MusicPlayer.Name)
            {
                return ProjectType.Music;
            }
            else if (organizationType.Name == OrganizationType.StartupPlayer.Name)
            {
                return ProjectType.Startup;
            }
            else
            {
                throw new NotImplementedException(string.Format(Messages.EntityNotAction, "OrganizationType: " + organizationType.Name, Labels.FoundM));
            }
        }

        #endregion
    }
}