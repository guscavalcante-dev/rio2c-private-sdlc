// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-27-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-31-2023
// ***********************************************************************
// <copyright file="ConferenceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceDto</summary>
    public class ConferenceDto
    {
        public Guid Uid { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }

        public Conference Conference { get; set; }
        public EditionEvent EditionEvent { get; set; }
        public RoomDto RoomDto { get; set; }

        public IEnumerable<ConferenceTitleDto> ConferenceTitleDtos { get; set; }
        public IEnumerable<ConferenceSynopsisDto> ConferenceSynopsisDtos { get; set; }
        public IEnumerable<ConferenceParticipantDto> ConferenceParticipantDtos { get; set; }
        public IEnumerable<ConferenceTrackDto> ConferenceTrackDtos { get; set; }
        public IEnumerable<ConferencePresentationFormatDto> ConferencePresentationFormatDtos { get; set; }
        public IEnumerable<ConferencePillarDto> ConferencePillarDtos { get; set; }

        public bool? IsParticipant { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceDto"/> class.</summary>
        public ConferenceDto()
        {
        }

        /// <summary>Gets the conference title dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public ConferenceTitleDto GetConferenceTitleDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.ConferenceTitleDtos?.FirstOrDefault(ctDto => ctDto.LanguageDto.Code == languageCode);
        }

        /// <summary>Gets the conference synopsis dto by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public ConferenceSynopsisDto GetConferenceSynopsisDtoByLanguageCode(string languageCode)
        {
            return this.ConferenceSynopsisDtos?.FirstOrDefault(csDto => csDto.LanguageDto.Code == languageCode);
        }

        /// <summary>
        /// Gets the room name dto by language code.
        /// </summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public RoomNameDto GetRoomNameDtoByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.RoomDto?.RoomNameDtos?.FirstOrDefault(rnDto => rnDto.LanguageDto.Code == languageCode);
        }
    }
}