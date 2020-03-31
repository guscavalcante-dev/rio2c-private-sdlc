// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-30-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-30-2020
// ***********************************************************************
// <copyright file="NegotiationReportGroupedByRoomDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationReportGroupedByRoomDto</summary>
    public class NegotiationReportGroupedByRoomDto
    {
        public Room Room { get; set; }
        public List<NegotiationReportGroupedByStartDateDto> NegotiationReportGroupedByStartDateDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationReportGroupedByRoomDto"/> class.</summary>
        /// <param name="room">The room.</param>
        /// <param name="negotiations">The negotiations.</param>
        public NegotiationReportGroupedByRoomDto(Room room, List<Negotiation> negotiations)
        {
            this.Room = room;
            this.NegotiationReportGroupedByStartDateDtos = negotiations?
                                                            .GroupBy(n => new { n.StartDate, n.EndDate })?
                                                            .OrderBy(g => g.Key.StartDate)
                                                            .Select(n => new NegotiationReportGroupedByStartDateDto(n.Key.StartDate, n.Key.EndDate, n.ToList()))?
                                                            .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationReportGroupedByRoomDto"/> class.</summary>
        public NegotiationReportGroupedByRoomDto()
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