// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-14-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="OrganizationApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>OrganizationApiResponse</summary>
    public class OrganizationApiResponse : ApiBaseResponse
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
        public List<OrganizationLanguageValueApiResponse> DescriptionsApiResponses { get; set; }

        [JsonProperty("interestsGroups")]
        public List<OrganizationInterestGroupApiResponse> InterestGroupApiResponses { get; set; }
    }

    /// <summary>LanguageValueApiResponse</summary>
    public class OrganizationLanguageValueApiResponse
    {
        [JsonProperty("culture")]
        public string Culture { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    #region Interests

    /// <summary>OrganizationInterestGroupApiResponse</summary>
    public class OrganizationInterestGroupApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("interests")]
        public List<OrganizationInterestApiResponse> InterestsApiResponses { get; set; }
    }

    /// <summary>OrganizationInterestApiResponse</summary>
    public class OrganizationInterestApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    #endregion
}