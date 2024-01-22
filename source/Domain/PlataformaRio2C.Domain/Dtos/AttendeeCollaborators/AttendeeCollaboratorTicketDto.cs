// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-18-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorTicketDto</summary>
    public class AttendeeCollaboratorTicketDto
    {
        public AttendeeCollaboratorTicket AttendeeCollaboratorTicket { get; set; }
        public AttendeeSalesPlatformTicketType AttendeeSalesPlatformTicketType { get; set; }

        public int GetMusicBandsTotalCount()
        {
            if (this.AttendeeCollaboratorTicket == null || this.AttendeeSalesPlatformTicketType == null)
            {
                return 0;
            }

            // Projetos de musica cadastrados
            var attendeeMusicBands = this.AttendeeCollaboratorTicket.AttendeeCollaborator.AttendeeMusicBandCollaborators.Select(ambc => ambc.AttendeeMusicBand);
           
            return attendeeMusicBands.Count();
        }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorTicketDto"/> class.</summary>
        public AttendeeCollaboratorTicketDto()
        {
        }
    }
}