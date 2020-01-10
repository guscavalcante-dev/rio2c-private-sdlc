// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-09-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="ConferenceBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferenceBaseApiResponse</summary>
    public class ConferenceBaseApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid", Order = 301)]
        public Guid Uid { get; set; }

        [JsonProperty("event", Order = 302)]
        public EditionEventBaseApiResponse Event { get; set; }

        [JsonProperty("title", Order = 303)]
        public string Title { get; set; }

        [JsonProperty("synopsis", Order = 304)]
        public string Synopsis { get; set; }

        [JsonProperty("room", Order = 305)]
        public RoomBaseApiResponse Room { get; set; }

        [JsonProperty("date", Order = 306)]
        public string Date { get; set; }

        [JsonProperty("startTime", Order = 307)]
        public string StartTime { get; set; }

        [JsonProperty("endTime", Order = 308)]
        public string EndTime { get; set; }

        [JsonProperty("durationMinutes", Order = 309)]
        public int DurationMinutes { get; set; }

        [JsonProperty("tracks", Order = 701)]
        public List<TrackBaseApiResponse> Tracks { get; set; }

        [JsonProperty("presentationFormats", Order = 702)]
        public List<PresentationFormatBaseApiResponse> PresentationFormats { get; set; }
    }
}