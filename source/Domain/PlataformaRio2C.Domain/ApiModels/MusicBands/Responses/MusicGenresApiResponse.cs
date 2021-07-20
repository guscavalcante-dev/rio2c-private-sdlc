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
        [JsonProperty("musicGenres")]
        public List<ApiListItemBaseResponse> MusicGenres { get; set; }
    }
}