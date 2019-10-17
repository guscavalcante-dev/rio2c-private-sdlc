// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 10-17-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-17-2019
// ***********************************************************************
// <copyright file="SendForgotPasswordEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendForgotPasswordEmailAsync</summary>
    public class SendForgotPasswordEmailAsync : EmailBaseCommand
    {
        public string PasswordResetToken { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SendForgotPasswordEmailAsync"/> class.</summary>
        /// <param name="passwordResetToken">The password reset token.</param>
        /// <param name="recipientUserId">The recipient user identifier.</param>
        /// <param name="recipientUserUid">The recipient user uid.</param>
        /// <param name="recipientFirstName">First name of the recipient.</param>
        /// <param name="recipientFullName">Full name of the recipient.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendForgotPasswordEmailAsync(
            string passwordResetToken,
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
            this.PasswordResetToken = passwordResetToken;
        }

        /// <summary>Initializes a new instance of the <see cref="SendForgotPasswordEmailAsync"/> class.</summary>
        public SendForgotPasswordEmailAsync()
        {
        }
    }
}