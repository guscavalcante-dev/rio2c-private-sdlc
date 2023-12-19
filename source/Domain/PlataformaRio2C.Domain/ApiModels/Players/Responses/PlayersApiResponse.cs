// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-15-2023
// ***********************************************************************
// <copyright file="PlayersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersApiResponse</summary>
    public class PlayersApiResponse : ListBaseModel
    {
        [JsonProperty("players")]
        public List<PlayerApiResponse> Players { get; set; }
    }
}