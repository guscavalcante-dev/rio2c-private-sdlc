// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-07-2023
// ***********************************************************************
// <copyright file="OnboardPlayerOrganizationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardPlayerOrganizationData</summary>
    public class OnboardPlayerOrganizationData : OrganizationSiteBaseCommand
    {
        public OrganizationType OrganizationType { get; private set; }

        public int? ProjectTypeId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerOrganizationData"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        /// <param name="isVirtualMeetingRequired">if set to <c>true</c> [is virtual meeting required].</param>
        public OnboardPlayerOrganizationData(
            OrganizationDto entity, 
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            bool isDescriptionRequired, 
            bool isAddressRequired, 
            bool isImageRequired,
            int projectTypeId,
            bool isVirtualMeetingRequired = true)
            : base(entity, languagesDtos, countriesBaseDtos, activities, targetAudiences, isDescriptionRequired, isAddressRequired, isImageRequired, projectTypeId,isVirtualMeetingRequired)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardPlayerOrganizationData"/> class.</summary>
        public OnboardPlayerOrganizationData()
        {
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
            string userInterfaceLanguage,
            int? projectTypeId
        )
        {
            this.OrganizationType = organizationType;
            this.ProjectTypeId = projectTypeId;
            this.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}