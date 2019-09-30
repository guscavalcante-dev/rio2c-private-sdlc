// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-30-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="PlayerApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
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
        public List<CollaboratorApiResponse> CollaboratorsApiResponses { get; set; }
    }

    /// <summary>LanguageValueApiResponse</summary>
    public class LanguageValueApiResponse
    {
        [JsonProperty("culture")]
        public string Culture { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    #region Interests

    /// <summary>InterestGroupApiResponse</summary>
    public class InterestGroupApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("interests")]
        public List<InterestApiResponse> InterestsApiResponses { get; set; }
    }

    /// <summary>InterestApiResponse</summary>
    public class InterestApiResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    #endregion

    #region Collaborators

    /// <summary>CollaboratorApiResponse</summary>
    public class CollaboratorApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("jobTitles")]
        public List<LanguageValueApiResponse> JobTitlesApiResponses { get; set; }

        [JsonProperty("miniBios")]
        public List<LanguageValueApiResponse> MiniBiosApiResponses { get; set; }
    }

    #endregion
}