// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-15-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>SocialMediaPlatformDto</summary>
    public class SocialMediaPlatformDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public string EndpointUrl { get; set; }
        public bool IsSyncActive { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SocialMediaPlatformDto"/> class.</summary>
        public SocialMediaPlatformDto()
        {
        }
    }
}
