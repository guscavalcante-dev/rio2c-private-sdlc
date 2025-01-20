using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos.Music.BusinessRoundProject
{
    public class MusicBusinessRoundProjectInterestDto
    {
        public MusicBusinessRoundProjectInterest MusicBusinessRoundProjectInterest { get; set; }
        public Interest Interest { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectInterestDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectInterestDto()
        {
        }
    }

}
