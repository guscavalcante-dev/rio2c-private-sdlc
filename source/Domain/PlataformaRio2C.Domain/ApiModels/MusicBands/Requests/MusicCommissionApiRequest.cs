// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 10-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-16-2023
// ***********************************************************************
// <copyright file="MusicCommissionApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicCommissionApiRequest</summary>
    public class MusicCommissionApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
    }
}