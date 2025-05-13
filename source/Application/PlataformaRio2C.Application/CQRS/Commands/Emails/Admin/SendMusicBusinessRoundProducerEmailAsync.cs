// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-25-2021
//
// Last Modified By : Rafael Ribeiro 
// Last Modified On : 03-18-2025
// ***********************************************************************
// <copyright file="SendMusicBusinessRoundProducerEmailAsync.cs" company="Softo">
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
    /// SendMusicBusinessRoundProducerEmailAsync
    /// </summary>
    public class SendMusicBusinessRoundProducerEmailAsync : EmailBaseCommand
    {
        public MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto musicBusinessRoundAttendeeOrganizationBaseDto { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendProducerMusicBusinessRoundEmailAsync"/> class.
        /// </summary>
        /// <param name="musicbusinessroundAttendeeOrganizationBaseDto">The negotiation attendee organization base dto.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendMusicBusinessRoundProducerEmailAsync(
            MusicBusinessRoundNegotiationAttendeeCollaboratorBaseDto musicbusinessroundAttendeeOrganizationBaseDto,
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
            this.musicBusinessRoundAttendeeOrganizationBaseDto = musicbusinessroundAttendeeOrganizationBaseDto;
            this.UserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="SendMusicBusinessRoundProducerEmailAsync"/> class.</summary>
        public SendMusicBusinessRoundProducerEmailAsync()
        {
        }
    }
}