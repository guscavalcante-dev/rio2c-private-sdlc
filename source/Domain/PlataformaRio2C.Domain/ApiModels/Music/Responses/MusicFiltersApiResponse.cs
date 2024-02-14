// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 09-10-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-10-2021
// ***********************************************************************
// <copyright file="MusicFiltersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicFiltersApiResponse</summary>
    public class MusicFiltersApiResponse : ApiBaseResponse
    {
        [JsonProperty("musicBandTypes")]
        public List<ApiListItemBaseResponse> MusicBandTypes { get; set; }

        [JsonProperty("musicGenres")]
        public List<ApiListItemBaseResponse> MusicGenres { get; set; }

        [JsonProperty("targetAudiences")]
        public List<ApiListItemBaseResponse> TargetAudiences { get; set; }
    }
}