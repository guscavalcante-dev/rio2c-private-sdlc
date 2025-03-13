// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Ribeiro
// Created          : 03-10-2025
//
// Last Modified By : Rafael Ribeiro
// Last Modified On : 03-10-2025
// ***********************************************************************
// <copyright file="MusicBusinessRoundNegotiationReportGroupedByRoomDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBusinessRoundNegotiationReportGroupedByRoomDto</summary>
    public class MusicBusinessRoundNegotiationReportGroupedByRoomDto
    {
        public Room Room { get; set; }
        public List<MusicBusinessRoundNegotiationReportGroupedByStartDateDto> MusicBusinessRoundNegotiationReportGroupedByStartDateDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationReportGroupedByRoomDto"/> class.</summary>
        /// <param name="room">The room.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationReportGroupedByRoomDto(Room room, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.Room = room;
            this.MusicBusinessRoundNegotiationReportGroupedByStartDateDtos = negotiations?
                                                            .GroupBy(n => new { n.StartDate, n.EndDate })?
                                                            .OrderBy(g => g.Key.StartDate)
                                                            .Select(n => new MusicBusinessRoundNegotiationReportGroupedByStartDateDto(n.Key.StartDate, n.Key.EndDate, n.ToList()))?
                                                            .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationReportGroupedByRoomDto"/> class.</summary>
        public MusicBusinessRoundNegotiationReportGroupedByRoomDto()
        {
        }

        /// <summary>Gets the room name by language code.</summary>
        /// <param name="languageCode">The language code.</param>
        /// <returns></returns>
        public RoomName GetRoomNameByLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = "pt-br";
            }

            return this.Room.RoomNames?.FirstOrDefault(rn => rn.Language.Code == languageCode) ??
                   this.Room.RoomNames?.FirstOrDefault(rn => rn.Language.Code == "pt-br");
        }
    }
}