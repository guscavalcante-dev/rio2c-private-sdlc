// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
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
        public List<PlayersApiListItem> Players { get; set; }
    }

    /// <summary>PlayersApiListItem</summary>
    public class PlayersApiListItem
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 101)]
        public string Name { get; set; }

        [JsonProperty("tradeName", Order = 200)]
        public string TradeName { get; set; }

        [JsonProperty("companyName", Order = 300)]
        public string CompanyName { get; set; }

        [JsonProperty("picture", Order = 400)]
        public string Picture { get; set; }

        [JsonProperty("highlightPosition", Order = 301)]
        public int? HighlightPosition { get; set; }
    }
}