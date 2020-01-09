// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-09-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="EditionEventApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>EditionEventApiResponse</summary>
    public class EditionEventApiResponse : EditionEventBaseApiResponse
    {
        [JsonProperty("startDate", Order = 300)]
        public string StartDate { get; set; }

        [JsonProperty("endDate", Order = 400)]
        public string EndDate { get; set; }

        [JsonProperty("durationDays", Order = 400)]
        public int DurationDays { get; set; }
    }
}