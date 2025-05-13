using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundNegotiationDto
    {
        public MusicBusinessRoundNegotiation Negotiation { get; set; }
        public MusicBusinessRoundProjectBuyerEvaluationDto ProjectBuyerEvaluationDto { get; set; }
        public RoomDto RoomDto { get; set; }

        public UserBaseDto UpdaterDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MusicBusinessRoundNegotiationDto"/> class.</summary>
        public MusicBusinessRoundNegotiationDto()
        {
        }
    }
}
