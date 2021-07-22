// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-21-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionsListItemApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationTrackOptionsListItemApiResponse</summary>
    public class InnovationOrganizationTrackOptionsListItemApiResponse : ApiListItemBaseResponse
    {
        [JsonProperty("description", Order = 300)]
        public string Description { get; set; }
    }
}