// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="MusicBandTypesApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicBandTypesApiResponse</summary>
    public class MusicBandTypesApiResponse : ApiBaseResponse
    {
        [JsonProperty("musicBandTypes")]
        public List<MusicBandTypeListApiItem> MusicBandTypes { get; set; }
    }

    /// <summary>MusicBandTypeListApiItem</summary>
    public class MusicBandTypeListApiItem
    {
        [JsonProperty("id", Order = 100)]
        public int Id { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }
    }
}