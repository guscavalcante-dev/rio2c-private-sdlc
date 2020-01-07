// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="ConferenceParticipantDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConferenceParticipantDto</summary>
    public class ConferenceParticipantDto
    {
        public ConferenceParticipant ConferenceParticipant { get; set; }
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public ConferenceParticipantRoleDto ConferenceParticipantRoleDto { get; set; }
        public ConferenceDto ConferenceDto { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConferenceParticipantDto"/> class.</summary>
        public ConferenceParticipantDto()
        {
        }
    }
}