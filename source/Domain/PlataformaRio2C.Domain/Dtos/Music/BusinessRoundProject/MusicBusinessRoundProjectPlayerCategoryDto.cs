using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectPlayerCategoryDto
    {
        public MusicBusinessRoundProjectPlayerCategory MusicBusinessRoundProjectPlayerCategory { get; set; }
        public PlayerCategory PlayerCategory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectPlayerCategoryDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectPlayerCategoryDto()
        {
        }
    }
}
