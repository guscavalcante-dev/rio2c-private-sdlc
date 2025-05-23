﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Daniel Giese Rodrigues
// Created          : 04-23-2025
//
// Last Modified By : Daniel Giese Rodrigues
// Last Modified On : 04-23-2025
// ***********************************************************************
// <copyright file="SendMusicBusinessRoundPlayerNegotiationsEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>
    /// SendPlayerNegotiationsEmailAsync
    /// </summary>
    public class SendMusicBusinessRoundPlayerNegotiationsEmailAsync : EmailBaseCommand
    {
        public MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto NegotiationAttendeeOrganizationBaseDto { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendPlayerNegotiationsEmailAsync"/> class.
        /// </summary>
        /// <param name="negotiationAttendeeOrganizationBaseDto">The negotiation attendee organization base dto.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendMusicBusinessRoundPlayerNegotiationsEmailAsync(
            MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto negotiationAttendeeOrganizationBaseDto,
            int recipientUserId,
            Guid recipientUserUid,
            string recipientFirstName,
            string recipientFullName,
            string recipientEmail,
            Edition edition,
            int userId,
            string userInterfaceLanguage)
            : base(recipientUserId, recipientUserUid, recipientFirstName, recipientFullName, recipientEmail, edition, userInterfaceLanguage)
        {
            this.NegotiationAttendeeOrganizationBaseDto = negotiationAttendeeOrganizationBaseDto;
            this.UserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="SendPlayerNegotiationsEmailAsync"/> class.</summary>
        public SendMusicBusinessRoundPlayerNegotiationsEmailAsync()
        {
        }
    }
}