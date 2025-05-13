// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 02-19-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-19-2024
// ***********************************************************************
// <copyright file="SendCreatorCommissionWelcomeEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendCreatorCommissionWelcomeEmailAsync</summary>
    public class SendCreatorCommissionWelcomeEmailAsync : EmailBaseCommand
    {
        public Guid Collaboratoruid { get; private set; }
        public string UserSecurityToken { get; private set; }
        public int UserId { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendCreatorCommissionWelcomeEmailAsync"/> class.</summary>
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
        public SendCreatorCommissionWelcomeEmailAsync(
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

        /// <summary>Initializes a new instance of the <see cref="SendCreatorCommissionWelcomeEmailAsync"/> class.</summary>
        public SendCreatorCommissionWelcomeEmailAsync()
        {
        }
    }
}