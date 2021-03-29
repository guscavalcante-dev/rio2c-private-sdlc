// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 03-27-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="TargetAudiencesApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>TargetAudiencesApiResponse</summary>
    public class TargetAudiencesApiResponse : ApiBaseResponse
    {
        [JsonProperty("musicBandTypes")]
        public List<TargetAudienceListApiItem> TargetAudiences { get; set; }
    }

    /// <summary>TargetAudienceListApiItem</summary>
    public class TargetAudienceListApiItem
    {
        [JsonProperty("id", Order = 100)]
        public int Id { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }
    }
}