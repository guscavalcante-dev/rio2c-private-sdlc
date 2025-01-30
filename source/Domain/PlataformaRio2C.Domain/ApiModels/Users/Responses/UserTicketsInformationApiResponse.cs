// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-20-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-15-2024
// ***********************************************************************
// <copyright file="UserTicketsInformationApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class UserTicketsInformationApiResponse
    {
        [JsonProperty("document", Order = 100)]
        public string Document { get; set; }

        [JsonProperty("ticketsCount", Order = 200)]
        public int TicketsCount { get; set; }

        [JsonProperty("ticketTypes", Order = 201)]
        public IEnumerable<CollaboratorTicketType> TicketsTypes { get; set; }

        [JsonProperty("hasBusinessRoundsMusicBandsSubscriptionsAvailable", Order = 300)]
        public bool HasBusinessRoundsMusicBandsSubscriptionsAvailable { get; set; }

        [JsonProperty("hasPitchingMusicBandsSubscriptionsAvailable", Order = 301)]
        public bool HasPitchingMusicBandsSubscriptionsAvailable { get; set; }

        [JsonProperty("hasBusinessRoundsStartupsSubscriptionsAvailable", Order = 302)]
        public bool HasBusinessRoundsStartupsSubscriptionsAvailable { get; set; }

        [JsonProperty("hasPitchinStartupsSubscriptionsAvailable", Order = 303)]
        public bool HasPitchingStartupsSubscriptionsAvailable { get; set; }

        [JsonProperty("musicProject", Order = 400)]
        public MusicProject MusicProject { get; set; }

        [JsonProperty("startupProject", Order = 500)]
        public StartupProject StartupProject { get; set; }

        [JsonProperty("status", Order = 10000)]
        public string Status { get; set; }

        [JsonProperty("messages", Order = 10001)]
        public string[] Messages { get; set; }

        [JsonProperty("error", Order = 10002)]
        public ApiError Error { get; set; }
    }

    public class BaseProject
    {
        [JsonProperty("pitchingProjectsSubscriptionsAvailable", Order = 100)]
        public int PitchingProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("businessRoundsProjectsSubscriptionsAvailable", Order = 200)]
        public int BusinessRoundsProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("messages", Order = 300)]
        public string[] Messages { get; set; }
    }

    public class MusicProject : BaseProject
    {
    }

    public class StartupProject : BaseProject
    {
    }

    public class CollaboratorTicketType
    {
        [JsonProperty("ticketClassName", Order = 100)]
        public string TicketClassName { get; set; }

        [JsonProperty("collaboratorTypeId", Order = 200)]
        public int CollaboratorTypeId { get; set; }

        [JsonProperty("collaboratorTypeName", Order = 300)]
        public string CollaboratorTypeName { get; set; }
    }

}
