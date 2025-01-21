using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    public class MusicBusinessRoundProjectDto
    {
        public Guid Uid { get; set; }
        public bool IsFakeProject { get; set; }
        public int SellerAttendeeCollaboratorId { get; set; }
        public AttendeeCollaboratorDto SellerAttendeeCollaboratorDto { get; set; }
        public string PlayerCategoriesThatHaveOrHadContract { get; set; }
        public string AttachmentUrl { get; set; }
        public DateTimeOffset? FinishDate { get; set; }
        public int ProjectBuyerEvaluationsCount { get; set; }

        public IEnumerable<MusicBusinessRoundProjectTargetAudienceDto> MusicBusinessRoundProjectTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectInterestDto> MusicBusinessRoundProjectInterestDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectPlayerCategoryDto> PlayerCategoriesDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectExpectationsForMeetingDto> MusicBusinessRoundProjectExpectationsForMeetingDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectBuyerEvaluationDto> MusicBusinessRoundProjectBuyerEvaluationDtos { get; set; }

        #region Translations

        /// <summary>
        /// Gets the project expectations dto by language code.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public MusicBusinessRoundProjectExpectationsForMeetingDto GetProjectExpectationsDtoByLanguageCode(string culture)
        {
            return this.MusicBusinessRoundProjectExpectationsForMeetingDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MusicBusinessRoundProjectDto"/> class.
        /// </summary>
        public MusicBusinessRoundProjectDto()
        {
        }
    }

}
