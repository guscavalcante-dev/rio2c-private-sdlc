// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-07-2023
// ***********************************************************************
// <copyright file="SpeakersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiResponse</summary>
    public class SpeakersApiResponse : ListBaseModel
    {
        [JsonProperty("speakers")]
        public List<SpeakerListApiItem> Speakers { get; set; }
    }

    /// <summary>SpeakersApiListItem</summary>
    public class SpeakerListApiItem
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("badgeName", Order = 200)]
        public string BadgeName { get; set; }

        [JsonProperty("name", Order = 300)]
        public string Name { get; set; }

        [JsonProperty("highlightPosition", Order = 301)]
        public int? HighlightPosition { get; set; }

        [JsonProperty("picture", Order = 400)]
        public string Picture { get; set; }

        [JsonProperty("jobTitle", Order = 500)]
        public string JobTitle { get; set; }

        [JsonProperty("conferences", Order = 600)]
        public List<SpeakerConferenceBaseApiResponse> Conferences { get; set; }

    }
}