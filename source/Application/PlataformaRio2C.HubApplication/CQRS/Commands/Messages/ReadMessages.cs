// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="ReadMessages.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Application.CQRS.Commands;

namespace PlataformaRio2C.HubApplication.CQRS.Commands
{
    /// <summary>ReadMessages</summary>
    public class ReadMessages : BaseCommand
    {
        public int OtherUserId { get; set; }
        public Guid OtherUserUid { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ReadMessages"/> class.</summary>
        /// <param name="otherUserId">The other user identifier.</param>
        /// <param name="otherUserUid">The other user uid.</param>
        /// <param name="currentUserId">The current user identifier.</param>
        /// <param name="currentUserUid">The current user uid.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        public ReadMessages(
            int otherUserId,
            Guid otherUserUid,
            int currentUserId,
            Guid currentUserUid,
            string userInterfaceLanguage)
        {
            this.OtherUserId = otherUserId;
            this.OtherUserUid = otherUserUid;

            this.UpdatePreSendProperties(
                currentUserId,
                currentUserUid,
                null,
                null,
                userInterfaceLanguage);
        }
    }
}