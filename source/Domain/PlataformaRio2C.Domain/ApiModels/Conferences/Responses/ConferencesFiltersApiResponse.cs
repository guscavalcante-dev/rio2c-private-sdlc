// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-10-2020
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
        [JsonProperty("editionDates", Order = 101)]
        public List<string> EditionDates { get; set; }

        [JsonProperty("events", Order = 102)]
        public List<EditionEventApiResponse> EventsApiResponses { get; set; }

        [JsonProperty("rooms", Order = 103)]
        public List<ConferencesFilterItemApiResponse> RoomsApiResponses { get; set; }
        
        [JsonProperty("pillars", Order = 104)]
        public List<PillarBaseApiResponse> PillarsApiResponses { get; set; }

        [JsonProperty("tracks", Order = 105)]
        public List<TrackBaseApiResponse> TracksApiResponses { get; set; }

        [JsonProperty("presentationFormats", Order = 106)]
        public List<ConferencesFilterItemApiResponse> PresentationFormatsApiResponses { get; set; }
    }

    /// <summary>ConferencesFilterItemApiResponse</summary>
    public class ConferencesFilterItemApiResponse
    {
        [JsonProperty("uid", Order = 101)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 102)]
        public string Name { get; set; }
    }
}