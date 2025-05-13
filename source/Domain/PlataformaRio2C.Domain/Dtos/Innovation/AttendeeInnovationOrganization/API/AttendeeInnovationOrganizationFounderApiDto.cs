// ***********************************************************************
// Assembly         : PlataformaRio2C.Application
// Author           : Renan Valentim
// Created          : 07-14-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-09-2023
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

namespace PlataformaRio2C.Domain.Dtos
{
    /// <summary>AttendeeInnovationOrganizationFounderApiDto</summary>
    public class AttendeeInnovationOrganizationFounderApiDto
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }

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

        [JsonIgnore]
        public WorkDedication WorkDedication { get; set; }

        /// <summary>Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounderApiDto"/> class.</summary>
        public AttendeeInnovationOrganizationFounderApiDto()
        {
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.ValidationResult = new ValidationResult();

            if (this.Curriculum == null || this.Curriculum?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(Curriculum)), new string[] { nameof(Curriculum) }));
            }

            if (this.FullName == null || this.FullName?.Length == 0)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(FullName)), new string[] { nameof(FullName) }));
            }

            if (this.WorkDedicationUid == null || this.WorkDedicationUid == Guid.Empty)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.TheFieldIsRequired, nameof(WorkDedicationUid)), new string[] { nameof(WorkDedicationUid) }));
            }

            return this.ValidationResult.IsValid;
        }
    }
}