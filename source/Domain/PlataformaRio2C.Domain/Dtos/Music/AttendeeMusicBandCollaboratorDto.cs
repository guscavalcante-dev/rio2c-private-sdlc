// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 02-28-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="AttendeeMusicBandCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MusicBandTargetAudienceDto</summary>
    public class AttendeeMusicBandCollaboratorDto
    {
        public AttendeeMusicBandCollaborator AttendeeMusicBandCollaborator { get; set; }
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public User User { get; set; }
        public AddressDto AddressDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeMusicBandCollaboratorDto"/> class.</summary>
        public AttendeeMusicBandCollaboratorDto()
        {
        }
    }
}