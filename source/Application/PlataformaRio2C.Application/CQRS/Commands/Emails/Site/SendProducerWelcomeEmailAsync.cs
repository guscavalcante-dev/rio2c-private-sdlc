// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="SendProducerWelcomeEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendProducerWelcomeEmailAsync</summary>
    public class SendProducerWelcomeEmailAsync : EmailBaseCommand
    {
        public string UserSecurityToken { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendProducerWelcomeEmailAsync"/> class.</summary>
        /// <param name="userSecurityToken">The user security token.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="edition">The edition.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendProducerWelcomeEmailAsync(
            string userSecurityToken,
            int recipientUserId, 
            Guid recipientUserUid, 
            string recipientFirstName, 
            string recipientFullName, 
            string recipientEmail, 
            Edition edition,
            string userInterfaceLanguage)
            : base(recipientUserId, recipientUserUid, recipientFirstName, recipientFullName, recipientEmail, edition, userInterfaceLanguage)
        {
            this.UserSecurityToken = userSecurityToken;
        }

        /// <summary>Initializes a new instance of the <see cref="SendProducerWelcomeEmailAsync"/> class.</summary>
        public SendProducerWelcomeEmailAsync()
        {
        }
    }
}