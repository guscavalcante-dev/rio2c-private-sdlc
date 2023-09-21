// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 09-21-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-21-2023
// ***********************************************************************
// <copyright file="AudiovisualCommissionsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>AudiovisualCommissionsApiResponse</summary>
    public class AudiovisualCommissionsApiResponse : ListBaseModel
    {
        [JsonProperty("commissions")]
        public List<AudiovisualCommissionListApiItem> Commissions { get; set; }
    }

    /// <summary>SpeakersApiListItem</summary>
    public class AudiovisualCommissionListApiItem
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("picture", Order = 300)]
        public string Picture { get; set; }
    }
}