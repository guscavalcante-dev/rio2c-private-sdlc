// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-29-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-29-2019
// ***********************************************************************
// <copyright file="OnboardProducerOrganizationData.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>OnboardProducerOrganizationData</summary>
    public class OnboardMusicProducerOrganizationData : OrganizationSiteBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="OnboardProducerOrganizationData"/> class.</summary>
        /// <param name="entity">The entity.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        /// <param name="isImageRequired">if set to <c>true</c> [is image required].</param>
        public OnboardMusicProducerOrganizationData(
            OrganizationDto entity,
            List<LanguageDto> languagesDtos,
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            bool isDescriptionRequired,
            bool isAddressRequired,
            bool isImageRequired,
            int projectTypeId)
            : base(entity, languagesDtos, countriesBaseDtos, activities, targetAudiences, isDescriptionRequired, isAddressRequired, isImageRequired, projectTypeId)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OnboardProducerOrganizationData"/> class.</summary>
        public OnboardMusicProducerOrganizationData()
        {
        }
    }
}