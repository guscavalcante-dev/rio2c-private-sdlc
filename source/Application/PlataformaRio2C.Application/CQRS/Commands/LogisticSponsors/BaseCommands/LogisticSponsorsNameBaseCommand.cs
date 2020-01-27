// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Arthur Souza
// Last Modified On : 01-27-2020
// ***********************************************************************
// <copyright file="LogisticSponsorsNameBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>LogisticSponsorsNameBaseCommand</summary>
    public class LogisticSponsorsNameBaseCommand
    {
        [Display(Name = "Title", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(65, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsNameBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public LogisticSponsorsNameBaseCommand(ConferenceTitleDto entity)
        {
            this.Value = entity.ConferenceTitle?.Value;
            this.LanguageCode = entity.LanguageDto.Code;
            this.LanguageName = entity.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsNameBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public LogisticSponsorsNameBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsNameBaseCommand"/> class.</summary>
        public LogisticSponsorsNameBaseCommand()
        {
        }
    }
}