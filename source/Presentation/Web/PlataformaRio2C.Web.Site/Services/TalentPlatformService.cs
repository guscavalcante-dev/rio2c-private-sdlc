// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-22-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-22-2019
// ***********************************************************************
// <copyright file="TalentPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

namespace PlataformaRio2C.Web.Site.Services
{
    /// <summary>TalentPlatformService</summary>
    public class TalentPlatformService
    {
        private readonly string talentsUrl;
        private readonly string talentAuthUrl;
        private readonly string talentLoginUrl;

        /// <summary>Initializes a new instance of the <see cref="TalentPlatformService"/> class.</summary>
        public TalentPlatformService()
        {
            this.talentsUrl = ConfigurationManager.AppSettings["TalentsUrl"];
            this.talentAuthUrl = ConfigurationManager.AppSettings["TalentAuthUrl"];
            this.talentLoginUrl = ConfigurationManager.AppSettings["TalentLoginUrl"];

            if (string.IsNullOrEmpty(talentsUrl) || string.IsNullOrEmpty(talentAuthUrl) || string.IsNullOrEmpty(talentLoginUrl))
            {
                throw new DomainException(Messages.RedirectTalentsPlatformNotActive);
            }
        }

        /// <summary>Logins the specified user access control dto.</summary>
        /// <param name="userAccessControlDto">The user access control dto.</param>
        /// <param name="userInterfaceLanguage">The user interface language.</param>
        /// <returns></returns>
        public TalentAccessDto Login(UserAccessControlDto userAccessControlDto, string userInterfaceLanguage)
        {
            if (userAccessControlDto?.User == null || userAccessControlDto?.Collaborator == null)
            {
                throw new Exception($"{Messages.CouldNotSignInTalentsPlatform} (userAccessControlDto is null).");
            }

            var user = new
            {
                Username = userAccessControlDto.User.Email,
                Email = userAccessControlDto.User.Email,
                PasswordHash = userAccessControlDto.User.PasswordHash,
                FirstName = userAccessControlDto.Collaborator.FirstName,
                LastName = userAccessControlDto.Collaborator.LastNames
            };

            var jsonString = JsonConvert.SerializeObject(user);

            var response = this.ExecuteRequest<TalentApiDto>(this.talentsUrl + this.talentAuthUrl, HttpMethod.Post, jsonString);
            if (response == null || response.Error)
            {
                throw new DomainException(Messages.CouldNotSignInTalentsPlatform);
            }

            response.Result.Url = this.talentsUrl + "/" + string.Format(this.talentLoginUrl, userInterfaceLanguage) + "?tk=" + response.Result.Secret;

            return response.Result;
        }

        #region Private Methos

        /// <summary>Executes the request.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private T ExecuteRequest<T>(string apiUrl, HttpMethod httpMethod, string jsonString)
        {
            using (WebClient client = new WebClient())
            {
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback (delegate { return true; });

                var response = httpMethod == HttpMethod.Get ? client.DownloadString(apiUrl) :
                                                              client.UploadString(apiUrl, httpMethod.ToString(), jsonString);

                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        #endregion
    }

    #region Models

    /// <summary>TalentApiDto</summary>
    public class TalentApiDto
    {
        [JsonProperty("error")]
        public bool Error { get; set; }

        [JsonProperty("cod")]
        public int Cod { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("result")]
        public TalentAccessDto Result { get; set; }
    }

    /// <summary>TalentAccessDto</summary>
    public class TalentAccessDto
    {
        [JsonProperty("secret")]
        public string Secret { get; set; }

        [JsonProperty("expire")]
        public DateTime Expire { get; set; }

        public string Url { get; set; }
    }

    #endregion
}