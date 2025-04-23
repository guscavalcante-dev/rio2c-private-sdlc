// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Daniel Giese Rodrigues
// Created          : 04-22-2025
//
// Last Modified By :  Daniel Giese Rodrigues
// Last Modified On : 04-22-2025
// ***********************************************************************
// <copyright file="LoginApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.ApiModels.Auth.Response
{
    public class LoginApiResponse
    {
        [JsonProperty("token")]
        [SwaggerParameterDescription(description: "The JWT Token")]
        public string Token { get; set; }

        [JsonProperty("expirationDate")]
        [SwaggerParameterDescription(description: "The Expiration Token Date")]
        public DateTime ExpirationDate { get; set; }
    }
}
