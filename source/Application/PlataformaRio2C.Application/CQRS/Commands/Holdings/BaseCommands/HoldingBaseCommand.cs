// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-16-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-16-2019
// ***********************************************************************
// <copyright file="HoldingBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>HoldingBaseCommand</summary>
    public class HoldingBaseCommand : BaseCommand<AppValidationResult>
    {
        public CropperImageBaseCommand CropperImage { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public string Name { get; set; }

        public IEnumerable<HoldingDescriptionBaseCommand> Descriptions { get; set; }
        public List<LanguageDto> LanguagesDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="HoldingBaseCommand"/> class.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        public HoldingBaseCommand(List<LanguageDto> languagesDtos)
        {
            this.LanguagesDtos = languagesDtos;
        }

        /// <summary>Initializes a new instance of the <see cref="HoldingBaseCommand"/> class.</summary>
        public HoldingBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="languagesDtos">The languages dtos.</param>
        public void UpdateBaseProperties(List<LanguageDto> languagesDtos)
        {
            this.LanguagesDtos = languagesDtos;
        }
    }
}