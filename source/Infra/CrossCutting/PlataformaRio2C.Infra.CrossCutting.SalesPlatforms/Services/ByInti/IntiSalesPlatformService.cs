// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 06-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 11-24-2022
// ***********************************************************************
// <copyright file="IntiSalesPlatformService.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti
{
    /// <summary>
    /// IntiSalesPlatformService
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ISalesPlatformService" />
    class IntiSalesPlatformService : ISalesPlatformService
    {
        private readonly string apiUrl = "https://api.ticketsforfun.byinti.com";
        private readonly string apiKey;
        private readonly SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntiSalesPlatformService" /> class.
        /// </summary>
        /// <param name="attendeeSalesPlatformDto">The attendee sales platform dto.</param>
        public IntiSalesPlatformService(AttendeeSalesPlatformDto attendeeSalesPlatformDto)
        {
            this.apiKey = attendeeSalesPlatformDto.SalesPlatform.ApiKey;
        }

        /// <summary>Initializes a new instance of the <see cref="EventbriteSalesPlatformService"/> class.</summary>
        /// <param name="salesPlatformWebhookRequestDto">The sales platform webhook request dto.</param>
        public IntiSalesPlatformService(SalesPlatformWebhookRequestDto salesPlatformWebhookRequestDto)
        {
            this.apiKey = salesPlatformWebhookRequestDto.SalesPlatformDto.ApiKey;
            this.salesPlatformWebhookRequestDto = salesPlatformWebhookRequestDto;
        }

        /// <summary>Executes the request.</summary>
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
        /// Gets the attendees by event identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public List<SalesPlatformAttendeeDto> GetAttendeesByEventId(string eventId)
        {
            throw new NotImplementedException();
        }

        #region Private Methods

        /// <summary>Deserializes the payload.</summary>
        /// <param name="payload">The payload.</param>
        /// <returns></returns>
        private IntiPayload DeserializePayload(string payload)
        {
            return JsonConvert.DeserializeObject<IntiPayload>(payload);
        }

        #endregion
    }
}