using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;
using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Dtos;
using PlataformaRio2C.Infra.CrossCutting.Tools.Exceptions;

using PlataformaRio2C.Infra.CrossCutting.SalesPlatforms.Services.ByInti.Models;

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

            var payload = JsonConvert.DeserializeObject<IntiPayload>(salesPlatformWebhookRequestDto.SalesPlatformWebhookRequest.Payload);

            var listAttendeeDto = new List<SalesPlatformAttendeeDto>();
            var att = new SalesPlatformAttendeeDto(payload);
            listAttendeeDto.Add(att);

            return new Tuple<string, List<SalesPlatformAttendeeDto>>(payload.GetSalesPlatformAttendeeStatus(), listAttendeeDto);
        }
    }
}