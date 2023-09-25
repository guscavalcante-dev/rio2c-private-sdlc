// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Domain
// Author           : Renan Valentim
// Created          : 09-22-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-22-2023
// ***********************************************************************
// <copyright file="AudiovisualCommissionApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>AudiovisualCommissionApiResponse</summary>
    public class AudiovisualCommissionApiResponse : ApiBaseResponse
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("picture", Order = 300)]
        public string Picture { get; set; }

        [JsonProperty("miniBio", Order = 400)]
        public string MiniBio { get; set; }
    }
}