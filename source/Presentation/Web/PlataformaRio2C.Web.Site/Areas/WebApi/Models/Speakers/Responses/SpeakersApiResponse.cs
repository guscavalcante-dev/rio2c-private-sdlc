// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="SpeakersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>SpeakersApiResponse</summary>
    public class SpeakersApiResponse : ListBaseModel
    {
        [JsonProperty("speakers")]
        public List<SpeakersApiListItem> Speakers { get; set; }
    }

    /// <summary>SpeakersApiListItem</summary>
    public class SpeakersApiListItem
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("highlightPosition")]
        public int? HighlightPosition { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }
    }
}