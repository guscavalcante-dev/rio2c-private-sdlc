// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-24-2019
// ***********************************************************************
// <copyright file="EventbriteSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using PlataformaRio2C.Application.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite
{
    /// <summary>EventbriteSalesPlatformService</summary>
    public class EventbriteSalesPlatformService : ISalesPlatformService
    {
        private string ApiUrl = "https://www.eventbriteapi.com/v3/";

        private readonly string appKey; //Rio2C: WZNU5FWLAVRAQCIWCMNW // Rafael: D4ZOJ2GY6VK2ECV6IIPD
        private readonly string eventId = "63245927271"; // Rio2C: 63245927271 // Rafael: 65120229359
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public EventbriteSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.appKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            //this.eventId = ConfigurationManager.AppSettings["EventbriteEventId"];
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }

        /// <summary>Executes the request.</summary>
        public void ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            var payload = this.DeserializePayload(this.salesPlatformWebhookRequestDto.Payload);

            switch (payload.Config.Action)
            {
                // Attendees updates
                case Action.EventbriteAttendeeUpdated:
                case Action.EventbriteAttendeeCheckedIn:
                case Action.EventbriteAttendeeCheckedOut:
                    this.GetAttendee(payload.ApiUrl);
                    break;

                // Orders updates
                case Action.EventbriteOrderPlaced:
                case Action.EventbriteOrderRefunded:
                case Action.EventbriteOrderUpdated:
                    this.GetOrder(payload.ApiUrl);
                    break;

                // Other Updates
                default:
                    throw new DomainException($"Eventbrite action ({payload.Config.Action}) not configured.");
            }
        }

        /// <summary>Gets the attendee.</summary>
        /// <param name="apiUrl">The API URL.</param>
        public void GetAttendee(string apiUrl)
        {
            var response = this.ExecuteRequest<Attendee>(apiUrl, HttpMethod.Get, null);
        }

        /// <summary>Gets the order.</summary>
        /// <param name="apiUrl">The API URL.</param>
        public void GetOrder(string apiUrl)
        {
            var response = this.ExecuteRequest<Order>(apiUrl, HttpMethod.Get, null);
        }

        //public void GetEvent()
        //{
        //    var response = this.ExecuteRequest<Event>($"events/{this.eventId}/", HttpMethod.Get, null);
        //}

        #region Private methods

        /// <summary>Deserializes the payload.</summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private Payload DeserializePayload(string payload)
        {
            return JsonConvert.DeserializeObject<Payload>(payload);
        }

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
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8;");
                client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + this.appKey);

                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;

                var response = httpMethod == HttpMethod.Get ? client.DownloadString(apiUrl) :
                                                              client.UploadString(apiUrl, httpMethod.ToString(), jsonString);

                return JsonConvert.DeserializeObject<T>(response);
            }
        }

        #endregion
    }
}

/*
Payload Exemplates:

--- Order
{
  "api_url": "https://www.eventbriteapi.com/v3/orders/975751475/",
  "config": {
    "action": "order.updated",
    "endpoint_url": "https://288681c7.ngrok.io/api/v1.0/eventbrite/test",
    "user_id": "315954824605",
    "webhook_id": "1750106"
  }
}


--- Attendee
{
  "api_url": "https://www.eventbriteapi.com/v3/events/63245927271/attendees/1245580008/",
  "config": {
    "action": "attendee.updated",
    "endpoint_url": "https://288681c7.ngrok.io/api/v1.0/eventbrite/test",
    "user_id": "315954824605",
    "webhook_id": "1750106"
  }
}
*/
