// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-03-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="UpdateConferenceParticipant.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>UpdateConferenceParticipant</summary>
    public class UpdateConferenceParticipant : CreateConferenceParticipant
    {
        public ConferenceParticipantDto ConferenceParticipantDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="UpdateConferenceParticipant"/> class.</summary>
        /// <param name="conferenceDto">The conference dto.</param>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="conferenceParticipantRoleDtos">The conference participant role dtos.</param>
        public UpdateConferenceParticipant(
            ConferenceDto conferenceDto,
            Guid? collaboratorUid,
            List<ConferenceParticipantRoleDto> conferenceParticipantRoleDtos)
            : base(conferenceDto?.Conference?.Uid, conferenceParticipantRoleDtos)
        {
            this.ConferenceParticipantDto = conferenceDto?.ConferenceParticipantDtos?.FirstOrDefault(cpd => cpd.AttendeeCollaboratorDto.Collaborator.Uid == collaboratorUid);
            if (this.ConferenceParticipantDto == null)
            {
                throw new DomainException(string.Format(Messages.EntityNotAction, Labels.Speaker, Labels.FoundM));
            }

            this.CollaboratorUid = this.ConferenceParticipantDto.AttendeeCollaboratorDto?.Collaborator?.Uid;
            this.ConferenceParticipanteRoleUid = this.ConferenceParticipantDto.ConferenceParticipantRoleDto?.ConferenceParticipantRole?.Uid;
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipant"/> class.</summary>
        public UpdateConferenceParticipant()
        {
        }
    }
}