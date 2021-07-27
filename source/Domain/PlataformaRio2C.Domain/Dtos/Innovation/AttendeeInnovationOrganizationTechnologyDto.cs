// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationTechnologyDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationTechnologyDto</summary>
    public class AttendeeInnovationOrganizationTechnologyDto
    {
        public InnovationOrganizationTechnologyOption InnovationOrganizationTechnologyOption { get; set; }
        public AttendeeInnovationOrganizationTechnology AttendeeInnovationOrganizationTechnology { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationTechnologyDto"/> class.</summary>
        public AttendeeInnovationOrganizationTechnologyDto()
        {
        }
    }
}