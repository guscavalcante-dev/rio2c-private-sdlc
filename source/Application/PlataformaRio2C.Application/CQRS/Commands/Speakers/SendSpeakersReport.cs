// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 01-25-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="SendSpeakersReport.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>DeleteInnovationOrganization</summary>
    public class SendSpeakersReport : BaseCommand
    {
        public string[] SendToEmails { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendSpeakersReport" /> class.
        /// </summary>
        /// <param name="sendToEmails">The send to emails.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendSpeakersReport(
            string[] sendToEmails,
            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            this.SendToEmails = sendToEmails;
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendSpeakersReport" /> class.
        /// </summary>
        /// <param name="sendToEmails">The send to emails.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userUid">The user uid.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionUid">The edition uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public SendSpeakersReport(

            int userId,
            Guid userUid,
            int? editionId,
            Guid? editionUid,
            string userInterfaceLanguage)
        {
            base.UpdatePreSendProperties(userId, userUid, editionId, editionUid, userInterfaceLanguage);
        }
    }
}