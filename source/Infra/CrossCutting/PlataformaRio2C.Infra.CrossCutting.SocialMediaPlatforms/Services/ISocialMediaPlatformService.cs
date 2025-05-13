// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-12-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-12-2023
// ***********************************************************************
// <copyright file="ISocialMediaPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Dtos;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services
{
    /// <summary>ISocialMediaPlatformService</summary>
    public interface ISocialMediaPlatformService
    {
        List<SocialMediaPlatformPublicationDto> GetPosts();
    }
}