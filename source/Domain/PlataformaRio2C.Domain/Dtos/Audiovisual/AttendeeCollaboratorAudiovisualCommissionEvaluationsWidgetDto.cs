// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-02-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto</summary>
    public class AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeCollaboratorAudiovisualCommissionEvaluationDto> AttendeeCollaboratorAudiovisualCommissionEvaluationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto"/> class.</summary>
        public AttendeeCollaboratorAudiovisualCommissionEvaluationsWidgetDto()
        {
        }
    }
}