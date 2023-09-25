// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 09-22-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 09-22-2023
// ***********************************************************************
// <copyright file="AudiovisualCommissionApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>AudiovisualCommissionApiRequest</summary>
    public class AudiovisualCommissionApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
    }
}