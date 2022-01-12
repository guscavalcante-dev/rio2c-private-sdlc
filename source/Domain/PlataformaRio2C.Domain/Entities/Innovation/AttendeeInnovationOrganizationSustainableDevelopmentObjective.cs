// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Rafael Franco
// Created          : 12-01-2022
//
// Last Modified By : Rafael Franco
// Last Modified On : 12-01-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationSustainableDevelopmentObjective.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

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
        public virtual InnovationOrganizationSustainableDevelopmentObjectivesOption InnovationOrganizationSustainableDevelopmentObjectivesOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeInnovationOrganizationSustainableDevelopmentObjective"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOption">The innovation organization sustainable development objective option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjective(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationSustainableDevelopmentObjectivesOption innovationOrganizationSustainableDevelopmentObjectivesOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationSustainableDevelopmentObjectivesOption = innovationOrganizationSustainableDevelopmentObjectivesOption;
            this.AdditionalInfo = additionalInfo;

            this.AttendeeInnovationOrganizationId = attendeeInnovationOrganization?.Id ?? 0;
            this.InnovationOrganizationSustainableDevelopmentObjectiveOptionId = innovationOrganizationSustainableDevelopmentObjectivesOption?.Id ?? 0;

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationSustainableDevelopmentObjective"/> class.
        /// </summary>
        /// <param name="innovationOrganizationSustainableDevelopmentObjectivesOption">The innovation organization sustainable development objective option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjective(
            InnovationOrganizationSustainableDevelopmentObjectivesOption innovationOrganizationSustainableDevelopmentObjectivesOption,
            string additionalInfo,
            int userId)
        {
            this.InnovationOrganizationSustainableDevelopmentObjectivesOption = innovationOrganizationSustainableDevelopmentObjectivesOption;
            this.InnovationOrganizationSustainableDevelopmentObjectiveOptionId = innovationOrganizationSustainableDevelopmentObjectivesOption?.Id ?? 0;
            this.AdditionalInfo = additionalInfo?.Trim();

            this.IsDeleted = false;
            this.CreateDate = this.UpdateDate = DateTime.UtcNow;
            this.CreateUserId = this.UpdateUserId = userId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeInnovationOrganizationSustainableDevelopmentObjective"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationSustainableDevelopmentObjective()
        {}

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
