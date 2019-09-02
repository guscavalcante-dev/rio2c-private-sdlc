// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 09-02-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-02-2019
// ***********************************************************************
// <copyright file="SendWelmcomeEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendWelmcomeEmailAsync</summary>
    public class SendWelmcomeEmailAsync : EmailBaseCommand
    {
        public string UserSecurityToken { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsync"/> class.</summary>
        /// <param name="userSecurityToken">The user security token.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendWelmcomeEmailAsync(
            string userSecurityToken,
            int recipientUserId, 
            Guid recipientUserUid, 
            string recipientFirstName, 
            string recipientFullName, 
            string recipientEmail, 
            int? editionId,
            string editionName, 
            int editionUrlCode, 
            string userInterfaceLanguage)
            : base(recipientUserId, recipientUserUid, recipientFirstName, recipientFullName, recipientEmail, editionId, editionName, editionUrlCode, userInterfaceLanguage)
        {
            this.UserSecurityToken = userSecurityToken;
        }

        /// <summary>Initializes a new instance of the <see cref="SendWelmcomeEmailAsync"/> class.</summary>
        public SendWelmcomeEmailAsync()
        {
        }
    }
}