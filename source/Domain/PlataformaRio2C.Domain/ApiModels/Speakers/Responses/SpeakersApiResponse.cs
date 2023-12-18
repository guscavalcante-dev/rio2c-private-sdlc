// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 02-07-2023
// ***********************************************************************
// <copyright file="SpeakersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiResponse</summary>
    public class SpeakersApiResponse : ListBaseModel
    {
        [JsonProperty("speakers")]
        public List<SpeakerApiResponse> Speakers { get; set; }
    }
}