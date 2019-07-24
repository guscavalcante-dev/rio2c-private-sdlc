// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-24-2019
// ***********************************************************************
// <copyright file="Payload.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>Payload</summary>
    public class Payload
    {
        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

        [JsonProperty("config")]
        public PayloadConfig Config { get; set; }
    }

    /// <summary>PayloadConfig</summary>
    public class PayloadConfig
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("endpoint_url")]
        public string EndpointUrl { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("webhook_id")]
        public string WebhookId { get; set; }
    }
}