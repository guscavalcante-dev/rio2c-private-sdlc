﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 03-08-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-08-2020
// ***********************************************************************
// <copyright file="OrganizationDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>OrganizationDropdownDto</summary>
    public class OrganizationDropdownDto
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("name", Order = 200)]
        public string Name { get; set; }

        [JsonProperty("tradeName", Order = 300)]
        public string TradeName { get; set; }

        [JsonProperty("companyName", Order = 400)]
        public string CompanyName { get; set; }

        [JsonProperty("picture", Order = 500)]
        public string Picture { get; set; }
    }
}