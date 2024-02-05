// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayerExecutiveBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerExecutiveBaseApiResponse</summary>
    public class PlayerExecutiveBaseApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("highlightPosition", Order = 300)]
        public int? HighlightPosition { get; set; }

        [JsonProperty("tradeName", Order = 400)]
        public string TradeName { get; set; }

        [JsonProperty("companyName", Order = 500)]
        public string CompanyName { get; set; }

        [JsonProperty("picture", Order = 600)]
        public string Picture { get; set; }

        [JsonProperty("isDeleted", Order = 601)]
        public bool IsDeleted { get; set; }

        [JsonProperty("descriptions", Order = 700)]
        public IEnumerable<LanguageValueApiResponse> DescriptionsApiResponses { get; set; }

        [JsonProperty("interestsGroups", Order = 800)]
        public IEnumerable<InterestGroupApiResponse> InterestGroupApiResponses { get; set; }

        [JsonProperty("collaborators", Order = 900)]
        public IEnumerable<PlayerCollaboratorApiResponse> PlayerCollaboratorApiResponses { get; set; }      
    }
}