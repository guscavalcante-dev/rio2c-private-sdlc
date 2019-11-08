// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-06-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-08-2019
// ***********************************************************************
// <copyright file="ProjectDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ProjectDto</summary>
    public class ProjectDto
    {
        public Project Project { get; set; }
        public ProjectType ProjectType { get; set; }
        public IEnumerable<ProjectTitleDto> ProjectTitleDtos { get; set; }
        public IEnumerable<ProjectLogLineDto> ProjectLogLineDtos { get; set; }
        public IEnumerable<ProjectSummaryDto> ProjectSummaryDtos { get; set; }
        public IEnumerable<ProjectProductionPlanDto> ProjectProductionPlanDtos { get; set; }
        public IEnumerable<ProjectAdditionalInformationDto> ProjectAdditionalInformationDtos { get; set; }
        public IEnumerable<ProjectInterestDto> ProjectInterestDtos { get; set; }
        public IEnumerable<ProjectTargetAudienceDto> ProjectTargetAudienceDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ProjectDto"/> class.</summary>
        public ProjectDto()
        {
        }
    }
}