// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Daniel Giese Rodrigues
// Created          : 04-22-2025
//
// Last Modified By :  Daniel Giese Rodrigues
// Last Modified On : 04-22-2025
// ***********************************************************************
// <copyright file="LoginApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels.Auth.Request
{
    public class LoginApiRequest
    {
        [JsonProperty("username")]
        [SwaggerParameterDescription(description: "username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [SwaggerParameterDescription(description: "password")]
        public string Password { get; set; }
    }
}
