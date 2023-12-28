// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-28-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInterestsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorInterestsWidgetDto</summary>
    public class AttendeeCollaboratorInterestsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeCollaboratorInterestDto> AttendeeCollaboratorInterestDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInterestsWidgetDto"/> class.</summary>
        public AttendeeCollaboratorInterestsWidgetDto()
        {
        }

        /// <summary>
        /// Gets the attendee collaborator interest dto by interest uid.
        /// </summary>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        public AttendeeCollaboratorInterestDto GetAttendeeCollaboratorInterestDtoByInterestUid(Guid interestUid)
        {
            return this.AttendeeCollaboratorInterestDtos?.FirstOrDefault(caci => caci.Interest.Uid == interestUid);
        }
    }
}