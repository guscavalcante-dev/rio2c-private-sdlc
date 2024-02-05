// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="InnovationOrganizationObjectivesOptionApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>InnovationOrganizationObjectivesOptionApiDto</summary>
    public class InnovationOrganizationObjectivesOptionApiDto
    {
        [JsonRequired]
        [JsonProperty("uid")]
        public Guid Uid { get; set; }

        [JsonProperty("additionalInfo")]
        public string AdditionalInfo { get; set; }

        [JsonIgnore]
        public InnovationOrganizationObjectivesOption InnovationOrganizationObjectivesOption { get; set; }

        /// <summary>Initializes a new instance of the <see cref="InnovationOrganizationObjectivesOptionApiDto"/> class.</summary>
        public InnovationOrganizationObjectivesOptionApiDto()
        {
        }
    }
}