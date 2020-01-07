// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantRoleDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceParticipantRoleDto</summary>
    public class ConferenceParticipantRoleDto
    {
        public ConferenceParticipantRole ConferenceParticipantRole { get; set; }
        public IEnumerable<ConferenceParticipantRoleTitleDto> ConferenceParticipantRoleTitleDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantRoleDto"/> class.</summary>
        public ConferenceParticipantRoleDto()
        {
        }

        /// <summary>Gets the conference participant role title dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public ConferenceParticipantRoleTitleDto GetConferenceParticipantRoleTitleDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.ConferenceParticipantRoleTitleDtos?.FirstOrDefault(rn => rn.LanguageDto.Code == languageCode) ??
                   this.ConferenceParticipantRoleTitleDtos?.FirstOrDefault(rn => rn.LanguageDto.Code == "pt-br");
        }
    }
}