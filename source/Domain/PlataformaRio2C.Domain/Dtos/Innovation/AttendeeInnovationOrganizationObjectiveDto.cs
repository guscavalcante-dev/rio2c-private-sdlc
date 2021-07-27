// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationObjectiveDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationObjectiveDto</summary>
    public class AttendeeInnovationOrganizationObjectiveDto
    {
        public InnovationOrganizationObjectivesOption InnovationOrganizationObjectivesOption { get; set; }
        public AttendeeInnovationOrganizationObjective AttendeeInnovationOrganizationObjective { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationObjectiveDto"/> class.</summary>
        public AttendeeInnovationOrganizationObjectiveDto()
        {
        }
    }
}