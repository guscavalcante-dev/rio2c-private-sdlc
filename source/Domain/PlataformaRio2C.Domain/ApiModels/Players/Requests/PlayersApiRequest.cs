// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-19-2019
// ***********************************************************************
// <copyright file="PlayersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersApiRequest</summary>
    public class PlayersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("activitiesUids")]
        public string ActivitiesUids { get; set; }

        [JsonProperty("targetAudiencesUids")]
        public string TargetAudiencesUids { get; set; }

        [JsonProperty("interestsUids")]
        public string InterestsUids { get; set; }
    }
}