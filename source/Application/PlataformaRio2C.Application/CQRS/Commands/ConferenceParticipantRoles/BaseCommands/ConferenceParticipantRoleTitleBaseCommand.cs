// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleTitleBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel.DataAnnotations;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>ConferenceParticipantRoleTitleBaseCommand</summary>
    public class ConferenceParticipantRoleTitleBaseCommand
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [StringLength(100, MinimumLength = 1, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PropertyBetweenLengths")]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitleBaseCommand"/> class.</summary>
        /// <param name="conferenceParticipantRoleTitleDto">The conferenceParticipantRole name dto.</param>
        public ConferenceParticipantRoleTitleBaseCommand(ConferenceParticipantRoleTitleDto conferenceParticipantRoleTitleDto)
        {
            this.Value = conferenceParticipantRoleTitleDto.ConferenceParticipantRoleTitle?.Value;
            this.LanguageCode = conferenceParticipantRoleTitleDto.LanguageDto.Code;
            this.LanguageName = conferenceParticipantRoleTitleDto.LanguageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitleBaseCommand"/> class.</summary>
        /// <param name="languageDto">The language dto.</param>
        public ConferenceParticipantRoleTitleBaseCommand(LanguageDto languageDto)
        {
            this.LanguageCode = languageDto.Code;
            this.LanguageName = languageDto.Name;
        }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleTitleBaseCommand"/> class.</summary>
        public ConferenceParticipantRoleTitleBaseCommand()
        {
        }
    }
}