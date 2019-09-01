// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="CreateCollaboratorTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateCollaboratorTicket</summary>
    public class CreateCollaboratorTicket : BaseCommand
    {
        public SalesPlatformAttendeeDto SalesPlatformAttendeeDto { get; private set; }
        public Edition Edition { get; private set; }
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; private set; }
        public Role Role { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="CreateCollaboratorTicket"/> class.</summary>
        /// <param name="salesPlatformAttendeeDto">The sales platform attendee dto.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        /// <param name="role">The role.</param>
        public CreateCollaboratorTicket(SalesPlatformAttendeeDto salesPlatformAttendeeDto, Edition edition, AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType, Role role)
        {
            this.SalesPlatformAttendeeDto = salesPlatformAttendeeDto;
            this.Edition = edition;
            this.AttendeeSalesPlatformTicketType = attendeeSalesPlatformTicketType;
            this.Role = role;
        }
    }
}