﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-01-2025
// ***********************************************************************
// <copyright file="PlayerBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerBaseApiResponse</summary>
    public class PlayerBaseApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

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