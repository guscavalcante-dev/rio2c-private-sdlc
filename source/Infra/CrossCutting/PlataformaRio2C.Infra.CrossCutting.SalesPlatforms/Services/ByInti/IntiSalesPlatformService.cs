using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

//using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Eventbrite.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti 
{
    class IntiSalesPlatformService : ISalesPlatformService
    {

        private string ApiUrl = "https://api.ticketsforfun.byinti.com";

        private readonly string appKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public IntiSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.appKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }


        /// <summary>Executes the request.</summary>
        public Tuple<string, List<SalesPlatformAttendeeDto>> ExecuteRequest()
        {
            if (this.salesPlatformWebhookRequestDto == null)
            {
                throw new DomainException("The webhook request is required.");
            }

            /*
            return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.Config.GetSalesPlatformAction(), this.GetAttendee(payload.ApiUrl));

            SalesPlatformAttendeeDto att = new SalesPlatformAttendeeDto()
            {

            };
            */

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

        /// <summary>Gets the attendee.</summary>
        /// <param name="apiUrl">The API URL.</param>
        public List<SalesPlatformAttendeeDto> GetAttendee(string apiUrl)
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
        public List<SalesPlatformAttendeeDto> GetOrder(string apiUrl)
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

        //public void GetEvent()
        //{
        //    var response = this.ExecuteRequest<Event>($"events/{this.eventId}/", HttpMethod.Get, null);
        //}

        #region Private methods

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