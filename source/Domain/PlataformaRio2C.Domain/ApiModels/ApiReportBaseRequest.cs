// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-27-2023
// ***********************************************************************
// <copyright file="ApiReportBaseRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ApiReportBaseRequest</summary>
    public class ApiReportBaseRequest : ApiBaseRequest
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("sendToEmails")]
        public string SendToEmails { get; set; }
    }
}