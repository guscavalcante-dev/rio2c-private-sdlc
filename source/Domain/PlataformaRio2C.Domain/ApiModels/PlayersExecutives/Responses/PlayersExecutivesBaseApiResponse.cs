// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayersExecutivesBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersExecutivesBaseApiResponse</summary>
    public class PlayersExecutivesBaseApiResponse : ListBaseModel
    {
        [JsonProperty("players")]
        public List<PlayerExecutiveBaseApiResponse> Players { get; set; }
    }
}