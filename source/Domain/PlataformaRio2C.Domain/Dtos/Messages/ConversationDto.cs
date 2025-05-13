// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-21-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 02-15-2020
// ***********************************************************************
// <copyright file="ConversationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConversationDto</summary>
    public class ConversationDto
    {
        public User OtherUser { get; set; }
        public AttendeeCollaboratorDto OtherAttendeeCollaboratorDto { get; set; }
        public DateTimeOffset? LastMessageDate { get; set; }
        public int UnreadMessagesCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConversationDto"/> class.</summary>
        public ConversationDto()
        {
        }
    }
}