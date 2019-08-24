// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="CreateOrganization.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateOrganization</summary>
    public class CreateOrganization : OrganizationBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public CreateOrganization(List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos, List<CountryBaseDto> countriesBaseDtos)
        {
            this.UpdateBaseProperties(null, holdingBaseDtos, languagesDtos, countriesBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateOrganization"/> class.</summary>
        public CreateOrganization()
        {
        }
    }
}