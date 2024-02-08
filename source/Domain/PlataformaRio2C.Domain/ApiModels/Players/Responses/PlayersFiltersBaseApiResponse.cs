// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayersFiltersBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************  
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersFiltersBaseApiResponse</summary>
    public class PlayersFiltersBaseApiResponse : ApiBaseResponse
    {
        [JsonProperty("activities")]
        public List<ActivityApiResponse> ActivityApiResponses { get; set; }

        [JsonProperty("targetaudiences")]
        public List<TargetAudienceApiResponse> TargetAudienceApiResponses { get; set; }

        [JsonProperty("interestsGroups")]
        public List<InterestGroupApiResponse> InterestGroupApiResponses { get; set; }
    }
}