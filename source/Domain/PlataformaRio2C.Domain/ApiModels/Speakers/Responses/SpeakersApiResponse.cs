// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 12-18-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="SpeakersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using Newtonsoft.Json;
using PlataformaRio2C.Domain.ApiModels.Responses;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>SpeakersApiResponse</summary>
    public class SpeakersApiResponse : ListBaseModel
    {
        [JsonProperty("speakers")]
        public List<SpeakersApiListItem> Speakers { get; set; }
    }

    /// <summary>SpeakersApiListItem</summary>
    public class SpeakersApiListItem : CollaboratorBaseApiResponse
    {
        [JsonProperty("highlightPosition", Order = 301)]
        public int? HighlightPosition { get; set; }

        [JsonProperty("companies", Order = 701)]
        public List<OrganizationBaseApiResponse> Companies { get; set; }
    }
}