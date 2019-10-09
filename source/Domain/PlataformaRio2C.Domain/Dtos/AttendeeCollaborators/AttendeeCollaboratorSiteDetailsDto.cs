// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-09-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-09-2019
// ***********************************************************************
// <copyright file="AttendeeCollaboratorSiteDetailsDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorSiteDetailsDto</summary>
    public class AttendeeCollaboratorSiteDetailsDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorSiteDetailsDto"/> class.</summary>
        public AttendeeCollaboratorSiteDetailsDto()
        {
        }
    }
}