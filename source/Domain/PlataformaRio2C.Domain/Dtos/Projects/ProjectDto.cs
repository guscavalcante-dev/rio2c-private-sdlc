// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-31-2020
// ***********************************************************************
// <copyright file="ProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectDto</summary>
    public class ProjectDto
    {
        public bool IsFakeProject { get; set; }
        public Project Project { get; set; }
        public ProjectType ProjectType { get; set; }
        public AttendeeOrganizationDto SellerAttendeeOrganizationDto { get; set; }
        public ProjectCommissionEvaluationDto ProjectCommissionEvaluationDto { get; set; }

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
        public IEnumerable<InterestGroup> InterestGroupsMatches { get; set; }
        public IEnumerable<ProjectCommissionEvaluationDto> ProjectCommissionEvaluationDtos { get; set; }

        public string ProjectTitle { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectDto"/> class.</summary>
        public ProjectDto()
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

        /// <summary>Gets the total interest groups matches count.</summary>
        /// <returns></returns>
        public int GetTotalInterestGroupsMatchesCount()
        {
            return this.InterestGroupsMatches?.Count() ?? 0;
        }

        #endregion

        #region Target Audiences

        /// <summary>Gets the target audience dto by target audience uid.</summary>
        /// <param name="targetAudienceUid">The target audience uid.</param>
        /// <returns></returns>
        public ProjectTargetAudienceDto GetTargetAudienceDtoByTargetAudienceUid(Guid targetAudienceUid)
        {
            return this.ProjectTargetAudienceDtos?.FirstOrDefault(ptad => ptad.TargetAudience.Uid == targetAudienceUid);
        }

        #endregion

        #region Project Buyer Evaluations

        /// <summary>Gets the project buyer evaluation maximum.</summary>
        /// <returns></returns>
        public int GetProjectBuyerEvaluationMax()
        {
            return this.SellerAttendeeOrganizationDto.GetProjectMaxBuyerEvaluationsCount();
        }

        /// <summary>Gets the projects buyer evaluations used.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsUsed()
        {
            return this.Project?.ProjectBuyerEvaluationsCount ?? 0;
        }

        /// <summary>Gets the projects buyer evaluations available.</summary>
        /// <returns></returns>
        public int GetProjectsBuyerEvaluationsAvailable()
        {
            return this.GetProjectBuyerEvaluationMax() - this.GetProjectsBuyerEvaluationsUsed();
        }

        #endregion

        #region Commission Evaluations

        /// <summary>
        /// Gets the project commission evaluation dto by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ProjectCommissionEvaluationDto GetProjectCommissionEvaluationDtoByUserId(int? userId)
        {
            if (!userId.HasValue)
                return null;

            if (this.ProjectCommissionEvaluationDtos == null)
            {
                this.ProjectCommissionEvaluationDtos = new List<ProjectCommissionEvaluationDto>();
            }

            return this.ProjectCommissionEvaluationDtos.FirstOrDefault(w => w.EvaluatorUser?.Id == userId);
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