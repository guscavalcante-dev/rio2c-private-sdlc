// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="ApiPageBaseRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>ApiPageBaseRequest</summary>
    public class ApiPageBaseRequest
    {
        [JsonProperty("edition")]
        public int? Edition { get; set; }

        [JsonProperty("keywords")]
        public string Keywords { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }
    }
}