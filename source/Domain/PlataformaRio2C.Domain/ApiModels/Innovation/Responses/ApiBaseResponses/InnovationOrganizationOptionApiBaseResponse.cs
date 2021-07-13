// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-13-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-13-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationOptionApiBaseResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationOptionApiBaseResponse</summary>
    public class InnovationOrganizationOptionApiBaseResponse : ApiBaseResponse
    {
        [JsonProperty("InnovationOrganizationOptions")]
        public List<BaseListItemApiResponse> InnovationOrganizationOptions { get; set; }
    }
}