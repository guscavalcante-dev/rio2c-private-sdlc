// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-17-2021
// ***********************************************************************
// <copyright file="InnovationCollaboratorDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationCollaboratorDto</summary>
    public class InnovationCollaboratorDto : CollaboratorDto
    {
        public IEnumerable<AttendeeInnovationOrganizationTrackDto> AttendeeInnovationOrganizationTracksDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InnovationCollaboratorDto"/> class.
        /// </summary>
        public InnovationCollaboratorDto()
        {
        }
    }
}