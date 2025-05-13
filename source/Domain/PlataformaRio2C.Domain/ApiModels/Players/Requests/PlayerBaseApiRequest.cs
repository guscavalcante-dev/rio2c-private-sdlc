// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-03-2024
// ***********************************************************************
// <copyright file="PlayerBaseApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerBaseApiRequest</summary>
    public class PlayerBaseApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        [SwaggerParameterDescription(description: "The Player Uid")]
        public Guid? Uid { get; set; }
    }
}