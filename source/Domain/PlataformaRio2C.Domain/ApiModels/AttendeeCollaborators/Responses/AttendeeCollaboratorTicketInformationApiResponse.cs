// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-02-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-02-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTicketInformationApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class AttendeeCollaboratorTicketInformationApiResponse : ApiBaseResponse
    {
        [JsonProperty("ticketCode", Order = 100)]
        public string TicketCode { get; set; }

        [JsonProperty("ticketExists", Order = 200)]
        public bool TicketExists { get; set; }
    }
}
