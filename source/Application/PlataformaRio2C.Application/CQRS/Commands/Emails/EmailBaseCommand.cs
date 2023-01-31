// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-27-2023
// ***********************************************************************
// <copyright file="EmailBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>EmailBaseCommand</summary>
    public class EmailBaseCommand : IRequest<AppValidationResult>
    {
        public int RecipientUserId { get; set; }
        public Guid RecipientUserUid { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientFullName { get; set; }
        public string RecipientEmail { get; set; }

        public Edition Edition { get; private set; }
        public string UserInterfaceLanguage { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailBaseCommand" /> class.
        /// </summary>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Name of the recipient ffull.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public EmailBaseCommand(
            int recipientUserId, 
            Guid recipientUserUid, 
            string recipientFirstName, 
            string recipientFullName, 
            string recipientEmail, 
            Edition edition, 
            string userInterfaceLanguage)
        {
            this.RecipientUserId = recipientUserId;
            this.RecipientUserUid = recipientUserUid;
            this.RecipientFirstName = recipientFirstName?.Trim();
            this.RecipientFullName = recipientFullName?.Trim();
            this.RecipientEmail = recipientEmail?.Trim();
            this.Edition = edition;
            this.UserInterfaceLanguage = userInterfaceLanguage?.Trim();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailBaseCommand" /> class.
        /// </summary>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public EmailBaseCommand(
            string recipientEmail,
            Edition edition,
            string userInterfaceLanguage)
        {
            this.RecipientEmail = recipientEmail?.Trim();
            this.Edition = edition;
            this.UserInterfaceLanguage = userInterfaceLanguage?.Trim();
        }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        public EmailBaseCommand()
        {
        }
    }
}