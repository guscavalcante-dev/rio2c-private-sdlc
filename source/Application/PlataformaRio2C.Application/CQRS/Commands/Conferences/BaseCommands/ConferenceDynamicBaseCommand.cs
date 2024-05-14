// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-09-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-09-2024
// ***********************************************************************
// <copyright file="ConferenceDynamicBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ConferenceDynamicBaseCommand</summary>
    public class ConferenceDynamicBaseCommand
    {
        [Display(Name = "ConferenceDynamic", ResourceType = typeof(Labels))]
        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(1000, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamicBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public ConferenceDynamicBaseCommand(ConferenceDynamicDto entity)
        {
            this.Value = entity.ConferenceDynamic?.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamicBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public ConferenceDynamicBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDynamicBaseCommand"/> class.</summary>
        public ConferenceDynamicBaseCommand()
        {
        }
    }
}