// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-03-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-06-2019
// ***********************************************************************
// <copyright file="NotificationEmailConversationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NotificationEmailConversationDto</summary>
    public class NotificationEmailConversationDto : ConversationDto
    {
        public User RecipientUser { get; set; }
        public Collaborator RecipientCollaborator { get; set; }
        public Language RecipientLanguage { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        /// <summary>Initializes a new instance of the <see cref="NotificationEmailConversationDto"/> class.</summary>
        public NotificationEmailConversationDto()
        {
        }
    }
}