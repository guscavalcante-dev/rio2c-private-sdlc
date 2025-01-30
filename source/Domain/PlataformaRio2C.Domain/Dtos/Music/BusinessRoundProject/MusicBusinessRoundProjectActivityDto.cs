using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectActivityDto
    {
        public MusicBusinessRoundProjectActivity MusicBusinessRoundProjectActivity { get; set; }
        public Activity Activity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectActivityDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectActivityDto()
        {
        }
    }

}
