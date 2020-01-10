// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-10-2020
// ***********************************************************************
// <copyright file="ConferencesApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferencesApiResponse</summary>
    public class ConferencesApiResponse : ListBaseModel
    {
        [JsonProperty("conferences")]
        public List<ConferencesApiListItem> Conferences { get; set; }
    }

    /// <summary>ConferencesApiListItem</summary>
    public class ConferencesApiListItem
    {
        [JsonProperty("uid", Order = 101)]
        public Guid Uid { get; set; }

        [JsonProperty("event", Order = 102)]
        public EditionEventBaseApiResponse Event { get; set; }

        [JsonProperty("title", Order = 103)]
        public string Title { get; set; }

        [JsonProperty("synopsis", Order = 104)]
        public string Synopsis { get; set; }

        [JsonProperty("room", Order = 105)]
        public RoomBaseApiResponse Room { get; set; }

        [JsonProperty("date", Order = 106)]
        public string Date { get; set; }

        [JsonProperty("startTime", Order = 107)]
        public string StartTime { get; set; }

        [JsonProperty("endTime", Order = 108)]
        public string EndTime { get; set; }

        [JsonProperty("durationMinutes", Order = 109)]
        public int DurationMinutes { get; set; }

        [JsonProperty("tracks", Order = 701)]
        public List<TrackBaseApiResponse> Tracks { get; set; }

        [JsonProperty("presentationFormats", Order = 702)]
        public List<PresentationFormatBaseApiResponse> PresentationFormats { get; set; }
    }
}