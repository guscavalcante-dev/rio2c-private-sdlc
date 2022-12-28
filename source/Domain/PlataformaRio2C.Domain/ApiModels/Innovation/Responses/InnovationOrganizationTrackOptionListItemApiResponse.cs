// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-21-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationOrganizationTrackOptionListItemApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationOrganizationTrackOptionListItemApiResponse</summary>
    public class InnovationOrganizationTrackOptionListItemApiResponse : ApiListItemBaseResponse
    {
        [JsonProperty("description", Order = 300)]
        public string Description { get; set; }

        [JsonProperty("groupUid", Order = 400)]
        public string GroupUid { get; set; }

        [JsonProperty("groupName", Order = 500)]
        public string GroupName { get; set; }
    }
}