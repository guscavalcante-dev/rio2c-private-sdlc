// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-02-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInnovationEvaluationsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorInnovationEvaluationsWidgetDto</summary>
    public class AttendeeCollaboratorInnovationEvaluationsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeInnovationOrganizationEvaluationDto> AttendeeInnovationOrganizationEvaluationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorInnovationEvaluationsWidgetDto"/> class.</summary>
        public AttendeeCollaboratorInnovationEvaluationsWidgetDto()
        {
        }
    }
}