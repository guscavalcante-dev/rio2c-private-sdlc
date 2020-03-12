// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Arthur Souza
// Created          : 01-27-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-12-2020
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
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(48, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LogisticSponsorsNameBaseCommand"/> class.</summary>
        /// <param name="entity">The entity.</param>
        public LogisticSponsorsNameBaseCommand(AttendeeLogisticSponsorBaseDto entity, LanguageDto languageDto)
        {
            this.Value = entity?.GetNameByLanguageCode(languageDto.Code);;
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
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