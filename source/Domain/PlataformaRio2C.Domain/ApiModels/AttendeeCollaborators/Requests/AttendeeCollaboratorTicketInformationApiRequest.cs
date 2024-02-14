// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-02-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketInformationApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class AttendeeCollaboratorTicketInformationApiRequest : ApiBaseRequest
    {
        [JsonProperty("key")]
        [SwaggerParameterDescription(description: "The API Key.", isRequired: true)]
        public string Key { get; set; }

        [JsonProperty("ticketCode")]
        [SwaggerParameterDescription(description: "The E-ticket code to validate.", isRequired: true)]
        public string TicketCode { get; set; }
    }
}
