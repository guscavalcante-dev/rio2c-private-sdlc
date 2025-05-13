// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-20-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-20-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationReportDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationReportDto</summary>
    public class AttendeeInnovationOrganizationReportDto
    {
        public Guid InnovationOrganizationUid { get; set; }
        public int InnovationOrganizationId { get; set; }
        public Guid AttendeeInnovationOrganizationUid { get; set; }
        public int AttendeeInnovationOrganizationId { get; set; }

        public string CompanyName { get; set; }
        public string ServiceName { get; set; }
        public string Document { get; set; }
        public string Description { get; set; }
        public int? FoundationYear { get; set; }
        public string BusinessDefinition { get; set; }
        public string BusinessFocus { get; set; }
        public string BusinessDifferentials { get; set; }
        public string BusinessEconomicModel { get; set; }
        public string BusinessOperationalModel { get; set; }
        public string BusinessStage { get; set; }
        public string ProductOrServiceName { get; set; }
        public string Website { get; set; }
        public string VideoUrl { get; set; }

        public DateTimeOffset? CreateDate { get; set; }
        public DateTimeOffset? UpdateDate { get; set; }
        public decimal AccumulatedRevenue { get; set; }
        public bool? WouldYouLikeParticipateBusinessRound { get; set; }
        public bool? WouldYouLikeParticipatePitching { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public DateTimeOffset? PresentationUploadDate { get; set; }
        public string PresentationFileExtension { get; set; }

        public CollaboratorDto ResponsibleCollaboratorDto { get; set; }

        public IEnumerable<AttendeeInnovationOrganizationTrackDto> AttendeeInnovationOrganizationTrackDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationCompetitorDto> AttendeeInnovationOrganizationCompetitorDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationTechnologyDto> AttendeeInnovationOrganizationTechnologyDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationExperienceDto> AttendeeInnovationOrganizationExperienceDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationObjectiveDto> AttendeeInnovationOrganizationObjectiveDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto> AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationFounderDto> AttendeeInnovationOrganizationFounderDtos { get; set; }



        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationReportDto"/> class.</summary>
        public AttendeeInnovationOrganizationReportDto()
        {
        }
    }
}