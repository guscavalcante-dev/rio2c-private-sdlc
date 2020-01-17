// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="TrackNameBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>TrackNameBaseCommand</summary>
    public class TrackNameBaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="TrackNameBaseCommand"/> class.</summary>
        /// <param name="trackDto">The track dto.</param>
        /// <param name="languageDto">The language dto.</param>
        public TrackNameBaseCommand(TrackDto trackDto, LanguageDto languageDto)
        {
            this.Value = trackDto?.Track?.GetNameByLanguageCode(languageDto.Code);
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="TrackNameBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public TrackNameBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="TrackNameBaseCommand"/> class.</summary>
        public TrackNameBaseCommand()
        {
        }
    }
}