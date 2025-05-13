// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.CrossCutting.SalesPlatforms
// Author           : Renan Valentim
// Created          : 08-15-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-02-2023
// ***********************************************************************
// <copyright file="InstagramResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.SocialMediaPlatforms.Services.Instagram.Models
{
    public class InstagramResponse
    {
        [JsonProperty("data")]
        public List<Publication> Publications { get; set; }

        [JsonProperty("paging")]
        public Paging Paging { get; set; }
    }

    public class Publication
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("shortcode")]
        public string ShortCode { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("media_product_type")]
        public string MediaProductType { get; set; }

        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("media_url")]
        public string MediaUrl { get; set; }

        public bool IsVideo => this.MediaType == "VIDEO";
    }

    public class Paging
    {
        [JsonProperty("cursors")]
        public Cursors Cursors { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }
    }

    public class Cursors
    {
        [JsonProperty("before")]
        public string Before { get; set; }

        [JsonProperty("after")]
        public string After { get; set; }
    }
}