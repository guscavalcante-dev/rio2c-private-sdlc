// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-03-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-15-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTicketDto</summary>
    public class AttendeeCollaboratorTicketDto
    {
        public string Barcode { get; set; }


        public AttendeeSalesPlatformTicketTypeDto AttendeeSalesPlatformTicketTypeDto { get; set; }

        public AttendeeCollaboratorTicket AttendeeCollaboratorTicket { get; set; }

        [Obsolete("Use the 'AttendeeSalesPlatformTicketTypeDto' properties instead of this. This property will be deleted!")]
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTicketDto"/> class.</summary>
        public AttendeeCollaboratorTicketDto()
        {
        }
    }
}