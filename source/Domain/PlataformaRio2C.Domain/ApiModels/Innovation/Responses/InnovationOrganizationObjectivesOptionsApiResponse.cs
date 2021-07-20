// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-01-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationObjectivesOptionsApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationObjectivesOptionsApiResponse</summary>
    public class InnovationOrganizationObjectivesOptionsApiResponse : ApiBaseResponse
    {
        [JsonProperty("organizationObjectives")]
        public List<ApiListItemBaseResponse> InnovationOrganizationObjectivesOptions { get; set; }
    }
}