// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="CollaboratorsDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>CollaboratorsDropdownDto</summary>
    public class CollaboratorsDropdownDto
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

        [JsonProperty("companies", Order = 701)]
        public List<CollaboratorsDropdownOrganizationDto> Companies { get; set; }
    }

    /// <summary>CollaboratorsDropdownOrganizationDto</summary>
    public class CollaboratorsDropdownOrganizationDto
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("tradeName", Order = 200)]
        public string TradeName { get; set; }

        [JsonProperty("companyName", Order = 300)]
        public string CompanyName { get; set; }

        [JsonProperty("picture", Order = 400)]
        public string Picture { get; set; }
    }
}