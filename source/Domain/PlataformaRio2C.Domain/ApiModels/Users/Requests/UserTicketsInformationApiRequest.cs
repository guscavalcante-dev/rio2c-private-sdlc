// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-20-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-20-2024
// ***********************************************************************
// <copyright file="UserTicketsInformationApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class UserTicketsInformationApiRequest : ApiBaseRequest
    {
        [JsonProperty("key")]
        [SwaggerParameterDescription(description: "The API Key.", isRequired: true)]
        public string Key { get; set; }

        [JsonProperty("email")]
        [SwaggerParameterDescription(description: "The email to get tickets information from.", isRequired: true)]
        
        public string Email { get; set; }
    }
}
