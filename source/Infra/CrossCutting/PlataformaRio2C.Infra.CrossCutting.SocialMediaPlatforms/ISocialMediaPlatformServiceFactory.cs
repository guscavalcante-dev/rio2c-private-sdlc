// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-11-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-11-2023
// ***********************************************************************
// <copyright file="ISocialMediaPlatformServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
{
    /// <summary>ISocialMediaPlatformServiceFactory</summary>
    public interface ISocialMediaPlatformServiceFactory
    {
        ISocialMediaPlatformService Get(SocialMediaPlatformDto salesPlatformDto);
    }
}