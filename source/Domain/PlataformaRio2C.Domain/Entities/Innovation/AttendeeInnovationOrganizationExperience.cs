// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-04-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationExperience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationExperience.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationExperience : Entity
    {
        public int AttendeeInnovationOrganizationId { get; set; }
        public int InnovationOrganizationExperienceOptionId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual InnovationOrganizationExperienceOption InnovationOrganizationExperienceOption { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationExperience"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationExperienceOption">The innovation organization experience option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeInnovationOrganizationExperience(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationExperienceOption innovationOrganizationExperienceOption, 
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationExperienceOption = innovationOrganizationExperienceOption;
            this.AdditionalInfo = additionalInfo;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationExperience"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationExperience()
        {

        }

        /// <summary>
        /// Updates the specified attendee innovation organization.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="innovationOrganizationExperienceOption">The innovation organization experience option.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(AttendeeInnovationOrganization attendeeInnovationOrganization,
            InnovationOrganizationExperienceOption innovationOrganizationExperienceOption,
            string additionalInfo,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.InnovationOrganizationExperienceOption = innovationOrganizationExperienceOption;
            this.AdditionalInfo = additionalInfo;

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
