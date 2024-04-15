// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Updated          : 15-04-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 15-04-2024
// ***********************************************************************
// <copyright file="UpdateAvailability.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateAvailability</summary>
    public class UpdateAvailability : AvailabilityBaseCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAvailability" /> class.
        /// </summary>
        /// <param name="attendeeCollaboratorBaseDto">The attendee collaborator base dto.</param>
        public UpdateAvailability(AttendeeCollaboratorBaseDto attendeeCollaboratorBaseDto)
        {
            this.AttendeeCollaboratorUid = attendeeCollaboratorBaseDto.Uid;
            this.AvailabilityBeginDate = attendeeCollaboratorBaseDto.AvailabilityEndDate;
            this.AvailabilityEndDate = attendeeCollaboratorBaseDto.AvailabilityEndDate;
        }

        public UpdateAvailability() { }
    }
}