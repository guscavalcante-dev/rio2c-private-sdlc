// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-02-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-02-2021
// ***********************************************************************
// <copyright file="AttendeeCollaboratorMusicBandEvaluationsWidgetDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeCollaboratorMusicBandEvaluationsWidgetDto</summary>
    public class AttendeeCollaboratorMusicBandEvaluationsWidgetDto
    {
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public List<AttendeeMusicBandEvaluationDto> AttendeeMusicBandEvaluationDtos { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeCollaboratorMusicEvaluationsWidgetDto"/> class.</summary>
        public AttendeeCollaboratorMusicBandEvaluationsWidgetDto()
        {
        }
    }
}