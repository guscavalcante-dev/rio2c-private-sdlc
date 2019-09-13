// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-13-2019
// ***********************************************************************
// <copyright file="CreateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateOrganization</summary>
    public class CreateOrganization : OrganizationBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        /// <param name="activities">The activities.</param>
        /// <param name="targetAudiences">The target audiences.</param>
        /// <param name="groupedInterests">The grouped interests.</param>
        /// <param name="isDescriptionRequired">if set to <c>true</c> [is description required].</param>
        /// <param name="isAddressRequired">if set to <c>true</c> [is address required].</param>
        public CreateOrganization(
            List<HoldingBaseDto> holdingBaseDtos,
            List<LanguageDto> languagesDtos, 
            List<CountryBaseDto> countriesBaseDtos,
            List<Activity> activities,
            List<TargetAudience> targetAudiences,
            List<IGrouping<InterestGroup, Interest>> groupedInterests,
            bool isDescriptionRequired, 
            bool isAddressRequired)
        {
            this.UpdateBaseProperties(null, holdingBaseDtos, languagesDtos, countriesBaseDtos, activities, targetAudiences, groupedInterests, isDescriptionRequired, isAddressRequired);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        public CreateOrganization()
        {
        }
    }
}