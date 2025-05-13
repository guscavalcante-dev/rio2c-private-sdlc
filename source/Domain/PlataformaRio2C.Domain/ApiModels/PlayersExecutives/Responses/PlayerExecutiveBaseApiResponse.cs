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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerExecutiveBaseApiResponse</summary>
    public class PlayerExecutiveBaseApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("badgeName")]
        public string BadgeName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("highlightPosition")]
        public int? HighlightPosition { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("jobTitles")]
        public List<LanguageValueApiResponse> JobTitlesApiResponses { get; set; }

        [JsonProperty("miniBios")]
        public List<LanguageValueApiResponse> MiniBiosApiResponses { get; set; }
    }
}