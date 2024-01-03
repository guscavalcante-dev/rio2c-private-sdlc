// ***********************************************************************
// Assembly         : PlataformaRio2C.Domain
// Author           : Renan Valentim
// Created          : 12-27-2023
//
// Last Modified By : Renan Valentim
// Last Modified On : 12-27-2023
// ***********************************************************************
// <copyright file="AttendeeCollaboratorTargetAudience.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Domain.Validation;
using System;

namespace PlataformaRio2C.Domain.Entities
{
    /// <summary>
    /// Class AttendeeCollaboratorTargetAudience.
    /// Implements the <see cref="PlataformaRio2C.Domain.Entities.Entity" />
    /// </summary>
    /// <seealso cref="PlataformaRio2C.Domain.Entities.Entity" />
    public class AttendeeCollaboratorTargetAudience : Entity
    {
        public int AttendeeCollaboratorId { get; set; }
        public int TargetAudienceId { get; set; }
        public string AdditionalInfo { get; set; }

        public virtual AttendeeCollaborator AttendeeCollaborator { get; private set; }
        public virtual TargetAudience TargetAudience { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudience"/> class.
        /// </summary>
        /// <param name="attendeeCollaborator">The attendee collaborator.</param>
        /// <param name="targetAudience">The targetAudience.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorTargetAudience(
            AttendeeCollaborator attendeeCollaborator,
            TargetAudience targetAudience,
            string additionalInfo,
            int userId)
        {
            this.AttendeeCollaborator = attendeeCollaborator;
            this.TargetAudience = targetAudience;

            this.AttendeeCollaboratorId = attendeeCollaborator?.Id ?? 0;
            this.TargetAudienceId = targetAudience?.Id ?? 0;
            this.AdditionalInfo = additionalInfo;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudience"/> class.
        /// </summary>
        /// <param name="targetAudience">The targetAudience.</param>
        /// <param name="additionalInfo">The additional information.</param>
        /// <param name="userId">The user identifier.</param>
        public AttendeeCollaboratorTargetAudience(
            TargetAudience targetAudience,
            string additionalInfo,
            int userId)
        {
            this.TargetAudience = targetAudience;
            this.TargetAudienceId = targetAudience?.Id ?? 0;
            this.AdditionalInfo = additionalInfo;

            base.SetCreateDate(userId);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttendeeCollaboratorTargetAudience"/> class.
        /// </summary>
        public AttendeeCollaboratorTargetAudience()
        {
        }

        /// <summary>
        /// Updates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        public void Update(int userId)
        {
            this.IsDeleted = false;
            this.UpdateDate = DateTime.UtcNow;
            this.UpdateUserId = userId;
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
