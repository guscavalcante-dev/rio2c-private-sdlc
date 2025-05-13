// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Domain
// Author           : Renan Valentim
// Created          : 10-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-16-2023
// ***********************************************************************
// <copyright file="MusicCommissionApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicCommissionApiResponse</summary>
    public class MusicCommissionApiResponse : ApiBaseResponse
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

        [JsonProperty("miniBio", Order = 600)]
        public string MiniBio { get; set; }
    }
}