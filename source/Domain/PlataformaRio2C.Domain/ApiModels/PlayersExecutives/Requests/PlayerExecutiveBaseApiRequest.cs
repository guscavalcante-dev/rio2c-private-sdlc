// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 02-03-2024
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayerExecutiveBaseApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerExecutiveBaseApiRequest</summary>
    public class PlayerExecutiveBaseApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        [SwaggerParameterDescription(description: "The Player Executive Uid")]
        public Guid? Uid { get; set; }
    }
}