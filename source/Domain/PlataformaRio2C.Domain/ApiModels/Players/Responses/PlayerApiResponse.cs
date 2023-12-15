// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-30-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-15-2023
// ***********************************************************************
// <copyright file="PlayerApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerApiResponse</summary>
    public class PlayerApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string TradeName { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("descriptions")]
        public List<LanguageValueApiResponse> DescriptionsApiResponses { get; set; }

        [JsonProperty("interestsGroups")]
        public List<InterestGroupApiResponse> InterestGroupApiResponses { get; set; }

        [JsonProperty("collaborators")]
        public List<PlayerCollaboratorApiResponse> CollaboratorsApiResponses { get; set; }
    }
}