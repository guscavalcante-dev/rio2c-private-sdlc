// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 05-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-03-2024
// ***********************************************************************
// <copyright file="SendExecutiveAgendaEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendExecutiveAgendaEmailAsync</summary>
    public class SendExecutiveAgendaEmailAsync : EmailBaseCommand
    {
        public Guid Collaboratoruid { get; private set; }
        public string UserSecurityToken { get; private set; }
        public int UserId { get; private set; }
        public IEnumerable<AttendeeCollaboratorTypeDto> AttendeeCollaboratorTypeDtos { get; set; }
        public IEnumerable<ConferenceDto> ConferenceDtos { get; set; }
        public IEnumerable<NegotiationBaseDto> NegotiationBaseDtos { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendExecutiveAgendaEmailAsync" /> class.
        /// </summary>
        /// <param name="collaboratorUid">The collaborator uid.</param>
        /// <param name="userSecurityToken">The user security token.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <param name="attendeeCollaboratorTypeDtos">The attendee collaborator type dtos.</param>
        /// <param name="conferenceDtos">The conference dtos.</param>
        /// <param name="negotiationBaseDtos">The negotiation base dtos.</param>
        public SendExecutiveAgendaEmailAsync(
            Guid collaboratorUid,
            string userSecurityToken,
            int recipientUserId, 
            Guid recipientUserUid, 
            string recipientFirstName, 
            string recipientFullName, 
            string recipientEmail, 
            Edition edition,
            int userId,
            string userInterfaceLanguage,
            IEnumerable<AttendeeCollaboratorTypeDto> attendeeCollaboratorTypeDtos,
            IEnumerable<ConferenceDto> conferenceDtos,
            IEnumerable<NegotiationBaseDto> negotiationBaseDtos)
            : base(recipientUserId, recipientUserUid, recipientFirstName, recipientFullName, recipientEmail, edition, userInterfaceLanguage)
        {
            this.Collaboratoruid = collaboratorUid;
            this.UserSecurityToken = userSecurityToken;
            this.UserId = userId;
            this.AttendeeCollaboratorTypeDtos = attendeeCollaboratorTypeDtos;
            this.ConferenceDtos = conferenceDtos;
            this.NegotiationBaseDtos = negotiationBaseDtos;
        }

        /// <summary>Initializes a new instance of the <see cref="SendExecutiveAgendaEmailAsync"/> class.</summary>
        public SendExecutiveAgendaEmailAsync()
        {
        }
    }
}