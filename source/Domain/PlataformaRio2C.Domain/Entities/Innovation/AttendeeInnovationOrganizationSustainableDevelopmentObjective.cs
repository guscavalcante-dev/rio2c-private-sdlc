// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 12-01-2022
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-04-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationSustainableDevelopmentObjective.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationSustainableDevelopmentObjective.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationSustainableDevelopmentObjective : Entity
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public int InnovationOrganizationSustainableDevelopmentObjectiveOptionId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual InnovationOrganizationSustainableDevelopmentObjectivesOption InnovationOrganizationSustainableDevelopmentObjectiveOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeInnovationOrganizationSustainableDevelopmentObjective"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOption">The innovation organization sustainable development objective option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjective(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationSustainableDevelopmentObjectivesOption innovationOrganizationSustainableDevelopmentObjectiveOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationSustainableDevelopmentObjectiveOption = innovationOrganizationSustainableDevelopmentObjectiveOption;
            this.AdditionalInfo = additionalInfo;

            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization?.Id ?? 0;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationSustainableDevelopmentObjective"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjective()
        {
        }

        /// <summary>
        /// Updates the specified attendee innovation organization.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectiveOption">The innovation organization sustainable development objective option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationSustainableDevelopmentObjectivesOption innovationOrganizationSustainableDevelopmentObjectiveOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationSustainableDevelopmentObjectiveOption = innovationOrganizationSustainableDevelopmentObjectiveOption;
            this.AdditionalInfo = additionalInfo;
            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization?.Id ?? 0;

            this.SetUpdateDate(userId);
        }

        #region Valitations

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <returns><c>true</c> if this instance is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid()
        {
            if (this.ValidationResult == null)
            {
                this.ValidationResult = new ValidationResult();
            }

            return this.ValidationResult.IsValid;
        }

        #endregion
    }
}
