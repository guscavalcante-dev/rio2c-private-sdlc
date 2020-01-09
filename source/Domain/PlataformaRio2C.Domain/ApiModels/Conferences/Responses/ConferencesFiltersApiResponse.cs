// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="ConferencesFiltersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************  
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferencesFiltersApiResponse</summary>
    public class ConferencesFiltersApiResponse : ApiBaseResponse
    {
        [JsonProperty("editionDates")]
        public List<string> EditionDates { get; set; }

        [JsonProperty("events")]
        public List<EditionEventApiResponse> EventsApiResponses { get; set; }

        [JsonProperty("rooms")]
        public List<ConferencesFilterItemApiResponse> RoomsApiResponses { get; set; }

        [JsonProperty("tracks")]
        public List<TrackBaseApiResponse> TracksApiResponses { get; set; }

        [JsonProperty("presentationFormats")]
        public List<ConferencesFilterItemApiResponse> PresentationFormatsApiResponses { get; set; }
    }

    /// <summary>ConferencesFilterItemApiResponse</summary>
    public class ConferencesFilterItemApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}