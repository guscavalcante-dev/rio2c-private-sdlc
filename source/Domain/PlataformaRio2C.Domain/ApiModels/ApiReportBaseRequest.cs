// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="ApiReportBaseRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ApiReportBaseRequest</summary>
    public class ApiReportBaseRequest : ApiBaseRequest
    {
        [JsonProperty("key")]
        [SwaggerParameterDescription(description: "The API Key.")]
        public string Key { get; set; }

        [JsonProperty("sendToEmails")]
        [SwaggerParameterDescription("The emails that will receive the report, separated by comma.", "person@email.com, person2@email.com")]
        public string SendToEmails { get; set; }
    }
}