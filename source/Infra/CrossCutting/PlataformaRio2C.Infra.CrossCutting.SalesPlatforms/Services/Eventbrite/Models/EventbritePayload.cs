// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-24-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-01-2019
// ***********************************************************************
// <copyright file="EventbritePayload.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models
{
    /// <summary>EventbritePayload</summary>
    public class EventbritePayload
    {
        [JsonProperty("api_url")]
        public string ApiUrl { get; set; }

        [JsonProperty("config")]
        public EventbritePayloadConfig Config { get; set; }
    }

    /// <summary>EventbritePayloadConfig</summary>
    public class EventbritePayloadConfig
    {
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("endpoint_url")]
        public string EndpointUrl { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("webhook_id")]
        public string WebhookId { get; set; }

        /// <summary>Gets the sales platform action.</summary>
        /// <returns></returns>
        public string GetSalesPlatformAction()
        {
            switch (Action.ToLowerInvariant())
            {
                // Attendees updates
                case EventbriteAction.AttendeeUpdated:
                case EventbriteAction.OrderPlaced:
                case EventbriteAction.OrderRefunded:
                case EventbriteAction.OrderUpdated:
                    return SalesPlatformAction.AttendeeUpdated;

                case EventbriteAction.AttendeeCheckedIn:
                    return SalesPlatformAction.AttendeeCheckedIn;

                case EventbriteAction.AttendeeCheckedOut:
                    return SalesPlatformAction.AttendeeCheckedOut;

                // Other Updates
                default:
                    return Action;
            }
        }
    }
}