// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-28-2019
// ***********************************************************************
// <copyright file="CreateMessage.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>CreateMessage</summary>
    public class CreateMessage : BaseCommand
    {
        public int RecipientId { get; set; }
        public Guid RecipientUid { get; set; }
        public string Text { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CreateMessage"/> class.</summary>
        /// <param name="recipientId">The recipient identifier.</param>
        /// <param name="recipientUid">The recipient uid.</param>
        /// <param name="text">The text.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public CreateMessage(
            int recipientId,
            Guid recipientUid,
            string text,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.RecipientId = recipientId;
            this.RecipientUid = recipientUid;
            this.Text = text;

            this.UpdatePreSendProperties(
                userId,
                userUid,
                editionId,
                editionUid,
                userInterfaceLanguage);
        }
    }
}