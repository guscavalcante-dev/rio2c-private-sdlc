// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Domain
// Author           : Renan Valentim
// Created          : 10-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-16-2023
// ***********************************************************************
// <copyright file="MusicCommissionsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicCommissionsApiResponse</summary>
    public class MusicCommissionsApiResponse : ListBaseModel
    {
        [JsonProperty("commissions")]
        public List<MusicCommissionListApiItem> Commissions { get; set; }
    }

    /// <summary>SpeakersApiListItem</summary>
    public class MusicCommissionListApiItem
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("picture", Order = 300)]
        public string Picture { get; set; }

        [JsonProperty("jobTitle", Order = 400)]
        public string JobTitle { get; set; }

        [JsonProperty("organizationsNames", Order = 500)]
        public string OrganizationsNames { get; set; }
    }
}