// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationFounderApiDto.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationFounderApiDto</summary>
    public class AttendeeInnovationOrganizationFounderApiDto
    {
        /*
        * MANGO | FUNDADORES -> Nome  * 
        * FORM REQUESTED | 1.	Identificação do Participante: | Nome dos fundadores*; 
        */
        [JsonRequired]
        [JsonProperty("fullName")]
        public string FullName { get; set; } //todoremove OK

        /*
        * MANGO | FUNDADORES -> Currículo  * 
        * FORM REQUESTED | 1.	Identificação do Participante: | Curriculum * (no máximo até 700 caracteres com espaço)
        */
        [JsonRequired]
        [JsonProperty("curriculum")]
        public string Curriculum { get; set; } //todoremove OK

        /*
        * MANGO | FUNDADORES -> Tempo de Dedicação *
        * FORM REQUESTED | 1.	Identificação do Participante: | Dedicação * (pode marcar apenas um)
        */
        [JsonRequired]
        [JsonProperty("workDedicationUid")]
        public Guid WorkDedicationUid { get; set; } //todoremove OK

        [JsonIgnore]
        public AttendeeInnovationOrganizationFounder AttendeeInnovationOrganizationFounder { get; set; }

        [JsonIgnore]
        public WorkDedication WorkDedication { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounderApiDto"/> class.</summary>
        public AttendeeInnovationOrganizationFounderApiDto()
        {
        }
    }
}