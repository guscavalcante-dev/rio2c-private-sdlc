// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-26-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-05-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationDto.cs" company="Softo">
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
    /// <summary>AttendeeInnovationOrganizationDto</summary>
    public class AttendeeInnovationOrganizationDto
    {
        public bool WouldYouLikeParticipateBusinessRound { get; set; }

        public InnovationOrganization InnovationOrganization { get; set; }
        public AttendeeInnovationOrganization AttendeeInnovationOrganization { get; set; }
        public AttendeeInnovationOrganizationEvaluationDto AttendeeInnovationOrganizationEvaluationDto { get; set; }

        public IEnumerable<AttendeeInnovationOrganizationFounderDto> AttendeeInnovationOrganizationFounderDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationCompetitorDto> AttendeeInnovationOrganizationCompetitorDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationCollaboratorDto> AttendeeInnovationOrganizationCollaboratorDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationTrackDto> AttendeeInnovationOrganizationTrackDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationExperienceDto> AttendeeInnovationOrganizationExperienceDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationTechnologyDto> AttendeeInnovationOrganizationTechnologyDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationObjectiveDto> AttendeeInnovationOrganizationObjectiveDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationEvaluationDto> AttendeeInnovationOrganizationEvaluationDtos { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto> AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto { get; set; }

        /// <summary>
        /// Gets the attendee innovation organization evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationEvaluationDto GetAttendeeInnovationOrganizationEvaluationByUserId(int? userId)
        {
            if (!userId.HasValue)
                return null;

            if(this.AttendeeInnovationOrganizationEvaluationDtos == null)
            {
                this.AttendeeInnovationOrganizationEvaluationDtos = new List<AttendeeInnovationOrganizationEvaluationDto>();
            }

            return this.AttendeeInnovationOrganizationEvaluationDtos.FirstOrDefault(w => w.EvaluatorUser?.Id == userId);
        }

        /// <summary>
        /// Gets the attendee collaborator innovation organization track dto by track option uid.
        /// </summary>
        /// <param name="trackOptionUid">The track option uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationTrackDto GetAttendeeInnovationOrganizationTrackByTrackOptionUid(Guid trackOptionUid)
        {
            return this.AttendeeInnovationOrganizationTrackDtos?.FirstOrDefault(aiotDto => aiotDto.InnovationOrganizationTrackOption.Uid == trackOptionUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization track by track option group uid.
        /// </summary>
        /// <param name="trackOptionGroupUid">The track option group uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationTrackDto GetAttendeeInnovationOrganizationTrackByTrackOptionGroupUid(Guid trackOptionGroupUid)
        {
            return this.AttendeeInnovationOrganizationTrackDtos?.FirstOrDefault(aiotDto => aiotDto.InnovationOrganizationTrackOptionGroup?.Uid == trackOptionGroupUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization objective by objective option uid.
        /// </summary>
        /// <param name="objectiveOptionUid">The objective option uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationObjectiveDto GetAttendeeInnovationOrganizationObjectiveByObjectiveOptionUid(Guid objectiveOptionUid)
        {
            return this.AttendeeInnovationOrganizationObjectiveDtos?.FirstOrDefault(aiotDto => aiotDto.InnovationOrganizationObjectivesOption.Uid == objectiveOptionUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization objective by objective option uid.
        /// </summary>
        /// <param name="objectiveOptionUid">The objective option uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto GetAttendeeInnovationOrganizationSustainableDevelopmentObjectiveOptionUid(Guid objectiveOptionUid)
        {
            return this.AttendeeInnovationOrganizationSustainableDevelopmentObjectiveDto?.FirstOrDefault(aiosDto => aiosDto.InnovationOrganizationSustainableDevelopmentObjectivesOption.Uid == objectiveOptionUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization technology by technology option uid.
        /// </summary>
        /// <param name="technologyOptionUid">The technology option uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationTechnologyDto GetAttendeeInnovationOrganizationTechnologyByTechnologyOptionUid(Guid technologyOptionUid)
        {
            return this.AttendeeInnovationOrganizationTechnologyDtos?.FirstOrDefault(aiotDto => aiotDto.InnovationOrganizationTechnologyOption.Uid == technologyOptionUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization experience by experience option uid.
        /// </summary>
        /// <param name="experienceOptionUid">The experience option uid.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationExperienceDto GetAttendeeInnovationOrganizationExperienceByExperienceOptionUid(Guid experienceOptionUid)
        {
            return this.AttendeeInnovationOrganizationExperienceDtos?.FirstOrDefault(aiotDto => aiotDto.InnovationOrganizationExperienceOption.Uid == experienceOptionUid);
        }

        /// <summary>
        /// Gets the attendee innovation organization evaluation by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public AttendeeInnovationOrganizationCollaboratorDto GetAttendeeInnovationOrganizationCollaboratorByEditionId(int? editionId)
        {
            if (!editionId.HasValue)
                return null;

            if (this.AttendeeInnovationOrganizationCollaboratorDtos == null)
            {
                this.AttendeeInnovationOrganizationCollaboratorDtos = new List<AttendeeInnovationOrganizationCollaboratorDto>();
            }

            return this.AttendeeInnovationOrganizationCollaboratorDtos.FirstOrDefault(w => w.AttendeeCollaborator.EditionId == editionId);
        }


        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationDto"/> class.</summary>
        public AttendeeInnovationOrganizationDto()
        {
        }
    }
}