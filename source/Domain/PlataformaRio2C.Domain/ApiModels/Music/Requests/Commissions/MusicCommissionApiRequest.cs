// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 10-16-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="MusicCommissionApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>MusicCommissionApiRequest</summary>
    public class MusicCommissionApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        [SwaggerParameterDescription(description: "The Music Commision Member Uid")]
        public Guid? Uid { get; set; }
    }
}