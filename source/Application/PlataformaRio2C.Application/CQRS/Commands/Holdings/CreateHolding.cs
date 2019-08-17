// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
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
            this.CropperImage = new CropperImageBaseCommand(false, null);
            this.UpdateDescriptions(languagesDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateHolding"/> class.</summary>
        public CreateHolding()
        {
        }

        /// <summary>Updates the descriptions.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        private void UpdateDescriptions(List<LanguageDto> languagesDtos)
        {
            this.Descriptions = new List<HoldingDescriptionBaseCommand>();
            foreach (var languageDto in languagesDtos)
            {
                this.Descriptions.Add(new HoldingDescriptionBaseCommand(languageDto));
            }
        }
    }
}