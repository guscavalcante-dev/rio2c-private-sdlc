// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-23-2019
// ***********************************************************************
// <copyright file="CreateHolding.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateHolding</summary>
    public class CreateHolding : HoldingBaseCommand
    {
        /// <summary>Initializes a new instance of the <see cref="CreateHolding"/> class.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        public CreateHolding(List<LanguageDto> languagesDtos)
        {
            this.UpdateBaseProperties(null, languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateHolding"/> class.</summary>
        public CreateHolding()
        {
        }
    }
}