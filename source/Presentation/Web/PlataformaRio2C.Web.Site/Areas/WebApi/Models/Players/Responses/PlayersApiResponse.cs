// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="PlayersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>PlayersApiResponse</summary>
    public class PlayersApiResponse : ListBaseModel
    {
        [JsonProperty("players")]
        public List<PlayersApiListItem> Players { get; set; }
    }

    /// <summary>PlayersApiListItem</summary>
    public class PlayersApiListItem : OrganizationBaseApiResponse
    {
        [JsonProperty("name", Order = 101)]
        public string Name { get; set; }

        [JsonProperty("highlightPosition", Order = 301)]
        public int? HighlightPosition { get; set; }
    }
}