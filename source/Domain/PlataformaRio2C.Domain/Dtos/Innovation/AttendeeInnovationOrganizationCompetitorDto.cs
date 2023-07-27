// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-27-2023
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCompetitorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationCompetitorDto</summary>
    public class AttendeeInnovationOrganizationCompetitorDto //: CollaboratorDto
    {
        public string Name { get; set; }

        public AttendeeInnovationOrganization AttendeeInnovationOrganization { get; set; }
        public AttendeeInnovationOrganizationCompetitor AttendeeInnovationOrganizationCompetitor { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCompetitorDto"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationCompetitorDto()
        {
        }
    }
}