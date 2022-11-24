// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="SymplaSalesPlatformService.cs" company="Softo">
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
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla
{
    /// <summary>SymplaSalesPlatformService</summary>
    public class SymplaSalesPlatformService : ISalesPlatformService
    {
        private readonly string apiUrl = "https://api.sympla.com.br/public/v3/";
        private readonly string apiKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymplaSalesPlatformService" /> class.
        /// </summary>
        /// <param name="attendeeSalesPlatformDto">The attendee sales platform dto.</param>
        public SymplaSalesPlatformService(AttendeeSalesPlatformDto attendeeSalesPlatformDto)
        {
            this.apiKey = attendeeSalesPlatformDto.SalesPlatform.ApiKey;
        }

        /// <summary>Initializes a new instance of the <see cref="SymplaSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public SymplaSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
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
        /// The payload is required.
        /// </exception>
        public Tuple<string, List<SalesPlatformAttendeeDto>> ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            var payload = this.DeserializePayload(salesPlatformWebhookRequestDto.SalesPlatformWebhookRequest.Payload);
            if (payload == null)
            {
                throw new DomainException("The payload is required.");
            }

            var salesPlatformAttendeeDtos = new List<SalesPlatformAttendeeDto>
            {
                new SalesPlatformAttendeeDto(payload)
            };

            return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.GetSalesPlatformAction(), salesPlatformAttendeeDtos);

            //TODO: Implementar Checkin e Checkout do participante. Precisa ver se a sympla envia esses eventos!
            //var payload = this.DeserializePayload(this.salesPlatformWebhookRequestDto.SalesPlatformWebhookRequest.Payload);
            //switch (payload.Config.Action)
            //{
            //    // Attendees updates
            //    case SymplaAction.AttendeeUpdated:
            //    case SymplaAction.AttendeeCheckedIn:
            //    case SymplaAction.AttendeeCheckedOut:
            //        return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.Config.GetSalesPlatformAction(), this.GetAttendee(payload.ApiUrl));

            //    //// Orders updates
            //    //case Action.EventbriteOrderPlaced:
            //    //case Action.EventbriteOrderRefunded:
            //    //case Action.EventbriteOrderUpdated:
            //    //    return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.Config.GetAction(), this.GetOrder(payload.ApiUrl));

            //    // Other Updates
            //    default:
            //        throw new DomainException($"Sympla action ({payload.Config.Action}) not configured.");
            //}
        }

        /// <summary>
        /// Gets the attendees by event identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        public List<SalesPlatformAttendeeDto> GetAttendeesByEventId(string eventId)
        {
            var response = this.ExecuteRequest($"events/{eventId}/participants", HttpMethod.Get, null);
            JObject responseJObject = JObject.Parse(response);
            List<JToken> originalPayloads = responseJObject["data"].ToList();

            var symplaPagedPayload = JsonConvert.DeserializeObject<SymplaPagedPayload>(response);

            var salesPlatformAttendeeDtos = new List<SalesPlatformAttendeeDto>();
            foreach (var payload in symplaPagedPayload.Payloads)
            {
                // Populates the original payload received by API, without deserialization.
                // It's necessary to save the original payload at database.
                payload.OriginalPayload = originalPayloads.FirstOrDefault(s => s["id"].Value<int>() == payload.Id)?.ToString().ToJsonMinified(); 

                salesPlatformAttendeeDtos.Add(new SalesPlatformAttendeeDto(payload));
            }

            return salesPlatformAttendeeDtos;
        }

        ///// <summary>Gets the attendee.</summary>
        ///// <param name="apiUrl">The API URL.</param>
        //public List<SalesPlatformAttendeeDto> GetAttendee(string apiUrl)
        //{
        //    var attendee = this.ExecuteRequest<EventbriteAttendee>(apiUrl, HttpMethod.Get, null);
        //    if (attendee == null)
        //    {
        //        return null;
        //    }

        //    var attendees = new List<SalesPlatformAttendeeDto>();
        //    attendees.Add(new SalesPlatformAttendeeDto(attendee));

        //    return attendees;
        //}

        ///// <summary>Gets the order.</summary>
        ///// <param name="apiUrl">The API URL.</param>
        //public List<SalesPlatformAttendeeDto> GetOrder(string apiUrl)
        //{
        //    var response = this.ExecuteRequest<EventbriteOrder>(apiUrl + "?expand=attendees", HttpMethod.Get, null);
        //    if (response == null)
        //    {
        //        return null;
        //    }

        //    var attendees = new List<SalesPlatformAttendeeDto>();
        //    foreach (var attendee in response.Attendees)
        //    {
        //        attendees.Add(new SalesPlatformAttendeeDto(attendee));
        //    }

        //    return attendees;
        //}

        #region Private methods

        /// <summary>
        /// Deserializes the payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private SymplaPayload DeserializePayload(string payload)
        {
            var symplaPayload = JsonConvert.DeserializeObject<SymplaPayload>(payload);

            symplaPayload.OriginalPayload = payload.ToJsonMinified();

            return symplaPayload;
        }

        /// <summary>
        /// Executes the request.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private string ExecuteRequest(string path, HttpMethod httpMethod, string jsonString)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("s_token", this.apiKey); 

                ServicePointManager.Expect100Continue = false;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 |
                                                       SecurityProtocolType.Tls12;
                string url = this.apiUrl + path;
                var response = httpMethod == HttpMethod.Get ? client.DownloadString(url) :
                                                              client.UploadString(url, httpMethod.ToString(), jsonString);

                return response;
            }
        }

        /// <summary>
        /// Executes the request and desserialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private T ExecuteRequest<T>(string path, HttpMethod httpMethod, string jsonString)
        {
            var response = this.ExecuteRequest(path, httpMethod, jsonString);
            return JsonConvert.DeserializeObject<T>(response);
        }

        #endregion
    }
}