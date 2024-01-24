// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-20-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-20-2024
// ***********************************************************************
// <copyright file="UserTicketsInformationApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class UserTicketsInformationApiResponse
    {
        [SwaggerParameterDescription(description: "The email to get tickets information from.")]
        [JsonProperty("email", Order = 100)]
        public string Email { get; set; }

        [JsonProperty("hasTicket", Order = 200)]
        public bool HasTicket { get; set; }

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


    //TODO: MOVER TODOS PARA ARQUIVOS INDEPENDENTES!
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
}
