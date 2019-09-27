// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-27-2019
// ***********************************************************************
// <copyright file="EmailBaseCommand.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MediatR;

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

        public int? EditionId { get; set; }
        public string EditionName { get; set; }
        public int EditionUrlCode { get; set; }
        public string UserInterfaceLanguage { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFfullName">Name of the recipient ffull.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public EmailBaseCommand(
            int recipientUserId, 
            Guid recipientUserUid, 
            string recipientFirstName, 
            string recipientFfullName, 
            string recipientEmail, 
            int? editionId, 
            string editionName, 
            int editionUrlCode, 
            string userInterfaceLanguage)
        {
            this.UpdateBaseProperties(recipientUserId, recipientUserUid, recipientFirstName, recipientFfullName, recipientEmail, editionId, editionName, editionUrlCode, userInterfaceLanguage);
        }

        /// <summary>Initializes a new instance of the <see cref="EmailBaseCommand"/> class.</summary>
        public EmailBaseCommand()
        {
        }

        /// <summary>Updates the base properties.</summary>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFfullName">Name of the recipient ffull.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        protected void UpdateBaseProperties(
            int recipientUserId,
            Guid recipientUserUid,
            string recipientFirstName,
            string recipientFfullName,
            string recipientEmail,
            int? editionId,
            string editionName,
            int editionUrlCode,
            string userInterfaceLanguage)
        {
            this.RecipientUserId = recipientUserId;
            this.RecipientUserUid = recipientUserUid;
            this.RecipientFirstName = recipientFirstName?.Trim();
            this.RecipientFullName = recipientFfullName?.Trim();
            this.RecipientEmail = recipientEmail?.Trim();
            this.EditionId = editionId;
            this.EditionName = editionName?.Trim();
            this.EditionUrlCode = editionUrlCode;
            this.UserInterfaceLanguage = userInterfaceLanguage?.Trim();
        }
    }
}