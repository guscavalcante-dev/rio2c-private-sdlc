// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-27-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 11-27-2019
// ***********************************************************************
// <copyright file="MessageDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MessageDto</summary>
    public class MessageDto
    {
        public User SenderUser { get; set; }
        public Collaborator SenderCollaborator { get; set; }
        public User RecipientUser { get; set; }
        public Collaborator RecipientCollaborator { get; set; }
        public Message Message { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MessageDto"/> class.</summary>
        public MessageDto()
        {
        }
    }
}