// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Renan Valentim
// Created          : 07-01-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2022
// ***********************************************************************
// <copyright file="InnovationFiltersApiResponse.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.ApiModels
{
    /// <summary>InnovationFiltersApiResponse</summary>
    public class InnovationFiltersApiResponse : ApiBaseResponse
    {
        [JsonProperty("organizationExperiences")]
        public List<ApiListItemBaseResponse> InnovationOrganizationExperienceOptions { get; set; }

        [JsonProperty("organizationObjectives")]
        public List<ApiListItemBaseResponse> InnovationOrganizationObjectivesOptions { get; set; }

        [JsonProperty("organizationSustainableDevelopmentObjectives")]
        public List<InnovationOrganizationSustainableDevelopmentObjectivesOptionListItemApiResponse> InnovationOrganizationSustainableDevelopmentObjectivesOptions { get; set; }

        [JsonProperty("organizationTechnologies")]
        public List<ApiListItemBaseResponse> InnovationOrganizationTechnologyOptions { get; set; }

        [JsonProperty("workDedications")]
        public List<ApiListItemBaseResponse> WorkDedications { get; set; }

        // "OrganizationTracks" was changed to "OrganizationCreativeEconomyThemes" by customer request.
        // Original Documentation: Enquadre seu produto ou serviço em uma das verticais e temas da economia criativa abaixo*
        [JsonProperty("organizationCreativeEconomyThemes")] 
        public List<InnovationOrganizationTrackOptionListItemApiResponse> InnovationOrganizationTrackOptions { get; set; }

    }
}