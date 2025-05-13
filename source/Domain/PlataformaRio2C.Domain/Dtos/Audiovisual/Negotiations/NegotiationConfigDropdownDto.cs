// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 03-24-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 03-25-2020
// ***********************************************************************
// <copyright file="NegotiationConfigDropdownDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>NegotiationConfigDropdownDto</summary>
    public class NegotiationConfigDropdownDto
    {
        [JsonProperty("uid", Order = 100)]
        public Guid Uid { get; set; }

        [JsonProperty("startDate", Order = 200)]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("endDate", Order = 300)]
        public DateTimeOffset EndDate { get; set; }
    }
}