// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-01-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-26-2019
// ***********************************************************************
// <copyright file="DeleteCollaboratorTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteCollaboratorTicket</summary>
    public class DeleteCollaboratorTicket : BaseCommand
    {
        public Collaborator Collaborator { get; private set; }
        public SalesPlatformAttendeeDto SalesPlatformAttendeeDto { get; private set; }
        public Edition Edition { get; private set; }
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }
        public CollaboratorType CollaboratorType { get; set; }
        public Role Role { get; set; }

        /// <summary>Initializes a new instance of the <see cref="DeleteCollaboratorTicket"/> class.</summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="salesPlatformAttendeeDto">The sales platform attendee dto.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="collaboratorType">Type of the collaborator.</param>
        /// <param name="role">The role.</param>
        public DeleteCollaboratorTicket(
            Collaborator collaborator, 
            SalesPlatformAttendeeDto salesPlatformAttendeeDto, 
            Edition edition,
            AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType,
            CollaboratorType collaboratorType,
            Role role)
        {
            this.Collaborator = collaborator;
            this.SalesPlatformAttendeeDto = salesPlatformAttendeeDto;
            this.Edition = edition;
            this.AttendeeSalesPlatformTicketType = attendeeSalesPlatformTicketType;
            this.CollaboratorType = collaboratorType;
            this.Role = role;
        }
    }
}