// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 06-25-2021
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 06-26-2021
// ***********************************************************************
// <copyright file="SendProducerNegotiationsEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>
    /// SendProducerNegotiationsEmailAsync
    /// </summary>
    public class SendProducerNegotiationsEmailAsync : EmailBaseCommand
    {
        public NegotiationAttendeeOrganizationBaseDto NegotiationAttendeeOrganizationBaseDto { get; private set; }
        public int UserId { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendProducerNegotiationsEmailAsync"/> class.
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
        public SendProducerNegotiationsEmailAsync(
            NegotiationAttendeeOrganizationBaseDto negotiationAttendeeOrganizationBaseDto,
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

        /// <summary>Initializes a new instance of the <see cref="SendProducerNegotiationsEmailAsync"/> class.</summary>
        public SendProducerNegotiationsEmailAsync()
        {
        }
    }
}