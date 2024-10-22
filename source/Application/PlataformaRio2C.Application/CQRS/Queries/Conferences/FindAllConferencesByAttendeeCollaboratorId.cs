// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="FindAllConferencesByAttendeeCollaboratorIdQuery.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllConferencesByAttendeeCollaboratorIdQuery</summary>
    public class FindAllConferencesByAttendeeCollaboratorId : BaseQuery<List<ConferenceDto>>
    {
        public int AttendeeCollaboratorId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllConferencesByAttendeeCollaboratorId" /> class.
        /// </summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        public FindAllConferencesByAttendeeCollaboratorId(int attendeeCollaboratorId)
        {
            this.AttendeeCollaboratorId = attendeeCollaboratorId;
        }
    }
}