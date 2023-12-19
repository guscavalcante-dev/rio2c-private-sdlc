// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-07-2023
// ***********************************************************************
// <copyright file="SpeakerApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Dtos;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakerApiResponse</summary>
    public class SpeakerApiResponse : ApiBaseResponse
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

        [JsonProperty("miniBio", Order = 500)]
        public string MiniBio { get; set; }

        [JsonProperty("jobTitle", Order = 600)]
        public string JobTitle { get; set; }

        [JsonProperty("site", Order = 601)]
        public string Site { get; set; }

        [JsonProperty("socialNetworks", Order = 700)]
        public IEnumerable<SpeakerSocialNetworkApiResponse> SocialNetworks { get; set; }

        [JsonProperty("tracks", Order = 701)]
        public IEnumerable<TrackBaseApiResponse> Tracks { get; set; }

        [JsonProperty("companies", Order = 702)]
        public IEnumerable<SpeakerOrganizationApiResponse> Companies { get; set; }

        [JsonProperty("conferences", Order = 703)]
        public IEnumerable<SpeakerConferenceApiResponse> Conferences { get; set; }
    }

    /// <summary>SpeakerSocialNetworkApiResponse</summary>
    public class SpeakerSocialNetworkApiResponse
    {
        [JsonProperty("slug", Order = 100)]
        public string Slug { get; set; }

        [JsonProperty("url", Order = 101)]
        public string Url { get; set; }
    }

    /// <summary>SpeakerOrganizationApiResponse</summary>
    public class SpeakerOrganizationApiResponse
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("tradeName", Order = 200)]
        public string TradeName { get; set; }

        [JsonProperty("companyName", Order = 300)]
        public string CompanyName { get; set; }

        [JsonProperty("picture", Order = 400)]
        public string Picture { get; set; }
    }

    /// <summary>SpeakerConferenceApiResponse</summary>
    public class SpeakerConferenceApiResponse
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
    }
}