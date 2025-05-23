﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-28-2022
// ***********************************************************************
// <copyright file="ConferenceTitleBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ConferenceTitleBaseCommand</summary>
    public class ConferenceTitleBaseCommand
    {
        [Display(Name = "Title", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(200, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTitleBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public ConferenceTitleBaseCommand(ConferenceTitleDto entity)
        {
            this.Value = entity.ConferenceTitle?.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTitleBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public ConferenceTitleBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceTitleBaseCommand"/> class.</summary>
        public ConferenceTitleBaseCommand()
        {
        }
    }
}