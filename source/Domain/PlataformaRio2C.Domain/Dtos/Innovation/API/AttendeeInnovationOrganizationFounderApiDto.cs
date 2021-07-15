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
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

        [JsonIgnore]
        private int CurriculumMaxLenght = 710;

        [JsonRequired]
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonRequired]
        [JsonProperty("curriculum")]
        public string Curriculum { get; set; }

        [JsonRequired]
        [JsonProperty("workDedicationUid")]
        public Guid WorkDedicationUid { get; set; }

        [JsonIgnore]
        public AttendeeInnovationOrganizationFounder AttendeeInnovationOrganizationFounder { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounderApiDto"/> class.</summary>
        public AttendeeInnovationOrganizationFounderApiDto()
        {
        }

        /// <summary>Returns true if ... is valid.</summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            this.ValidateCurriculum();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateCurriculum()
        {
            if (this.Curriculum.Length > this.CurriculumMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Curriculum), this.CurriculumMaxLenght, 1), new string[] { nameof(Curriculum) }));
            }
        }
    }
}