// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-13-2023
// ***********************************************************************
// <copyright file="ListBaseModel.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ListBaseModel</summary>
    public class ListBaseModel : ApiBaseResponse
    {
        [JsonProperty("hasPreviousPage", Order = 9001)]
        public bool HasPreviousPage { get; set; }

        [JsonProperty("hasNextPage", Order = 9002)]
        public bool HasNextPage { get; set; }

        [JsonProperty("totalItemCount", Order = 9003)]
        public int TotalItemCount { get; set; }

        [JsonProperty("pageCount", Order = 9004)]
        public int PageCount { get; set; }

        [JsonProperty("pageNumber", Order = 9005)]
        public int PageNumber { get; set; }

        [JsonProperty("pageSize", Order = 9006)]
        public int PageSize { get; set; }

        [JsonProperty("timeStamp", Order = 9007)]
        public string TimeStamp { get; set; }

        public ListBaseModel()
        {
            this.TimeStamp = DateTimeOffset.UtcNow.ToString(PublicApiDateTimeFormat.Default);
        }
    }
}