// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="SendUnreadConversationEmailAsync.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SendUnreadConversationEmailAsync</summary>
    public class SendUnreadConversationEmailAsync : EmailBaseCommand
    {
        public NotificationEmailConversationDto NotificationEmailConversationDto { get; private set; }

        /// <summary>Initializes a new instance of the <see cref="SendUnreadConversationEmailAsync"/> class.</summary>
        /// <param name="notificationEmailConversationDto">The notification email conversation dto.</param>
        /// <param name="editionId">The edition identifier.</param>
        /// <param name="editionName">Name of the edition.</param>
        /// <param name="editionUrlCode">The edition URL code.</param>
        public SendUnreadConversationEmailAsync(
            NotificationEmailConversationDto notificationEmailConversationDto,
            int? editionId,
            string editionName, 
            int editionUrlCode)
            : base(
                  notificationEmailConversationDto.RecipientUser.Id,
                  notificationEmailConversationDto.RecipientUser.Uid,
                  notificationEmailConversationDto.RecipientUser.Name.GetFirstWord(),
                  notificationEmailConversationDto.RecipientUser.Name,
                  notificationEmailConversationDto.RecipientUser.Email, 
                  editionId, 
                  editionName, 
                  editionUrlCode,
                  notificationEmailConversationDto.RecipientLanguage?.Code ?? "pt-br")
        {
            this.NotificationEmailConversationDto = notificationEmailConversationDto;
        }

        /// <summary>Initializes a new instance of the <see cref="SendUnreadConversationEmailAsync"/> class.</summary>
        public SendUnreadConversationEmailAsync()
        {
        }
    }
}