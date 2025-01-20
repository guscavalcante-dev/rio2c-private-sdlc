using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos.Music.BusinessRoundProject
{
    public class MusicBusinessRoundProjectDto
    {
        public int SellerAttendeeOrganizationId { get; set; }
        public AttendeeOrganizationDto SellerAttendeeOrganizationDto { get; set; }
        public string PlayerCategoriesThatHaveOrHadContract { get; set; }
        public string ExpectationsForOneToOneMeetings { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
        public int ProjectBuyerEvaluationsCount { get; set; }

        public IEnumerable<MusicBusinessRoundProjectTargetAudienceDto> MusicBusinessRoundProjectTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectInterestDto> MusicBusinessRoundProjectInterestDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectPlayerCategoryDto> PlayerCategoriesDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectDto()
        {
        }
    }

}
