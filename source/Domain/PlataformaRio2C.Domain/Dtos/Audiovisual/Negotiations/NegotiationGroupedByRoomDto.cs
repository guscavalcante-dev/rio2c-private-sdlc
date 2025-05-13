// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 03-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="NegotiationGroupedByRoomDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationGroupedByRoomDto</summary>
    public class NegotiationGroupedByRoomDto
    {
        public Room Room { get; set; }
        public List<NegotiationGroupedByRoundDto> NegotiationGroupedByRoundDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByRoomDto"/> class.</summary>
        /// <param name="room">The room.</param>
        /// <param name="negotiations">The negotiations.</param>
        public NegotiationGroupedByRoomDto(Room room, List<Negotiation> negotiations)
        {
            this.Room = room;
            this.NegotiationGroupedByRoundDtos = negotiations?
                                                    .GroupBy(n => n.RoundNumber)?
                                                    .OrderBy(g => g.Key)
                                                    .Select(n => new NegotiationGroupedByRoundDto(n.Key, n.ToList()))?
                                                    .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByRoomDto"/> class.</summary>
        public NegotiationGroupedByRoomDto()
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