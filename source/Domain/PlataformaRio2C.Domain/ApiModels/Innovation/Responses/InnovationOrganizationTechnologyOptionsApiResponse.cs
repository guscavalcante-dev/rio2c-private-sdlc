// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationTechnologyOptionsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationTechnologyOptionsApiResponse</summary>
    public class InnovationOrganizationTechnologyOptionsApiResponse : ApiBaseResponse
    {
        [JsonProperty("organizationTechnologies")]
        public List<ApiListItemBaseResponse> InnovationOrganizationTechnologyOptions { get; set; }
    }
}