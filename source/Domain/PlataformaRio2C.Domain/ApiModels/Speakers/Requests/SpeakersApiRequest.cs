// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-07-2023
// ***********************************************************************
// <copyright file="SpeakersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiRequest</summary>
    public class SpeakersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("highlights")]
        public int? Highlights { get; set; }

        [JsonProperty("conferencesUids")]
        public string ConferencesUids { get; set; }

        [JsonProperty("conferencesDates")]
        public string ConferencesDates { get; set; }

        [JsonProperty("conferencesRoomsUids")]
        public string ConferencesRoomsUids { get; set; }
    }
}