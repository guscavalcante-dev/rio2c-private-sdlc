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
        public List<PlayersApiListItem> Players { get; set; }
    }

    /// <summary>PlayersApiListItem</summary>
    public class PlayersApiListItem
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("companyName", Order = 300)]
        public string CompanyName { get; set; }

        [JsonProperty("tradeName", Order = 400)]
        public string TradeName { get; set; }
        
        [JsonProperty("picture", Order = 500)]
        public string Picture { get; set; }

        [JsonProperty("highlightPosition", Order = 600)]
        public int? HighlightPosition { get; set; }

        [JsonProperty("descriptions", Order = 700)]
        public List<LanguageValueApiResponse> DescriptionsApiResponses { get; set; }

        [JsonProperty("interestsGroups", Order = 800)]
        public List<InterestGroupApiResponse> InterestGroupApiResponses { get; set; }

        [JsonProperty("collaborators", Order = 900)]
        public List<PlayerCollaboratorApiResponse> CollaboratorsApiResponses { get; set; }
    }
}