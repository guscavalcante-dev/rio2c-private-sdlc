// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-13-2020
// ***********************************************************************
// <copyright file="SpeakersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiRequest</summary>
    public class SpeakersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("highlights")]
        public int? Highlights { get; set; }
    }
}