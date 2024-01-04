// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-15-2023
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
using System.ComponentModel;

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
        [SwaggerParameterDescription("Returns only registers created or updated after this date. (UTC)", PublicApiDateTimeFormat.Default)]
        public DateTime? ModifiedAfterDate { get; set; }

        [JsonProperty("showDetails")]
        [SwaggerParameterDescription(description: "Shows extra fields.")]
        public bool? ShowDetails { get; set; }

        [JsonProperty("ShowDeleted")]
        [SwaggerParameterDescription(description: "Shows Deleted fields.")]
        [SwaggerDefaultValue(false)]
        public bool ShowDeleted { get; set; }
    }
}