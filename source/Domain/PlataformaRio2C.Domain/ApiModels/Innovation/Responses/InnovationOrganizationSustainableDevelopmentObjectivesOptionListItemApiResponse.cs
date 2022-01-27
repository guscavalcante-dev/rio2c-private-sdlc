// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Franco
// Created          : 13-01-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 13-01-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse</summary>
    public class InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse : ApiListItemBaseResponse
    {
        [JsonProperty("description", Order = 300)]
        public string Description { get; set; }
        [JsonProperty("order", Order = 301)]
        public int DisplayOrder { get; set; }
    }
}