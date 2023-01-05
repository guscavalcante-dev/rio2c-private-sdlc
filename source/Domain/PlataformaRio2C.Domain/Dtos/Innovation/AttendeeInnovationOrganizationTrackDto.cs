// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-05-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTrackDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;
using System.Linq;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationTrackDto</summary>
    public class AttendeeInnovationOrganizationTrackDto
    {
        public InnovationOrganizationTrackOptionGroup InnovationOrganizationTrackOptionGroup { get; set; }
        public InnovationOrganizationTrackOption InnovationOrganizationTrackOption { get; set; }
        public AttendeeInnovationOrganizationTrack AttendeeInnovationOrganizationTrack { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTrackDto"/> class.</summary>
        public AttendeeInnovationOrganizationTrackDto()
        {
        }
    }
}