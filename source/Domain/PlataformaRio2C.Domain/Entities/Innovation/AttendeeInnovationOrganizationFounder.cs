﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 07-12-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 03-04-2022
// ***********************************************************************
// <copyright file="AttendeeInnovationOrganizationFounder.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeInnovationOrganizationFounder.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeInnovationOrganizationFounder : Entity
    {
        public static readonly int FullNameMaxLenght = 200;
        public static readonly int CurriculumMaxLenght = 710;

        public int AttendeeInnovationOrganizationId { get; set; }
        public string Fullname { get; set; }
        public string Curriculum { get; set; }
        public int WorkDedicationId { get; set; }

        public virtual AttendeeInnovationOrganization AttendeeInnovationOrganization { get; private set; }
        public virtual WorkDedication WorkDedication { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounder"/> class.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="workDedication">The work dedication.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="curriculum">The curriculum.</param>
        public AttendeeInnovationOrganizationFounder(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            WorkDedication workDedication,
            string fullname,
            string curriculum,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.WorkDedication = workDedication;
            this.Fullname = fullname;
            this.Curriculum = curriculum;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeInnovationOrganizationFounder"/> class.
        /// </summary>
        public AttendeeInnovationOrganizationFounder()
        {

        }

        /// <summary>
        /// Updates the specified attendee innovation organization.
        /// </summary>
        /// <param name="attendeeInnovationOrganization">The attendee innovation organization.</param>
        /// <param name="workDedication">The work dedication.</param>
        /// <param name="fullname">The fullname.</param>
        /// <param name="curriculum">The curriculum.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(
            AttendeeInnovationOrganization attendeeInnovationOrganization,
            WorkDedication workDedication,
            string fullname,
            string curriculum,
            int userId)
        {
            this.AttendeeInnovationOrganization = attendeeInnovationOrganization;
            this.WorkDedication = workDedication;
            this.Fullname = fullname;
            this.Curriculum = curriculum;

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

            this.ValidateFullname();
            this.ValidateCurriculum();

            return this.ValidationResult.IsValid;
        }

        /// <summary>
        /// Validates the curriculum.
        /// </summary>
        private void ValidateCurriculum()
        {
            if (this.Curriculum.Length > CurriculumMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Curriculum), CurriculumMaxLenght, 1), new string[] { nameof(Curriculum) }));
            }
        }

        /// <summary>
        /// Validates the fullname.
        /// </summary>
        private void ValidateFullname()
        {
            if (this.Fullname.Length > FullNameMaxLenght)
            {
                this.ValidationResult.Add(new ValidationError(string.Format(Messages.PropertyBetweenLengths, nameof(Fullname), FullNameMaxLenght, 1), new string[] { nameof(Fullname) }));
            }
        }

        #endregion
    }
}
