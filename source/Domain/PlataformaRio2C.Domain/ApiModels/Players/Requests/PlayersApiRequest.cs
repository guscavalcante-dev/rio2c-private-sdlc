// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-13-2023
// ***********************************************************************
// <copyright file="PlayersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Statics;
using System;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>PlayersApiRequest</summary>
    public class PlayersApiRequest : ApiPageBaseRequest
    {
        [JsonProperty("activitiesUids")]
        [SwaggerParameterDescription(description: "Activities Uids separated by comma.")]
        public string ActivitiesUids { get; set; }

        [JsonProperty("targetAudiencesUids")]
        [SwaggerParameterDescription(description: "Target Audiences Uids separated by comma.")]
        public string TargetAudiencesUids { get; set; }

        [JsonProperty("interestsUids")]
        [SwaggerParameterDescription(description: "Interests Uids separated by comma.")]
        public string InterestsUids { get; set; }

        [JsonProperty("modifiedAfterDate")]
        [SwaggerParameterDescription("Returns only registers created or updated after this date. (UTC)", PublicApiDateTimeFormat.Default)]
        public DateTime? ModifiedAfterDate { get; set; }

        [JsonProperty("showDetails")]
        [SwaggerParameterDescription(description: "Shows extra fields.")]
        public bool? ShowDetails { get; set; }
    }
}