// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-14-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-14-2023
// ***********************************************************************
// <copyright file="SocialMediaPlatformsServiceFactory.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
{
    /// <summary>SocialMediaPlatformsServiceFactory</summary>
    public class SocialMediaPlatformServiceFactory : ISocialMediaPlatformServiceFactory
    {
        /// <summary>Initializes a new instance of the <see cref="SocialMediaPlatformServiceFactory"/> class.</summary>
        public SocialMediaPlatformServiceFactory()
        {
        }

        /// <summary>
        /// Gets the specified social media platform dto.
        /// </summary>
        /// <param name="socialMediaPlatformDto">The social media platform dto.</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">Unknown social media platform name.</exception>
        public ISocialMediaPlatformService Get(SocialMediaPlatformDto socialMediaPlatformDto)
        {
            if (socialMediaPlatformDto?.Name == SocialMediaPlatformName.Instagram)
            {
                return new InstagramSocialMediaPlatformService(socialMediaPlatformDto);
            }

            throw new DomainException($"Unknown Social Media Platform name: '{socialMediaPlatformDto?.Name}'");
        }
    }
}