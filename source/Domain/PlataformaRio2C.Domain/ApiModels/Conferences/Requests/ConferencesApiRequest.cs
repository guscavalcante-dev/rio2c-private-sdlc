// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="ConferencesApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferencesApiRequest</summary>
    public class ConferencesApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("editionDates")]
        public string EditionDates { get; set; }

        [JsonProperty("eventsUids")]
        public string EventsUids { get; set; }

        [JsonProperty("roomsUids")]
        public string RoomsUids { get; set; }

        [JsonProperty("tracksUids")]
        public string TracksUids { get; set; }

        [JsonProperty("pillarsUids")]
        public string PillarsUids { get; set; }

        [JsonProperty("presentationFormatsUids")]
        public string PresentationFormatsUids { get; set; }
    }
}