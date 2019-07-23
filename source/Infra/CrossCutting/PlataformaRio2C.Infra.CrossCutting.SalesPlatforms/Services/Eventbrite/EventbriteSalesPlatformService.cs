// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 07-12-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 07-23-2019
// ***********************************************************************
// <copyright file="EventbriteSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Configuration;
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
        private readonly string eventId; // Rio2C: 63245927271 // Rafael: 65120229359
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public EventbriteSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.appKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            this.eventId = ConfigurationManager.AppSettings["EventbriteEventId"];
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }

        /// <summary>Executes the request.</summary>
        public void ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            this.GetAttendee();
        }

        public void GetLastEvent()
        {
            //var evt = this.ctx.GetEvent();
            //var attendees = evt.Attendees;
        }

        public void GetEvent()
        {
            var response = this.ExecuteRequest<Event>($"events/{this.eventId}/", HttpMethod.Get, null);
        }

        /// <summary>Gets the attendee.</summary>
        public void GetAttendee()
        {
            var response = this.ExecuteRequest<Attendee>($"events/{this.eventId}/attendees/{1245580008}/", HttpMethod.Get, null);
        }

        /// <summary>Executes the request.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiLink">The API link.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="jsonString">The json string.</param>
        /// <returns></returns>
        private T ExecuteRequest<T>(string apiLink, HttpMethod httpMethod, string jsonString)
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8;");
                client.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + this.appKey);

                try
                {
                    ServicePointManager.Expect100Continue = false;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                           SecurityProtocolType.Tls11 |
                                                           SecurityProtocolType.Tls12;

                    var response = httpMethod == HttpMethod.Get ? client.DownloadString(this.ApiUrl + apiLink) :
                                                                  client.UploadString(this.ApiUrl + apiLink, httpMethod.ToString(), jsonString);

                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                    {
                        var respStream = ex.Response.GetResponseStream();
                        if (respStream != null)
                        {
                            var resp = new StreamReader(respStream).ReadToEnd();
                            return JsonConvert.DeserializeObject<T>(resp);
                        }
                    }

                    throw;
                }
            }
        }

    }
}
