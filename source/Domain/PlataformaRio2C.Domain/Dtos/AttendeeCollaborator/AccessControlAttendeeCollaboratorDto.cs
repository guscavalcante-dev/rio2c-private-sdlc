// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="AccessControlAttendeeCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AccessControlAttendeeCollaboratorDto</summary>
    public class AccessControlAttendeeCollaboratorDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public User User { get; set; }
        public Language Language { get; set; }
        public bool IsPendingAttendeeCollaboratorOnboarding { get; set; }
        public bool IsPendingAttendeeOrganizationOnboarding { get; set; }

        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<AttendeeCollaboratorTicket> AttendeeCollaboratorTickets { get; set; }
        public IEnumerable<TicketType> TicketTypes { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AccessControlAttendeeCollaboratorDto"/> class.</summary>
        public AccessControlAttendeeCollaboratorDto()
        {
        }
    }
}