// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-21-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-21-2019
// ***********************************************************************
// <copyright file="ConversationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>ConversationDto</summary>
    public class ConversationDto
    {
        public User User { get; set; }
        public AttendeeCollaboratorDto AttendeeCollaboratorDto { get; set; }
        public DateTime? LastMessage { get; set; }
        public int UnreadMessagesCount { get; set; }

        /// <summary>Initializes a new instance of the <see cref="ConversationDto"/> class.</summary>
        public ConversationDto()
        {
        }
    }
}