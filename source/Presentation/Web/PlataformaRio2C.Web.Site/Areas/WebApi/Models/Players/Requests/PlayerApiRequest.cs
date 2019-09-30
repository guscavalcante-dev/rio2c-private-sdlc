// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 09-30-2019
// ***********************************************************************
// <copyright file="PlayerApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>PlayerApiRequest</summary>
    public class PlayerApiRequest
    {
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }

        [JsonProperty("edition")]
        public int? Edition { get; set; }
    }
}