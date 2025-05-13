// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-07-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-07-2020
// ***********************************************************************
// <copyright file="UpdateConferenceParticipantRoleMainInformation.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceParticipantRoleMainInformation</summary>
    public class UpdateConferenceParticipantRoleMainInformation : CreateConferenceParticipantRole
    {
        public Guid ConferenceParticipantRoleUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceParticipantRoleMainInformation"/> class.</summary>
        /// <param name="conferenceParticipantRoleDto">The conferenceParticipantRole dto.</param>
        /// <param name="languagesDtos">The languages dtos.</param>
        public UpdateConferenceParticipantRoleMainInformation(
            ConferenceParticipantRoleDto conferenceParticipantRoleDto,
            List<LanguageDto> languagesDtos)
            : base(conferenceParticipantRoleDto, languagesDtos)
        {
            this.ConferenceParticipantRoleUid = conferenceParticipantRoleDto?.ConferenceParticipantRole?.Uid ?? Guid.Empty;
        }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceParticipantRoleMainInformation"/> class.</summary>
        public UpdateConferenceParticipantRoleMainInformation()
        {
        }
    }
}