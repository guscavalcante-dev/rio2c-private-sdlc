// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-12-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-12-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformPublicationDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram.Models;
using System;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Dtos
{
    /// <summary>SocialMediaPlatformPublicationDto</summary>
    public class SocialMediaPlatformPublicationDto
    {
        public string Id { get; set; }
        public string ThumbnailUrl { get; set; }
        public string PublicationMediaUrl { get; set; }
        public string PublicationText { get; set; }
        public bool IsVideo { get; set; }
        public DateTime CreatedAt { get; set; } 

        /// <summary>
        /// Initializes a new instance of the <see cref="SocialMediaPlatformPublicationDto"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public SocialMediaPlatformPublicationDto(Node node)
        {
            this.Id = node.Shortcode;
            this.ThumbnailUrl = node.ThumbnailSrc;
            this.PublicationMediaUrl = node.IsVideo == true ? node.VideoUrl : node.DisplayUrl;
            this.PublicationText = node.EdgeMediaToCaption.Edges.FirstOrDefault().Node.Text;
            this.IsVideo = node.IsVideo;
            this.CreatedAt = DateTimeOffset.FromUnixTimeSeconds(node.TakenAtTimestamp).LocalDateTime;
        }
    }
}