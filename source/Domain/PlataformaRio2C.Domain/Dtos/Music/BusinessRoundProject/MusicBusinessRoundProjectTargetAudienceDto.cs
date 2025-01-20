using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos.Music.BusinessRoundProject
{
    public class MusicBusinessRoundProjectTargetAudienceDto
    {
        public MusicBusinessRoundProjectTargetAudience MusicBusinessRoundProjectTargetAudience { get; set; }
        public TargetAudience TargetAudience { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectTargetAudienceDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectTargetAudienceDto()
        {
        }
    }
}
