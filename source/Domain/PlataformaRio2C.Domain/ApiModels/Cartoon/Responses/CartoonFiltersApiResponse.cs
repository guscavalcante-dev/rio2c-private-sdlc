// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 01-24-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-24-2022
// ***********************************************************************
// <copyright file="CartoonFiltersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>CartoonFiltersApiResponse</summary>
    public class CartoonFiltersApiResponse : ApiBaseResponse
    {
        [JsonProperty("projectFormats")]
        public List<ApiListItemBaseResponse> ProjectFormats { get; set; }

    }
}