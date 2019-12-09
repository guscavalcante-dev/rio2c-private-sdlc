// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Dantas Ruiz
// Created          : 11-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-03-2019
// ***********************************************************************
// <copyright file="MessageHubDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>MessageHubDto</summary>
    public class MessageHubDto
    {
        public Guid SenderUserUid { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string SenderNameInitials { get; set; }
        public string SenderImageUrl { get; set; }
        public Guid RecipientUserUid { get; set; }
        public string RecipientEmail { get; set; }
        public string Text { get; set; }
        public string SendDate { get; set; }
        public string SendDateFormatted { get; set; }
        public string ReadDate { get; set; }
        public string ReadDateFormatted { get; set; }

        /// <summary>Initializes a new instance of the <see cref="MessageHubDto"/> class.</summary>
        public MessageHubDto()
        {
        }
    }
}