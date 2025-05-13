// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-06-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-06-2020
// ***********************************************************************
// <copyright file="RoomNameBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>RoomNameBaseCommand</summary>
    public class RoomNameBaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="RoomNameBaseCommand"/> class.</summary>
        /// <param name="roomNameDto">The room name dto.</param>
        public RoomNameBaseCommand(RoomNameDto roomNameDto)
        {
            this.Value = roomNameDto.RoomName?.Value;
            this.LanguageCode = roomNameDto.LanguageDto.Code;
            this.LanguageName = roomNameDto.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="RoomNameBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public RoomNameBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="RoomNameBaseCommand"/> class.</summary>
        public RoomNameBaseCommand()
        {
        }
    }
}