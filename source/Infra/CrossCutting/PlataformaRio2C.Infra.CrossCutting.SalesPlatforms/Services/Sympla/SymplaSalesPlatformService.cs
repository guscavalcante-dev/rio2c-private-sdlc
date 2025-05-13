// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 11-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-30-2022
// ***********************************************************************
// <copyright file="SymplaSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla.Models;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.Sympla
{
    /// <summary>SymplaSalesPlatformService</summary>
    public class SymplaSalesPlatformService : ISalesPlatformService
    {
        private readonly string apiUrl = "https://api.sympla.com.br/public/v3/";
        private readonly string apiKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;
        private readonly SalesPlatformDto salesPlatformDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="SymplaSalesPlatformService" /> class.
        /// </summary>
        /// <param name="salesPlatformDto">The attendee sales platform dto.</param>
        /// <param name="salesPlatformWebhookRequestRepository">The sales platform webhook request repository.</param>
        public SymplaSalesPlatformService(SalesPlatformDto salesPlatformDto)
        {
            this.apiKey = salesPlatformDto?.ApiKey;
            this.salesPlatformDto = salesPlatformDto;
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
        }

        /// <summary>
        /// Gets the attendees.
        /// </summary>
        /// <param name="reimportAllAttendees">if set to <c>true</c> [reimport all attendees].</param>
        /// <returns></returns>
        /// <exception cref="PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions.DomainException">The sale platform is required.</exception>
        public List<SalesPlatformAttendeeDto> GetAttendees(bool reimportAllAttendees = false)
        {
            if (this.salesPlatformDto == null)
            {
                throw new DomainException("The sale platform is required.");
            }

            var salesPlatformAttendeeDtos = new List<SalesPlatformAttendeeDto>();

            foreach (var attendeeSalesPlatform in salesPlatformDto.AttendeeSalesPlatforms)
            {
                List<SymplaOrder> symplaOrders = new List<SymplaOrder>();
                if (reimportAllAttendees)
                {
                    symplaOrders = this.GetAllOrders(attendeeSalesPlatform.SalesPlatformEventid);
                }
                else
                {
                    symplaOrders = this.GetUpdatedOrders(attendeeSalesPlatform.SalesPlatformEventid, attendeeSalesPlatform.LastSalesPlatformOrderDate);
                }

                foreach (var symplaOrder in symplaOrders)
                {
                    var symplaParticipants = this.GetParticipantsByOrderId(attendeeSalesPlatform.SalesPlatformEventid, symplaOrder.Id);
                    foreach (var participant in symplaParticipants)
                    {
                        // The properties below doesn't exists in the Participants API, so they have to be taken from Order
                        participant.EventId = symplaOrder?.EventId.ToString();
                        participant.UpdatedDateString = symplaOrder?.UpdatedDateString;

                        salesPlatformAttendeeDtos.Add(new SalesPlatformAttendeeDto(participant));
                    }
                }
            }

            return salesPlatformAttendeeDtos;
        }

        #region Participants

        /// <summary>
        /// Gets the participants.
        /// </summary>
        /// <returns></returns>
        private List<SymplaParticipant> GetParticipants(string eventId)
        {
            var symplaParticipants = new List<SymplaParticipant>();
            bool hasNextPage = true;
            int page = 1;

            while (hasNextPage)
            {
                var symplaParticipantsPaged = this.ExecuteRequest<SymplaParticipantsPaged>($"events/{eventId}/participants?cancelled_filter=include&page_size=200&page={page++}", HttpMethod.Get, null);
                if (symplaParticipantsPaged.Participants.Count > 0)
                {
                    symplaParticipants.AddRange(symplaParticipantsPaged.Participants);
                }

                hasNextPage = symplaParticipantsPaged.Pagination.HasNext;
            }

            return symplaParticipants;
        }

        /// <summary>
        /// Gets the participants by order identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        private List<SymplaParticipant> GetParticipantsByOrderId(string eventId, string orderId)
        {
            var symplaParticipants = new List<SymplaParticipant>();
            bool hasNextPage = true;
            int page = 1;

            while (hasNextPage)
            {
                var symplaParticipantsPaged = this.ExecuteRequest<SymplaParticipantsPaged>($"events/{eventId}/orders/{orderId}/participants?cancelled_filter=include&page_size=200&page={page}", HttpMethod.Get, null);

                if (symplaParticipantsPaged.Participants.Count > 0)
                {
                    symplaParticipants.AddRange(symplaParticipantsPaged.Participants);
                }

                hasNextPage = symplaParticipantsPaged.Pagination.HasNext;
            }

            return symplaParticipants;
        }

        #endregion

        #region Orders

        /// <summary>
        /// Gets the updated orders.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="lastOrderUpdatedDate">The last order updated date.</param>
        /// <returns></returns>
        private List<SymplaOrder> GetUpdatedOrders(string eventId, DateTime? lastOrderUpdatedDate)
        {
            var symplaOrdersUpdated = new List<SymplaOrder>();
            var hasNoNextPageOrIsLastProcessedOrderFound = false;
            int page = 1;

            while (!hasNoNextPageOrIsLastProcessedOrderFound)
            {
                var symplaOrdersPaged = this.GetOrdersByPage(eventId, page++);

                if (lastOrderUpdatedDate.HasValue)
                {
                    var ordersUpdated = symplaOrdersPaged.SymplaOrders.Where(so => so.UpdatedDate > lastOrderUpdatedDate).ToList();
                    if (ordersUpdated.Count > 0)
                    {
                        symplaOrdersUpdated.AddRange(ordersUpdated);
                    }
                }
                else
                {
                    symplaOrdersUpdated.AddRange(symplaOrdersPaged.SymplaOrders);
                }

                // Stops the search when it finds the last Order processed or reach at last page.
                // Orders below this date has already been processed before!
                hasNoNextPageOrIsLastProcessedOrderFound = symplaOrdersPaged.SymplaOrders.FirstOrDefault(so => so.UpdatedDate == lastOrderUpdatedDate) != null
                                                            || !symplaOrdersPaged.Pagination.HasNext;
            }

            return symplaOrdersUpdated;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        private List<SymplaOrder> GetAllOrders(string eventId)
        {
            var symplaOrders = new List<SymplaOrder>();
            var hasNoNextPageOrIsLastProcessedOrderFound = false;
            int page = 1;

            while (!hasNoNextPageOrIsLastProcessedOrderFound)
            {
                var symplaOrdersPaged = this.GetOrdersByPage(eventId, page++);
                symplaOrders.AddRange(symplaOrdersPaged.SymplaOrders);

                // Stops the search when it finds the last Order processed or reach at last page.
                // Orders below this date has already been processed before!
                hasNoNextPageOrIsLastProcessedOrderFound = !symplaOrdersPaged.Pagination.HasNext;
            }

            return symplaOrders;
        }

        /// <summary>
        /// Gets the orders by page.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        private SymplaOrdersPaged GetOrdersByPage(string eventId, int page)
        {
            return this.ExecuteRequest<SymplaOrdersPaged>($"events/{eventId}/orders?status=true&field_sort=updated_date&sort=DESC&page_size=100&page={page}", HttpMethod.Get, null);
        }

        /// <summary>
        /// Gets the order by identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        private SymplaOrder GetOrderById(string eventId, string orderId)
        {
            var response = this.ExecuteRequest($"events/{eventId}/orders/{orderId}", HttpMethod.Get, null);

            JObject jObject = JObject.Parse(response);
            var symplaOrder = jObject.SelectToken("data", false)?.ToObject<SymplaOrder>(); // Get JSON inside "data" root node

            return symplaOrder;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Deserializes the payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private SymplaParticipant DeserializePayload(string payload)
        {
            var symplaPayload = JsonConvert.DeserializeObject<SymplaParticipant>(payload);

            symplaPayload.PayloadString = payload.ToJsonMinified();

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