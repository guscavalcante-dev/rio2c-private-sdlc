﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="CollaboratorBaseApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Domain.ApiModels.Responses
{
    /// <summary>CollaboratorBaseApiResponse</summary>
    public class CollaboratorBaseApiResponse
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("badgeName", Order = 200)]
        public string BadgeName { get; set; }

        [JsonProperty("name", Order = 300)]
        public string Name { get; set; }

        [JsonProperty("picture", Order = 400)]
        public string Picture { get; set; }

        [JsonProperty("miniBio", Order = 500)]
        public string MiniBio { get; set; }

        [JsonProperty("jobTitle", Order = 600)]
        public string JobTitle { get; set; }
    }
}