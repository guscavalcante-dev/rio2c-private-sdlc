// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="ApiPageBaseRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ApiPageBaseRequest</summary>
    public class ApiPageBaseRequest : ApiBaseRequest
    {
        [JsonProperty("keywords")]
        [SwaggerParameterDescription(description: "The keywords.")]
        public string Keywords { get; set; }

        [JsonProperty("page")]
        [SwaggerParameterDescription("The page.", "1")]
        public int? Page { get; set; }

        [JsonProperty("pageSize")]
        [SwaggerParameterDescription( "The page size.", "10")]
        public int? PageSize { get; set; }
    }
}