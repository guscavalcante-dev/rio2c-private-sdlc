// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="PresentationFormatNameBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>PresentationFormatNameBaseCommand</summary>
    public class PresentationFormatNameBaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormatNameBaseCommand"/> class.</summary>
        /// <param name="presentationFormatDto">The presentation format dto.</param>
        /// <param name="languageDto">The language dto.</param>
        public PresentationFormatNameBaseCommand(PresentationFormatDto presentationFormatDto, LanguageDto languageDto)
        {
            this.Value = presentationFormatDto?.PresentationFormat?.GetNameByLanguageCode(languageDto.Code);
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormatNameBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public PresentationFormatNameBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="PresentationFormatNameBaseCommand"/> class.</summary>
        public PresentationFormatNameBaseCommand()
        {
        }
    }
}