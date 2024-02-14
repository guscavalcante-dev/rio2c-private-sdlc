// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-06-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInnovationOrganizationTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorInnovationOrganizationTrackDto</summary>
    public class AttendeeCollaboratorInnovationOrganizationTrackDto
    {
        public string AdditionalInfo { get; set; }

        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; set; }
        public InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationOrganizationTrackDto"/> class.</summary>
        public AttendeeCollaboratorInnovationOrganizationTrackDto()
        {
        }
    }
}