// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationExperienceDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationExperienceDto</summary>
    public class AttendeeInnovationOrganizationExperienceDto
    {
        public InnovationOrganizationExperienceOption InnovationOrganizationExperienceOption { get; set; }
        public AttendeeInnovationOrganizationExperience AttendeeInnovationOrganizationExperience { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationExperienceDto"/> class.</summary>
        public AttendeeInnovationOrganizationExperienceDto()
        {
        }
    }
}