// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 10-18-2019
// ***********************************************************************
// <copyright file="PlayersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Web.Site.Areas.WebApi.Models
{
    /// <summary>PlayersApiRequest</summary>
    public class PlayersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("activitiesUids")]
        public List<Guid> ActivitiesUids { get; set; }

        [JsonProperty("targetAudiencesUids")]
        public List<Guid> TargetAudiencesUids { get; set; }

        [JsonProperty("interestsUids")]
        public List<Guid> InterestsUids { get; set; }
    }
}