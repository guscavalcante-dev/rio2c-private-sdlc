// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 01-16-2020
// ***********************************************************************
// <copyright file="SpeakerApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakerApiRequest</summary>
    public class SpeakerApiRequest : ApiBaseRequest
    {
        [JsonProperty("uid")]
        public Guid? Uid { get; set; }
    }
}