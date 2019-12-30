// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="PlayersFiltersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************  
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersFiltersApiResponse</summary>
    public class PlayersFiltersApiResponse : ApiBaseResponse
    {
        [JsonProperty("activities")]
        public List<ActivityApiResponse> ActivityApiResponses { get; set; }

        [JsonProperty("targetaudiences")]
        public List<TargetAudienceApiResponse> TargetAudienceApiResponses { get; set; }

        [JsonProperty("interestsGroups")]
        public List<InterestGroupApiResponse> InterestGroupApiResponses { get; set; }
    }
}