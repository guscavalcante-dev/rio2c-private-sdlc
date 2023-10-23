// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="ConferencesApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferencesApiRequest</summary>
    public class ConferencesApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("editionDates")]
        [SwaggerParameterDescription("The Editions Dates separated by comma.", "2022, 2023")]
        public string EditionDates { get; set; }

        [JsonProperty("eventsUids")]
        [SwaggerParameterDescription(description: "Events Uids separated by comma.")]
        public string EventsUids { get; set; }

        [JsonProperty("roomsUids")]
        [SwaggerParameterDescription(description: "Rooms Uids separated by comma.")]
        public string RoomsUids { get; set; }

        [JsonProperty("tracksUids")]
        [SwaggerParameterDescription(description: "Tracks Uids separated by comma.")]
        public string TracksUids { get; set; }

        [JsonProperty("pillarsUids")]
        [SwaggerParameterDescription(description: "Pillars Uids separated by comma.")]
        public string PillarsUids { get; set; }

        [JsonProperty("presentationFormatsUids")]
        [SwaggerParameterDescription(description: "Presentation Formats Uids separated by comma.")]
        public string PresentationFormatsUids { get; set; }
    }
}