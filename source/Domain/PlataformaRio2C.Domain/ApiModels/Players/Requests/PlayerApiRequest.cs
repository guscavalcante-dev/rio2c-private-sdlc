// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="PlayerApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayerApiRequest</summary>
    public class PlayerApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        [SwaggerParameterDescription(description: "The Player Uid")]
        public Guid? Uid { get; set; }
    }
}