﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 10-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="ActivityApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ActivityApiResponse</summary>
    public class ActivityApiResponse
    {
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}