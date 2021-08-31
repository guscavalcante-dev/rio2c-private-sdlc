// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-18-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-18-2021
// ***********************************************************************
// <copyright file="CommissionAttendeeCollaboratorInterestsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CommissionAttendeeCollaboratorInterestsWidgetDto</summary>
    public class CommissionAttendeeCollaboratorInterestsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<CommissionAttendeeCollaboratorInterestDto> CommissionAttendeeCollaboratorInterestDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CommissionAttendeeCollaboratorInterestsWidgetDto"/> class.</summary>
        public CommissionAttendeeCollaboratorInterestsWidgetDto()
        {
        }

        /// <summary>
        /// Gets the commission attendee collaborator interest dto by interest uid.
        /// </summary>
        /// <param name="interestUid">The interest uid.</param>
        /// <returns></returns>
        public CommissionAttendeeCollaboratorInterestDto GetCommissionAttendeeCollaboratorInterestDtoByInterestUid(Guid interestUid)
        {
            return this.CommissionAttendeeCollaboratorInterestDtos?.FirstOrDefault(caci => caci.Interest.Uid == interestUid);
        }
    }
}