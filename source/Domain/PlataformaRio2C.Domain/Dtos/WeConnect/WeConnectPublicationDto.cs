// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-18-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-21-2023
// ***********************************************************************
// <copyright file="WeConnectPublicationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    public class WeConnectPublicationDto
    {
        public Guid Uid { get; set; }
        public string SocialMediaPlatformPublicationId { get; set; }
        public string PublicationText { get; set; }
        public DateTimeOffset? ImageUploadDate { get; set; }
        public bool IsVideo { get; set; }
        public bool IsFixedOnTop { get; set; }
        public DateTimeOffset CreateDate { get; set; }

        public SocialMediaPlatformDto SocialMediaPlatformDto { get; set; }

        /// <summary>
        /// Gets the publication URL.
        /// </summary>
        /// <returns></returns>
        public string GetPublicationUrl()
        {
            return this.SocialMediaPlatformDto?.PublicationsRootUrl + this.SocialMediaPlatformPublicationId;
        }
    }
}
