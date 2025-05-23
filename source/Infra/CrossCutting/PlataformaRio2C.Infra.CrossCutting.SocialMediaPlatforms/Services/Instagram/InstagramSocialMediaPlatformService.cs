﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms
// Author           : Renan Valentim
// Created          : 08-12-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-02-2023
// ***********************************************************************
// <copyright file="InstagramSocialMediaPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram
{
    /// <summary>InstagramSocialMediaPlatformService</summary>
    public class InstagramSocialMediaPlatformService : ISocialMediaPlatformService
    {
        private readonly string apiUrl;
        private readonly string apiKey; // The Instagram API V1 is public. Hasn't ApiKey.

        public InstagramSocialMediaPlatformService()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstagramSocialMediaPlatformService"/> class.
        /// </summary>
        /// <param name="socialMediaPlatformDto">The social media platform dto.</param>
        public InstagramSocialMediaPlatformService(SocialMediaPlatformDto socialMediaPlatformDto)
        {
            this.apiUrl = socialMediaPlatformDto?.EndpointUrl;
            this.apiKey = socialMediaPlatformDto?.ApiKey;
        }

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <returns></returns>
        public List<SocialMediaPlatformPublicationDto> GetPosts()
        {
            var instagramResponse = this.ExecuteRequest<InstagramResponse>(HttpMethod.Get, null);

            List<SocialMediaPlatformPublicationDto> result = new List<SocialMediaPlatformPublicationDto>();

            foreach (var publication in instagramResponse?.Publications)
            {
                result.Add(new SocialMediaPlatformPublicationDto(publication));
            }

            return result;
        }

        #region Private methods

        /// <summary>
        /// Executes the request and desserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private T ExecuteRequest<T>(HttpMethod httpMethod, string jsonString)
        {
            using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8;");
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                string url = this.apiUrl + this.apiKey;
                var response = httpMethod == HttpMethod.Get ? client.DownloadString(url) :
                                                              client.UploadString(url, httpMethod.ToString(), jsonString);

                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        #endregion
    }
}