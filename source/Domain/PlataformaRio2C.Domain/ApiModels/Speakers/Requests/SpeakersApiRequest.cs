// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 05-21-2024
// ***********************************************************************
// <copyright file="SpeakersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiRequest</summary>
    public class SpeakersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("highlights")]
        [SwaggerParameterDescription(description: "Show the Highlights.", "0=Hide | 1=Show")]
        public int? Highlights { get; set; }

        [JsonProperty("conferencesUids")]
        [SwaggerParameterDescription(description: "The Conferences Uids separated by comma.")]
        public string ConferencesUids { get; set; }

        [JsonProperty("conferencesDates")]
        [SwaggerParameterDescription("The Conferences Dates separated by comma.", "2022, 2023")]
        public string ConferencesDates { get; set; }

        [JsonProperty("conferencesRoomsUids")]
        [SwaggerParameterDescription(description: "The Conferences Rooms Uids separated by comma.")]
        public string ConferencesRoomsUids { get; set; }

        [JsonProperty("modifiedAfterDate")]
        [SwaggerParameterDescription("Returns only Speakers created or updated after this date. (UTC)", PublicApiDateTimeFormat.Default)]
        public DateTime? ModifiedAfterDate { get; set; }

        [JsonProperty("showDetails")]
        [SwaggerParameterDescription(description: "Shows extra fields.")]
        [SwaggerDefaultValue(false)]
        public bool? ShowDetails { get; set; }

        [JsonProperty("showDeleted")]
        [SwaggerParameterDescription(description: "Shows deleted Speakers.")]
        [SwaggerDefaultValue(false)]
        public bool ShowDeleted { get; set; }

        [JsonProperty("skipIsApiDisplayEnabledVerification")]
        [SwaggerParameterDescription(description: "Skips the IsApiDisplayEnabled verification and returns all Speakers registered in the Edition.")]
        [SwaggerDefaultValue(false)]
        public bool SkipIsApiDisplayEnabledVerification { get; set; }
    }
}