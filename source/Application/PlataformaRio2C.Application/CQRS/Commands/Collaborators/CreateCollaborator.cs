// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-26-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-26-2019
// ***********************************************************************
// <copyright file="CreateCollaborator.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateCollaborator</summary>
    public class CreateCollaborator : CollaboratorBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        /// <param name="holdingBaseDtos">The holding base dtos.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        /// <param name="countriesBaseDtos">The countries base dtos.</param>
        public CreateCollaborator(List<HoldingBaseDto> holdingBaseDtos, List<LanguageDto> languagesDtos, List<CountryBaseDto> countriesBaseDtos)
        {
            this.UpdateBaseProperties(null, holdingBaseDtos, languagesDtos, countriesBaseDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateCollaborator"/> class.</summary>
        public CreateCollaborator()
        {
        }
    }
}