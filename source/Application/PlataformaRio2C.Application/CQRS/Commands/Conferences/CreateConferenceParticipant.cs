// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 01-02-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-03-2020
// ***********************************************************************
// <copyright file="CreateConferenceParticipant.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateConferenceParticipant</summary>
    public class CreateConferenceParticipant : BaseCommand
    {
        [Display(Name = "Conference", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ConferenceUid { get; set; }

        [Display(Name = "Speaker", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? CollaboratorUid { get; set; }

        [Display(Name = "Function", ResourceType = typeof(Labels))]
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        public Guid? ConferenceParticipanteRoleUid { get; set; }

        public List<ConferenceParticipantRoleDto> ConferenceParticipantRoleDtos { get; private set; }

        public Guid? InitialCollaboratorUid { get; set; }
        public string InitialCollaboratorName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipant"/> class.</summary>
        /// <param name="conferenceUid">The conference uid.</param>
        /// <param name="conferenceParticipantRoleDtos">The conference participant role dtos.</param>
        public CreateConferenceParticipant(
            Guid? conferenceUid,
            List<ConferenceParticipantRoleDto> conferenceParticipantRoleDtos)
        {
            this.ConferenceUid = conferenceUid;
            this.UpdateDropdownProperties(conferenceParticipantRoleDtos);
        }

        /// <summary>Initializes a new instance of the <see cref="CreateConferenceParticipant"/> class.</summary>
        public CreateConferenceParticipant()
        {
        }

        /// <summary>Updates the dropdown properties.</summary>
        /// <param name="conferenceParticipantRoleDtos">The conference participant role dtos.</param>
        public void UpdateDropdownProperties(List<ConferenceParticipantRoleDto> conferenceParticipantRoleDtos)
        {
            this.ConferenceParticipantRoleDtos = conferenceParticipantRoleDtos;
        }
    }
}