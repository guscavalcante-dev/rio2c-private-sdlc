// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-21-2023
// ***********************************************************************
// <copyright file="UpdateOrganizationAdminMainInformation.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateOrganizationAdminMainInformation</summary>
    public class UpdateOrganizationAdminMainInformation : UpdateOrganizationMainInformationBaseCommand
    {
        public bool IsAudiovisualBuyer => (this.OrganizationType != null && this.OrganizationType?.Name == OrganizationType.AudiovisualPlayer.Name);

        public Guid? OrganizationTypeUid { get; set; }

        [Display(Name = "Holding", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsAudiovisualBuyer), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? HoldingUid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [RequiredIf(nameof(IsAudiovisualBuyer), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(81, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Name { get; set; }

        [Display(Name = "TradeName", ResourceType = typeof(Labels))]
        //[RequiredIf(nameof(IsAudiovisualBuyer), "False", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string TradeName { get; set; }

        public bool IsVirtualMeetingRequired { get; set; }

        [Display(Name = "MeetingType", ResourceType = typeof(Labels))]
        [RadioButtonRequiredIf(nameof(IsVirtualMeetingRequired), "True", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public bool? IsVirtualMeeting { get; set; }

        public int[] ApiHighlightPositions = new[] { 1, 2, 3, 4, 5 };

        [Display(Name = "DisplayOnSite", ResourceType = typeof(Labels))]
        public bool IsApiDisplayEnabled { get; set; }

        [Display(Name = "HighlightPosition", ResourceType = typeof(Labels))]
        public int? ApiHighlightPosition { get; set; }

        public OrganizationType OrganizationType { get; private set; }
        public List<HoldingBaseDto> HoldingBaseDtos { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationAdminMainInformation"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateOrganizationAdminMainInformation(
            AttendeeOrganizationMainInformationWidgetDto entity,
            OrganizationType organizationType,
            List<HoldingBaseDto> holdingBaseDtos,
            List<LanguageDto> languagesDtos)
            : base (entity, languagesDtos, false, false, false, false)
        {
            this.OrganizationTypeUid = organizationType?.Uid;
            this.OrganizationType = organizationType;

            this.HoldingUid = entity?.Organization?.Holding?.Uid;
            this.Name = entity?.Organization?.Name;
            this.TradeName = entity?.Organization?.TradeName;
            this.IsVirtualMeeting = entity?.Organization?.IsVirtualMeeting;
            this.IsVirtualMeetingRequired = organizationType?.Uid == OrganizationType.AudiovisualPlayer.Uid;

            this.UpdateModelsAndLists(holdingBaseDtos);
            this.UpdateApiConfigurations(entity, organizationType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrganizationAdminMainInformation"/> class.
        /// </summary>
        public UpdateOrganizationAdminMainInformation()
        {
        }

        /// <summary>
        /// Updates the models and lists.
        /// </summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        public void UpdateModelsAndLists(List<HoldingBaseDto> holdingBaseDtos)
        {
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

            if (!this.IsAudiovisualBuyer)
            {
                this.Name = TradeName;
            }
        }

        #region Private Methods

        /// <summary>Updates the API configurations.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="organizationType">Type of the organization.</param>
        private void UpdateApiConfigurations(AttendeeOrganizationMainInformationWidgetDto entity, OrganizationType organizationType)
        {
            var attendeeOrganizationType = entity?.AttendeeOrganization?.AttendeeOrganizationTypes?.FirstOrDefault(aotd => aotd.OrganizationType.Uid == organizationType.Uid);
            if (attendeeOrganizationType == null)
            {
                return;
            }

            this.IsApiDisplayEnabled = attendeeOrganizationType?.IsApiDisplayEnabled ?? false;
            this.ApiHighlightPosition = this.IsApiDisplayEnabled ? attendeeOrganizationType?.ApiHighlightPosition : null;
        }

        #endregion
    }
}