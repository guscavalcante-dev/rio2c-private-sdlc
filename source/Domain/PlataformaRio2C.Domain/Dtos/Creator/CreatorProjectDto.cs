// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-27-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2024
// ***********************************************************************
// <copyright file="CreatorProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CreatorProjectDto</summary>
    public class CreatorProjectDto : BaseDto
    {
        public string Name { get;  set; }
        public string Document { get;  set; }
        public string AgentName { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Email { get; set; }
        public string Curriculum { get;  set; }
        public string Title { get;  set; }
        public string Logline { get;  set; }
        public string Description { get;  set; }
        public string MotivationToDevelop { get;  set; }
        public string MotivationToTransform { get;  set; }
        public string DiversityAndInclusionElements { get;  set; }
        public string ThemeRelevation { get;  set; }
        public string MarketingStrategy { get;  set; }
        public string SimilarAudiovisualProjects { get;  set; }
        public string OnlinePlatformsWhereProjectIsAvailable { get;  set; }
        public string OnlinePlatformsAudienceReach { get;  set; }
        public string ProjectAwards { get;  set; }
        public string ProjectPublicNotice { get;  set; }
        public string PreviouslyDevelopedProjects { get;  set; }
        public string AssociatedInstitutions { get;  set; }
        public DateTimeOffset? ArticleFileUploadDate { get;  set; }
        public string ArticleFileExtension { get;  set; }
        public DateTimeOffset? ClippingFileUploadDate { get;  set; }
        public string ClippingFileExtension { get;  set; }
        public DateTimeOffset? OtherFileUploadDate { get;  set; }
        public string OtherFileExtension { get;  set; }
        public string OtherFileDescription { get; set; }
        public string Links { get;  set; }
        public DateTimeOffset TermsAcceptanceDate { get;  set; }

        public IEnumerable<AttendeeCreatorProjectDto> AttendeeCreatorProjectDtos { get; set; }
        public IEnumerable<InterestDto> InterestDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreatorProjectDto"/> class.</summary>
        public CreatorProjectDto()
        {
        }

        /// <summary>
        /// Gets the title abbreviation.
        /// </summary>
        /// <returns></returns>
        public string GetTitleAbbreviation()
        {
            return this.Title?.GetTwoLetterCode();
        }

        /// <summary>
        /// Gets the interest dtos by group uid.
        /// </summary>
        /// <param name="interestGroupUid">The interest group uid.</param>
        /// <param name="culture">The culture.</param>
        /// <returns></returns>
        public List<InterestDto> GetInterestDtosByGroupUid(Guid interestGroupUid)
        {
            return this.InterestDtos?.Where(dto => dto.InterestGroupUid == interestGroupUid)?.ToList();
        }

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <returns></returns>
        public List<string> GetLinks()
        {
            if (string.IsNullOrEmpty(this.Links))
                return new List<string>() { "-" };

            string pattern = @"(?<url>https?://[^\s]+)";

            MatchCollection matches = Regex.Matches(this.Links, pattern);

            List<string> links = new List<string>();

            foreach (Match match in matches)
            {
                links.Add(match.Groups["url"].Value);
            }

            return links;
        }

        /// <summary>
        /// This method is a temporary solution that should be deleted in the future!
        /// It converts the UID used in MyRio to the UID used by the people who developed the creator form (Rio2C CTO has it). 
        /// There is only one formatting difference between these values, which is applied in this method.
        /// </summary>
        /// <returns></returns>
        public string GetImageUid()
        {
            string imageUid = this.Uid.ToString().Remove(23, 1);

            return imageUid.ToUpper();
        }
    }
}