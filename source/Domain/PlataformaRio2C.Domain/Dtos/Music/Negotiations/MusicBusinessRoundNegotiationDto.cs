using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationDto
    {
        public MusicBusinessRoundNegotiation Negotiation { get; set; }
        public MusicBusinessRoundProjectBuyerEvaluationDto ProjectBuyerEvaluationDto  { get; set; }
        public RoomDto RoomDto { get; set; }
        public UserBaseDto UpdaterDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NegotiationDto"/> class.</summary>
        public MusicBusinessRoundNegotiationDto()
        {
        }
    }
}
