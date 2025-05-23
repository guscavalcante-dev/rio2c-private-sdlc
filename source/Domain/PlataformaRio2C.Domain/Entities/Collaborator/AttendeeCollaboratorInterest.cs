﻿// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 08-17-2021
//
// Last Modified By : Renan Valentim
// Last Modified On : 01-17-2024
// ***********************************************************************
// <copyright file="AttendeeCollaboratorInterest.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeCollaboratorInterest.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeCollaboratorInterest : Entity
    {
        public int AttendeeCollaboratorId { get; set; }
        public int InterestId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual Interest Interest { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInterest" /> class.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="interest">The interest.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorInterest(
            AttendeeCollaborator attendeeCollaborator,
            Interest interest,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.Interest = interest;

            this.AttendeeCollaboratorId = attendeeCollaborator?.Id ?? 0;
            this.InterestId = interest?.Id ?? 0;
            this.AdditionalInfo = additionalInfo;

            this.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInterest" /> class.
        /// </summary>
        /// <param name="interest">The interest.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorInterest(
            Interest interest,
            string additionalInfo,
            int userId)
        {
            this.Interest = interest;
            this.InterestId = interest?.Id ?? 0;
            this.AdditionalInfo = additionalInfo;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorInterest"/> class.
        /// </summary>
        public AttendeeCollaboratorInterest()
        {
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public void Update(string additionalInfo, int userId)
        {
            this.AdditionalInfo = additionalInfo?.Trim();

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
