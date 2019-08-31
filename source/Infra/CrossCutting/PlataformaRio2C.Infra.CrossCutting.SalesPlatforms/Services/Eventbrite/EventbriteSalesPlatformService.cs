// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-31-2019
// ***********************************************************************
// <copyright file="EventbriteSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite
{
    /// <summary>EventbriteSalesPlatformService</summary>
    public class EventbriteSalesPlatformService : ISalesPlatformService
    {
        private string ApiUrl = "https://www.eventbriteapi.com/v3/";

        private readonly string appKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public EventbriteSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.appKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }

        /// <summary>Executes the request.</summary>
        public List<SalesPlatformAttendeeDto> ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            var payload = this.DeserializePayload(this.salesPlatformWebhookRequestDto.SalesPlatformWebhookRequest.Payload);

            switch (payload.Config.Action)
            {
                // Attendees updates
                case Action.EventbriteAttendeeUpdated:
                case Action.EventbriteAttendeeCheckedIn:
                case Action.EventbriteAttendeeCheckedOut:
                    return this.GetAttendee(payload.ApiUrl);

                // Orders updates
                case Action.EventbriteOrderPlaced:
                case Action.EventbriteOrderRefunded:
                case Action.EventbriteOrderUpdated:
                    return this.GetOrder(payload.ApiUrl);

                // Other Updates
                default:
                    throw new DomainException($"Eventbrite action ({payload.Config.Action}) not configured.");
            }
        }

        /// <summary>Gets the attendee.</summary>
        /// <param name="apiUrl">The API URL.</param>
        public List<SalesPlatformAttendeeDto> GetAttendee(string apiUrl)
        {
            var attendee = this.ExecuteRequest<Attendee>(apiUrl, HttpMethod.Get, null);
            if (attendee == null)
            {
                return null;
            }

            var attendees = new List<SalesPlatformAttendeeDto>();
            attendees.Add(new SalesPlatformAttendeeDto(attendee));

            return attendees;
        }

        /// <summary>Gets the order.</summary>
        /// <param name="apiUrl">The API URL.</param>
        public List<SalesPlatformAttendeeDto> GetOrder(string apiUrl)
        {
            var response = this.ExecuteRequest<Order>(apiUrl + "?expand=attendees", HttpMethod.Get, null);
            if (response == null)
            {
                return null;
            }

            var attendees = new List<SalesPlatformAttendeeDto>();
            foreach (var attendee in response.Attendees)
            {
                attendees.Add(new SalesPlatformAttendeeDto(attendee));
            }

            return attendees;
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

--- Statuses
    + started     - order is created
    + pending     - order is pending
    + placed      - order is placed by the ticket buyer
    + abandoned   - order is abandoned by the ticket buyer

--- Payload Examples:

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
