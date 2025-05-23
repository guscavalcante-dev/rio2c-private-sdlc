﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationCompetitorApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationCompetitorApiDto</summary>
    public class AttendeeInnovationOrganizationCompetitorApiDto
    {
        [JsonRequired]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationCompetitorApiDto"/> class.</summary>
        public AttendeeInnovationOrganizationCompetitorApiDto()
        {
        }
    }
}