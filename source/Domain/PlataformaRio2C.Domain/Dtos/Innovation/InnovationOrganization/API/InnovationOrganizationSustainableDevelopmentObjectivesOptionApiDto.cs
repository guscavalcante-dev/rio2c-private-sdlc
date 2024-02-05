// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Rafael Franco
// Created          : 13/01/2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 13/01/2022
// ***********************************************************************
// <copyright file="InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using System;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto</summary>
    public class InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonIgnore]
        public InnovationOrganizationSustainableDevelopmentObjectivesOption InnovationOrganizationSustainableDevelopmentObjectivesOption { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto"/> class.</summary>
        public InnovationOrganizationSustainableDevelopmentObjectivesOptionApiDto()
        {
        }
    }
}