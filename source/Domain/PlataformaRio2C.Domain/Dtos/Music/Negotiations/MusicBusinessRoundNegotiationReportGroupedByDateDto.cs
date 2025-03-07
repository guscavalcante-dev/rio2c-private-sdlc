using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationReportGroupedByDateDto
    {
        public DateTime Date { get; set; }
        public List<MusicBusinessRoundNegotiationReportGroupedByRoomDto> MusicBusinessRoundNegotiationReportGroupedByRoomDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationReportGroupedByDateDto"/> class.</summary>
        /// <param name="date">The date.</param>
        /// <param name="negotiations">The negotiations.</param>
        public MusicBusinessRoundNegotiationReportGroupedByDateDto(DateTime date, List<MusicBusinessRoundNegotiation> negotiations)
        {
            this.Date = date;
            this.MusicBusinessRoundNegotiationReportGroupedByRoomDtos = negotiations?
                                                            .GroupBy(n => n.Room)?
                                                            .OrderBy(n => n.Key.Id)
                                                            .Select(n => new MusicBusinessRoundNegotiationReportGroupedByRoomDto(n.Key, n.ToList()))?
                                                            .ToList();
        }

        /// <summary>Initializes a new instance of the <see cref="NegotiationGroupedByDateDto"/> class.</summary>
        public MusicBusinessRoundNegotiationReportGroupedByDateDto()
        {
        }
    }
}
