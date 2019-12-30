// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-18-2019
// ***********************************************************************
// <copyright file="CreateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateOrganization</summary>
    public class CreateOrganization : OrganizationBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        /// <param name="organizationType">Type of the organization.</param>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="interestsDtos">The interests dtos.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isRestrictionSpecificRequired">if set to <c>true</c> [is restriction specific required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public CreateOrganization(
            OrganizationType organizationType,
            List<HoldingBaseDto> holdingBaseDtos,
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<InterestDto> interestsDtos,
            bool isDescriptionRequired, 
            bool isAddressRequired, 
            bool isRestrictionSpecificRequired, 
            bool isImageRequired)
        {
            this.UpdateBaseProperties(
                null,
                organizationType,
                holdingBaseDtos, 
                languagesDtos, 
                countriesBaseDtos, 
                activities, 
                targetAudiences,
                interestsDtos, 
                isDescriptionRequired, 
                isAddressRequired, 
                isRestrictionSpecificRequired,
                isImageRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        public CreateOrganization()
        {
        }
    }
}