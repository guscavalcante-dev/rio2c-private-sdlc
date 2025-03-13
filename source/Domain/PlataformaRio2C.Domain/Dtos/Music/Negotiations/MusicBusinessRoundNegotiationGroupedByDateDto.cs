using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationGroupedByDateDto
    {
        public DateTime Date { get; set; }
        public List<MusicBusinessRoundNegotiationGroupedByRoomDto> NegotiationGroupedByRoomDtos { get; set; }
        public List<MusicBusinessRoundNegotiationDto> NegotiationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByDateDto"/> class.</summary>
        /// <param name="date">The date.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationGroupedByDateDto(DateTime date, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.Date = date;
            this.NegotiationGroupedByRoomDtos = negotiations?
                                                    .GroupBy(n => n.Room)?
                                                    .OrderBy(n => n.Key.Id)
                                                    .Select(n => new MusicBusinessRoundNegotiationGroupedByRoomDto(n.Key, n.ToList()))?
                                                    .ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByDateDto"/> class.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="negotiationsDtos">The negotiations dtos.</param>
        public MusicBusinessRoundNegotiationGroupedByDateDto(DateTime date, List<MusicBusinessRoundNegotiationDto> negotiationsDtos)
        {
            this.Date = date;
            this.NegotiationDtos = negotiationsDtos;
        }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationGroupedByDateDto"/> class.</summary>
        public MusicBusinessRoundNegotiationGroupedByDateDto()
        {
        }
    }
}
