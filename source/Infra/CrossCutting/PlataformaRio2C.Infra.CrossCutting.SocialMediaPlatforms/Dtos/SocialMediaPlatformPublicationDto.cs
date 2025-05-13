// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-12-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-02-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformPublicationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram.Models;
using System;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Dtos
{
    /// <summary>SocialMediaPlatformPublicationDto</summary>
    public class SocialMediaPlatformPublicationDto
    {
        public string Id { get; set; }
        public string PublicationMediaUrl { get; set; }
        public string PublicationText { get; set; }
        public bool IsVideo { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaPlatformPublicationDto"/> class.
        /// </summary>
        /// <param name="publication">The node.</param>
        public SocialMediaPlatformPublicationDto(Publication publication)
        {
            this.Id = publication.ShortCode;
            this.PublicationMediaUrl = publication.MediaUrl;
            this.PublicationText = publication.Caption;
            this.IsVideo = publication.IsVideo;
            this.CreatedAt = publication.Timestamp;
        }
    }
}