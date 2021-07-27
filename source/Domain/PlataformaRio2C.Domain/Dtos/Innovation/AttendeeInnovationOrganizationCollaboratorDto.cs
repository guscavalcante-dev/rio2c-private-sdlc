// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationCollaboratorDto</summary>
    public class AttendeeInnovationOrganizationCollaboratorDto //: CollaboratorDto
    {
        public AttendeeCollaborator AttendeeCollaborator { get; set; }
        public Collaborator Collaborator { get; set; }
        public AttendeeInnovationOrganization AttendeeInnovationOrganization { get; set; }
        public IEnumerable<AttendeeInnovationOrganizationTrackDto> AttendeeInnovationOrganizationTracksDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCollaboratorDto"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationCollaboratorDto()
        {
        }
    }
}