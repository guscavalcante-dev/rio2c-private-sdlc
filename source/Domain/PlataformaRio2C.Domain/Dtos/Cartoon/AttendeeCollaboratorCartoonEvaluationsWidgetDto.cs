// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-29-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-29-2022
// ***********************************************************************
// <copyright file="AttendeeCollaboratorCartoonEvaluationsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorCartoonEvaluationsWidgetDto</summary>
    public class AttendeeCollaboratorCartoonEvaluationsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeCartoonProjectEvaluationDto> AttendeeCartoonProjectEvaluationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorCartoonEvaluationsWidgetDto"/> class.</summary>
        public AttendeeCollaboratorCartoonEvaluationsWidgetDto()
        {
        }
    }
}