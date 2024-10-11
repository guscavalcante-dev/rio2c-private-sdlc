// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 10-01-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-01-2024
// ***********************************************************************
// <copyright file="FindAllConferencesByAttendeeCollaboratorIdResponseDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Dtos
{
    public class FindAllConferencesByAttendeeCollaboratorIdResponseDto
    {
        public List<ConferenceDto> ConferenceDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAllConferencesByAttendeeCollaboratorIdResponseDto"/> class.
        /// </summary>
        /// <param name="conferenceDtos">The conference dtos.</param>
        public FindAllConferencesByAttendeeCollaboratorIdResponseDto(List<ConferenceDto> conferenceDtos)
        {
            this.ConferenceDtos = conferenceDtos;
        }
    }
}
