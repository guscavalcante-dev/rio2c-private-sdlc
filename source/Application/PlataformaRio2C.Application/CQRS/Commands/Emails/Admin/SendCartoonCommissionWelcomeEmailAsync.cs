// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Franco
// Created          : 02-04-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 02-04-2022
// ***********************************************************************
// <copyright file="SendCartoonCommissionWelcomeEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendCartoonCommissionWelcomeEmailAsync</summary>
    public class SendCartoonCommissionWelcomeEmailAsync : EmailBaseCommand
    {
        public Guid Collaboratoruid { get; private set; }
        public string UserSecurityToken { get; private set; }
        public int UserId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendCartoonCommissionWelcomeEmailAsync"/> class.</summary>
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
        public SendCartoonCommissionWelcomeEmailAsync(
            Guid collaboratorUid,
            string userSecurityToken,
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
            this.Collaboratoruid = collaboratorUid;
            this.UserSecurityToken = userSecurityToken;
            this.UserId = userId;
        }

        /// <summary>Initializes a new instance of the <see cref="SendCartoonCommissionWelcomeEmailAsync"/> class.</summary>
        public SendCartoonCommissionWelcomeEmailAsync()
        {
        }
    }
}