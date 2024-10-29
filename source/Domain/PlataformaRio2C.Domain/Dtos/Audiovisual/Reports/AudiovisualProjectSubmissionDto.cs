// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Admin
// Author           : William Sergio Almado Junior
// Created          : 01-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-02-2021
// ***********************************************************************
// <copyright file="AudiovisualProjectSubmissionDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AudiovisualProjectSubmissionDto</summary>
    public class AudiovisualProjectSubmissionDto
    {
        //public int ProoducerCount { get; set; }
        //public int ProjectPerProducerCount { get; set; }
        //public bool IsFakeProject { get; set; }
        public Project Project { get; set; }
        public ProjectType ProjectType { get; set; }
        public ProjectModalityDto ProjectModalityDto { get; set; }
        public AttendeeOrganizationDto SellerAttendeeOrganizationDto { get; set; }
        public IEnumerable<ProjectTitleDto> ProjectTitleDtos { get; set; }
        public IEnumerable<ProjectLogLineDto> ProjectLogLineDtos { get; set; }
        public IEnumerable<ProjectSummaryDto> ProjectSummaryDtos { get; set; }
        public IEnumerable<ProjectProductionPlanDto> ProjectProductionPlanDtos { get; set; }
        public IEnumerable<ProjectAdditionalInformationDto> ProjectAdditionalInformationDtos { get; set; }
        public IEnumerable<ProjectInterestDto> ProjectInterestDtos { get; set; }
        public IEnumerable<ProjectTargetAudienceDto> ProjectTargetAudienceDtos { get; set; }
        public IEnumerable<ProjectImageLinkDto> ProjectImageLinkDtos { get; set; }
        public IEnumerable<ProjectTeaserLinkDto> ProjectTeaserLinkDtos { get; set; }
        public IEnumerable<ProjectBuyerEvaluationDto> ProjectBuyerEvaluationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AudiovisualProjectSubmissionDto"/> class.</summary>
        public AudiovisualProjectSubmissionDto()
        {
        }

        #region Interests

        /// <summary>Gets all interests by interest group uid.</summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <returns></returns>
        public List<ProjectInterestDto> GetAllInterestsByInterestGroupUid(Guid interestGroupUid)
        {
            return this.ProjectInterestDtos?.Where(pid => pid.InterestGroup.Uid == interestGroupUid)?.ToList();
        }

        /// <summary>Gets the interest dto by interest uid.</summary>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        public ProjectInterestDto GetInterestDtoByInterestUid(Guid interestUid)
        {
            return this.ProjectInterestDtos?.FirstOrDefault(pid => pid.Interest.Uid == interestUid);
        }

        #endregion

        #region Translations

        /// <summary>Gets the title dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ProjectTitleDto GetTitleDtoByLanguageCode(string culture)
        {
            return this.ProjectTitleDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the log line dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ProjectLogLineDto GetLogLineDtoByLanguageCode(string culture)
        {
            return this.ProjectLogLineDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the summary dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ProjectSummaryDto GetSummaryDtoByLanguageCode(string culture)
        {
            return this.ProjectSummaryDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the production plan dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ProjectProductionPlanDto GetProductionPlanDtoByLanguageCode(string culture)
        {
            return this.ProjectProductionPlanDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        /// <summary>Gets the additional information dto by language code.</summary>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public ProjectAdditionalInformationDto GetAdditionalInformationDtoByLanguageCode(string culture)
        {
            return this.ProjectAdditionalInformationDtos?.FirstOrDefault(ptd => ptd.Language.Code?.ToLowerInvariant() == culture?.ToLowerInvariant());
        }

        #endregion
    }
}