// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-08-2020
// ***********************************************************************
// <copyright file="ApiBaseRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ApiBaseRequest</summary>
    public class ApiBaseRequest
    {
        [JsonProperty("edition")]
        public int? Edition { get; set; }

        [JsonProperty("culture")]
        public string Culture { get; set; }
    }
}