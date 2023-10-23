// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 09-25-2019
//
// Last Modified By : Renan Valentim
// Last Modified On : 10-23-2023
// ***********************************************************************
// <copyright file="PlayersApiRequest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;

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
    }
}