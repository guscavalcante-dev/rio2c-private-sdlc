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
namespace PlataformaRio2C.Application.CQRS.Queries
{
    /// <summary>FindAllConferencesByAttendeeCollaboratorIdQuery</summary>
    public class FindAllConferencesByAttendeeCollaboratorIdQuery : BaseQuery<FindAllConferencesByAttendeeCollaboratorIdResponseDto>
    {
        public int AttendeeCollaboratorId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllConferencesByAttendeeCollaboratorIdQuery" /> class.
        /// </summary>
        /// <param name="attendeeCollaboratorId">The attendee collaborator identifier.</param>
        public FindAllConferencesByAttendeeCollaboratorIdQuery(int attendeeCollaboratorId)
        {
            this.AttendeeCollaboratorId = attendeeCollaboratorId;
        }
    }
}