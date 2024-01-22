// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 01-20-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-20-2024
// ***********************************************************************
// <copyright file="UserProjectsSubscriptionsAvailableApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    public class UserProjectsSubscriptionsAvailableApiResponse
    {
        [JsonProperty("email", Order = 100)]
        public string Email { get; set; }

        [JsonProperty("hasMusicProjectsSubscriptionsAvailable", Order = 200)]
        public bool HasMusicProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("hasStartupProjectsSubscriptionsAvailable", Order = 201)]
        public bool HasStartupProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("status", Order = 10000)]
        public string Status { get; set; }

        [JsonProperty("messages", Order = 10001)]
        public string[] Messages { get; set; }

        [JsonProperty("error", Order = 10002)]
        public ApiError Error { get; set; }

        #region Markets

        public MusicMarket MusicMarket { get; set; }

        public StartupMarket StartupMarket { get; set; }

        #endregion
    }


    //TODO: MOVER TODOS PARA ARQUIVOS INDEPENDENTES!
    public class MarketBase
    {
        [JsonProperty("pitchingProjectsSubscriptionsAvailable", Order = 100)]
        public int PitchingProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("businessRoundsProjectsSubscriptionsAvailable", Order = 200)]
        public int BusinessRoundsProjectsSubscriptionsAvailable { get; set; }

        [JsonProperty("messages", Order = 300)]
        public string[] Messages { get; set; }
    }

    public class MusicMarket : MarketBase
    {

    }

    public class StartupMarket : MarketBase
    {

    }
}
