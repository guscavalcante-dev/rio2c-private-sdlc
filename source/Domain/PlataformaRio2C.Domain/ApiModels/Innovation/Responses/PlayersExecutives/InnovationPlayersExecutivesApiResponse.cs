// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="InnovationPlayersExecutivesApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationPlayersExecutivesApiResponse</summary>
    public class InnovationPlayersExecutivesApiResponse : ListBaseModel
    {
        [JsonProperty("playersExecutives")]
        public List<InnovationPlayerExecutiveApiResponse> PlayersExecutives { get; set; }
    }
}