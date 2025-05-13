// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-09-2020
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="ConferenceApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferenceApiRequest</summary>
    public class ConferenceApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        [SwaggerParameterDescription(description: "The Conference Uid")]
        public Guid? Uid { get; set; }
    }
}