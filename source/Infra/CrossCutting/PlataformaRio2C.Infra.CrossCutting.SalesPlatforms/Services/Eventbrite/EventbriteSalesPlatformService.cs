// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="EventbriteSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
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
        private readonly string apiUrl = "https://www.eventbriteapi.com/v3/";
        private readonly string apiKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventbriteSalesPlatformService" /> class.
        /// </summary>
        /// <param name="attendeeSalesPlatformDto">The attendee sales platform dto.</param>
        public EventbriteSalesPlatformService(AttendeeSalesPlatformDto attendeeSalesPlatformDto)
        {
            this.apiKey = attendeeSalesPlatformDto.SalesPlatform.ApiKey;
        }

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public EventbriteSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.apiKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">
        /// The webhook request is required.
        /// or
        /// Eventbrite action ({payload.Config.Action}) not configured.
        /// </exception>
        public Tuple<string, List<SalesPlatformAttendeeDto>> ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            var payload = this.DeserializePayload(this.salesPlatformWebhookRequestDto.SalesPlatformWebhookRequest.Payload);

            switch (payload.Config.Action)
            {
                // Attendees updates
                case EventbriteAction.AttendeeUpdated:
                case EventbriteAction.AttendeeCheckedIn:
                case EventbriteAction.AttendeeCheckedOut:
                    return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.Config.GetSalesPlatformAction(), this.GetAttendee(payload.ApiUrl));

                //// Orders updates
                //case Action.EventbriteOrderPlaced:
                //case Action.EventbriteOrderRefunded:
                //case Action.EventbriteOrderUpdated:
                //    return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.Config.GetAction(), this.GetOrder(payload.ApiUrl));

                // Other Updates
                default:
                    throw new DomainException($"Eventbrite action ({payload.Config.Action}) not configured.");
            }
        }

        /// <summary>
        /// Gets the attendees by event identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<SalesPlatformAttendeeDto> GetAttendeesByEventId(string eventId)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        /// <summary>Gets the attendee.</summary>
        /// <param name="apiUrl">The API URL.</param>
        private List<SalesPlatformAttendeeDto> GetAttendee(string apiUrl)
        {
            var attendee = this.ExecuteRequest<EventbriteAttendee>(apiUrl, HttpMethod.Get, null);
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
        private List<SalesPlatformAttendeeDto> GetOrder(string apiUrl)
        {
            var response = this.ExecuteRequest<EventbriteOrder>(apiUrl + "?expand=attendees", HttpMethod.Get, null);
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

        /// <summary>Deserializes the payload.</summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private EventbritePayload DeserializePayload(string payload)
        {
            return JsonConvert.DeserializeObject<EventbritePayload>(payload);
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
                client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + this.apiKey);

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