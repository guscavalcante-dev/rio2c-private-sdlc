// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 12-10-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-11-2019
// ***********************************************************************
// <copyright file="EmailRecipientDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Entities;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>EmailRecipientDto</summary>
    public class EmailRecipientDto : ConversationDto
    {
        public User RecipientUser { get; set; }
        public Collaborator RecipientCollaborator { get; set; }
        public Language RecipientLanguage { get; set; }

        /// <summary>Initializes a new instance of the <see cref="EmailRecipientDto"/> class.</summary>
        public EmailRecipientDto()
        {
        }
    }
}