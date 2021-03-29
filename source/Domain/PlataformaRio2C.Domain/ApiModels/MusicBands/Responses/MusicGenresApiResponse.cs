// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="MusicGenresApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicGenresApiResponse</summary>
    public class MusicGenresApiResponse : ApiBaseResponse
    {
        [JsonProperty("musicBandTypes")]
        public List<MusicGenreListApiItem> MusicGenres { get; set; }
    }

    /// <summary>MusicGenreListApiItem</summary>
    public class MusicGenreListApiItem
    {
        [JsonProperty("id", Order = 100)]
        public int Id { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }
    }
}