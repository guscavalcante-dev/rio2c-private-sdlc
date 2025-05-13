// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 08-11-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 08-11-2023
// ***********************************************************************
// <copyright file="SynchronizeWeConnectPublications.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Application.CQRS.Commands
{
    /// <summary>SynchronizeWeConnectPublications</summary>
    public class SynchronizeWeConnectPublications : BaseCommand
    {
        public string SocialMediaPlatformName { get; private set; }
        public string ApiKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SynchronizeWeConnectPublications"/> class.
        /// </summary>
        /// <param name="socialMediaPlatformName">Name of the social media platform.</param>
        /// <param name="apiKey">The API key.</param>
        public SynchronizeWeConnectPublications(
            string socialMediaPlatformName,
            string apiKey)
        {
            this.SocialMediaPlatformName = socialMediaPlatformName;
            this.ApiKey = apiKey;
        }
    }
}