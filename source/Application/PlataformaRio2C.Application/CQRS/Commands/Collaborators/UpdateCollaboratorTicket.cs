// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 08-31-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="UpdateCollaboratorTicket.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateCollaboratorTicket</summary>
    public class UpdateCollaboratorTicket : BaseCommand
    {
        public Collaborator Collaborator { get; private set; }
        public SalesPlatformAttendeeDto SalesPlatformAttendeeDto { get; private set; }
        public Edition Edition { get; private set; }
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateCollaboratorTicket"/> class.</summary>
        /// <param name="collaborator">The collaborator.</param>
        /// <param name="salesPlatformAttendeeDto">The sales platform attendee dto.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="attendeeSalesPlatformTicketType">Type of the attendee sales platform ticket.</param>
        public UpdateCollaboratorTicket(Collaborator collaborator, SalesPlatformAttendeeDto salesPlatformAttendeeDto, Edition edition, AttendeeSalesPlatformTicketType attendeeSalesPlatformTicketType)
        {
            this.Collaborator = collaborator;
            this.SalesPlatformAttendeeDto = salesPlatformAttendeeDto;
            this.Edition = edition;
            this.AttendeeSalesPlatformTicketType = attendeeSalesPlatformTicketType;
        }
    }
}