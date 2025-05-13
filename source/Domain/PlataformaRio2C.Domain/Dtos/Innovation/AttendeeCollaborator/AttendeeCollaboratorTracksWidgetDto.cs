// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-30-2022
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTracksWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTracksWidgetDto</summary>
    public class AttendeeCollaboratorTracksWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeCollaboratorInnovationOrganizationTrackDto> AttendeeCollaboratorInnovationOrganizationTrackDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTracksWidgetDto"/> class.</summary>
        public AttendeeCollaboratorTracksWidgetDto()
        {
        }

        /// <summary>
        /// Gets the attendee collaborator innovation organization track dto by track option uid.
        /// </summary>
        /// <param name="trackOptionUid">The track option uid.</param>
        /// <returns></returns>
        public AttendeeCollaboratorInnovationOrganizationTrackDto GetAttendeeCollaboratorInnovationOrganizationTrackDtoByTrackOptionUid(Guid trackOptionUid)
        {
            return this.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(dto => dto.InnovationOrganizationTrackOption.Uid == trackOptionUid);
        }

        /// <summary>
        /// Gets the attendee collaborator innovation organization track dto by track option group uid.
        /// </summary>
        /// <param name="trackOptionGroupUid">The track option group uid.</param>
        /// <returns></returns>
        public AttendeeCollaboratorInnovationOrganizationTrackDto GetAttendeeCollaboratorInnovationOrganizationTrackDtoByTrackOptionGroupUid(Guid trackOptionGroupUid)
        {
            return this.AttendeeCollaboratorInnovationOrganizationTrackDtos?.FirstOrDefault(dto => dto.InnovationOrganizationTrackOption?.InnovationOrganizationTrackOptionGroup?.Uid == trackOptionGroupUid);
        }
    }
}