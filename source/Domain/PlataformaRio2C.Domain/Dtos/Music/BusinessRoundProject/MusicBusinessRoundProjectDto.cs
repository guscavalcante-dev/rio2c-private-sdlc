using PlataformaRio2C.Domain.Entities;
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

        public DateTimeOffset CreateDate { get; set; }
        public IEnumerable<MusicBusinessRoundProjectTargetAudienceDto> MusicBusinessRoundProjectTargetAudienceDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectInterestDto> MusicBusinessRoundProjectInterestDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectPlayerCategoryDto> MusicBusinessRoundProjectPlayerCategoryDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectExpectationsForMeetingDto> MusicBusinessRoundProjectExpectationsForMeetingDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectBuyerEvaluationDto> MusicBusinessRoundProjectBuyerEvaluationDtos { get; set; }
        public IEnumerable<MusicBusinessRoundProjectActivityDto> MusicBusinessRoundProjectActivityDtos { get; set; }

        public bool IsFinished()
        {
            return this.FinishDate.HasValue;
        }

        #region Target Audiences

        /// <summary>
        /// Gets the target audience dto by target audience uid.
        /// </summary>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        public MusicBusinessRoundProjectTargetAudienceDto GetTargetAudienceDtoByTargetAudienceUid(Guid targetAudienceUid)
        {
            return this.MusicBusinessRoundProjectTargetAudienceDtos?.FirstOrDefault(ptad => ptad.TargetAudience.Uid == targetAudienceUid);
        }

        #endregion

        #region Interests

        /// <summary>Gets all interests by interest group uid.</summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        public List<MusicBusinessRoundProjectInterestDto> GetAllInterestsByInterestGroupUid(Guid interestGroupUid)
        {
            return this.MusicBusinessRoundProjectInterestDtos?.Where(pid => pid.InterestGroup?.Uid == interestGroupUid)?.ToList();
        }

        /// <summary>Gets the interest dto by interest uid.</summary>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        public MusicBusinessRoundProjectInterestDto GetInterestDtoByInterestUid(Guid interestUid)
        {
            return this.MusicBusinessRoundProjectInterestDtos?.FirstOrDefault(pid => pid.Interest.Uid == interestUid);
        }

        /// <summary>Gets the total interest groups matches count.</summary>
        /// <returns></returns>
        public int GetTotalInterestGroupsMatchesCount()
        {
            return this.MusicBusinessRoundProjectInterestDtos?.Count() ?? 0;
        }

        #endregion

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

           #region Project Buyer Evaluations

        /// <summary>Gets the project buyer evaluation maximum.</summary>
        /// <returns></returns>
        public int GetProjectBuyerEvaluationMax()
        {
           return this.SellerAttendeeCollaboratorDto.GetProjectMaxBuyerEvaluationsCount();
        }

        /// <summary>Gets the projects buyer evaluations used.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsUsed()
        {
            this.ProjectBuyerEvaluationsCount = this.MusicBusinessRoundProjectBuyerEvaluationDtos.Count();
            return this.ProjectBuyerEvaluationsCount;
        }

        /// <summary>Gets the projects buyer evaluations available.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsAvailable()
        {
            return this.GetProjectBuyerEvaluationMax() - this.GetProjectsBuyerEvaluationsUsed();
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
