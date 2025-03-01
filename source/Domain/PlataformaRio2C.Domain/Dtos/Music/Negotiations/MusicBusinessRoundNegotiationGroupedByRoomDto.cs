using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationGroupedByRoomDto
    {
        public Room Room { get; set; }
        public List<MusicBusinessRoundNegotiationGroupedByRoundDto> NegotiationGroupedByRoundDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoomDto"/> class.</summary>
        /// <param name="room">The room.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationGroupedByRoomDto(Room room, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.Room = room;
            this.NegotiationGroupedByRoundDtos = negotiations?
                                                    .GroupBy(n => n.RoundNumber)?
                                                    .OrderBy(g => g.Key)
                                                    .Select(n => new MusicBusinessRoundNegotiationGroupedByRoundDto(n.Key, n.ToList()))?
                                                    .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByRoomDto"/> class.</summary>
        public MusicBusinessRoundNegotiationGroupedByRoomDto()
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
