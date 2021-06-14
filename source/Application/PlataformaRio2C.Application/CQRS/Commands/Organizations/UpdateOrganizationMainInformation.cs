// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="UpdateOrganizationMainInformation.cs" company="Softo">
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
using Constants = PlataformaRio2C.Domain.Constants;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationMainInformation</summary>
    public class UpdateOrganizationMainInformation : BaseCommand
    {
        public Guid OrganizationUid { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? HoldingUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(81, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string CompanyName { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        public bool IsCompanyNumberRequired { get; set; }
        [Display(Name = "CompanyDocument", ResourceType = typeof(Labels))]
        [RequiredIf("IsCompanyNumberRequired", "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [ValidCompanyNumber]
        [StringLength(50, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Document { get; set; }

        public bool IsVirtualMeetingRequired => (this.OrganizationType != null && this.OrganizationType?.Name == Constants.OrganizationType.AudiovisualBuyer);
        [Display(Name = "AcceptsVirtualMeeting", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsVirtualMeetingRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsVirtualMeeting { get; set; }


        public int[] ApiHighlightPositions = new[] { 1, 2, 3, 4, 5 };

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public int? ApiHighlightPosition { get; set; }


        public List<OrganizationDescriptionBaseCommand> Descriptions { get; set; }
        public CropperImageBaseCommand CropperImage { get; set; }

        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }
        public OrganizationType OrganizationType { get; private set; }
        public Country Country { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateOrganizationMainInformation"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public UpdateOrganizationMainInformation(
            AttemdeeOrganizationSiteMainInformationWidgetDto entity,
            OrganizationType organizationType,
            List<HoldingBaseDto> holdingBaseDtos,
            List<LanguageDto> languagesDtos,
            bool isDescriptionRequired,
            bool isImageRequired)
        {
            this.OrganizationUid = entity.Organization.Uid;
            this.HoldingUid = entity?.Organization?.Holding?.Uid;

            this.Name = entity.Organization.Name;
            this.CompanyName = entity.Organization.CompanyName;
            this.TradeName = entity.Organization.TradeName;
            this.IsCompanyNumberRequired = entity.Country?.IsCompanyNumberRequired == true;
            this.Document = entity.Organization.Document;
            
            this.OrganizationType = organizationType;
            this.IsVirtualMeeting = entity?.AttendeeOrganization?.IsVirtualMeeting == true;

            this.UpdateDescriptions(entity, languagesDtos, isDescriptionRequired);
            this.UpdateCropperImage(entity, isImageRequired);
            this.UpdateModelsAndLists(entity.Country, holdingBaseDtos);
            this.UpdateApiConfigurations(entity, organizationType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationMainInformation"/> class.
        /// </summary>
        public UpdateOrganizationMainInformation()
        {
        }

        /// <summary>Updates the models and lists.</summary>
        /// <param name="country">The country.</param>
        public void UpdateModelsAndLists(
            Country country,
            List<HoldingBaseDto> holdingBaseDtos)
        {
            this.Country = country;
            this.HoldingBaseDtos = holdingBaseDtos;
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
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, UserInterfaceLanguage);
        }

        #region Private Methods

        /// <summary>Updates the API configurations.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationType">Type of the organization.</param>
        private void UpdateApiConfigurations(AttemdeeOrganizationSiteMainInformationWidgetDto entity, OrganizationType organizationType)
        {
            var attendeeOrganizationType = entity?.AttendeeOrganization?.AttendeeOrganizationTypes?.FirstOrDefault(aotd => aotd.OrganizationType.Uid == organizationType.Uid);
            if (attendeeOrganizationType == null)
            {
                return;
            }

            this.IsApiDisplayEnabled = attendeeOrganizationType?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = this.IsApiDisplayEnabled ? attendeeOrganizationType?.ApiHighlightPosition : null;
        }

        /// <summary>Updates the cropper image.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        private void UpdateCropperImage(AttemdeeOrganizationSiteMainInformationWidgetDto entity, bool isImageRequired)
        {
            this.CropperImage = new CropperImageBaseCommand(entity.Organization.ImageUploadDate, entity.Organization.Uid, FileRepositoryPathType.OrganizationImage, isImageRequired);
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        private void UpdateDescriptions(AttemdeeOrganizationSiteMainInformationWidgetDto entity, List<LanguageDto> languagesDtos, bool isDescriptionRequired)
        {
            this.Descriptions = new List<OrganizationDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                var description = entity?.DescriptionsDtos?.FirstOrDefault(d => d.LanguageDto.Code == languageDto.Code);
                this.Descriptions.Add(description != null ? new OrganizationDescriptionBaseCommand(description, isDescriptionRequired) :
                                                            new OrganizationDescriptionBaseCommand(languageDto, isDescriptionRequired));
            }
        }

        #endregion
    }
}