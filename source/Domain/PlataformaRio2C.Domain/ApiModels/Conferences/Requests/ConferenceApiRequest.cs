// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 01-09-2020
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-09-2020
// ***********************************************************************
// <copyright file="ConferenceApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>ConferenceApiRequest</summary>
    public class ConferenceApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
    }
}